using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Trading.Model.Common;

namespace Trading.Common
{
    public class SubscribeClient
    {
        /// <summary>        
        ///  实例化订阅客户端        
        ///   </summary>        
        public Action<Object, MqttMsgPublishEventArgs> ClientPublishReceivedAction { get; set; }
        public SubscribeClient(MqttEntity model)
        {
            string topic = model.topic;
            string host =model.host;
            // 实例化Mqtt客户端 
            MqttClient client = new MqttClient(host);

            // 注册接收消息事件 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            client.Connect(model.groupId + "@@@" + model.clientId, model.userName, model.password);

            // 订阅主题 "/home/temperature"， 订阅质量 QoS 2 
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
       
        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received         
            string str = string.Format("subscriber,topic:{0},content:{1}", e.Topic, Encoding.UTF8.GetString(e.Message));
        }
    }

}
