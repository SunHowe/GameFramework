//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Localization;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 本地化辅助器基类。
    /// </summary>
    public abstract class LocalizationHelperBase : MonoBehaviour, ILocalizationHelper
    {
        /// <summary>
        /// 获取系统语言。
        /// </summary>
        public abstract Language SystemLanguage
        {
            get;
        }
    }
}
