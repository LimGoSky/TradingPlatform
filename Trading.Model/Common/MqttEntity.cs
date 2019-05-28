using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Common
{
    /// <summary>
    /// MQTT接口信息
    /// </summary>
    public class MqttEntity
    {
        public static MqttEntity _instance { get; set; }
        public static MqttEntity Instance()
        {
            if (_instance == null)
                _instance = new MqttEntity();
            return _instance;
        }
        public string groupId { get; set; }
        public string host { get; set; }
        public string password { get; set; }
        public string topic { get; set; }
        public string userName { get; set; }
        public string clientId { get; set; }
    }
}
