﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradingPlatform {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    public sealed partial class InterfacePath : global::System.Configuration.ApplicationSettingsBase {
        
        private static InterfacePath defaultInstance = ((InterfacePath)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new InterfacePath())));
        
        public static InterfacePath Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://trade.xgj.alibaba.com/trading/order/add")]
        public string maimai {
            get {
                return ((string)(this["maimai"]));
            }
            set {
                this["maimai"] = value;
            }
        }
    }
}
