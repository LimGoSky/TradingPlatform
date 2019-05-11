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
using Trading.Model.Model_Business;

namespace TradingPlatform.Business
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }
        #region 标题栏事件

        ///// <summary>
        ///// 窗口移动事件
        ///// </summary>
        //private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        this.DragMove();
        //    }
        //}

        //int i = 0;

        ///// <summary>
        ///// 窗口最小化
        ///// </summary>
        //private void btn_min_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized; //设置窗口最小化
        //}

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion 标题栏事件
    }
}
