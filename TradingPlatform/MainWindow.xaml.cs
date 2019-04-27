using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.ViewModel.Quotation;

namespace TradingPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //LoginLogic model = new LoginLogic();
            //ResultModel result = model.SendMessage("15620938880", CheckCodeTypeEnum.REGISTER.ToString());
            //Log4Helper.Info(this.GetType(), "abc");
            InitializeComponent();


            var dic = new Dictionary<string, string>();
            dic.Add("sub-1", "/topic/latest_quotation");
            dic.Add("sub-2", "/topic/ask_bid_CL1906");
            //WebSocketUtility ws = WebSocketUtility.Create("ws://k.quotation.qianzijr.com/webSocket/market", dic);
            //ws.Connect(delegate (string data)
            //{
            //    Quotation obj = JsonHelper.JsonToObj<Quotation>(data);
            //    List<Quotation> objList = new List<Quotation>();
            //    objList.Add(obj);
            //    this.grid_saffer.Dispatcher.Invoke(new Action(() => { this.grid_saffer.ItemsSource = objList; }));
            //});
        }

        #region 标题栏事件

        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        /// <summary>
        /// 窗口移动事件
        /// </summary>
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        int i = 0;
        /// <summary>
        /// 标题栏双击事件
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;

            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                this.WindowState = this.WindowState == WindowState.Maximized ?
                              WindowState.Normal : WindowState.Maximized;
            }
        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; //设置窗口最小化
        }

        /// <summary>
        /// 窗口最大化
        /// </summary>
        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            this.btn_fangda.Visibility = Visibility.Collapsed;
            this.btn_huanyuan.Visibility = Visibility.Visible;
            rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
            this.Left = 0;//设置位置
            this.Top = 0;
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            this.Width = rc.Width;
            this.Height = rc.Height;
            this.btn_fangda.Visibility =Visibility.Collapsed;
            this.btn_huanyuan.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// 窗口还原
        /// </summary>
        private void btn_normal_Click(object sender, RoutedEventArgs e)
        {
            this.btn_fangda.Visibility =Visibility.Visible;
            this.btn_huanyuan.Visibility = Visibility.Collapsed;
            this.Left = rcnormal.Left;
            this.Top = rcnormal.Top;
            this.Width = rcnormal.Width;
            this.Height = rcnormal.Height;
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                btn_max_Click(null, null);
            }
        }
        #endregion 标题栏事件



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Business.MainPage win = new Business.MainPage();
            this.frame1.Content = win.Content;
        }
    }
}
