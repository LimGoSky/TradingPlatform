using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trading.Demo
{
    using Trading.Common;
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.btn1.IsEnabled = false;
            var dic = new Dictionary<string, string>();
            dic.Add("sub-1", "/topic/latest_quotation_detail_CL1906");
            dic.Add("sub-2", "/topic/ask_bid_CL1906");
            WebSocketUtility ws = WebSocketUtility.Create("ws://k.quotation.qianzijr.com/webSocket/market",dic);
            ws.Connect(onMessage);
        }
        
        void onMessage(string data)
        {
            this.txtBox.Dispatcher.Invoke(new Action(() => { this.txtBox.Text = data; }));
            //Action action = () =>
            //{
            //    txtBox.Text = data;
            //};
            //this.txtBox.Dispatcher.BeginInvoke(action);
            //_this.txtBox.Text = data;
        }
    }
}
