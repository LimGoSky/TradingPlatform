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

namespace TradingPlatform.Business
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            #region 获取MQTT连接信息
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);

            string result = ApiHelper.SendPostByHeader("http://trade.xgj.alibaba.com/common/mqttInfo", dic, header, "post");

            ResultModel<MqttEntity> loginsession = JsonHelper.JsonToObj<ResultModel<MqttEntity>>(result);
            MqttEntity._instance = loginsession.data;
            #endregion

            #region 连接MQTT
            SubscribeClient subscribeClient = new SubscribeClient(MqttEntity._instance);
            #endregion

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
                string result = ApiHelper.SendPostByHeader("http://trade.xgj.alibaba.com/trading/position/list", dic, header, "post");
                //ResultModel<HoldModel> resultmodel = JsonHelper.JsonToObj<ResultModel<HoldModel>>(result);

                //this.grid_hold.Dispatcher.Invoke(new Action(() => { this.grid_hold.ItemsSource = resultmodel.data.list; }));
            }
            if (selectindex == 1)//委托
            {
                string result = ApiHelper.SendPostByHeader("http://trade.xgj.alibaba.com/trading/order/list", dic, header, "post");
                ResultModel<EntrustModel> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel>>(result);

                this.grid_entrust.Dispatcher.Invoke(new Action(() => { this.grid_entrust.ItemsSource = resultmodel.data.list; }));
            }
            if (selectindex == 2)//成交记录
            {
                string result = ApiHelper.SendPostByHeader("http://trade.xgj.alibaba.com/trading/trade/list", dic, header, "post");
                ResultModel<RecordModel> resultmodel = JsonHelper.JsonToObj<ResultModel<RecordModel>>(result);

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
            dic.Add("direction", "BUY");
            dic.Add("instrumentId", "");
            dic.Add("limitPrice", "100");
            dic.Add("offsetFlag", "OPEN");
            dic.Add("priceType", "MARKET_PRICE");
            dic.Add("stopPrice", "100");
            dic.Add("volume", "1");
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.maimai, dic, header, "post");
            ResultModel resultmodel = JsonHelper.JsonToObj<ResultModel>(result);
            if (resultmodel.code== 200)
            {
                MessageBox.Show("提交成功！");
            }
        }
        /// <summary>
        /// 卖出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
