using GameFramework.Timer;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 定时器功能模块拓展方法。
    /// </summary>
    public static class TimerFeatureExtensions
    {
        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, TimerCallback callback)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(callback, userData);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, int interval, TimerCallback callback)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, int interval, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, int interval, int repeatTimes, TimerCallback callback)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this FeatureContainer container, int interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddFrameTimer(interval, repeatTimes, callback, userData);
        }
        
        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(this FeatureContainer container, float interval, TimerCallback callback)
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
        public static int AddTimer(this FeatureContainer container, float interval, TimerCallback callback, object userData)
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
        public static int AddTimer(this FeatureContainer container, float interval, int repeatTimes, TimerCallback callback)
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
        public static int AddTimer(this FeatureContainer container, float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return container.AddFeature<TimerFeature>().AddTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="timerId">定时器id。</param>
        public static void RemoveTimer(this FeatureContainer container, int timerId)
        {
            var feature = container.GetFeature<TimerFeature>();
            if (feature == null)
            {
                return;
            }
            
            feature.RemoveTimer(timerId);
        }
        
        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, TimerCallback callback)
        {
            return owner.FeatureContainer.AddFrameTimer(callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddFrameTimer(callback, userData);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, int interval, TimerCallback callback)
        {
            return owner.FeatureContainer.AddFrameTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, int interval, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddFrameTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, int interval, int repeatTimes, TimerCallback callback)
        {
            return owner.FeatureContainer.AddFrameTimer(interval, repeatTimes, callback);
        }

        /// <summary>
        /// 添加重复指定次数的帧定时器，会在每间隔interval帧回调一次。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔帧数 最小值为1</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddFrameTimer(this IFeatureContainerOwner owner, int interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddFrameTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(this IFeatureContainerOwner owner, float interval, TimerCallback callback)
        {
            return owner.FeatureContainer.AddTimer(interval, callback);
        }

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(this IFeatureContainerOwner owner, float interval, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddTimer(interval, callback, userData);
        }

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        public static int AddTimer(this IFeatureContainerOwner owner, float interval, int repeatTimes, TimerCallback callback)
        {
            return owner.FeatureContainer.AddTimer(interval, repeatTimes, callback);
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
        public static int AddTimer(this IFeatureContainerOwner owner, float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            return owner.FeatureContainer.AddTimer(interval, repeatTimes, callback, userData);
        }

        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="timerId">定时器id。</param>
        public static void RemoveTimer(this IFeatureContainerOwner owner, int timerId)
        {
            owner.FeatureContainer.RemoveTimer(timerId);
        }
    }
}