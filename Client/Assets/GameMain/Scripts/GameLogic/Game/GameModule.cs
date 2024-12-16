using System;
using System.Collections.Generic;
using GameFramework;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 玩法管理模块。允许统一时间有多个玩法实例处于运行状态。
    /// </summary>
    public sealed class GameModule : GameLogicBase<GameModule>
    {
        /// <summary>
        /// 玩法实例列表。
        /// </summary>
        private readonly List<GameBase> m_GameList = new List<GameBase>();

        /// <summary>
        /// 自增长id。
        /// </summary>
        private int m_IdIncrease = 0;
        
        protected override void OnAwake()
        {
        }

        protected override void OnShutdown()
        {
            StopAllGame();
        }

        /// <summary>
        /// 开始新的玩法。
        /// </summary>
        /// <param name="game">玩法实例。</param>
        /// <returns>玩法实例id</returns>
        public int StartGame(GameBase game)
        {
            game.Id = ++m_IdIncrease;
            m_GameList.Add(game);
            
            // 初始化玩法。
            game.Awake();
            return game.Id;
        }

        /// <summary>
        /// 开始新的玩法。
        /// </summary>
        public int StartGame<T>() where T : GameBase, new()
        {
            return StartGame(new T());
        }

        /// <summary>
        /// 停止指定实例id的玩法。
        /// </summary>
        public void StopGame(int gameId)
        {
            var index = m_GameList.FindIndex(game => game.Id == gameId);
            if (index < 0)
            {
                return;
            }
            
            var game = m_GameList[index];
            m_GameList.RemoveAt(index);
            
            // 销毁玩法。
            game.Shutdown();
        }

        /// <summary>
        /// 停止指定类型的玩法。
        /// </summary>
        /// <param name="gameType">玩法类型。</param>
        public void StopGame(Type gameType)
        {
            var index = m_GameList.FindIndex(game => game.GetType() == gameType);
            if (index < 0)
            {
                return;
            }
            
            var game = m_GameList[index];
            m_GameList.RemoveAt(index);
            
            // 销毁玩法。
            game.Shutdown();
        }

        /// <summary>
        /// 停止指定类型的玩法。
        /// </summary>
        public void StopGame<T>() where T : GameBase
        {
            StopGame(typeof(T));
        }

        /// <summary>
        /// 停止所有符合指定类型的玩法。
        /// </summary>
        /// <param name="gameType">玩法类型。</param>
        public void StopAllGame(Type gameType)
        {
            for (var i = 0; i < m_GameList.Count; i++)
            {
                var game = m_GameList[i];
                if (game.GetType() != gameType)
                {
                    continue;
                }
                
                // 将当前index与最后index交换
                var lastIndex = m_GameList.Count - 1;
                if (i != lastIndex)
                {
                    m_GameList[i] = m_GameList[lastIndex];
                }
                
                m_GameList.RemoveAt(lastIndex);
                
                // 销毁玩法。
                try
                {
                    game.Shutdown();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                }
            }
        }

        /// <summary>
        /// 停止所有符合指定类型的玩法。
        /// </summary>
        public void StopAllGame<T>() where T : GameBase
        {
            StopAllGame(typeof(T));
        }

        /// <summary>
        /// 停止所有玩法。
        /// </summary>
        public void StopAllGame()
        {
            if (m_GameList.Count == 0)
            {
                return;
            }
            
            foreach (var game in m_GameList)
            {
                try
                {
                    // 销毁玩法。
                    game.Shutdown();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                }
            }

            m_GameList.Clear();
        }
    }
}