using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Common
{
    /// <summary>
    /// 短信响应状态枚举
    /// </summary>
    public enum MessageStateEnum
    {
        OK = 200,
        验证码错误 = 301,
        短信发送频繁 =302,
        发送失败 = 303,
        连续多次输入验证码错误, 验证码已失效, 请重新获取 = 304,
        参数错误 = 401,
        用户已存在 = 420,
        用户不存在 = 421,
        密码错误 = 422,
        网络错误 = 500,
    }

    /// <summary>
    /// 发送短信验证码类型
    /// </summary>
    public enum CheckCodeTypeEnum
    {
        LOGIN = 1,
        REGISTER =2,
        RESET_PASSWORD = 3
    }
}
