﻿using System;
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
        /// <summary>
        /// 当前交易所id
        /// </summary>
        public string exchangeId { get; set; }
        /// <summary>
        /// 当前产品id
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 当前合约id
        /// </summary>
        public string contractId { get; set; }
        public ContractModel contractModel;
        public MainPage()
        {
            #region 获取MQTT连接信息
            try
            {

                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> header = new Dictionary<string, string>();
                string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
                header.Add("GeneralParam", GeneralParam);
                header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);

                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.mqttinfo, dic, header, "post");

                ResultModel<MqttEntity> loginsession = JsonHelper.JsonToObj<ResultModel<MqttEntity>>(result);
                MqttEntity._instance = loginsession.data;
                MqttEntity._instance.clientId = SoftwareInformation.Instance().deviceId;
                #endregion

                #region 连接MQTT
                SubscribeClient subscribeClient = new SubscribeClient(MqttEntity._instance);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
        }
        /// <summary>
        /// 页面加载完成执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
            //计算买入卖出按钮上数值
            ReloadTotal();
            this.labTitle.Content = this.contractId = "CL1906";
        }
        /// <summary>
        /// 主页切换合约
        /// </summary>
        /// <param name="exchangeId"></param>
        /// <param name="productId"></param>
        /// <param name="contractId"></param>
        public void ChangePageInfo(string exchangeId, string productId, string contractId,string newprice)
        {
            if (this.contractId != contractId)//如果和当前合约不一致就修改
            {
                this.exchangeId = exchangeId;
                this.productId = productId;
                this.contractId = contractId;
                this.labTitle.Content = contractId;
                Init();
                activanum = 0;
            }
        }
        private int activanum = 0;
        /// <summary>
        /// 当前窗口活动时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            if (activanum == 0)
            {
                activanum++;
                Init();
            }
        }
        /// <summary>
        /// 初始化合约信息
        /// </summary>
        public void Init()
        {
            ///加载合约配置信息
            IntilContract();
        }
        /// <summary>
        /// 最小变动价位
        /// </summary>
        public double priceTick;
        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int volumeMultiple;
        /// <summary>
        /// 加载合约配置信息
        /// </summary>
        public void IntilContract()
        {
            #region 初始化合约配置
            if (!string.IsNullOrEmpty(this.contractId) && this.contractId != "填写合约代码")
            {
                try
                {
                    Dictionary<string, string> dic_Contract = new Dictionary<string, string>();
                    dic_Contract.Add("contractId", this.contractId);
                    Dictionary<string, string> header_Contract = new Dictionary<string, string>();
                    string result = ApiHelper.SendPostByHeader(InterfacePath.Default.heyuepeizhi, dic_Contract, header_Contract, "post");
                    ResultModel<ContractModel> resultmodel_Contract = JsonHelper.JsonToObj<ResultModel<ContractModel>>(result);
                    contractModel = resultmodel_Contract.data;

                    this.txt_topic.Text = this.contractId;
                    this.volume.Text = contractModel.volumeMultiple.ToString();
                    this.priceTick = Convert.ToDouble(contractModel.priceTick);
                    this.volumeMultiple = Convert.ToInt32(contractModel.volumeMultiple);
                    this.limitPrice.Text = contractModel.latestPrice;
                }
                catch (Exception ex)
                {
                    //这里不做处理，因为合约代码可能不完整引发请求异常
                }
            }
            else {
                this.priceTick = 0.01;
                this.volumeMultiple = 1;
            }
            #endregion
        }
        /// <summary>
        /// 修改合约编号更新合约信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_topic_KeyUp(object sender, KeyEventArgs e)
        {
            this.contractId = this.txt_topic.Text;
            ///加载合约配置信息
            IntilContract();
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
            if (selectindex == 2)//委托
            {
                //string result = ApiHelper.SendPostByHeader(InterfacePath.Default.weituo, dic, header, "post");
                //ResultModel<EntrustModel_List> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel_List>>(result);
                //if (resultmodel.code == 402)
                //{
                //    ReloadLogin();
                //}
                ResultModel<EntrustModel_List> resultmodel = new ResultModel<EntrustModel_List>();
                LoadList(resultmodel, 2);
                this.grid_entrust.Dispatcher.Invoke(new Action(() => { this.grid_entrust.ItemsSource = resultmodel.data.list; }));
            }
            if (selectindex == 3)//成交记录
            {
                //string result = ApiHelper.SendPostByHeader(InterfacePath.Default.chengjiaojilu, dic, header, "post");
                //ResultModel<RecordModel> resultmodel = JsonHelper.JsonToObj<ResultModel<RecordModel>>(result);
                //if (resultmodel.code == 402)
                //{
                //    ReloadLogin();
                //}
                ResultModel<RecordModel> resultmodel = new ResultModel<RecordModel>();
                LoadList(resultmodel, 3);
                this.grid_record.Dispatcher.Invoke(new Action(() => { this.grid_record.ItemsSource = resultmodel.data.list; }));
            }

        }
        public T LoadList<T>(T t, int selectindex)
        {
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
            if (selectindex == 2)//委托
            {
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.weituo, dic, header, "post");
                ResultModel<EntrustModel_List> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel_List>>(result);
                if (resultmodel.code == 402)
                {
                    ReloadLogin();
                }
                if ((t as ResultModel<EntrustModel_List>).data == null)
                {
                    (t as ResultModel<EntrustModel_List>).data = resultmodel.data;
                    return t;
                }
                foreach (EntrustModel item in resultmodel.data.list)
                {
                    (t as ResultModel<EntrustModel_List>).data.list.Add(item);
                }
                return t;
            }
            if (selectindex == 3)//成交记录
            {
                string result = ApiHelper.SendPostByHeader(InterfacePath.Default.chengjiaojilu, dic, header, "post");
                ResultModel<RecordModel> resultmodel = JsonHelper.JsonToObj<ResultModel<RecordModel>>(result);
                if (resultmodel.code == 402)
                {
                    ReloadLogin();
                }

                if ((t as ResultModel<RecordModel>).data == null)
                {
                    (t as ResultModel<RecordModel>).data = resultmodel.data;
                    return t;
                }
                foreach (RecordModel_List item in resultmodel.data.list)
                {
                    (t as ResultModel<RecordModel>).data.list.Add(item);
                }
                return t;
            }
            return t;
        }
        /// <summary>
        /// 买入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("direction", "BUY");//BUY,SELL
            dic.Add("instrumentId", this.contractId);
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
            dic.Add("direction", "SELL");//BUY,SELL
            dic.Add("instrumentId", this.contractId);
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
            dic.Add("instrumentId", this.contractId);
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
            this.volume.Text = (Convert.ToInt32(this.volume.Text) + this.volumeMultiple).ToString();
            ReloadTotal();
        }
        private void JiaoYiShouShuReduce_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToInt32(this.volume.Text) > 0)
            {
                this.volume.Text = (Convert.ToInt32(this.volume.Text) - this.volumeMultiple).ToString();
                ReloadTotal();
            }
        }

        private void JiaoYiDanJiaAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.limitPrice.Text = (Convert.ToDouble(this.limitPrice.Text) + this.priceTick).ToString();
            ReloadTotal();
        }
        private void JiaoYiDanJiaReduce_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToDouble(this.limitPrice.Text) > 0)
            {
                this.limitPrice.Text = (Convert.ToDouble(this.limitPrice.Text) - this.priceTick).ToString();
                ReloadTotal();
            }
        }
        /// <summary>
        /// 重新计算买入卖出数值
        /// </summary>
        public void ReloadTotal()
        {
            this.txtPurchase.Text = this.txtSellOut.Text = (Convert.ToDouble(this.volume.Text) * Convert.ToDouble(this.limitPrice.Text)).ToString("0.00");
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
            ResultModel<EntrustModel_List> resultmodel = JsonHelper.JsonToObj<ResultModel<EntrustModel_List>>(result);
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
            MainPage view = new MainPage();
            view.Show();
        }

    }
}
