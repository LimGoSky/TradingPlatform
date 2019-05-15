using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.Common
{
    public class BussinesLoginer
    {
        private BussinesLoginer() { }
        private static BussinesLoginer _bussinesLoginer = new BussinesLoginer();
        public static BussinesLoginer bussinesLoginer
        {
            get { return _bussinesLoginer; }
        }
        public string user { get; set; }
        public string sessionId { get; set; }

    }
}
