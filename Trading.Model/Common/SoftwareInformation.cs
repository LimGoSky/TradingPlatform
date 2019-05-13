using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Common
{
    public static class SoftwareInformation
    {
        /// <summary>
        /// api版本:1.0.0 
        /// </summary>
        public static string apiVersion { get; set; }
        /// <summary>
        /// app版本:1.0.0
        /// </summary>
        public static string appVersion { get; set; }
        /// <summary>
        /// 来源:PC
        /// </summary>
        public static string origin { get; set; }
        /// <summary>
        /// 系统:WINDOWS
        /// </summary>
        public static string os { get; set; }
        /// <summary>
        /// 系统版本:从系统获取
        /// </summary>
        public static string osVersion { get; set; }
        /// <summary>
        /// 设备号:全局唯一,用于唯一区分一台设备
        /// </summary>
        public static string deviceId { get; set; }
        /// <summary>
        /// 推广渠道: 默认填写self
        /// </summary>
        public static string channel { get; set; }
        /// <summary>
        /// app市场渠道:默认填写self
        /// </summary>
        public static string appChannel { get; set; }
        /// <summary>
        /// 软件包名:默认填写TTCJ
        /// </summary>
        public static string appType { get; set; }
        /// <summary>
        /// 设备制造商:从系统获取
        /// </summary>
        public static string manufacturer { get; set; }
        /// <summary>
        /// 设备型号:从系统获取
        /// </summary>
        public static string model { get; set; }
    }
}
