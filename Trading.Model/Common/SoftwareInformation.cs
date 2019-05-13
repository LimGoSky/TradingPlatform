using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Common
{
    public class SoftwareInformation
    {
        public static SoftwareInformation _instance { get; set; }
        public static SoftwareInformation Instance()
        {
            if (_instance == null)
                _instance = new SoftwareInformation();
            return _instance;
        }
        public SoftwareInformation()
        {
            deviceId = Guid.NewGuid().ToString();
            apiVersion = "1.0.0 ";
            appVersion = "1.0.0 ";
            origin = "PC ";
            os = "WINDOWS";
            osVersion = System.Environment.OSVersion.VersionString;
            channel = "self";
            appChannel = "self";
            appType = "TTCJ ";
            manufacturer = "";//设备制造商获取不到
            model = "";//设备型号获取不到
        }

        /// <summary>
        /// api版本:1.0.0 
        /// </summary>
        public string apiVersion { get; set; }
        /// <summary>
        /// app版本:1.0.0
        /// </summary>
        public string appVersion { get; set; }
        /// <summary>
        /// 来源:PC
        /// </summary>
        public string origin { get; set; }
        /// <summary>
        /// 系统:WINDOWS
        /// </summary>
        public string os { get; set; }
        /// <summary>
        /// 系统版本:从系统获取
        /// </summary>
        public string osVersion { get; set; }
        /// <summary>
        /// 设备号:全局唯一,用于唯一区分一台设备
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 推广渠道: 默认填写self
        /// </summary>
        public string channel { get; set; }
        /// <summary>
        /// app市场渠道:默认填写self
        /// </summary>
        public string appChannel { get; set; }
        /// <summary>
        /// 软件包名:默认填写TTCJ
        /// </summary>
        public string appType { get; set; }
        /// <summary>
        /// 设备制造商:从系统获取
        /// </summary>
        public string manufacturer { get; set; }
        /// <summary>
        /// 设备型号:从系统获取
        /// </summary>
        public string model { get; set; }

        /// <summary> 
        /// PC类型 
        /// </summary> 
        /// <returns></returns> 
        private string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        private string GetDeviceID() {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    deviceId = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return deviceId;
            }
            catch 
            {

                return Guid.NewGuid().ToString();
            }
        }
    }
}
