
namespace CMCS.Monitor.Win.Core
{
	/// <summary>
	/// 硬件设备类
	/// </summary>
	public class Hardwarer
	{
		static IOC.JMDM20DIOV2.JMDM20DIOV2Iocer iocer = new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer();
		/// <summary>
		/// IO控制器
		/// </summary>
		public static IOC.JMDM20DIOV2.JMDM20DIOV2Iocer Iocer
		{
			get { return iocer; }
		}
	}
}
