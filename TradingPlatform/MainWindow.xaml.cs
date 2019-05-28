using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.View.MainWindowControl;
using TradingPlatform.ViewModel.Quotation;
using System.Windows.Navigation;
using TradingPlatform.Business;
using TradingPlatform.Common;
using TradingPlatform.View.BusinessLogin;
using Trading.Model.Model_Main;
using TradingPlatform.View.Login;
using System.Windows.Media;

namespace TradingPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetExchange();//绑定交易所
            BindList();//绑定列表
        }
        /// <summary>
        /// 交易所列表
        /// </summary>
        public List<ExchangeModel> exchangeModels;
        /// <summary>
        /// 所有合约集合
        /// </summary>
        public List<QuotationChildren> objList;
        /// <summary>
        /// 当前选择的合约集合
        /// </summary>
        public List<contractDtoDetail> objDetailList;
        /// <summary>
        /// 当前选中交易所Id
        /// </summary>
        public string exchangeId;
        /// <summary>
        /// 当前产品id
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 当前合约id
        /// </summary>
        public string contractId { get; set; }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public void BindList()
        {
            Task.Run((Action)(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("condition", "true");
                dic.Add("exchangeId", (string)this.exchangeId);
                Dictionary<string, string> header = new Dictionary<string, string>();
                string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
                header.Add("GeneralParam", GeneralParam);
                header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.hangqinglist, dic, header, "post");

                Quotation resultmodel = JsonHelper.JsonToObj<Quotation>(result);

                objList = resultmodel.data;
                this.grid_saffer.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.secondMenu.Items.Count == 0)
                    {
                        for (int i = 0; i < resultmodel.data.Count; i++)
                        {
                            ListBox listBox = new ListBox();
                            listBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#191919"));
                            listBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808080"));
                            listBox.BorderThickness = new Thickness(0);
                            listBox.Margin = new Thickness(0);
                            listBox.FontFamily = new FontFamily("pingfang hk"); ;
                            listBox.FontSize = 14;
                            listBox.ItemContainerStyle = Application.Current.Resources["CheckTextBlockFontStyle"] as Style;
                            ListBoxItem listBoxItem = new ListBoxItem();
                            TextBlock txt = new TextBlock();
                            txt.Text = resultmodel.data[i].productName;
                            txt.MouseLeftButtonDown += SecondToolBar_MouseLeftButtonDown; ;
                            listBoxItem.Content = txt;
                            if (i == 0)
                            {
                                listBoxItem.IsSelected = true;
                            }
                            listBox.Items.Add(listBoxItem);
                            this.secondMenu.Items.Add(listBox);
                            if (i == 0)
                            {
                                this.productId = resultmodel.data[i].productId;
                            }
                        }
                    }
                    if (objList.Count > 0)
                    {
                        if (objDetailList != null && objDetailList.Count > 0)
                        {
                            QuotationChildren model = objList.Find(x => x.productId == this.productId);
                            if (model != null)
                            {
                                objDetailList = model.contractDtoList;
                            }
                        }
                        else
                        {
                            objDetailList = objList[0].contractDtoList;
                        }
                        this.grid_saffer.ItemsSource = objDetailList;
                        Grid_saffer_SizeChanged(null, null);
                    }
                }));
            }));
        }

        private void SecondToolBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < this.secondMenu.Items.Count; i++)
            {
                ListBox listBox = (this.secondMenu.Items[i] as ListBox);
                if (listBox.Items.Count > 0)
                {
                    ListBoxItem listBoxItem = listBox.Items[0] as ListBoxItem;
                    if (listBoxItem.IsSelected)
                    {
                        listBoxItem.IsSelected = false;
                    }
                }
            }

            TextBlock txt = sender as TextBlock;
            ListBoxItem boxItem = txt.Parent as ListBoxItem;
            boxItem.IsSelected = true;

            //修改当前行情编号
            this.productId = objList.Find(x => x.productName == txt.Text).productId;
            //重新绑定列表
            BindList();
        }

        #region 标题栏窗口事件

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

        #region 标题栏事件
        /// <summary>
        /// 交易
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_TransAction(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(BussinesLoginer.bussinesLoginer.sessionId))
            {
                TradeLoginView bussinesLogin = new TradeLoginView();
                bussinesLogin.ShowDialog();
            }
            else
            {
                MainPage mainPage = new MainPage();
                mainPage.ShowDialog();
            }
        }
        #endregion

        #region 行情行双击事件
        /// <summary>
        /// 行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
            {
                datagrid_DoubleClick(sender);
                //页面传递数据
                foreach (Window win in App.Current.Windows)
                {
                    if (win.GetType() == typeof(MainPage))
                    {
                        (win as MainPage).ChangePageInfo(exchangeId, productId, contractId);
                        //win.Show();
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 行双击事件
        /// </summary>
        /// <param name="sender"></param>
        private void datagrid_DoubleClick(object sender)
        {
            DataGrid dg = (DataGrid)sender;
            contractDtoDetail rowSelected = dg.SelectedItem as contractDtoDetail;
            if (rowSelected != null && !string.IsNullOrEmpty(rowSelected.contractId))
            {
                string contractCode = contractId = rowSelected.contractId;
                string contractName = rowSelected.contractName;


                bool isExists = false;
                for (int i = 0; i < this.Tab_Page.Items.Count; i++)
                {
                    TabItem tab = this.Tab_Page.Items[i] as TabItem;
                    if (tab.Name == contractCode)
                    {
                        Dispatcher.InvokeAsync(() => Tab_Page.SelectedItem = tab);
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
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
                    Dispatcher.InvokeAsync(() => Tab_Page.SelectedItem = item);

                }
            }
        }
        #endregion

        #region 接口
        /// <summary>
        /// 获取交易所
        /// </summary>
        public void GetExchange()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> header = new Dictionary<string, string>();
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.jiaoyisuo, dic, header, "post");
            ResultModel<List<ExchangeModel>> resultModel = JsonHelper.JsonToObj<ResultModel<List<ExchangeModel>>>(result);
            exchangeModels = resultModel.data;
            Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < resultModel.data.Count; i++)
                {
                    if (!this.tooblar.Items.Contains(resultModel.data[i].name))
                    {
                        ListBox listBox = new ListBox();
                        listBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2a2a2a"));
                        listBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8B8787"));
                        listBox.BorderThickness = new Thickness(0);
                        listBox.Margin = new Thickness(0);
                        listBox.FontFamily = new FontFamily("pingfang hk"); ;
                        listBox.FontSize = 14;
                        listBox.ItemContainerStyle = Application.Current.Resources["CheckTextBlockFontStyle"] as Style;
                        ListBoxItem listBoxItem = new ListBoxItem();
                        TextBlock txt = new TextBlock();
                        txt.Text = resultModel.data[i].name;
                        txt.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
                        listBoxItem.Content = txt;
                        if (i==0)
                        {
                            listBoxItem.IsSelected = true;
                        }
                        listBox.Items.Add(listBoxItem);
                        this.tooblar.Items.Add(listBox);
                    }
                }
                exchangeId = resultModel.data[0].code;
            }));
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < this.tooblar.Items.Count; i++)
            {
                ListBox listBox = (this.tooblar.Items[i] as ListBox);
                if (listBox.Items.Count > 0)
                {
                    ListBoxItem listBoxItem = listBox.Items[0] as ListBoxItem;
                    if (listBoxItem.IsSelected)
                    {
                        listBoxItem.IsSelected = false;
                    }
                }
            }

            TextBlock txt = sender as TextBlock;
            ListBoxItem boxItem = txt.Parent as ListBoxItem;
            boxItem.IsSelected = true;

            //修改当前交易所
            this.exchangeId = exchangeModels.Find(x => x.name == txt.Text).code;
            //重新绑定列表
            BindList();

        }

        #endregion

        private void Box_exchange_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.tooblar.Width = this.Width;
        }

        private void Grid_saffer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.grid_saffer.IsLoaded)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    List<contractDtoDetail> list = this.grid_saffer.ItemsSource as List<contractDtoDetail>;
                    if (list != null && list.Count > 0)
                    {
                        this.grid_saffer.Height = (this.grid_saffer.Parent as WrapPanel).ActualHeight;
                        double tag = Convert.ToDouble(this.grid_saffer.Tag);//每行高度
                        double height = Convert.ToDouble(this.grid_saffer.Tag) * list.Count;//总高度
                        if (this.grid_saffer.Height - height > tag)//如果空白高度大于行高手动添加行
                        {
                            int rowNumber = Convert.ToInt32(Math.Round((this.grid_saffer.Height - height) / tag, 0));
                            for (int i = 0; i < rowNumber; i++)
                            {
                                objDetailList.Add(new contractDtoDetail());
                                this.grid_saffer.Items.Refresh();
                            }
                        }
                    }
                });
            }
        }

    }
}
