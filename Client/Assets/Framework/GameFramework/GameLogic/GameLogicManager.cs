using System;
using System.Collections.Generic;

namespace GameFramework
{
    /// <summary>
    /// 游戏逻辑管理器实现，
    /// </summary>
    internal sealed class GameLogicManager : GameFrameworkModule, IGameLogicManager
    {
        /// <summary>
        /// 是否已经销毁。
        /// </summary>
        public bool IsShutdown { get; private set; }

        /// <summary>
        /// 游戏逻辑列表。
        /// </summary>
        private readonly List<IGameLogic> m_GameLogicList = new();

        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void AddGameLogic(IGameLogic gameLogic)
        {
            if (IsShutdown)
            {
                GameFrameworkLog.Fatal("GameLogicManager is shutdown.");
                return;
            }

            try
            {
                gameLogic.Awake();
            }
            catch (Exception e)
            {
                GameFrameworkLog.Fatal(e.ToString());
                return;
            }

            m_GameLogicList.Add(gameLogic);
        }

        /// <summary>
        /// 移除受托管的游戏逻辑实例
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void RemoveGameLogic(IGameLogic gameLogic)
        {
            if (IsShutdown)
            {
                GameFrameworkLog.Fatal("GameLogicManager is shutdown.");
                return;
            }

            try
            {
                gameLogic.Shutdown();
            }
            catch (Exception e)
            {
                GameFrameworkLog.Fatal(e.ToString());
                return;
            }

            m_GameLogicList.Remove(gameLogic);
        }

        internal override void Shutdown()
        {
            for (var index = m_GameLogicList.Count - 1; index >= 0; index--)
            {
                var gameLogic = m_GameLogicList[index];
                try
                {
                    gameLogic.Shutdown();
                }
                catch (Exception e)
                {
                    GameFrameworkLog.Fatal(e.ToString());
                }
            }

            m_GameLogicList.Clear();
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            // 游戏逻辑不直接实现Update接口 若有帧更新的需求 统一从定时器模块获取定时器进行帧更新
        }
    }
}