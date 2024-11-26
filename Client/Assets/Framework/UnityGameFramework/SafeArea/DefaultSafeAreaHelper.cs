using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认的安全区域辅助器。
    /// </summary>
    public sealed class DefaultSafeAreaHelper : SafeAreaHelperBase
    {
        private void Update()
        {
            SetSafeArea(Screen.safeArea);
        }
    }
}