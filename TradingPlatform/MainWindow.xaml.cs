using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.View.MainWindowControl;
using TradingPlatform.ViewModel.Quotation;

namespace TradingPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer;
        public MainWindow()
        {
            //LoginLogic model = new LoginLogic();
            //ResultModel result = model.SendMessage("15620938880", CheckCodeTypeEnum.REGISTER.ToString());
            //Log4Helper.Info(this.GetType(), "abc");
            InitializeComponent();


            //Task task = Task.Factory.StartNew(() => {
            //    var dic = new Dictionary<string, string>();
            //    dic.Add("contractCode", "FDAX1906");
            //    WebSocketUtility ws = WebSocketUtility.Create("ws://k.quotation.qianzijr.com/webSocket/market", dic);
            //    ws.Connect(delegate (string data)
            //    {
            //        Quotation obj = JsonHelper.JsonToObj<Quotation>(data);
            //        List<Quotation> objList = new List<Quotation>();
            //        objList.Add(obj);
            //        this.grid_saffer.Dispatcher.Invoke(new Action(() => { this.grid_saffer.ItemsSource = objList; }));
            //    });
            //}, TaskCreationOptions.LongRunning);

            //var dic = new Dictionary<string, string>();
            //string result = ApiHelper.SendPost("http://k.quotation.qianzijr.com/app/quotation/latestPrice", dic, "get");
            BindList();
            InitTimer();
            this.timer.Start();
        }

        private void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 1000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        }
        public List<Quotation> objList = new List<Quotation>();
        public void BindList()
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                Quotation quotation = new Quotation();
                quotation.topic = "topic";
                quotation.data = new data();

                quotation.data.SerialNumber = i + 1;
                quotation.data.contractCode = "SK" + i;
                quotation.data.contractName = "合约名" + i;
                quotation.data.ask = random.Next(-10, 100).ToString();
                quotation.data.bid = random.Next(-10, 100).ToString();
                quotation.data.openPrice = random.Next(-10, 100).ToString();
                quotation.data.volume = random.Next(-10, 100).ToString();
                quotation.data.lowestPrice = random.Next(-10, 100).ToString();
                quotation.data.latestPrice = random.Next(-10, 100).ToString();
                quotation.data.highestPrice = random.Next(-10, 100).ToString();
                quotation.data.bidVol = random.Next(-10, 100).ToString();
                quotation.data.askVol = random.Next(-10, 100).ToString();

                objList.Add(quotation);
            }
            this.grid_saffer.Dispatcher.Invoke(new Action(() => { this.grid_saffer.ItemsSource = objList; }));
        }
        private void TimerUp(object sender, ElapsedEventArgs e)
        {
            Random random = new Random();
            int tmp = random.Next(0, 20);
            objList.FindAll(x => x.data.SerialNumber == tmp).ForEach(x =>
              {
                  x.data.ask = random.Next(-10, 100).ToString();
                  x.data.bid = random.Next(-10, 100).ToString();
                  x.data.openPrice = random.Next(-10, 100).ToString();
                  x.data.volume = random.Next(-10, 100).ToString();
                  x.data.lowestPrice = random.Next(-10, 100).ToString();
                  x.data.latestPrice = random.Next(-10, 100).ToString();
                  x.data.highestPrice = random.Next(-10, 100).ToString();
                  x.data.bidVol = random.Next(-10, 100).ToString();
                  x.data.askVol = random.Next(-10, 100).ToString();
              });
            this.grid_saffer.Dispatcher.Invoke(new Action(() =>
            {
                this.grid_saffer.ItemsSource = objList;
                this.grid_saffer.Items.Refresh();
            }));
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
            this.btn_fangda.Visibility = Visibility.Collapsed;
            this.btn_huanyuan.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// 窗口还原
        /// </summary>
        private void btn_normal_Click(object sender, RoutedEventArgs e)
        {
            this.btn_fangda.Visibility = Visibility.Visible;
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

        /// <summary>
        /// 行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {



            DataGrid dg = (DataGrid)sender;
            Quotation rowSelected = dg.SelectedItem as Quotation;
            if (rowSelected != null)
            {
                string contractCode = rowSelected.data.contractCode;
                string contractName = rowSelected.data.contractName;


                bool isExists = false;
                for (int i = 0; i < this.Tab_Page.Items.Count; i++)
                {
                    if ((this.Tab_Page.Items[i] as TabItem).Name == contractCode + (new Random().Next(0, 5).ToString()))
                    {
                        this.Tab_Page.SelectedIndex = i;
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
                    //TabItem tab_new = new TabItem() { Header = "New", Margin = new Thickness(2, 0, 0, 0) };
                    //Style tabstyle = (Style)this.FindResource("Tab_Page");
                    //tab_new.Style = tabstyle; ;
                    //tab_new.Name = "New" + (new Random().Next(0, 5).ToString());
                    //this.Tab_Page.Items.Add(tab_new);
                    //this.Tab_Page.SelectedItem = tab_new;

                    TabItemWithClose item = new TabItemWithClose();
                    item.Header = contractName;
                    item.Margin = new Thickness(1, 0, 1, 0);
                    item.Height = 50;
                    item.Name = contractCode;
                    Label lbl = new Label() { Content = string.Format("{0}", Tab_Page.Items.Count) };
                    StackPanel sPanel = new StackPanel();
                    sPanel.Children.Add(lbl);
                    item.Content = sPanel;
                    Tab_Page.Items.Add(item);
                    this.Tab_Page.SelectedItem = item;
                }
            }
        }
    }
}
