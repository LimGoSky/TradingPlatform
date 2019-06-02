using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Business
{
    public class ContractModel
    {
        /// <summary>
        /// 合约id
        /// </summary>
        public string contractId { get; set; }

        /// <summary>
        /// 合约名称
        /// </summary>
        public string contractName { get; set; }

        public string createTime { get; set; }


        /// <summary>
        /// 最后交易日
        /// </summary>
        public string endTradeDate { get; set; }

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string exchangeId { get; set; }


        /// <summary>
        /// 交易合约id
        /// </summary>
        public string exchangeInstId { get; set; }

        public string expireDate { get; set; }


        /// <summary>
        /// 手续费率
        /// </summary>
        public string freeRatio { get; set; }

        /// <summary>
        /// 固定手续费
        /// </summary>
        public string freeValue { get; set; }

        public string id { get; set; }

        /// <summary>
        /// 是否主力
        /// </summary>
        public string main { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public string marginRatio { get; set; }

        /// <summary>
        /// 固定保证金
        /// </summary>
        public string marginValue { get; set; }

        /// <summary>
        /// 最小变动价位
        /// </summary>
        public string priceTick { get; set; }


        /// <summary>
        /// 产品id
        /// </summary>
        public string productId { get; set; }

        public string productName { get; set; }

        /// <summary>
        /// 合约日期
        /// </summary>
        public string startDelivDate { get; set; }
        public string status { get; set; }
        public string updateTime { get; set; }



        public string version { get; set; }

        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public string volumeMultiple { get; set; }

        /// <summary>
        /// 最新价
        /// </summary>
        public string latestPrice { get; set; }
    }
}
