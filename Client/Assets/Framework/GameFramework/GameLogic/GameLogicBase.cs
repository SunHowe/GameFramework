using System;

namespace GameFramework
{
    /// <summary>
    /// 游戏逻辑抽象类。游戏逻辑将提供受托管的单例模式供业务层使用。
    /// </summary>
    public abstract class GameLogicBase<T> : IGameLogic where T : GameLogicBase<T>
    {
        public static T Instance { get; private set; }
        
        public void Awake()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException($"{GetType().Name} already exists.");
            }

            Instance = (T)this;
            OnAwake();
        }

        public void Shutdown()
        {
            if (Instance != this)
            {
                throw new InvalidOperationException($"{GetType().Name} is not the current instance.");
            }
            
            OnShutdown();
            Instance = null;
        }

        protected abstract void OnAwake();
        protected abstract void OnShutdown();
    }
}