using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 游戏逻辑托管组件，用于统一管理游戏逻辑实例的生命周期。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/GameLogic")]
    public class GameLogicComponent : GameFrameworkComponent<GameLogicComponent>
    {
        /// <summary>
        /// 是否已经销毁。
        /// </summary>
        public bool IsShutdown => m_GameLogicManager.IsShutdown;

        /// <summary>
        /// 游戏逻辑管理器实例。
        /// </summary>
        private IGameLogicManager m_GameLogicManager;

        protected override void Awake()
        {
            base.Awake();
            m_GameLogicManager = GameFrameworkEntry.GetModule<IGameLogicManager>();
            if (m_GameLogicManager == null)
            {
                Log.Fatal("GameLogic manager is invalid.");
                return;
            }
        }

        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void AddGameLogic(IGameLogic gameLogic)
        {
            m_GameLogicManager.AddGameLogic(gameLogic);
        }

        /// <summary>
        /// 移除受托管的游戏逻辑实例
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void RemoveGameLogic(IGameLogic gameLogic)
        {
            m_GameLogicManager.RemoveGameLogic(gameLogic);
        }
    }
}