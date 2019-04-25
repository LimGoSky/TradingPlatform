using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    using Trading.Common.StompHelper;
    using WebSocketSharp;
    /// <summary>
    /// 用于WebSocket连接请求
    /// </summary>
    public class WebSocketUtility
    {

        private WebSocketUtility _this;
        /// <summary>
        /// 请求地址
        /// </summary>
        private string url;

        private string[] protocols;
        /// <summary>
        /// WebSocket对象
        /// </summary>
        private WebSocket webSocket;

        /// <summary>
        /// 订阅列表
        /// </summary>
        private Dictionary<string, string> destinations;
        
        /// <summary>
        /// 接收消息回调
        /// </summary>
        private Action<string> onMessage;

        /// <summary>
        /// 
        /// </summary>
        private StompMessageSerializer serializer;
        private WebSocketUtility(string url, params string[] protocols)
        {
            if (protocols == null)
            {
                protocols = new string[0];
            }
            this.webSocket = new WebSocket(url, protocols);
        }

        /// <summary>
        /// 创建一个WebSocket请求对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="destinations">订阅目标（key为订阅id，key必须唯一,值为订阅目标）</param>
        /// <param name="protocols">相关协议</param>
        public static WebSocketUtility Create(string url, Dictionary<string, string> destinations,params string[] protocols)
        {
            var ws = new WebSocketUtility(url, protocols);
            ws.url = url;
            ws.protocols = protocols;
            ws.destinations = destinations;
            ws.serializer = new StompMessageSerializer();
            ws.RegisterCallback();

            return ws;
        }
        /// <summary>
        /// 注册回调事件
        /// </summary>
        /// <param name="onMessage">接受服务器返回的消息回调</param>
        /// <param name="onClose">连接关闭时回调</param>
        /// <param name="onOpen">连接打开时回调</param>
        /// <param name="onError">发生错误时回调</param>
        private void RegisterCallback()
        {
            webSocket.OnMessage += OnMessage;
            webSocket.OnClose += OnClose;
            webSocket.OnOpen += OnOpen;
            webSocket.OnError += OnError;

            webSocket.Connect();
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect(Action<string> onMessage)
        {
            this.onMessage = onMessage;
            var connect = new StompMessage(StompFrame.CONNECT);
            connect["accept-version"] = "1.1";
            connect["host"] = "";
            connect["heart-beat"] = "0,0";
            webSocket.Send(serializer.Serialize(connect));
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        private void SubscribeStomp()
        {
            //var sub = new StompMessage(StompFrame.SUBSCRIBE);
            ////unique Key per subscription
            //sub["id"] = "sub-0";
            //sub["destination"] = "/topic/latest_quotation_detail_CL1906"; 
            //webSocket.Send(serializer.Serialize(sub));

            //var sub1 = new StompMessage(StompFrame.SUBSCRIBE);
            ////unique Key per subscription
            //sub1["id"] = "sub-1";
            //sub1["a"] = "/topic/ask_bid_CL1906";
            //webSocket.Send(serializer.Serialize(sub1));

            foreach (var item in destinations)
            {
                var sub = new StompMessage(StompFrame.SUBSCRIBE);
                sub["id"] = item.Key;
                sub["destination"] = item.Value;
                webSocket.Send(serializer.Serialize(sub));
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnOpen(object sender, EventArgs e)
        {
            //UpdateListBox(" ws_OnOpen says: " + e.ToString());
            Connect(onMessage);
        }
        /// <summary>
        /// 接收消息回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine(" ws_OnMessage says: " + e.Data);
            StompMessage msg = serializer.Deserialize(e.Data);
            if (msg.Command == StompFrame.CONNECTED) //连接状态
            {
                //onMessage(e.Data);
                SubscribeStomp();
            }
            else if (msg.Command == StompFrame.MESSAGE) //返回消息
            {
                //var text = e.Data;
                onMessage(e.Data);
            }
        }

        /// <summary>
        /// 连接关闭回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClose(object sender, CloseEventArgs e)
        {
            //UpdateListBox(" ws_OnClose says: " + e.ToString());
            //ConnectStomp();
        }

        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnError(object sender, ErrorEventArgs e)
        {
            //UpdateListBox(DateTime.Now.ToString() + " ws_OnError says: " + e.ToString());
        }
    }
}
