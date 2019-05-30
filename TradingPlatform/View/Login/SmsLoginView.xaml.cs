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
using System.Windows.Threading;
using Trading.Common;
using Trading.Logic;
using Trading.Logic.Login;
using Trading.Model.Common;
using Trading.Model.Login;
using TradingPlatform.Common;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// SmsLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class SmsLoginView : Page
    {
        Timer timer;
        int secondCount = 59;//减秒
        string _phoneNo = "";

        public SmsLoginView(string phoneNo)
        {            InitializeComponent();

            this._phoneNo = phoneNo;

            labPhoneNo.Content = phoneNo.Substring(phoneNo.Length - 4, 4);

            InitTimer();
            this.timer.Start();
            //发送短信
            SendSms(phoneNo);
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

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView v = new RegisterView();
            NavigationService.Navigate(v);
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            string txtSms = SmsText.Text;
            if (txtSms == "")
            {
                MessageBox.Show("验证码不能为空！");
                return;
            }

            LoginLogic logic = new LoginLogic();
            ResultModel<Token> result = logic.LoginOrRegisterByCode(InterfacePath.Default.LoginOrRegisterByCode, _phoneNo, txtSms);
            if(result.code == 200)
            {
                #region 加载用户资料

                UserLogic userlogic = new UserLogic();
                ResultModel<UserInfoModel> userModel = userlogic.GetUserInfo(InterfacePath.Default.GetUserInfo, result.data.token);

                Loginer.LoginerUser.UserId = userModel.data.userId;
                Loginer.LoginerUser.NickName = userModel.data.nickname;
                Loginer.LoginerUser.ProFilePhoto = userModel.data.profilePhoto;
                Loginer.LoginerUser.CreateTime = userModel.data.createTime;
                Loginer.LoginerUser.Token = result.data.token;

                #endregion

                var windowCollection = Application.Current.Windows;
                foreach (var item in windowCollection)
                {
                    if (item.GetType() == typeof(LoginMainView))
                    {
                        Window w = (Window)item;
                        MainWindow m = new MainWindow();
                        m.Show();
                        w.Close();
                    }
                }
            }
            else
            {
                string msg = ((MessageStateEnum)result.code).ToString();
                Log4Helper.Error(this.GetType(), $"手机号：{_phoneNo}发送注册短信失败！原因：{msg}");
                MessageBox.Show(msg);
            }
            
        }

        /// <summary>
        /// 使用密码登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PwdLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            LoginView v = new LoginView();
            NavigationService.Navigate(v);
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            QuickLoginView v = new QuickLoginView();
            NavigationService.Navigate(v);
        }

        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRepeat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            secondCount = 60;
            this.timer.Start();
            SendSms(_phoneNo);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phoneNo"></param>
        private void SendSms(string phoneNo)
        {
            //发短信
            LoginLogic logic = new LoginLogic();
            ResultModel model = logic.SendMessage(InterfacePath.Default.SendMessage, phoneNo, CheckCodeTypeEnum.LOGIN.ToString());
            if (model.code != 200)
            {
                string result = ((MessageStateEnum)model.code).ToString();
                Log4Helper.Error(this.GetType(), $"手机号：{phoneNo}发送登录短信失败！原因：{result}");
                MessageBox.Show(result);
            }
        }
    }
}
