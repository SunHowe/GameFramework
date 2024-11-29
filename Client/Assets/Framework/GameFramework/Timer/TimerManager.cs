namespace GameFramework.Timer
{
    /// <summary>
    /// 定时器管理器。
    /// </summary>
    internal class TimerManager : GameFrameworkModule, ITimerManager
    {
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            throw new System.NotImplementedException();
        }

        internal override void Shutdown()
        {
            throw new System.NotImplementedException();
        }

        public int AddTimer(float interval, TimerCallback callback)
        {
            throw new System.NotImplementedException();
        }

        public int AddTimer(float interval, TimerCallback callback, object userData)
        {
            throw new System.NotImplementedException();
        }

        public int AddTimer(float interval, int repeatTimes, TimerCallback callback)
        {
            throw new System.NotImplementedException();
        }

        public int AddTimer(float interval, int repeatTimes, TimerCallback callback, object userData)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTimer(int timerId)
        {
            throw new System.NotImplementedException();
        }
    }
}