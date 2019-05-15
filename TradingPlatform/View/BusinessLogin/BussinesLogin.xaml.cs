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
using Trading.Model.Common;
using System.Net;
using System.IO;

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

        #region 标题栏窗口事件
        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion 标题栏事件
        private void BussinesLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("user", "1297005");
            dic.Add("password", "1297005");
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            string result = ApiHelper.SendPostByHeader("http://trade.xgj.alibaba.com/user/login", dic, header, "post");

            ResultModel<BussinesLoginer> loginsession = JsonHelper.JsonToObj<ResultModel<BussinesLoginer>>(result);
            BussinesLoginer.bussinesLoginer.sessionId = loginsession.data.sessionId;
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
    }
}
