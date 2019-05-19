using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Trading.Model.Common;
using Trading.Model.Login;

namespace Trading.Logic.Login
{
    public class UserLogic
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ResultModel<UserInfoModel> GetUserInfo(string token)
        {
            string strUrl = "http://front.future.alibaba.com/user/info";
            Dictionary<string, string> param = new Dictionary<string, string>();
            return JsonHelper.JsonToObj<ResultModel<UserInfoModel>>(ApiHelper.SendPost(strUrl, param, "post",token));
        }

        public ResultModel<UserInfoModel> UpdateUserInfo(string token)
        {
            string strUrl = "http://front.future.alibaba.com/user/info/update";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("nickname", "石头");
            param.Add("profilePhoto", "");
            param.Add("qq", "123");
            return JsonHelper.JsonToObj<ResultModel<UserInfoModel>>(ApiHelper.SendPost(strUrl, param, "post", token));
        }
    }
}
