using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Login
{
    /// <summary>
    /// 用户信息实体
    /// </summary>
    public class UserInfoModel
    {
        public string userId { get; set; }

        public string nickname { get; set; }

        public string profilePhoto { get; set; }

        public string createTime { get; set; }
    }
}
