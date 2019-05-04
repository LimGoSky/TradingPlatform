using GalaSoft.MvvmLight.Messaging;
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

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// FindPwdView.xaml 的交互逻辑
    /// </summary>
    public partial class FindPwdView : Page
    {
        public FindPwdView()
        {
            InitializeComponent();
            //密码框将文本改为黑点
            Pwd.TextDecorations = new TextDecorationCollection(new TextDecoration[] {
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

            //监听ViewModel发来的异步消息
            Messenger.Default.Register<string>(this, "resetPassWord", new Action<string>(Sure));
        }

        public void Sure(string msg)
        {
            if (msg == "OK")
            {
                MessageBox.Show("重置成功！");
            }
            else if (msg == "timeout" || msg == "error")
            {
                MessageBox.Show("重置失败！");
            }
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginView v = new LoginView();
            NavigationService.Navigate(v);
        }
    }
}
