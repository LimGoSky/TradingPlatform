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
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var windowCollection= Application.Current.Windows;
            foreach (var item in windowCollection)
            {
               if(item.GetType() == typeof(LoginMainView))
                {
                    Window w = (Window)item;
                    MainWindow m = new MainWindow();
                    m.Show();
                    w.Close();
                }
            }
        }

        /// <summary>
        /// 快捷登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuick_Click(object sender, RoutedEventArgs e)
        {
            QuickLoginView v = new QuickLoginView();
            NavigationService.Navigate(v);
        }
    }
}
