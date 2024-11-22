//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    public abstract class GameFrameworkComponent : MonoBehaviour
    {
        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected virtual void Awake()
        {
            GameEntry.RegisterComponent(this);
        }
    }

    /// <summary>
    /// 游戏框架组件抽象类，提供单例属性供外部使用。
    /// </summary>
    public abstract class GameFrameworkComponent<T> : GameFrameworkComponent where T : GameFrameworkComponent<T>
    {
        public static T Instance { get; private set; }

        protected override void Awake()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException($"Can not initialize Game Framework component '{typeof(T).FullName}' more than once.");
            }

            Instance = (T)this;
            base.Awake();
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}