using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Trading.Model.Common;

namespace Trading.Logic
{
    public class LoginLogic
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="checkCodeType"></param>
        public void SendMessage(string mobile,string checkCodeType)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("checkCodeType", checkCodeType);
                dic.Add("mobile", mobile);
                string strUrl = "http://front.future.alibaba.com/user/auth/checkCode/send";

                ResultModel res = HttpUtility<ResultModel>.Factory(strUrl,new { checkCodeType, mobile }).Post();
                //string result = web.DoPost(strUrl, dic);
            }
            catch (Exception ex)
            {
                Log4Helper.Error(this.GetType(), ex.Message,ex);
            }
        }


        public void Register()
        {

        }
    }
}
