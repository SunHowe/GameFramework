using System.Collections.Generic;
using GameFramework.Timer;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 定时器功能模块。用于管理由自己创建的定时器，并提供帧更新接口注册的支持。
    /// </summary>
    public sealed class TimerFeature : IFeature
    {
        private readonly HashSet<int> m_TimerIdSet = new HashSet<int>();

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(TimerCallback callback)
        {
            var id = TimerComponent.Instance.AddFrameTimer(callback);
            m_TimerIdSet.Add(id);
            return id;
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(TimerCallback callback, object userData)
        {
            var id = TimerComponent.Instance.AddFrameTimer(callback, userData);
            m_TimerIdSet.Add(id);
            return id;
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddFrameTimer(int interval, TimerCallback callback)
        {
            var id = TimerComponent.Instance.AddFrameTimer(interval, callback);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddFrameTimer(interval, callback, userData);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddFrameTimer(interval, repeatTimes, callback);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddFrameTimer(interval, repeatTimes, callback, userData);
            m_TimerIdSet.Add(id);
            return id;
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public int AddTimer(float interval, TimerCallback callback)
        {
            var id = TimerComponent.Instance.AddTimer(interval, callback);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddTimer(interval, callback, userData);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddTimer(interval, repeatTimes, callback);
            m_TimerIdSet.Add(id);
            return id;
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
            var id = TimerComponent.Instance.AddTimer(interval, repeatTimes, callback, userData);
            m_TimerIdSet.Add(id);
            return id;
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="timerId">定时器id。</param>
        public void RemoveTimer(int timerId)
        {
            if (!m_TimerIdSet.Remove(timerId))
            {
                return;
            }

            TimerComponent.Instance.RemoveTimer(timerId);
        }

        public void Awake(object featureOwner, FeatureContainer featureContainer)
        {
        }

        public void Shutdown()
        {
            foreach (var id in m_TimerIdSet)
            {
                TimerComponent.Instance.RemoveTimer(id);
            }
            
            m_TimerIdSet.Clear();
        }
    }
}