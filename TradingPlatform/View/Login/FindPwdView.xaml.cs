using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trading.Common;
using Trading.Common.Common;
using Trading.Logic;
using Trading.Model.Common;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// FindPwdView.xaml 的交互逻辑
    /// </summary>
    public partial class FindPwdView : Page
    {
        Timer timer;
        int secondCount = 60;//减秒
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

            InitTimer();
            this.timer.Stop();
        }

        private void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 1000;
            timer = new Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        }

        private void TimerUp(object sender, ElapsedEventArgs e)
        {
            this.Second.Dispatcher.Invoke(CountDown);
        }

        /// <summary>
        /// 减秒
        /// </summary>
        public void CountDown()
        {
            Second.Content = secondCount + "s";
            if (secondCount == 0)
            {
                this.timer.Stop();
            }
            else
            {
                secondCount--;
            }
        }

        public void Sure(string msg)
        {
            if (msg == "userNameError")
            {
                MessageBox.Show("手机号码格式不正确！");
                return;
            }
            else if (msg == "checkCodeError")
            {
                MessageBox.Show("验证码不能为空！");
                return;
            }
            else if (msg == "passWordError")
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            else if (msg == "200")
            {
                MessageBox.Show("找回成功！");
            }
            else
            {
                MessageBox.Show("注册失败，请联系管理员！");
                return;
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

        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRepeat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!PhoneHelper.ValidateMobile(PhoneNo.Text))
            {
                MessageBox.Show("手机号格式不正确！");
                return;
            }
            else
            {
                //发短信
                LoginLogic logic = new LoginLogic();
                ResultModel model = logic.SendMessage(PhoneNo.Text, CheckCodeTypeEnum.RESET_PASSWORD.ToString());
                if (model.code == 200)
                {
                    secondCount = 59;
                    BtnRepeat.Content = "重发";
                    this.timer.Start();
                }
                else
                {
                    string result = ((MessageStateEnum)model.code).ToString();
                    Log4Helper.Error(this.GetType(), $"手机号：{PhoneNo.Text}发送重置密码短信失败！原因：{result}");
                    MessageBox.Show(result);
                }
            }
        }
    }
}
