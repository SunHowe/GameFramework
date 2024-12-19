//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    internal sealed class ShowEntityInfo : IReference
    {
        private Type m_EntityLogicType;
        private object m_UserData;
        private AutoResetUniTaskCompletionSource<Entity> m_Task;

        public ShowEntityInfo()
        {
            m_EntityLogicType = null;
            m_UserData = null;
        }

        public Type EntityLogicType
        {
            get
            {
                return m_EntityLogicType;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        public AutoResetUniTaskCompletionSource<Entity> Task
        {
            get
            {
                return m_Task;
            }
        }

        public static ShowEntityInfo Create(Type entityLogicType, object userData, AutoResetUniTaskCompletionSource<Entity> task = null)
        {
            ShowEntityInfo showEntityInfo = ReferencePool.Acquire<ShowEntityInfo>();
            showEntityInfo.m_EntityLogicType = entityLogicType;
            showEntityInfo.m_UserData = userData;
            showEntityInfo.m_Task = task;
            return showEntityInfo;
        }

        public void Clear()
        {
            m_EntityLogicType = null;
            m_UserData = null;
            m_Task = null;
        }
    }
}
