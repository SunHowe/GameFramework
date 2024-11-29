namespace GameFramework.Timer
{
    /// <summary>
    /// 定时管理器。
    /// </summary>
    public interface ITimerManager
    {
        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        int AddTimer(float interval, TimerCallback callback);

        /// <summary>
        /// 添加无限重复的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        int AddTimer(float interval, TimerCallback callback, object userData);

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        int AddTimer(float interval, int repeatTimes, TimerCallback callback);

        /// <summary>
        /// 添加重复指定次数的定时器。
        /// </summary>
        /// <param name="interval">间隔时间。</param>
        /// <param name="repeatTimes">重复次数。</param>
        /// <param name="callback">定时器回调。</param>
        /// <param name="userData">用户数据。</param>
        /// <returns>定时器id 用于手动停止定时器。</returns>
        int AddTimer(float interval, int repeatTimes, TimerCallback callback, object userData);
        
        /// <summary>
        /// 通过定时器id移除定时器。
        /// </summary>
        /// <param name="timerId">定时器id。</param>
        void RemoveTimer(int timerId);
    }
}