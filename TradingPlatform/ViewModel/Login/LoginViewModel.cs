using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Trading.Common.Common;
using Trading.Logic;
using Trading.Logic.Login;
using Trading.Model.Common;
using Trading.Model.Login;
using TradingPlatform.Common;
using TradingPlatform.SysModule;
using System.Web;

namespace TradingPlatform.ViewModel.Login
{
    public class LoginViewModel: BaseViewModel
    {

        //登录按钮绑定的命令
        public RelayCommand LoginCmd { get; private set; }
        #region 用户名/密码

        private string _Report;
        private string userName = string.Empty;
        private string passWord = string.Empty;
        private bool _IsCancel = true;
        private bool _UserChecked;
        private bool _UserLogin;

        /// <summary>
        /// 进度报告
        /// </summary>
        public string Report
        {
            get { return _Report; }
            set { _Report = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        public bool UserChecked
        {
            get { return _UserChecked; }
            set { _UserChecked = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        public bool UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        public bool IsCancel
        {
            get { return _IsCancel; }
            set { _IsCancel = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 命令(Binding Command)

        private RelayCommand _signCommand;

        public RelayCommand SignCommand
        {
            get
            {
                if (_signCommand == null)
                {
                    _signCommand = new RelayCommand(() => Login());
                }
                return _signCommand;
            }
        }
        

        #endregion

        #region Login/Exit

        public void Login()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    this.IsCancel = false;
                    var result = new LoginLogic().Login(UserName, Password);
                    if (result.code == 200)//登录成功
                    {
                        //保存登录信息
                        SaveLoginInfo();

                        #region 加载用户资料

                        UserLogic logic = new UserLogic();
                        ResultModel<UserInfoModel> userModel = logic.GetUserInfo(result.data.token);

                        Loginer.LoginerUser.UserId = userModel.data.userId;
                        Loginer.LoginerUser.NickName = userModel.data.nickname;
                        Loginer.LoginerUser.ProFilePhoto = userModel.data.profilePhoto;
                        Loginer.LoginerUser.CreateTime = userModel.data.createTime;
                        Loginer.LoginerUser.Token = result.data.token;

                        #endregion

                    }

                    //发送登录消息
                    SendMsg("Login", result.code.ToString());

                    Log4Helper.Info(this.GetType(), $"手机号:{UserName},登录：{result.msg}");
                }
            }
            catch (Exception ex)
            {
                SendMsg("Login", "Error");
                Log4Helper.Error(this.GetType(), ex);
            }
        }
        #endregion

        #region 记住密码

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFile ini = new IniFile(cfgINI);
                UserName = ini.IniReadValue("Login", "User");
                Password =ini.IniReadValue("Login", "Password");
                UserChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";
                UserLogin = ini.IniReadValue("Login", "UserLogin") == "Y";
            }
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            IniFile ini = new IniFile(cfgINI);
            if (UserChecked)
            {
                ini.IniWriteValue("Login", "User", UserName);
                ini.IniWriteValue("Login", "Password", Password);
                ini.IniWriteValue("Login", "SaveInfo", UserChecked ? "Y" : "N");
                ini.IniWriteValue("Login", "UserLogin", UserLogin ? "Y" : "N");
            }
            else
            {
                ini.IniWriteValue("Login", "User", "");
                ini.IniWriteValue("Login", "Password", "");
                ini.IniWriteValue("Login", "SaveInfo", "N");
                ini.IniWriteValue("Login", "UserLogin","N");
            }
        }

        #endregion
    }
}
