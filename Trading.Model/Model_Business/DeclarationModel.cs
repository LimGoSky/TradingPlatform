using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Business
{
    /// <summary>
    /// 报单
    /// </summary>
    public class DeclarationModel
    {
        /// <summary>
        /// 触发条件,非IMMEDIATELY时为条件单,可用值:IMMEDIATELY,LAST_PRICE_GREATER,LAST_PRICE_GREATER_EQUAL,LAST_PRICE_LESS,LAST_PRICE_LESS_EQUAL,ASK_PRICE_GREATER,ASK_PRICE_GREATER_EQUAL,ASK_PRICE_LESS,ASK_PRICE_LESS_EQUAL,BID_PRICE_GREATER,BID_PRICE_GREATER_EQUAL,BID_PRICE_LESS,BID_PRICE_LESS_EQUAL,TOUCH,TOUCH_PROFIT,PARKED_ORDER
        /// </summary>
        public string contingentCondition { get; set; }
        /// <summary>
        /// 买卖,可用值:BUY,SELL
        /// </summary>
        public string direction { get; set; }
        public string instrumentId { get; set; }
        /// <summary>
        ///   limitPrice
        /// </summary>
        public double limitPrice { get; set; }
        /// <summary>
        /// 开平标志,可用值:OPEN,CLOSE
        /// </summary>
        public string offsetFlag { get; set; }
        /// <summary>
        /// 价格类型,市价/限价,可用值:MARKET_PRICE,LIMIT_PRICE
        /// </summary>
        public string priceType { get; set; }
        /// <summary>
        /// 条件单触发价格
        /// </summary>
        public double stopPrice { get; set; }
        public int volume { get; set; }
    }
}
