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
using System.Windows.Shapes;
using Trading.Common;
using TradingPlatform.Business;
using TradingPlatform.Common;

namespace TradingPlatform.View.BusinessLogin
{
    /// <summary>
    /// BussinesLogin.xaml 的交互逻辑
    /// </summary>
    public partial class BussinesLogin : Window
    {
        public BussinesLogin()
        {
            InitializeComponent();
        }

        private void BussinesLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("apiVersion", "1.0");
            dic.Add("appVersion", "1.0");
            dic.Add("origin", "PC");
            dic.Add("os", "WINDOWS");
            dic.Add("osVersion", "8.0.0");
            dic.Add("deviceId", "1dbbf52f_8d73_4653_a61a_3cd7dea9f4ba");
            dic.Add("channel", "self");
            dic.Add("appChannel", "self");
            dic.Add("appType", "TTCJ");
            dic.Add("manufacturer", "Lenovo");
            dic.Add("model", "Lenovo xxxx");

            string result = ApiHelper.SendPost("http://k.quotation.qianzijr.com/app/quotation/latestPrice", dic, "get");
            BussinesLoginer.bussinesLoginer.sessionId = Guid.NewGuid().ToString();
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
    }
}
