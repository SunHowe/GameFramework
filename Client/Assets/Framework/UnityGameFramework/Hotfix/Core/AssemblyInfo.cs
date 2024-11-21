using System;

namespace UnityGameFramework.Runtime
{
    [Serializable]
    public class AssemblyInfo
    {
        /// <summary>
        /// 程序集名字
        /// </summary>
        public string Name;
        
        /// <summary>
        /// 版本号 框架将采用文件的md5
        /// </summary>
        public string Version;
        
        /// <summary>
        /// 程序集定义的guid
        /// </summary>
        public string Guid;
    }
}