using GameFramework;
using GameFramework.Timer;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 定时器组件.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Timer")]
    public sealed class TimerComponent : GameFrameworkComponent<TimerComponent>
    {
        private ITimerManager m_TimerManager;

        protected override void Awake()
        {
            base.Awake();

            m_TimerManager = GameFrameworkEntry.GetModule<ITimerManager>();
            if (m_TimerManager == null)
            {
                Log.Fatal("Timer manager is invalid.");
                return;
            }
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(TimerCallback callback)
        {
            return m_TimerManager.AddFrameTimer(callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(TimerCallback callback, object userData)
        {
            return m_TimerManager.AddFrameTimer(callback, userData);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(int interval, TimerCallback callback)
        {
            return m_TimerManager.AddFrameTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(int interval, TimerCallback callback, object userData)
        {
            return m_TimerManager.AddFrameTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(int interval, int repeatTimes, TimerCallback callback)
        {
            return m_TimerManager.AddFrameTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(int interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return m_TimerManager.AddFrameTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddTimer(float interval, TimerCallback callback)
        {
            return m_TimerManager.AddTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddTimer(float interval, TimerCallback callback, object userData)
        {
            return m_TimerManager.AddTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddTimer(float interval, int repeatTimes, TimerCallback callback)
        {
            return m_TimerManager.AddTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddTimer(float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return m_TimerManager.AddTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="timerId">定时器id。</param>
        public void RemoveTimer(int timerId)
        {
            m_TimerManager.RemoveTimer(timerId);
        }

        /// <summary>
        /// 添加延迟一帧执行一次就销毁的定时器.
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddDelayCall(TimerCallback callback)
        {
            return m_TimerManager.AddTimer(0f, 1, callback);
        }

        /// <summary>
        /// 添加延迟一帧执行一次就销毁的定时器.
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddDelayCall(TimerCallback callback, object userData)
        {
            return m_TimerManager.AddTimer(0f, 1, callback, userData);
        }

        /// <summary>
        /// 添加执行一次就销毁的定时器.
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddOnceTimer(float interval, TimerCallback callback)
        {
            return m_TimerManager.AddTimer(interval, 1, callback);
        }

        /// <summary>
        /// 添加执行一次就销毁的定时器.
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddOnceTimer(float interval, TimerCallback callback, object userData)
        {
            return m_TimerManager.AddTimer(interval, 1, callback, userData);
        }
    }
}