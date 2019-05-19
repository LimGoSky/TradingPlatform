using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Trading.Model.Common;
using TradingPlatform.Business;
using TradingPlatform.Common;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// TradeLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class TradeLoginView : Window
    {
        public TradeLoginView()
        {
            InitializeComponent();

            //密码框将文本改为黑点
            PwdTextBox.TextDecorations = new TextDecorationCollection(new TextDecoration[] {
                new TextDecoration() {
                     Location= TextDecorationLocation.Strikethrough,
                      Pen= new Pen(Brushes.White, 10f) {
                          DashCap =  PenLineCap.Round,
                           StartLineCap= PenLineCap.Round,
                            EndLineCap= PenLineCap.Round,
                             DashStyle= new DashStyle(new double[] {0.0,1.2 }, 0.6f)
                      }
                }
            });
        }

        /// <summary>
        /// 无边框拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void BussinesLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("user", "1297005");
            dic.Add("password", "1297005");
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.bussinelogin, dic, header, "post");

            ResultModel<BussinesLoginer> loginsession = JsonHelper.JsonToObj<ResultModel<BussinesLoginer>>(result);
            BussinesLoginer.bussinesLoginer.sessionId = loginsession.data.sessionId;
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
    }
}
