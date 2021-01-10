using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.TrainInFactory;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;

namespace CMCS.DumblyConcealer.Tasks.TrainDiscriminator
{
	/// <summary>
	/// 火车车号识别-D报文
	/// </summary>
	public class TrainDiscriminatorDBW
	{
		private static TrainDiscriminatorDBW instance;

		private static String MachineCode_CHSB = GlobalVars.MachineCode_HCRCCHSB;

		public static TrainDiscriminatorDBW GetInstance()
		{
			if (instance == null)
			{
				instance = new TrainDiscriminatorDBW();
			}
			return instance;
		}

		private TrainDiscriminatorDBW()
		{
		}

		/// <summary>
		/// 同步报文
		/// </summary>
		/// <param name="output"></param>
		/// <param name="path">文件路径</param>
		/// <param name="flag">设备标识</param>
		/// <returns></returns>
		public int SyncDBWInfo(Action<string, eOutputType> output, string path, string flag)
		{
			int interface_dbwtime = CommonDAO.GetInstance().GetAppletConfigInt32("车号识别文件读取天数");
			int res = 0;

			// 数据最后有效时间
			DateTime lastEffectiveTime = DateTime.Now.AddDays(-interface_dbwtime);

			string carnumber = string.Empty;
			string carmodel = string.Empty;
			string date = string.Empty;
			string time = string.Empty;

			foreach (var item in Directory.GetFiles(path).Where(a => new FileInfo(a).LastWriteTime > lastEffectiveTime))
			{
				string s = File.ReadAllText(item);//读取文件数据

				string direction = Convert.ToInt32(s.Substring(18, 1)) == 1 ? "出厂" : "进厂";
				int carcount = Convert.ToInt32(s.Substring(82, 3));//车数 
				if (carcount == 0) continue;

				date = s.Substring(34, 8);  //日期
				time = s.Substring(42, 6);  //时间 

				//车辆时间
				DateTime carDate = Convert.ToDateTime(date.Insert(4, "-").Insert(7, "-") + " " + time.Insert(2, ":").Insert(5, ":"));

				if (carDate < lastEffectiveTime) continue;

				bool hasJCar = false;
				for (int i = 0; i < carcount; i++)
				{

					int startIndex = 88 + (i * 24);

					if (s.Length < startIndex + 24)
					{
						// LogerUtil.Error("车号识别器报文异常，文件名：" + item, new Exception("车辆信息长度不足" + (startIndex + 24)));
						break;
					}

					string car = s.Substring(startIndex, 24);
					if (car.StartsWith("J"))
					{
						hasJCar = true;
						continue;
					}

					carmodel = car.Substring(1, 6).Trim();//车型
					carnumber = car.Substring(7, 7).Trim();//车号
					if (string.IsNullOrEmpty(carnumber.Trim())) continue;

					string uniqKey = date + time + "_" + carnumber;

					////同步到车号识别表
					//if (Dbers.GetInstance().SelfDber.Count<CmcsTrainCarriagePass>(" where TrainNumber='" + carnumber + "'  and  PKID='" + uniqKey + "'") == 0)
					//{
					//	res += Dbers.GetInstance().SelfDber.Insert(new CmcsTrainCarriagePass()
					//	{
					//		PKID = uniqKey,
					//		TrainNumber = carnumber,
					//		PassTime = carDate,
					//		CarModel = carmodel,
					//		Direction = direction,
					//		MachineCode = flag,
					//		DataFlag = 0,
					//		OrderNum = hasJCar ? i : i + 1
					//	});
					//}

					//同步到车号识别表
					if (Dbers.GetInstance().SelfDber.Count<CmcsTrainRecognition>(" where CarNumber='" + carnumber + "'  and  Id='" + uniqKey + "'") == 0)
					{
						res += Dbers.GetInstance().SelfDber.Insert(new CmcsTrainRecognition()
						{
							Id = uniqKey,
							CreateDate = DateTime.Now,
							CarNumber = carnumber,
							CrossTime = carDate,
							CarModel = carmodel,
							Direction = direction,
							MachineCode = flag,
							Status = 1,
							DataFlag = 0,
							OrderNum = hasJCar ? i : i + 1
						});
					}
				}
			}
			output(string.Format("{0}车号识别同步车号识别数据{1}条", flag, res), eOutputType.Normal);
			return res;
		}
	}
}
