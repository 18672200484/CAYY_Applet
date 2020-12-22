using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车机械采样机接口 - 采样机卸料表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_Down_Opt")]
    public class KY_Down_Opt
    {
        private String _SupplierCode;
        /// <summary>
        /// 供应商代码
        /// </summary>
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }

        private Int32 _XL_Start;
        /// <summary>
        /// 卸料开始  当为1时，采样机开始准备卸料，进入卸料状态时将状态改为2，卸料故障时为3，卸料完成时燃管将其改为0 
        /// </summary>
        public Int32 XL_Start
        {
            get { return _XL_Start; }
            set { _XL_Start = value; }
        }

        private Int32 _XL_Finish;
        /// <summary>
        /// 卸料完成  采样机完成操作后，将值改为1，燃管读到1时改写为0
        /// </summary>
        public Int32 XL_Finish
        {
            get { return _XL_Finish; }
            set { _XL_Finish = value; }
        }

        private DateTime _LastDateTime;
        /// <summary>
        /// 最后更新时间  采样机完成操作后，将值改为1，燃管读到1时改写为0
        /// </summary>
        public DateTime LastDateTime
        {
            get { return _LastDateTime; }
            set { _LastDateTime = value; }
        }

        private String _CYJCode;
        /// <summary>
        /// 采样机编号 
        /// </summary>
        public String CYJCode
        {
            get { return _CYJCode; }
            set { _CYJCode = value; }
        }


    }
}
