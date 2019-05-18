using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Main
{
    /// <summary>
    /// 交易所
    /// </summary>
   public class ExchangeModel
    {
      public string code { get; set; }
      public string contractGroupDtoList { get; set; }
      public string name { get; set; }
      public string region { get; set; }
    }
}
