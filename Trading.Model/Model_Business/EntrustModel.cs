using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Business
{
    /// <summary>
    /// 委托实体
    /// </summary>
    public class EntrustModel_List
    {

        public string haveNext { get; set; }
        public List<EntrustModel> list { get; set; }
    }
    public class EntrustModel
    {
        /// <summary>
        /// 撤单时间
        /// </summary>
        public string cancelTime { get; set; }
        /// <summary>
        /// IMMEDIATELY为普通单,其余为条件单的触发条件
        /// </summary>
        public string contingentCondition { get; set; }
        /// <summary>
        /// 买卖方向,买/卖
        /// </summary>
        public string direction { get; set; }
        public string Direction { get { return direction == "BUY" ? "买入" : "卖出"; } }
        /// <summary>
        /// 交易所Id
        /// </summary>
        public string exchangeId { get; set; }
        /// <summary>
        /// 强平原因,暂不用
        /// </summary>
        public string forceCloseReason { get; set; }
        /// <summary>
        /// 合约编号
        /// </summary>
        public string instrumentId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string limitPrice { get; set; } 
        public string LimitPrice { get { return orderPriceType == "MARKET_PRICE" ? "市价" : limitPrice; } }
        /// <summary>
        /// 开平标志,开/平
        /// </summary>
        public string offsetFlag { get; set; }
        public string OffsetFlag {  get { return offsetFlag == "OPEN" ? "开" : "平"; } }
        /// <summary>
        /// 报单价格条件,市价单/限价单
        /// </summary>
        public string orderPriceType { get; set; }
        /// <summary>
        /// 报单状态
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 报单提交状态
        /// </summary>
        public string orderSubmitStatus { get; set; }
        /// <summary>
        /// 报单编号
        /// </summary>
        public string orderSysId { get; set; }
        /// <summary>
        /// 委托时间
        /// </summary>
        public string orderTime { get; set; }
        public string OrderTime { get { return orderTime; } }
        /// <summary>
        /// 状态信息
        /// </summary>
        public string statusMsg { get; set; }
        /// <summary>
        /// 条件单触发价格
        /// </summary>
        public string stopPrice { get; set; }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string touchTime { get; set; }
        /// <summary>
        /// 交易日时间戳
        /// </summary>
        public string tradingDay { get; set; }
        public string TradingDay { get { return ConvertStringToDateTime(tradingDay).ToString(); } }
        /// <summary>
        /// 是否强平,暂不用
        /// </summary>
        public bool userForceClose { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public string volumeTotalOriginal { get; set; }
        /// <summary>
        /// 已成交数量
        /// </summary>
        public string volumeTraded { get; set; }

        private string ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow).ToString("yyyy-MM-dd");
        }
    }
}
