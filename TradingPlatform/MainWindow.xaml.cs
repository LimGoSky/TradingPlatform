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
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 重新排版加载交易所产品
            ReloadExchange();
            #endregion
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
        public string productId;
        /// <summary>
        /// 当前合约id
        /// </summary>
        public string contractId;
        /// <summary>
        /// 当前最新价
        /// </summary>
        public string newprice;

        /// <summary>
        /// 初始化socket
        /// </summary>
        public void BindSocket()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                var dic = new Dictionary<string, string>();
                dic.Add("sub-0", "/topic/latest_quotation_" + exchangeId + "." + productId+".*");

                WebSocketUtility ws = WebSocketUtility.Create("ws://market.future.alibaba.com/webSocket/zd/market", dic);
                ws.Connect(delegate (string data)
                {


                });
            }, TaskCreationOptions.LongRunning);
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
                        (win as MainPage).ChangePageInfo(exchangeId, productId, contractId, newprice);
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
                string contractCode = this.contractId = rowSelected.contractId;
                string contractName = rowSelected.contractName;
                this.newprice = rowSelected.lastPrice;


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
        #region 交易所产品

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
                    ListBoxItem listBoxItem = new ListBoxItem();
                    TextBlock text = new TextBlock();
                    text.Text = resultModel.data[i].name;
                    text.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
                    listBoxItem.Content = text;
                    this.listbox_exchange.Items.Add(listBoxItem);
                }
                (this.listbox_exchange.Items[0] as ListBoxItem).IsSelected = true;
                exchangeId = resultModel.data[0].code;
                BindProductList();//绑定列表
            }));
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;

            //修改当前交易所
            this.exchangeId = exchangeModels.Find(x => x.name == txt.Text).code;
            //重新绑定列表
            this.listbox_product.Items.Clear();
            BindProductList();

        }
        /// <summary>
        /// 把超出长度的交易所放到列表中
        /// </summary>
        public void ReloadExchange()
        {
            this.listbox_exchange.Width = this.listbox_exchange.ActualWidth;
            if ((this.listbox_exchange.ActualWidth) > (this.Width - 80))//菜单比窗口长
            {
                for (int i = this.listbox_exchange.Items.Count; i > 0; i--)
                {

                    if (this.listbox_exchange.Width - (this.listbox_exchange.Items[i - 1] as ListBoxItem).ActualWidth < (this.Width - 120))
                    {
                        break;
                    }
                    ComboBoxItem menuItem = new ComboBoxItem();

                    TextBlock text = (this.listbox_exchange.Items[i - 1] as ListBoxItem).Content as TextBlock;


                    menuItem.Content = text.Text;
                    menuItem.Selected += ChangeItem_Selected;
                    menuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF383535"));
                    listbox_exchangeother.Items.Add(menuItem);
                    this.listbox_exchange.Width = (this.listbox_exchange.Width - (this.listbox_exchange.Items[i - 1] as ListBoxItem).ActualWidth);
                    this.listbox_exchange.Items.RemoveAt(i - 1);
                }
            }
            else {
                if (this.listbox_exchangeother.Items.Count>0)
                {
                    for (int i = this.listbox_exchangeother.Items.Count; i >0; i--)
                    {
                        if ((this.listbox_exchange.Width) < (this.Width - 200))
                        {
                            ListBoxItem listBoxItem = new ListBoxItem();
                            TextBlock text = new TextBlock();
                            text.Text = (this.listbox_exchangeother.Items[i - 1] as ComboBoxItem).Content.ToString();
                            text.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
                            listBoxItem.Content = text;
                            this.listbox_exchange.Items.Add(listBoxItem);
                            this.listbox_exchange.Width += (this.listbox_exchangeother.Items[i - 1] as ComboBoxItem).Width;
                            this.listbox_exchangeother.Items.RemoveAt(i - 1);
                        }
                    }
                }
                
            }
        }

        private void ChangeItem_Selected(object sender, RoutedEventArgs e)
        {

            this.listbox_exchange.SelectedIndex = -1;
            ComboBoxItem txt = sender as ComboBoxItem;

            //修改当前交易所
            this.exchangeId = exchangeModels.Find(x => x.name == txt.Content).code;
            //重新绑定列表
            BindProductList();
        }

        /// <summary>
        /// 把超出长度的产品放到列表中
        /// </summary>
        public void ReloadProduct()
        {
            if (!(this.listbox_product.Width > 0))
            {
                this.listbox_product.Width = this.Width;
            }
            if ((this.listbox_product.Width) > (this.Width - 80))
            {
                this.listbox_product.Width = 0;
                for (int i = 0; i < this.listbox_product.Items.Count; i++)
                {
                    if (this.listbox_product.Width < this.Width - 200)
                    {
                        if ((this.listbox_product.Items[i] as ListBoxItem).ActualWidth == 0)
                        {
                            this.listbox_product.Width += ((this.listbox_product.Items[i] as ListBoxItem).Content as TextBlock).Text.Length * 10;
                        }
                        else
                        {
                            this.listbox_product.Width += (this.listbox_product.Items[i] as ListBoxItem).ActualWidth;
                        }
                    }
                    else
                    {
                        ComboBoxItem menuItem = new ComboBoxItem();

                        TextBlock text = (this.listbox_product.Items[i - 1] as ListBoxItem).Content as TextBlock;

                        menuItem.Content = text.Text;
                        menuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF383535"));
                        menuItem.Selected += ProductItem_Selected;
                        listbox_productother.Items.Add(menuItem);

                        if (i < this.listbox_product.Items.Count)
                        {
                            this.listbox_product.Items.RemoveAt(i);
                        }
                    }
                }
            }
            else
            {
                if (this.listbox_productother.Items.Count > 0)
                {
                    for (int i = this.listbox_productother.Items.Count; i > 0; i--)
                    {
                        if ((this.listbox_product.Width) < (this.Width - 200))
                        {
                            ListBoxItem listBoxItem = new ListBoxItem();
                            TextBlock text = new TextBlock();
                            text.Text = (this.listbox_productother.Items[i - 1] as ComboBoxItem).Content.ToString();
                            text.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
                            listBoxItem.Content = text;
                            this.listbox_product.Items.Add(listBoxItem);
                            this.listbox_product.Width += (this.listbox_productother.Items[i - 1] as ComboBoxItem).Width;
                            this.listbox_exchangeother.Items.RemoveAt(i - 1);
                        }
                    }
                }

            }
        }

        private void ProductItem_Selected(object sender, RoutedEventArgs e)
        {
            this.listbox_product.SelectedIndex = -1;
            ComboBoxItem txt = sender as ComboBoxItem;

            //修改当前行情编号
            this.productId = objList.Find(x => x.productName == txt.Content.ToString()).productId;
            //重新绑定列表
            BindProductList();
        }

        /// <summary>
        /// 绑定产品和列表
        /// </summary>
        public void BindProductList()
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
                Dispatcher.Invoke(new Action(() =>
                {
                    if (this.listbox_product.Items.Count == 0)
                    {
                        for (int i = 0; i < resultmodel.data.Count; i++)
                        {
                            ListBoxItem listBoxItem = new ListBoxItem();
                            TextBlock txt = new TextBlock();
                            txt.Text = resultmodel.data[i].productName;
                            txt.MouseLeftButtonDown += SecondToolBar_MouseLeftButtonDown;
                            listBoxItem.Content = txt;
                            if (i == 0)
                            {
                                listBoxItem.IsSelected = true;
                            }
                            this.listbox_product.Items.Add(listBoxItem);
                            if (i == 0)
                            {
                                this.productId = resultmodel.data[i].productId;
                            }
                        }
                        ReloadProduct();
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
                        BindSocket();
                    }
                }));
            }));
        }
        private void SecondToolBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;

            //修改当前行情编号
            this.productId = objList.Find(x => x.productName == txt.Text).productId;
            //重新绑定列表
            BindProductList();
        }
        #endregion

        private void Box_exchange_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                ReloadExchange();
                ReloadProduct();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
