//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    internal sealed partial class UIManager : GameFrameworkModule, IUIManager
    {
        private sealed class OpenUIFormInfo : IReference
        {
            private int m_SerialId;
            private string m_UIFormAssetName;
            private UIGroup m_UIGroup;
            private bool m_PauseCoveredUIForm;
            private object m_UserData;
            private object m_FormInstance;
            private bool m_IsLoaded;
            private bool m_IsNewInstance;
            private float m_Duration;

            public OpenUIFormInfo()
            {
                m_SerialId = 0;
                m_UIFormAssetName = null;
                m_UIGroup = null;
                m_PauseCoveredUIForm = false;
                m_UserData = null;
                m_FormInstance = null;
                m_IsLoaded = false;
                m_IsNewInstance = false;
                m_Duration = 0f;
            }

            public int SerialId
            {
                get
                {
                    return m_SerialId;
                }
            }

            public string UIFormAssetName
            {
                get
                {
                    return m_UIFormAssetName;
                }
            }

            public UIGroup UIGroup
            {
                get
                {
                    return m_UIGroup;
                }
            }

            public bool PauseCoveredUIForm
            {
                get
                {
                    return m_PauseCoveredUIForm;
                }
            }

            public object UserData
            {
                get
                {
                    return m_UserData;
                }
            }

            public object FormInstance
            {
                get
                {
                    return m_FormInstance;
                }
            }

            public bool IsLoaded
            {
                get
                {
                    return m_IsLoaded;
                }
            }

            public bool IsNewInstance
            {
                get
                {
                    return m_IsNewInstance;
                }
            }

            public float Duration
            {
                get
                {
                    return m_Duration;
                }
            }

            /// <summary>
            /// 加载完成。
            /// </summary>
            public void OnLoadDone(object formInstance, float duration)
            {
                m_FormInstance = formInstance;
                m_IsLoaded = true;
                m_IsNewInstance = true;
                m_Duration = duration;
            }

            public static OpenUIFormInfo Create(int serialId, string uiFormAssetName, object formInstance, UIGroup uiGroup, bool pauseCoveredUIForm, object userData)
            {
                OpenUIFormInfo openUIFormInfo = ReferencePool.Acquire<OpenUIFormInfo>();
                openUIFormInfo.m_SerialId = serialId;
                openUIFormInfo.m_UIFormAssetName = uiFormAssetName;
                openUIFormInfo.m_FormInstance = formInstance;
                openUIFormInfo.m_UIGroup = uiGroup;
                openUIFormInfo.m_PauseCoveredUIForm = pauseCoveredUIForm;
                openUIFormInfo.m_UserData = userData;
                openUIFormInfo.m_IsLoaded = formInstance != null;
                openUIFormInfo.m_IsNewInstance = !openUIFormInfo.IsLoaded;
                openUIFormInfo.m_Duration = 0f;
                return openUIFormInfo;
            }

            public void Clear()
            {
                m_SerialId = 0;
                m_UIFormAssetName = null;
                m_FormInstance = null;
                m_UIGroup = null;
                m_PauseCoveredUIForm = false;
                m_UserData = null;
                m_IsLoaded = false;
                m_IsNewInstance = false;
                m_Duration = 0f;
            }
        }
    }
}
