using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Common
{
    /// <summary>
    /// 加解密資料物件
    /// </summary>
    public class SymCryptoModel
    {
        /// <summary>
        /// 金鑰
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 初始化向量
        /// </summary>
        public string IV { get; set; }
    }
}
