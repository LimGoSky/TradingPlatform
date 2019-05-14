using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Trading.Common
{
    public class SubscribeClient
    {
        /// <summary>        
        ///  实例化订阅客户端        
        ///   </summary>        
        public Action<Object, MqttMsgPublishEventArgs> ClientPublishReceivedAction { get; set; }
        public SubscribeClient(string clientId)
        {
            MqttClient client = new MqttClient("trade.xgj.alibaba.com");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.Connect(clientId);
            client.Subscribe(new string[] { "/common/mqttInfo" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received         
            ClientPublishReceivedAction.Invoke(sender, e);
        }
    }

}
