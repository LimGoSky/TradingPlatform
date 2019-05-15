using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Business
{
    public class HoldModel {
        public List<HoldModel_List> list { get; set; }
    }
    /// <summary>
    /// 持仓
    /// </summary>
    public class HoldModel_List
    {
        /// <summary>
        /// 持仓可用
        /// </summary>
        public int available { get; set; }

        /// <summary>
        /// 买卖方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 合约编号
        /// </summary>
        public string instrumentId { get; set; }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public int margin { get; set; }
        /// <summary>
        /// 开仓均价
        /// </summary>
        public int openPrice { get; set; }
        /// <summary>
        /// 持仓手数
        /// </summary>
        public int position { get; set; }


    }
}
