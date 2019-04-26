using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.ViewModel.Quotation
{
    public class Quotation
    {
        public string topic { get; set; }
        public data data { get; set; }

    }
    public class data
    {
        //买价
        public string ask { get; set; }
        //买量
        public string askVol { get; set; }

        //卖价
        public string bid { get; set; }

        //卖量
        public string bidVol { get; set; }


        //合约编号
        public string contractCode { get; set; }


        //合约名
        public string contractName { get; set; }


        //最高价
        public string highestPrice { get; set; }


        //最新价
        public string latestPrice { get; set; }


        //最低价
        public string lowestPrice { get; set; }


        //开盘价
        public string openPrice { get; set; }


        //成交量
        public string volume { get; set; }
    }
}
