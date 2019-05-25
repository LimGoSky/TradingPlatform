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
        public string code { get; set; }
        public string msg { get; set; }
        public List<QuotationChildren> data { get; set; }
    }
    public class QuotationChildren
    {
        public List<contractDtoDetail> contractDtoList { get; set; }
        public Int64? count { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
    }

    public class contractDtoList
    {
        public List<contractDtoDetail> contractDtoDetail { get; set; }

    }
    public class contractDtoDetail
    {
        public string iscontaint {

            get { return contractId == null ? "" : "/Styles/images/home_side_like.png"; }
        }
        /// <summary>
        /// 卖1价
        /// </summary>
        public string askPrice1 { get; set; }


        /// <summary>
        /// 卖1量
        /// </summary>
        public string askVolume1 { get; set; }
        /// <summary>
        ///  买1价
        /// </summary>
        public string bidPrice1 { get; set; }

        public string bidVolume1 { get; set; }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string contractId { get; set; }


        /// <summary>
        /// 合约名称
        /// </summary>
        public string contractName { get; set; }



        public string endTradeDate { get; set; }
        /// <summary>
        /// 交易所代码
        /// </summary>
        public string exchangeId { get; set; }



        public string highestPrice { get; set; }
        /// <summary>
        /// 最新价
        /// </summary>
        public string lastPrice { get; set; }






        /// <summary>
        /// 最新成交价
        /// </summary>
        public string lastVolume { get; set; }


        /// <summary>
        /// 跌停板价
        /// </summary>
        public string lowerLimitPrice { get; set; }



        public string lowestPrice { get; set; }


        /// <summary>
        /// 是否主力
        /// </summary>
        public bool main { get; set; }



        public string openInterest { get; set; }



        public string openPrice { get; set; }


        public string preClosePrice { get; set; }


        /// <summary>
        /// 昨结价
        /// </summary>
        public string preSettlementPrice { set; get; }

        /// <summary>
        /// 产品id
        /// </summary>
        public string productId { get; set; }



        /// <summary>
        /// 产品名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 总成交价
        /// </summary>
        public string totalVolume { get; set; }



        /// <summary>
        /// 涨停板价
        /// </summary>
        public string upperLimitPrice { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 合约编号
        /// </summary>
        public string contractCode { get; set; }


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
        /// 最新价
        /// </summary>
        public string latestPrice { get; set; }




        /// <summary>
        /// 成交量
        /// </summary>
        public string volume { get; set; }
    }
}
