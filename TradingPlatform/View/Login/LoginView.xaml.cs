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
            }
        }
    }
}
