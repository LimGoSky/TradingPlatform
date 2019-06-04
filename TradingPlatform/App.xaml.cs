using Lierda.WPFHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.Common;
using TradingPlatform.View.Login;
using TradingPlatform.ViewModel.Login;

namespace TradingPlatform
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        LierdaCracker cracker = new LierdaCracker();
        protected override void OnStartup(StartupEventArgs e)
        {
            cracker.Cracker();

            base.OnStartup(e);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ///退出交易登录
            if (!string.IsNullOrEmpty(BussinesLoginer.bussinesLoginer.sessionId))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> header = new Dictionary<string, string>();
                string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
                header.Add("GeneralParam", GeneralParam);
                header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.bussineloginout, dic, header, "post");
            }
        }
    }
}
