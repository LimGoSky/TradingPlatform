using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Mqtt
{
  public  class BussinesModel
    {
        public string data { get; set; }
        public DataModel model { get; set; }
        public string type { get; set; }

    }
    public class DataModel
    {
        public string cancelTime { get; set; }
        public string contingentCondition { get; set; }
        public string direction { get; set; }
        public string exchangeId { get; set; }
        public string forceCloseReason { get; set; }
        public string instrumentId { get; set; }
        public string limitPrice { get; set; }
        public string offsetFlag { get; set; }
        public string orderPriceType { get; set; }
        public string orderStatus { get; set; }
        public string orderSubmitStatus { get; set; }
        public string orderSysId { get; set; }
        public string orderTime { get; set; }
        public string statusMsg { get; set; }
        public string stopPrice { get; set; }
        public string tradingDay { get; set; }
        public string userForceClose { get; set; }
        public string volumeTotalOriginal { get; set; }
        public string volumeTraded { get; set; }
    }
}
