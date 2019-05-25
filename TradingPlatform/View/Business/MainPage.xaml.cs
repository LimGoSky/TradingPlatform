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
using TradingPlatform.Common;
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.View.BusinessLogin;
using TradingPlatform.View.Login;

namespace TradingPlatform.Business
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Window
    {
        public string instrumentId { get; set; }
        public MainPage()
        {
            #region 获取MQTT连接信息
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);

            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.mqttinfo, dic, header, "post");

            ResultModel<MqttEntity> loginsession = JsonHelper.JsonToObj<ResultModel<MqttEntity>>(result);
            MqttEntity._instance = loginsession.data;
            #endregion

            #region 连接MQTT
            SubscribeClient subscribeClient = new SubscribeClient(MqttEntity._instance);
            #endregion
            Init();
            InitializeComponent();
        }
        public void Init() {
            instrumentId = "CL1906";
            Task task = Task.Factory.StartNew(() =>
            {
                var dic = new Dictionary<string, string>();
                dic.Add("sub-0", "/topic/latest_quotation_CME.CL.CL1906");

                WebSocketUtility ws = WebSocketUtility.Create("ws://market.future.alibaba.com/webSocket/zd/market", dic);
                ws.Connect(delegate (string data)
                {
                    txt_topic.Text = data;
                });
            }, TaskCreationOptions.LongRunning);

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


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectindex = this.tab_bussines.SelectedIndex;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);

            if (selectindex == 0)//持仓
            {
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.chicang, dic, header, "post");
                //ResultModel<HoldModel> resultmodel = JsonHelper.JsonToObj<ResultModel<HoldModel>>(result);
                //if (resultmodel.code == 402)
                //{
                //    ReloadLogin();
                //}
                //if (resultmodel.data != null)
                //{
                //    this.grid_hold.Dispatcher.Invoke(new Action(() => { this.grid_hold.ItemsSource = resultmodel.data.list; }));
                //}
            }
            if (selectindex == 1)//委托
            {
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.weituo, dic, header, "post");
                ResultModel<EntrustModel> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel>>(result);
                if (resultmodel.code == 402)
                {
                    ReloadLogin();
                }
                this.grid_entrust.Dispatcher.Invoke(new Action(() => { this.grid_entrust.ItemsSource = resultmodel.data.list; }));
            }
            if (selectindex == 2)//成交记录
            {
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.chengjiaojilu, dic, header, "post");
                ResultModel<RecordModel> resultmodel = JsonHelper.JsonToObj<ResultModel<RecordModel>>(result);
                if (resultmodel.code == 402)
                {
                    ReloadLogin();
                }
                this.grid_record.Dispatcher.Invoke(new Action(() => { this.grid_record.ItemsSource = resultmodel.data.list; }));
            }

        }
        /// <summary>
        /// 买入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("contingentCondition", "IMMEDIATELY");
            dic.Add("direction", "BUY");//BUY,SELL
            dic.Add("instrumentId", this.instrumentId);
            dic.Add("limitPrice", this.limitPrice.Text);//若选择市价,则 priceType=MARKET_PRICE,limitPrice为空, 若选择对手价,最新价,排队价,或者用户手动输入,则priceType=LIMIT_PRICE，limitPrice为对应的价格
            if (this.rdo_kaicang.IsChecked == true)
            {
                dic.Add("offsetFlag", "OPEN");//开仓
            }
            else
            {
                dic.Add("offsetFlag", "CLOSE");//平仓
            }
            dic.Add("priceType", "LIMIT_PRICE");
            dic.Add("stopPrice", "100");
            dic.Add("volume", this.volume.Text);
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.maimai, dic, header, "post");
            ResultModel resultmodel = JsonHelper.JsonToObj<ResultModel>(result);
            if (resultmodel.code == 200)
            {
                MessageBox.Show("提交成功！");
            }
            else if (resultmodel.code == 402)
            {
                ReloadLogin();
            }
        }
        /// <summary>
        /// 卖出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellOut_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("contingentCondition", "IMMEDIATELY");
            dic.Add("direction", "SELL");//BUY,SELL
            dic.Add("instrumentId", this.instrumentId);
            dic.Add("limitPrice", this.limitPrice.Text);//若选择市价,则 priceType=MARKET_PRICE,limitPrice为空, 若选择对手价,最新价,排队价,或者用户手动输入,则priceType=LIMIT_PRICE，limitPrice为对应的价格
            if (this.rdo_kaicang.IsChecked == true)
            {
                dic.Add("offsetFlag", "OPEN");//开仓
            }
            else
            {
                dic.Add("offsetFlag", "CLOSE");//平仓
            }
            dic.Add("priceType", "LIMIT_PRICE");
            dic.Add("stopPrice", "100");
            dic.Add("volume", this.volume.Text);
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.maimai, dic, header, "post");
            ResultModel resultmodel = JsonHelper.JsonToObj<ResultModel>(result);
            if (resultmodel.code == 200)
            {
                MessageBox.Show("提交成功！");
            }
            else if (resultmodel.code == 402)
            {
                ReloadLogin();
            }
        }
        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheDan_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("exchangeId", "");
            dic.Add("instrumentId", this.instrumentId);
            dic.Add("orderSysId", "");
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.chedan, dic, header, "post");
            ResultModel resultmodel = JsonHelper.JsonToObj<ResultModel>(result);
            if (resultmodel.code == 200)
            {
                MessageBox.Show("提交成功！");
            }
            else if (resultmodel.code == 402)
            {
                ReloadLogin();
            }
        }
        /// <summary>
        /// 重新登录
        /// </summary>
        public void ReloadLogin()
        {
            MessageBox.Show("请重新登录！");
            this.Close();
            TradeLoginView bussinesLogin = new TradeLoginView();
            bussinesLogin.ShowDialog();


        }
        private void JiaoYiShouShuAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.volume.Text = (Convert.ToInt32(this.volume.Text) + 1).ToString();
        }
        private void JiaoYiShouShuReduce_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToInt32(this.volume.Text) > 0)
            {
                this.volume.Text = (Convert.ToInt32(this.volume.Text) - 1).ToString();
            }
        }

        private void JiaoYiDanJiaAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.limitPrice.Text = (Convert.ToDouble(this.limitPrice.Text) + 0.01).ToString();
        }
        private void JiaoYiDanJiaReduce_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToDouble(this.limitPrice.Text) > 0)
            {
                this.limitPrice.Text = (Convert.ToDouble(this.limitPrice.Text) - 0.01).ToString();
            }
        }

        public void TiaoJianDanList()
        {
            int selectindex = this.tab_bussines.SelectedIndex;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("condition", "true");
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);

            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.weituo, dic, header, "post");
            ResultModel<EntrustModel> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel>>(result);
            if (resultmodel.code == 402)
            {
                ReloadLogin();
            }
            this.grid_condition.Dispatcher.Invoke(new Action(() => { this.grid_condition.ItemsSource = resultmodel.data.list; }));
        }
        /// <summary>
        /// 弹出条件单编辑框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTiaoJianDan_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
