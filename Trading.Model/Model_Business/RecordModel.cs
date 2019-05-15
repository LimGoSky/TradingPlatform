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
    public class RecordModel {

        public string haveNext { get; set; }
        public List<EntrustModel_List> list { get; set; }
    }
    public class RecordModel_List
    {
        /// <summary>
        /// 手续费
        /// </summary>
        public int commission { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 成交时间
        /// </summary>
        public DateTime tradeTime { get; set; }

        /// <summary>
        /// 交易日时间戳
        /// </summary>
        public DateTime tradingDay { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int volume { get; set; }
        /// <summary>
        /// 买卖方向,买/卖
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 交易所Id
        /// </summary>
        public string exchangeId { get; set; }
        /// <summary>
        /// 合约编号
        /// </summary>
        public string instrumentId { get; set; }
        /// <summary>
        /// 开平标志,开/平
        /// </summary>
        public string offsetFlag { get; set; }
        /// <summary>
        /// 报单编号
        /// </summary>
        public string orderSysId { get; set; }
    }
}
