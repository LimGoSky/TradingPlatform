using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TradingPlatform.ViewModel.Login;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
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
        /// <param name="msg">消息</param>
        private void LoginOK(String msg)
        {
            if(msg == "LoginOK")
            {
                MainWindow model = new MainWindow();
                //打开主窗体
                model.Show();
                //关闭登陆框
                this.Close();
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
            if (view.UserLogin)
            {
                view.Login();
            }
            if(view.Password == "")
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

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "http://www.baidu.com";
            proc.Start();
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "http://www.baidu.com";
            proc.Start();
        }
    }
}
