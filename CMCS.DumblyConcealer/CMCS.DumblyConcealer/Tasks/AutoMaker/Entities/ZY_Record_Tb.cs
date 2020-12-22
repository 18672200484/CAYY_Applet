using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车制样机接口 - 制样记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("ZY_Record_Tb")]
    public class ZY_Record_Tb
    {
        private String _MachineCode;
        /// <summary>
        /// 制样机编号：APS1（1#制样机）；APS2（2#制样机）
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private String _SampleID;
        /// <summary>
        /// 制样编码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

        private String _PackCode;
        /// <summary>
        /// 封装码
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public String PackCode
        {
            get { return _PackCode; }
            set { _PackCode = value; }
        }

        private Int32 _ZYWeight;
        /// <summary>
        /// 来样重量
        /// </summary>
        public Int32 ZYWeight
        {
            get { return _ZYWeight; }
            set { _ZYWeight = value; }
        }

        private String _ZYType;
        /// <summary>
        /// 制样方式 =1：在线制样=2：离线制样
        /// </summary>
        public String ZYType
        {
            get { return _ZYType; }
            set { _ZYType = value; }
        }

        private String _SampleType;
        /// <summary>
        /// 出样类型 =1：6mm全水分样1
        /// =2：6mm全水分样2
        /// =3：3mm分析样
        /// =4：0.2mm一般试验分析
        /// =5：0.2mm存查样
        /// </summary>
        public String SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }

        private String _Size;
        /// <summary>
        /// 粒度
        /// =1：6mm
        /// =2：3mm
        /// =3：0.2mm
        /// </summary>
        public String Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        private DateTime _StartTime;
        /// <summary>
        /// 制样开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private DateTime _EndTime;
        /// <summary>
        /// 制样完成时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        private Int32 _SamepleWeight;
        /// <summary>
        /// 出样重量 单位：g
        /// </summary>
        public Int32 SamepleWeight
        {
            get { return _SamepleWeight; }
            set { _SamepleWeight = value; }
        }

        private String _UserName;
        /// <summary>
        /// 工作人员
        /// </summary>
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 0：未读取；1：已读取（制样机读该命令,读完写1）2:管控系统传输命令不符合要求(制样机需在报警表中告知不符合要求的原因)
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }
    }
}
