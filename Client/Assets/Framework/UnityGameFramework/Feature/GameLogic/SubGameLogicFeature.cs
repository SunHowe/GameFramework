using System;
using System.Collections.Generic;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 子游戏逻辑功能模块。可以用于管理子游戏逻辑。
    /// </summary>
    public class SubGameLogicFeature : IFeature
    {
        private readonly List<IGameLogic> m_GameLogicList = new List<IGameLogic>();
        
        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void AddGameLogic(IGameLogic gameLogic)
        {
            try
            {
                gameLogic.Awake();
            }
            catch (Exception e)
            {
                Log.Fatal(e.ToString());
                return;
            }
            
            m_GameLogicList.Add(gameLogic);
        }

        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        public T AddGameLogic<T>() where T : IGameLogic, new()
        {
            var gameLogic = new T();
            AddGameLogic(gameLogic);
            return gameLogic;
        }

        /// <summary>
        /// 移除受托管的游戏逻辑实例
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        public void RemoveGameLogic(IGameLogic gameLogic)
        {
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
        
        public void Awake(object featureOwner, FeatureContainer featureContainer)
        {
        }

        public void Shutdown()
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
    }
}