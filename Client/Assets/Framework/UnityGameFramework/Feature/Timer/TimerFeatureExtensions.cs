using GameFramework.Timer;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 定时器功能模块拓展方法。
    /// </summary>
    public static class TimerFeatureExtensions
    {
        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(FeatureContainer container, float interval, TimerCallback callback)
        {
            return container.AddFeature<TimerFeature>().AddTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(FeatureContainer container, float interval, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(FeatureContainer container, float interval, int repeatTimes, TimerCallback callback)
        {
            return container.AddFeature<TimerFeature>().AddTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(FeatureContainer container, float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="timerId">定时器id。</param>
        public static void RemoveTimer(FeatureContainer container, int timerId)
        {
            container.AddFeature<TimerFeature>().RemoveTimer(timerId);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(IFeatureContainerOwner owner, float interval, TimerCallback callback)
        {
            return owner.FeatureContainer.AddFeature<TimerFeature>().AddTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(IFeatureContainerOwner owner, float interval, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddFeature<TimerFeature>().AddTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(IFeatureContainerOwner owner, float interval, int repeatTimes, TimerCallback callback)
        {
            return owner.FeatureContainer.AddFeature<TimerFeature>().AddTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(IFeatureContainerOwner owner, float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddFeature<TimerFeature>().AddTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="timerId">定时器id。</param>
        public static void RemoveTimer(IFeatureContainerOwner owner, int timerId)
        {
            owner.FeatureContainer.AddFeature<TimerFeature>().RemoveTimer(timerId);
        }
    }
}