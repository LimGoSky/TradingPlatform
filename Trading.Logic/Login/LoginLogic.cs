using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Trading.Model.Common;

namespace Trading.Logic
{
    /// <summary>
    /// 用户登录管理
    /// </summary>
    public class LoginLogic
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="checkCodeType"></param>
        public ResultModel SendMessage(string mobile, string checkCodeType)
        {
            Log4Helper.Info(this.GetType(), $"手机号；{mobile}发送短信一次！");
            string strUrl = "http://front.future.alibaba.com/user/auth/checkCode/send";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("checkCodeType", checkCodeType);
            param.Add("mobile", mobile);
            param.Add("GeneralParam", JsonHelper.ToJson(SoftwareInformation.Instance()));
            string a = ApiHelper.SendPost(strUrl, param, "post");
            return JsonHelper.JsonToObj<ResultModel>(ApiHelper.SendPost(strUrl, param, "post"));

            //return new ResultModel();
        }

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="checkCodeType"></param>
        /// <param name="mobile">手机号</param>
        [Obsolete]
        public ResultModel CheckMessageCode(string checkCode, string checkCodeType, string mobile)
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/checkCode/verify";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("checkCode", checkCode);
            param.Add("checkCodeType", checkCodeType);
            param.Add("mobile", mobile);
            return JsonHelper.JsonToObj<ResultModel>(ApiHelper.SendPost(strUrl, param, "post"));
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        public ResultModel Login(string mobile, string password)
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/login/pass";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("mobile", mobile);
            param.Add("password", password);
            return JsonHelper.JsonToObj<ResultModel>(ApiHelper.SendPost(strUrl, param, "post"));
        }

        /// <summary>
        /// 短信验证码登录/注册
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public ResultModel LoginOrRegisterByCode(string mobile, string checkCode)
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/login/sms";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("mobile", mobile);
            param.Add("checkCode", checkCode);
            param.Add("GeneralParam", JsonHelper.ToJson(SoftwareInformation.Instance()));
            return JsonHelper.JsonToObj<ResultModel>(ApiHelper.SendPost(strUrl, param, "post"));
        }

        /// <summary>
        /// 令牌刷新
        /// </summary>
        /// <returns></returns>
        public ResultModel<Token> RefreshToken()
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/refreshToken";
            return JsonHelper.JsonToObj<ResultModel<Token>>(ApiHelper.SendPost(strUrl, new Dictionary<string, string>(), "post"));
        }

        /// <summary>
        /// 账户注册
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="nickname">昵称</param>
        /// <param name="password">密码</param>
        /// <param name="profilePhoto">头像</param>
        /// <param name="qq">qq</param>
        /// <returns></returns>
        public ResultModel<Token> Register(string checkCode, string mobile, string nickname, string password, string profilePhoto, string qq)
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/register";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("mobile", mobile);
            param.Add("checkCode", checkCode);
            param.Add("nickname", nickname);
            param.Add("password", password);
            param.Add("profilePhoto", profilePhoto);
            return JsonHelper.JsonToObj<ResultModel<Token>>(ApiHelper.SendPost(strUrl, param, "post"));
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ResultModel<Token> ResetPassWord(string checkCode, string mobile, string password)
        {
            string strUrl = "http://front.future.alibaba.com/user/auth/resetPass";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("checkCode", checkCode);
            param.Add("mobile", mobile);
            param.Add("password", password);
            return JsonHelper.JsonToObj<ResultModel<Token>>(ApiHelper.SendPost(strUrl, param, "post"));
        }

        public static bool Login()
        {
            return true;
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <returns>信息</returns>
        public async Task<ResultModel> Login1(string Account, string Password)
        {
            try
            {
                var task = await Task.Run(() =>
                {
                    return new ResultModel() { code = 200, data = null, msg = "登录成功！" };
                });
                bool result = task != null ? true : false;
                if (result)
                    return new ResultModel() { code = 200, data = null, msg = "登录成功！" };
                else
                    return new ResultModel() { code = 500, data = null, msg = "账号或密码错误,请确认!" };

            }
            catch (Exception ex)
            {
                Log4Helper.Error(this.GetType(), ex);
                return new ResultModel() { code = 500,data=null, msg="登录失败！" };
            }
        }

        /// <summary>
        /// 账户注册
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="nickname">昵称</param>
        /// <param name="password">密码</param>
        /// <param name="profilePhoto">头像</param>
        /// <param name="qq">qq</param>
        /// <returns></returns>
        public async Task<ResultModel> RegisterAccount(string checkCode, string mobile, string nickname, string password, string profilePhoto, string qq)
        {
            try
            {
                ResultModel<Token> model = new ResultModel<Token>();
                var task = await Task.Run(() =>
                {
                    model = Register(checkCode, mobile, nickname, password, profilePhoto, qq);
                    if (model.code == 200)
                    {
                        return model;
                    }
                    else
                    {
                        Log4Helper.Error(this.GetType(), $"手机号：{mobile},发送注册短信失败！原因：{((MessageStateEnum)model.code).ToString()},时间：{DateTime.Now.ToString()}" );
                        return null;
                    }
                });
                bool result = task != null ? true : false;
                if (result)
                    return new ResultModel() { code = 200, data = null, msg = "成功！" };
                else
                    return new ResultModel() { code = 500, data = null, msg = "失败,请确认!" };

            }
            catch (Exception ex)
            {
                Log4Helper.Error(this.GetType(), ex);
                return new ResultModel() { code = 500, data = null, msg = "失败！" };
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<ResultModel> ResetPassWord1(string checkCode, string mobile, string password)
        {
            try
            {
                var task = await Task.Run(() =>
                {
                    return new ResultModel() { code = 200, data = null, msg = "成功！" };
                });
                bool result = task != null ? true : false;
                if (result)
                    return new ResultModel() { code = 200, data = null, msg = "成功！" };
                else
                    return new ResultModel() { code = 500, data = null, msg = "失败,请确认!" };

            }
            catch (Exception ex)
            {
                Log4Helper.Error(this.GetType(), ex);
                return new ResultModel() { code = 500, data = null, msg = "失败！" };
            }
        }
    }
}
