using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.AccessDb;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler
{
    /// <summary>
    /// 汽车机械采样机接口业务
    /// </summary>
    public class EquCarJXSamplerDAO
    {
        /// <summary>
        /// EquCarJXSamplerDAO
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        public EquCarJXSamplerDAO(string machineCode)
        {
            this.MachineCode = machineCode;
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// 设备编码
        /// </summary>
        string MachineCode;
        /// <summary>
        /// 是否处于故障状态
        /// </summary>
        bool IsHitch = false;
        /// <summary>
        /// 上一次上位机心跳值
        /// </summary>
        string PrevHeartbeat = string.Empty;

        #region 数据转换方法（此处有点麻烦，后期调整接口方案）

        #endregion

        ///// <summary>
        ///// 同步实时信号到集中管控
        ///// </summary>
        ///// <param name="output"></param>
        ///// <param name="MachineCode">设备编码</param>
        ///// <returns></returns>
        //public int SyncSignal(Action<string, eOutputType> output)
        //{
        //	int res = 0;

        //	foreach (EquQCJXCYJSignal entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJSignal>())
        //	{
        //		if (entity.TagName == GlobalVars.EquHeartbeatName) continue;

        //		// 当心跳检测为故障时，则不更新系统状态，保持 eSampleSystemStatus.发生故障
        //		if (entity.TagName == eSignalDataName.系统.ToString() && IsHitch) continue;

        //		res += commonDAO.SetSignalDataValue(this.MachineCode, entity.TagName, entity.TagValue) ? 1 : 0;
        //	}
        //	output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

        //	return res;
        //}

        /// <summary>
        /// 同步实时信号到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <param name="MachineCode">设备编码</param>
        /// <returns></returns>
        public int SyncSignal(Action<string, eOutputType> output)
        {
            int res = 0;
            List<CmcsSignalData> cmcsSignalData = Dbers.GetInstance().SelfDber.Entities<CmcsSignalData>("where SignalPrefix=:SignalPrefix", new { SignalPrefix = this.MachineCode });
            if (cmcsSignalData.Where(a => a.SignalName.Contains("运行") && a.SignalValue == "1").Count() > 0)
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, "系统", "正在运行") ? 1 : 0;
            }
            else
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, "系统", "就绪待机") ? 1 : 0;
            }

            if (cmcsSignalData.Where(a => a.SignalName.Contains("故障") && a.SignalValue == "1").Count() > 0)
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, "系统", "发生故障") ? 1 : 0;
            }

            if (this.MachineCode == GlobalVars.MachineCode_QCJXCYJ_1)
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前车号.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.当前车号.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前车Id.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.当前车Id.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前运输记录Id.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.当前运输记录Id.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, "供应商名称", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, "供应商名称")) ? 1 : 0;
            }
            else if (this.MachineCode == GlobalVars.MachineCode_QCJXCYJ_2)
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前车号.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.当前车号.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前车Id.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.当前车Id.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.当前运输记录Id.ToString(), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.当前运输记录Id.ToString())) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, "供应商名称", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, "供应商名称")) ? 1 : 0;
            }

            output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

            return res;
        }

        /// <summary>
        /// 获取上位机运行状态表 - 心跳值
        /// 每隔30s读取该值，如果数值不变化则表示设备上位机出现故障
        /// </summary>
        /// <param name="MachineCode">设备编码</param>
        public void SyncHeartbeatSignal()
        {
            EquQCJXCYJSignal pDCYSignal = DcDbers.GetInstance().CarJXSampler_Dber.Entity<EquQCJXCYJSignal>("where TagName=:TagName", new { TagName = GlobalVars.EquHeartbeatName });
            ChangeSystemHitchStatus((pDCYSignal != null && pDCYSignal.TagValue == this.PrevHeartbeat));

            this.PrevHeartbeat = pDCYSignal != null ? pDCYSignal.TagValue : string.Empty;
        }

        /// <summary>
        /// 改变系统状态值
        /// </summary>
        /// <param name="isHitch">是否故障</param> 
        public void ChangeSystemHitchStatus(bool isHitch)
        {
            IsHitch = isHitch;

            if (IsHitch) commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.系统.ToString(), eEquInfSamplerSystemStatus.发生故障.ToString());
        }

        /// <summary>
        /// 同步集样罐信息到集中管控
        /// </summary>
        /// <param name="output"></param> 
        /// <returns></returns>
        public void SyncBarrel(Action<string, eOutputType> output, AccessDapperDber accessDapperDber)
        {
            int res = 0;


            DataTable infpdcybarrels = accessDapperDber.ExecuteDataTable("select * from 桶号点数");
            if (infpdcybarrels != null && infpdcybarrels.Rows.Count > 0)
            {
                for (int i = 0; i < infpdcybarrels.Rows.Count; i++)
                {
                    InfEquInfSampleBarrel oldEquInfSampleBarrel = Dbers.GetInstance().SelfDber.Entity<InfEquInfSampleBarrel>("where MachineCode=:MachineCode and BarrelNumber=:BarrelNumber"
                    , new { MachineCode = this.MachineCode, BarrelNumber = infpdcybarrels.Rows[i]["桶号"].ToString() });

                    if (oldEquInfSampleBarrel == null)
                    {
                        InfEquInfSampleBarrel entity = new InfEquInfSampleBarrel();

                        entity.BarrelNumber = infpdcybarrels.Rows[i]["桶号"].ToString();

                        entity.SampleCount = Convert.ToInt32(infpdcybarrels.Rows[i]["点数"].ToString());
                        entity.BarrelStatus = entity.SampleCount > 0 ? "未满" : "空桶";
                        DataTable dtSetUp = accessDapperDber.ExecuteDataTable("select * from 系统参数");
                        if (dtSetUp != null && dtSetUp.Rows.Count > 0)
                        {
                            int count = Convert.ToInt32(dtSetUp.Rows[0]["盛样点数"].ToString());
                            if (entity.SampleCount == count)
                            {
                                entity.BarrelStatus = "已满";
                            }
                        }


                        entity.MachineCode = this.MachineCode;
                        //InFactoryBatchId = entity.InFactoryBatchId,
                        entity.InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.MachineCode);

                        string dqth = commonDAO.GetSignalDataValue(MachineCode, "当前桶号");
                        if (entity.BarrelNumber == dqth)
                        {
                            entity.IsCurrent = 1;
                        }
                        else
                        {
                            entity.IsCurrent = 0;
                        }

                        entity.SampleCode = infpdcybarrels.Rows[i]["单位"].ToString();

                        entity.UpdateTime = DateTime.Now;
                        //BarrelType = entity.BarrelType,
                        if (Dbers.GetInstance().SelfDber.Insert(entity) > 0)
                        {
                            res++;
                        };
                    }
                    else
                    {
                        oldEquInfSampleBarrel.BarrelNumber = infpdcybarrels.Rows[i]["桶号"].ToString();
                        oldEquInfSampleBarrel.SampleCount = Convert.ToInt32(infpdcybarrels.Rows[i]["点数"].ToString());
                        oldEquInfSampleBarrel.BarrelStatus = oldEquInfSampleBarrel.SampleCount > 0 ? "未满" : "空桶";

                        DataTable dtSetUp = accessDapperDber.ExecuteDataTable("select * from 系统参数");
                        if (dtSetUp != null && dtSetUp.Rows.Count > 0)
                        {
                            int count = Convert.ToInt32(dtSetUp.Rows[0]["盛样点数"].ToString());
                            if (oldEquInfSampleBarrel.SampleCount == count)
                            {
                                oldEquInfSampleBarrel.BarrelStatus = "已满";
                            }
                        }

                        //oldEquInfSampleBarrel.InFactoryBatchId = equInfSampleBarrel.InFactoryBatchId;

                        string dqth = commonDAO.GetSignalDataValue(MachineCode, "当前桶号");
                        if (oldEquInfSampleBarrel.BarrelNumber == dqth)
                        {
                            oldEquInfSampleBarrel.IsCurrent = 1;
                        }
                        else
                        {
                            oldEquInfSampleBarrel.IsCurrent = 0;
                        }

                        oldEquInfSampleBarrel.SampleCode = infpdcybarrels.Rows[i]["单位"].ToString();
                        oldEquInfSampleBarrel.UpdateTime = DateTime.Now;
                        //oldEquInfSampleBarrel.DataFlag = equInfSampleBarrel.DataFlag;

                        if (Dbers.GetInstance().SelfDber.Update(oldEquInfSampleBarrel) > 0)
                        {
                            res++;
                        }
                    }



                    //{

                    //	entity.DataFlag = 1;
                    //	DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

                    //	res++;
                    //}
                }
            }


            output(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
        }

        /// <summary>
        /// 同步故障信息到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncQCJXCYJError(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (EquQCJXCYJError entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJError>("where DataFlag=0"))
            {
                if (commonDAO.SaveEquInfHitch(this.MachineCode, entity.ErrorTime, "故障代码 " + entity.ErrorCode + "，" + entity.ErrorDescribe))
                {
                    entity.DataFlag = 1;
                    DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

                    res++;
                }
            }

            output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
        }

        /// <summary>
        /// 同步采样命令
        /// </summary>
        /// <param name="output"></param>
        /// <param name="MachineCode">设备编码</param>
        public void SyncSampleCmd(Action<string, eOutputType> output)
        {
            int res = 0;

            // 集中管控 > 第三方 
            foreach (InfQCJXCYSampleCMD entity in CarSamplerDAO.GetInstance().GetWaitForSyncSampleCMD(this.MachineCode))
            {
                bool isSuccess = false;

                List<Interface_Data> list = DcDbers.GetInstance().CarJXSampler_Dber.Entities<Interface_Data>("where Sampler_No=:Sampler_No", new { Sampler_No = this.MachineCode == GlobalVars.MachineCode_QCJXCYJ_1 ? "CY01" : "CY02" });
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        DcDbers.GetInstance().CarJXSampler_Dber.Delete<Interface_Data>(item.Interface_Id);
                    }

                }

                Interface_Data samplecmdEqu = DcDbers.GetInstance().CarJXSampler_Dber.Get<Interface_Data>(entity.Id);
                if (samplecmdEqu == null)
                {
                    isSuccess = DcDbers.GetInstance().CarJXSampler_Dber.Insert(new Interface_Data
                    {
                        // 保持相同的Id
                        Interface_Id = entity.Id,
                        Sampler_No = this.MachineCode == GlobalVars.MachineCode_QCJXCYJ_1 ? "CY01" : "CY02",
                        Weighing_Id = entity.SerialNumber,
                        Car_Mark = entity.CarNumber,
                        Mine_Name = entity.SampleCode,
                        Point_Count = entity.PointCount,
                        Car_Length = entity.CarriageLength,
                        Car_Width = entity.CarriageWidth,
                        Car_Height = entity.CarriageHeight,
                        Chassis_Height = entity.CarriageBottomToFloor,
                        Tie_Rod_Place1 = entity.Obstacle1,
                        Tie_Rod_Place2 = entity.Obstacle2,
                        Tie_Rod_Place3 = entity.Obstacle3,
                        Tie_Rod_Place4 = entity.Obstacle4,
                        Tie_Rod_Place5 = entity.Obstacle5,
                        Tie_Rod_Place6 = entity.Obstacle6,
                        Sample_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Data_Status = 1
                    }) > 0;
                }
                else
                {
                    samplecmdEqu.Car_Mark = entity.CarNumber;
                    samplecmdEqu.Weighing_Id = entity.SerialNumber;
                    samplecmdEqu.Mine_Name = entity.SampleCode;
                    samplecmdEqu.Point_Count = entity.PointCount;
                    samplecmdEqu.Car_Length = entity.CarriageLength;
                    samplecmdEqu.Car_Width = entity.CarriageWidth;
                    samplecmdEqu.Car_Height = entity.CarriageHeight;
                    samplecmdEqu.Chassis_Height = entity.CarriageBottomToFloor;
                    samplecmdEqu.Tie_Rod_Place1 = entity.Obstacle1;
                    samplecmdEqu.Tie_Rod_Place2 = entity.Obstacle2;
                    samplecmdEqu.Tie_Rod_Place3 = entity.Obstacle3;
                    samplecmdEqu.Tie_Rod_Place4 = entity.Obstacle4;
                    samplecmdEqu.Tie_Rod_Place5 = entity.Obstacle5;
                    samplecmdEqu.Tie_Rod_Place6 = entity.Obstacle6;
                    samplecmdEqu.Sample_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    samplecmdEqu.Data_Status = 1;
                    isSuccess = DcDbers.GetInstance().CarJXSampler_Dber.Update(samplecmdEqu) > 0;
                }

                if (isSuccess)
                {
                    commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.采样编码.ToString(), entity.SampleCode);
                    entity.SyncFlag = 1;
                    Dbers.GetInstance().SelfDber.Update(entity);

                    res++;
                }
                output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);
            }


            res = 0;
            // 第三方 > 集中管控
            foreach (InfQCJXCYSampleCMD item in commonDAO.SelfDber.Entities<InfQCJXCYSampleCMD>("where ResultCode='默认' and MachineCode=:MachineCode order by CreationTime ", new { MachineCode = this.MachineCode }))
            {
                Interface_Data entity = DcDbers.GetInstance().CarJXSampler_Dber.Entity<Interface_Data>("where Interface_Id=:Interface_Id", new { Interface_Id = item.Id });
                if (entity != null)
                {
                    if (entity.Data_Status == 2)
                    {
                        item.StartTime = Convert.ToDateTime(entity.Sample_Time);
                        item.ResultCode = eEquInfCmdResultCode.成功.ToString();
                    }
                    else if (entity.Data_Status == 4)
                    {
                        item.ResultCode = "定位错误";
                    }
                    else if (entity.Data_Status == 5)
                    {
                        item.ResultCode = "系统故障";
                    }

                    if (Dbers.GetInstance().SelfDber.Update(item) > 0)
                    {
                        commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.采样编码.ToString(), string.Empty);
                        res++;
                    }
                    output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
                }
            }

            //foreach (Interface_Data entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<Interface_Data>("where Data_Status=2 order by Sample_Time desc"))
            //{
            //	InfQCJXCYSampleCMD samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfQCJXCYSampleCMD>(entity.Interface_Id);
            //	if (samplecmdInf == null) continue;

            //	//samplecmdInf.Point1 = entity.Point1;
            //	//samplecmdInf.Point2 = entity.Point2;
            //	//samplecmdInf.Point3 = entity.Point3;
            //	//samplecmdInf.Point4 = entity.Point4;
            //	//samplecmdInf.Point5 = entity.Point5;
            //	//samplecmdInf.Point6 = entity.Point6;
            //	samplecmdInf.StartTime = entity.Sample_Time;
            //	//samplecmdInf.EndTime = entity.EndTime;
            //	//samplecmdInf.SampleUser = entity.SampleUser;
            //	samplecmdInf.ResultCode = eEquInfCmdResultCode.成功.ToString();

            //	if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
            //	{
            //		// 我方已读
            //		//entity.DataFlag = 3;
            //		//this.EquDber.Update(entity);
            //		res++;
            //	}
            //}
            //output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
        }

        /// <summary>
        /// 同步历史卸样结果
        /// </summary>
        /// <param name="output"></param>
        /// <param name="MachineCode"></param>
        public void SyncUnloadResult(Action<string, eOutputType> output)
        {
            int res = 0;

            res = 0;
            // 第三方 > 集中管控
            foreach (EquQCJXCYJUnloadResult entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJUnloadResult>("where DataFlag=0"))
            {
                InfQCJXCYJUnloadResult oldUnloadResult = commonDAO.SelfDber.Get<InfQCJXCYJUnloadResult>(entity.Id);
                if (oldUnloadResult == null)
                {
                    // 查找采样命令
                    EquQCJXCYJSampleCmd qCJXCYJSampleCmd = DcDbers.GetInstance().CarJXSampler_Dber.Entity<EquQCJXCYJSampleCmd>("where SampleCode=:SampleCode", new { SampleCode = entity.SampleCode });
                    if (qCJXCYJSampleCmd != null)
                    {
                        // 生成采样桶记录
                        CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
                        {
                            BarrelCode = entity.BarrelCode,
                            BarrellingTime = entity.UnloadTime,
                            BarrelNumber = entity.BarrelNumber,
                            InFactoryBatchId = qCJXCYJSampleCmd.InFactoryBatchId,
                            SamplerName = commonDAO.GetMachineNameByCode(this.MachineCode),
                            SampleType = eSamplingType.机械采样.ToString(),
                            SamplingId = entity.SamplingId
                        };

                        if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
                        {
                            if (commonDAO.SelfDber.Insert(new InfQCJXCYJUnloadResult
                            {
                                SampleCode = entity.SampleCode,
                                BarrelCode = entity.BarrelCode,
                                UnloadTime = entity.UnloadTime,
                                DataFlag = entity.DataFlag
                            }) > 0)
                            {
                                entity.DataFlag = 1;
                                DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

                                res++;
                            }
                        }
                    }
                }
            }
            output(string.Format("同步卸样结果 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
        }
    }
}


