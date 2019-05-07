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
using Trading.Common.Common;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// QuickLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class QuickLoginView : Page
    {
        public QuickLoginView()
        {
            InitializeComponent();
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
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            string strPhone = PhoneNO.Text;
            if(!PhoneHelper.ValidateMobile(strPhone))
            {
                MessageBox.Show("手机号码格式不正确！");
                return;
            }
            SmsLoginView v = new SmsLoginView(strPhone);
            NavigationService.Navigate(v);
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
    }
}
