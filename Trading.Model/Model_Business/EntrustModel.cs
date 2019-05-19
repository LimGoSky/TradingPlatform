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
    public class EntrustModel {

        public string haveNext { get; set; }
        public List<EntrustModel_List> list { get; set; }
    }
    public class EntrustModel_List
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
        public int limitPrice { get; set; }
        /// <summary>
        /// 开平标志,开/平
        /// </summary>
        public string offsetFlag { get; set; }
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
        /// <summary>
        /// 状态信息
        /// </summary>
        public string statusMsg { get; set; }
        /// <summary>
        /// 条件单触发价格
        /// </summary>
        public int stopPrice { get; set; }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string touchTime { get; set; }
        /// <summary>
        /// 交易日时间戳
        /// </summary>
        public DateTime tradingDay { get; set; }
        /// <summary>
        /// 是否强平,暂不用
        /// </summary>
        public bool userForceClose { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int volumeTotalOriginal { get; set; }
        /// <summary>
        /// 已成交数量
        /// </summary>
        public int volumeTraded { get; set; }
    }
}
