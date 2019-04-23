using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Model_Business
{
    public class PriceStruct
    {
        public PriceStruct(string text,string value) {
            this.text = text;
            this.value=value;
        }
        public string text { get; set; }
        public string value { get; set; }
    
    }
}
