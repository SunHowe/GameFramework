namespace GameFramework.Timer
{
    /// <summary>
    /// 定时器回调。
    /// <param name="timerId">定时器id。</param>
    /// <param name="elapsedTime">距离上次回调已过去的时间。</param>
    /// <param name="invokeTimes">已触发回调的次数(包含这次)。</param>
    /// <param name="userData">用户数据。</param>
    /// </summary>
    public delegate void TimerCallback(int timerId, float elapsedTime, int invokeTimes, object userData);
}