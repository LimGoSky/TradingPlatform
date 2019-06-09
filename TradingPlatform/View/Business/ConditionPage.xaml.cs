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
using Trading.Common;
using Trading.Model.Common;
using TradingPlatform.Common;

namespace TradingPlatform.View.Business
{
    /// <summary>
    /// ConditionPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConditionPage : Window
    {
        /// <summary>
        /// 当前合约id
        /// </summary>
        public string contractId { get; set; }
        public ConditionPage(string contractId)
        {
            InitializeComponent();
            this.contractId = contractId;
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

        /// <summary>
        /// 添加条件
        /// </summary>
        private void AddCondition()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("direction", "BUY");//BUY,SELL

                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("GeneralParam", JsonHelper.ToJson(SoftwareInformation.Instance()));
                header.Add("Authorization", "Bearer " + Loginer.LoginerUser.Token);
                ResultModel result = JsonHelper.JsonToObj<ResultModel>(ApiHelper.SendPost(InterfacePath.Default.maimai, dic, header, "post"));
                if (result.code == 200)
                {
                    MessageBox.Show("设置成功！");
                }
                else
                {
                    MessageBox.Show("设置失败！");
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Error(this.GetType(), "设置条件单：" + ex);
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("instrumentId", "");//合约编号
            dic.Add("stopPrice", "");//输入价格
            dic.Add("volume", "");//数量
            //if (this.rdo_kaicang.IsChecked == true)//开平
            //{
            //    dic.Add("offsetFlag", "OPEN");//开仓
            //}
            //else
            //{
            //    dic.Add("offsetFlag", "CLOSE");//平仓
            //}
            dic.Add("direction", "");//方向 BUY,SELL

            //价格->可选择市价,或者是手动输入价格。 若选择市价则  priceType=MARKET_PRICE, limitPrice为空 ,若不选择市价手动输入价格,则  priceType=LIMIT_PRICE,limitPrice=输入值
            dic.Add("priceType", "MARKET_PRICE");

            //dic.Add("priceType", "LIMIT_PRICE");
            //dic.Add("limitPrice", this.limitPrice.Text);

            dic.Add("contingentCondition ", "");
            //dic.Add("volume", this.volume.Text);
            Dictionary<string, string> header = new Dictionary<string, string>();
            string GeneralParam = JsonHelper.ToJson(SoftwareInformation.Instance());
            header.Add("GeneralParam", GeneralParam);
            header.Add("Authorization", BussinesLoginer.bussinesLoginer.sessionId);
            string result = ApiHelper.SendPostByHeader(InterfacePath.Default.maimai, dic, header, "post");
            ResultModel resultmodel = JsonHelper.JsonToObj<ResultModel>(result);
            if (resultmodel.code == 200)
            {
                //刷新条件列表
            }
            else if (resultmodel.code == 402)
            {
                MessageBox.Show("提交失败！");
            }
        }
    }
}
