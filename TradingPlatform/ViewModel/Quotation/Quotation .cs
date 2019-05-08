using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TradingPlatform.ViewModel.Quotation
{
    public class Quotation
    {
        public string topic { get; set; }
        public data data { get; set; }

    }
    public class data
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber { get; set; }
        /// <summary>
        /// 合约编号
        /// </summary>
        public string contractCode { get; set; }


        /// <summary>
        /// 合约名
        /// </summary>
        public string contractName { get; set; }
        /// <summary>
        /// 买价
        /// </summary>
        public string ask { get; set; }
        /// <summary>
        /// 买量
        /// </summary>
        public string askVol { get; set; }

        /// <summary>
        /// 卖价
        /// </summary>
        public string bid { get; set; }

        /// <summary>
        /// 卖量
        /// </summary>
        public string bidVol { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public string highestPrice { get; set; }


        /// <summary>
        /// 最新价
        /// </summary>
        public string latestPrice { get; set; }


        /// <summary>
        /// 最低价
        /// </summary>
        public string lowestPrice { get; set; }


        /// <summary>
        /// 开盘价
        /// </summary>
        public string openPrice { get; set; }


        /// <summary>
        /// 成交量
        /// </summary>
        public string volume { get; set; }
    }
}
