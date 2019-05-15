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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TradingPlatform.ViewModel.Login;
using Trading.Model.Common;
using System.Management;
using Trading.Common;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// LoginViewNew.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
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

            //监听ViewModel发来的异步消息
            Messenger.Default.Register<string>(this, "LoginOK", new Action<string>(LoginOK));

            InitRemeber();
        }

        /// <summary>
        /// 登陆成功
        /// </summary>
        /// <param name="msg"></param>

        private void LoginOK(string msg)
        {
            if (msg == "LoginOK")
            {
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
        }

        /// <summary>
        /// 初始化记得密码
        /// </summary>
        private void InitRemeber()
        {
            LoginViewModel view = new LoginViewModel();
            view.ReadConfigInfo(); //读写配置参数
            this.DataContext = view;

            if (view.Password == "")
            {
                BrushConverter conv = new BrushConverter();
                Brush bru = conv.ConvertFromInvariantString("#FFCDC78A") as Brush;
                BtnLogin.Background = (Brush)bru;

                BrushConverter conv1 = new BrushConverter();
                Brush bru1 = conv1.ConvertFromInvariantString("#FF6A6262") as Brush;
                BtnLogin.Foreground = (Brush)bru1;
                BtnLogin.Cursor = Cursors.Arrow;
                BtnLogin.IsEnabled = false;
            }
            else
            {
                BrushConverter conv = new BrushConverter();
                Brush bru = conv.ConvertFromInvariantString("#FFEEE052") as Brush;
                BtnLogin.Background = (Brush)bru;
                BtnLogin.Foreground = Brushes.Black;
                BtnLogin.Cursor = Cursors.Hand;
                BtnLogin.IsEnabled = true;
            }
        }

        private void PwdTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (PwdTextBox.Text != "")
            {
                BrushConverter conv = new BrushConverter();
                Brush bru = conv.ConvertFromInvariantString("#FFEEE052") as Brush;
                BtnLogin.Background = (Brush)bru;
                BtnLogin.Foreground = Brushes.Black;
                BtnLogin.Cursor = Cursors.Hand;
                BtnLogin.IsEnabled = true;
            }
            else
            {
                BrushConverter conv = new BrushConverter();
                Brush bru = conv.ConvertFromInvariantString("#FFCDC78A") as Brush;
                BtnLogin.Background = (Brush)bru;

                BrushConverter conv1 = new BrushConverter();
                Brush bru1 = conv1.ConvertFromInvariantString("#FF6A6262") as Brush;
                BtnLogin.Foreground = (Brush)bru1;
                BtnLogin.Cursor = Cursors.Arrow;
                BtnLogin.IsEnabled = false;
            }
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForgetPwd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FindPwdView v = new FindPwdView();
            NavigationService.Navigate(v);
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

        }

        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView v = new RegisterView();
            NavigationService.Navigate(v);
        }

        /// <summary>
        /// 快捷登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabQuick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            QuickLoginView v = new QuickLoginView();
            NavigationService.Navigate(v);
        }

    }
}
