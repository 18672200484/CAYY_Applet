using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Utilities
{
    /// <summary>
    /// 化验指标计算辅助类
    /// </summary>
    public class AssayCalcUtil
    {
        /// <summary>
        /// 计算两个化验指标的平均值，默认保留两位小数,使用四舍五入
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="decimals">保留小数位</param>
        /// <returns></returns>
        public static decimal CalcAvgValueS(decimal value1, decimal value2, int decimals)
        {
            double d_value1 = (double)(Math.Truncate(value1 * 10000) / 10000m);
            double d_value2 = (double)(Math.Truncate(value2 * 10000) / 10000m);

            return (decimal)Math.Round((Math.Round(d_value1, decimals) + Math.Round(d_value2, decimals)) / 2d, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 计算两个化验指标的平均值，默认保留两位小数,使用四舍六入五成双
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="decimals">保留小数位</param>
        /// <returns></returns>
        public static decimal CalcAvgValue(decimal value1, decimal value2, int decimals = 2)
        {
            value1 = (decimal)Math.Truncate(value1 * 10000) / 10000m;
            value2 = (decimal)Math.Truncate(value2 * 10000) / 10000m;

            return Math.Round((Math.Round(value1, decimals) + Math.Round(value2, decimals)) / 2m, decimals);
        }

        /// <summary>
        /// 计算两个化验指标的平均值，默认保留两位小数,使用四舍六入五成双
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="decimals">保留小数位</param>
        /// <returns></returns>
        public static decimal CalcAvgValueCount(List<decimal> value)
        {
            decimal sumvalue = 0;
            foreach (decimal item in value)
            {
                sumvalue += Math.Round(Math.Truncate(item * 10000) / 10000m, 2);
            }

            return Math.Round(sumvalue / value.Count, 2);
        }

        /// <summary>
        /// 计算两个化验指标的平均值，
        /// 先用四舍六入五单双保留两位小数，再用四舍六入五单双保留一位小数
        /// 再取两个指标值的平均值，保留一位小数。
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static decimal GetAvgValue(decimal value1, decimal value2)
        {
            value1 = Math.Round((decimal)Math.Truncate(Math.Round((decimal)Math.Truncate(value1 * 10000) / 10000m, 2) * 10000) / 10000m, 1);
            value2 = Math.Round((decimal)Math.Truncate(Math.Round((decimal)Math.Truncate(value2 * 10000) / 10000m, 2) * 10000) / 10000m, 1);

            return Math.Round((value1 + value2) / 2m, 1);
        }

        /// <summary>
        /// 四舍六入五单双
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="value">保留小数位数</param>
        /// <returns></returns>
        public static decimal GetTrunValue(decimal value, int decl)
        {
            value = Math.Round((decimal)Math.Truncate(value * 10000) / 10000m, decl);
            return value;
        }

        /// <summary>
        /// 四舍六入五单双
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="decimals">保留小数位数</param>
        /// <returns></returns>
        public static decimal GetTruncate(decimal value, int decimals)
        {
            value = (decimal)Math.Truncate(value * 10000) / 10000m;
            return Math.Round(value, decimals);
        }

        /// <summary>
        /// 计算多个化验指标的平均值，默认保留两位小数,使用四舍六入五成双
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="decimals">保留小数位</param>
        /// <returns></returns>
        public static decimal CalcAvgValue(IList<decimal> values, int decimals = 2)
        {
            IList<decimal> value1 = new List<decimal>();
            value1 = values.Select(a => (decimal)Math.Truncate(a * 10000) / 10000m).ToList();
            //foreach (decimal value in values)
            //{
            //    value1.Add((decimal)Math.Truncate(value * 10000) / 10000m);
            //}
            decimal sum = value1.Sum(a => Math.Round(a, decimals));
            return Math.Round(sum / value1.Count() * 1m, decimals);
        }

        #region 国标重复性限判断

        /// <summary>
        /// 判断两个弹筒热值(Qb,ad)MJ/kg的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveQbad(decimal value1, decimal value2)
        {
            return Math.Abs((value1 - value2) * 1000) <= 120;
        }

        /// <summary>
        /// 判断两个空干基灰分(A,ad)%的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveStad(decimal value1, decimal value2)
        {
            decimal avg = CalcAvgValue(value1, value2);
            decimal diff = Math.Abs(value1 - value2);

            if (avg < 1)
                return diff <= 0.05m;
            else
                return diff <= 0.1m;
        }

        /// <summary>
        /// 判断两个全水分(Mt)%的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveMt(decimal value1, decimal value2)
        {
            decimal avg = CalcAvgValue(value1, value2);
            decimal diff = Math.Abs(value1 - value2);

            if (avg < 10)
                return diff <= 0.4m;
            else
                return diff <= 0.5m;
        }

        /// <summary>
        /// 判断两个空干基水分(M,ad)%的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveMad(decimal value1, decimal value2)
        {
            decimal avg = CalcAvgValue(value1, value2);
            decimal diff = Math.Abs(value1 - value2);

            if (avg < 5)
                return diff <= 0.2m;
            else if (avg >= 5 && avg <= 10)
                return diff <= 0.3m;
            else
                return diff <= 0.4m;
        }

        /// <summary>
        /// 判断两个空干基挥发分(V,ad)%的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveVad(decimal value1, decimal value2)
        {
            decimal avg = CalcAvgValue(value1, value2);
            decimal diff = Math.Abs(value1 - value2);

            if (avg < 20)
                return diff <= 0.3m;
            else if (avg >= 20 && avg <= 40)
                return diff <= 0.5m;
            else
                return diff <= 0.8m;
        }

        /// <summary>
        /// 判断两个空干基灰分(A,ad)%的值是否符合国标重复性限
        /// </summary>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns></returns>
        public static bool IsEffectiveAad(decimal value1, decimal value2)
        {
            decimal avg = CalcAvgValue(value1, value2);
            decimal diff = Math.Abs(value1 - value2);

            if (avg < 15)
                return diff <= 0.2m;
            else if (avg >= 15 && avg <= 30)
                return diff <= 0.3m;
            else
                return diff <= 0.5m;
        }

        #endregion

        #region 人工化验计算公式

        /// <summary>
        /// 根据公式计算全水分(Mt)%
        /// </summary>
        /// <param name="totalWeightAfterBurn">烘干后的总质量</param>
        /// <param name="containerWeight">称量瓶重量</param>
        /// <param name="sampleWeight">样重</param>
        /// <returns></returns>
        public decimal CalcMt(decimal totalWeightAfterBurn, decimal containerWeight, decimal sampleWeight)
        {
            if (sampleWeight == 0) return 0;
            if (totalWeightAfterBurn == 0) return 0;

            return (containerWeight + sampleWeight - totalWeightAfterBurn) / sampleWeight * 100;
        }

        /// <summary>
        /// 根据公式计算空干基水分(M,ad)%
        /// </summary>
        /// <param name="totalWeightAfterBurn">烘干后的总质量</param>
        /// <param name="containerWeight">称量瓶重量</param>
        /// <param name="sampleWeight">样重</param>
        /// <returns></returns>
        public decimal CalcMad(decimal totalWeightAfterBurn, decimal containerWeight, decimal sampleWeight)
        {
            if (sampleWeight == 0) return 0;
            if (totalWeightAfterBurn == 0) return 0;

            return (containerWeight + sampleWeight - totalWeightAfterBurn) / sampleWeight * 100;
        }

        /// <summary>
        /// 根据公式计算空干基挥发分(V,ad)%
        /// </summary>
        /// <param name="totalWeightAfterBurn">烘干后的总质量</param>
        /// <param name="containerWeight">称量瓶重量</param>
        /// <param name="sampleWeight">样重</param>
        /// <param name="mad">全水分(Mt)%</param>
        /// <returns></returns>
        public decimal CalcVad(decimal totalWeightAfterBurn, decimal containerWeight, decimal sampleWeight, decimal mad)
        {
            if (sampleWeight == 0) return 0;
            if (totalWeightAfterBurn == 0) return 0;

            return (containerWeight + sampleWeight - totalWeightAfterBurn) / sampleWeight * 100 - mad;
        }

        /// <summary>
        /// 根据公式计算空干基灰分(A,ad)%
        /// </summary>
        /// <param name="totalWeightAfterBurn">烘干后的总质量</param>
        /// <param name="containerWeight">称量瓶重量</param>
        /// <param name="sampleWeight">样重</param>
        /// <returns></returns>
        public decimal CalcAad(decimal totalWeightAfterBurn, decimal containerWeight, decimal sampleWeight)
        {
            if (sampleWeight == 0) return 0;
            if (totalWeightAfterBurn == 0) return 0;

            return (totalWeightAfterBurn - containerWeight) / sampleWeight * 100;
        }

        #endregion

        #region 检测指标是否在重复性限范围内
        /// <summary>
        /// 检测指标是否在重复性限范围内
        /// </summary>
        /// <param name="values">化验结果值</param>
        /// <param name="Type">类型</param>
        /// <returns></returns>
        public static bool CheckIsInArea(IList<decimal> values, string Type)
        {
            bool flag = true;
            decimal t = -1m;//重复性限值T
            if (values.Count <= 0) return false;

            decimal minVal = values.Min();
            switch (Type)
            {
                case "Mt":
                    if (minVal <= 10m)
                        t = 0.4m;
                    else
                        t = 0.5m;
                    break;
                case "Mad":
                    if (minVal < 5m)
                        t = 0.2m;
                    else if (minVal >= 5m && minVal <= 10m)
                        t = 0.3m;
                    else if (minVal > 10m)
                        t = 0.4m;
                    break;
                case "Aad":
                    if (minVal < 15m)
                        t = 0.2m;
                    else if (minVal >= 15m && minVal <= 30m)
                        t = 0.3m;
                    else if (minVal > 30m)
                        t = 0.5m;
                    break;
                case "Vad":
                    if (minVal < 20m)
                        t = 0.3m;
                    else if (minVal >= 20m && minVal <= 40m)
                        t = 0.5m;
                    else if (minVal > 40m)
                        t = 0.8m;
                    break;
                case "Stad":
                    if (minVal <= 1.5m)
                        t = 0.05m;
                    else if (minVal > 1.5m && minVal <= 4m)
                        t = 0.1m;
                    else if (minVal > 4m)
                        t = 0.2m;
                    break;
                case "Qbad":
                    t = 120m;
                    break;
                default:
                    break;
            }
            if (t == -1m) return false;

            decimal diffVal = values.Max() - values.Min();
            if (values.Count == 2 && Math.Abs(diffVal) > t)
                flag = false;
            else if (values.Count == 3 && Math.Abs(diffVal) > (1.2m * t))
                flag = false;
            else if (values.Count == 4 && Math.Abs(diffVal) > (1.3m * t))//大于1.3T并且没有任意三个值的极差<=1.2T
            {
                IList<Decimal> temp1 = new List<Decimal>() { values[0], values[1], values[2] };
                IList<Decimal> temp2 = new List<Decimal>() { values[0], values[1], values[3] };
                IList<Decimal> temp3 = new List<Decimal>() { values[0], values[2], values[3] };
                IList<Decimal> temp4 = new List<Decimal>() { values[1], values[2], values[3] };
                if (Math.Abs(temp1.Max() - temp1.Min()) <= (1.2m * t) || Math.Abs(temp2.Max() - temp2.Min()) <= (1.2m * t) || Math.Abs(temp3.Max() - temp3.Min()) <= (1.2m * t) || Math.Abs(temp4.Max() - temp4.Min()) <= (1.2m * t))
                    flag = true;
                else
                    flag = false;
            }
            else if (values.Count < 2 || values.Count > 4)
                flag = false;
            else
                flag = true;

            return flag;
        }
        #endregion
    }
}
