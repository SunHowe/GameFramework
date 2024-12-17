using System;
using System.Collections.Generic;
using GameFramework;

namespace GameLogic
{
    /// <summary>
    /// 玩法抽象类。
    /// </summary>
    public abstract class GameBase
    {
        /// <summary>
        /// 玩法实例id，由GameModule赋值。
        /// </summary>
        public int Id { get; internal set; }
        
        /// <summary>
        /// 属于该玩法的游戏逻辑列表。
        /// </summary>
        private readonly List<IGameLogic> m_GameLogicList = new();
        
        public void Awake(object userData)
        {
            OnAwake(userData);
            
            for (var index = 0; index < m_GameLogicList.Count; index++)
            {
                var gameLogic = m_GameLogicList[index];
                try
                {
                    gameLogic.Awake();
                }
                catch (Exception e)
                {
                    GameFrameworkLog.Fatal(e.ToString());
                }
            }
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

        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        protected void AddGameLogic(IGameLogic gameLogic)
        {
            m_GameLogicList.Add(gameLogic);
        }

        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        protected void AddGameLogic<T>() where T : IGameLogic, new()
        {
            AddGameLogic(new T());
        }
        
        /// <summary>
        /// 派生类重载该方法进行添加玩法的逻辑。
        /// </summary>
        protected abstract void OnAwake(object userData);
    }
}