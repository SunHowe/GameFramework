namespace GameFramework.Timer
{
    internal partial class TimerManager
    {
        private sealed class TimerInfo : IReference
        {
            /// <summary>
            /// 定时器的唯一id。
            /// </summary>
            public int TimerId { get; private set; }
            
            /// <summary>
            /// 定时器回调周期.单位：秒
            /// </summary>
            public float TimerInterval { get; private set; }
            
            /// <summary>
            /// 定时器回调重复次数, 0代表不限制次数。
            /// </summary>
            public int TimerRepeatTimes { get; private set; }
            
            /// <summary>
            /// 定时器回调函数。
            /// </summary>
            public TimerCallback TimerCallback { get; private set; }
            
            /// <summary>
            /// 定时器用户数据。
            /// </summary>
            public object UserData { get; private set; }
            
            /// <summary>
            /// 已触发回调的次数。
            /// </summary>
            public int InvokeTimes { get; set; }
            
            /// <summary>
            /// 前一次触发回调的时间。
            /// </summary>
            public float PreviousInvokeTime { get; set; }
            
            /// <summary>
            /// 下一次触发回调的时间。
            /// </summary>
            public float NextInvokeTime { get; set; }
            
            /// <summary>
            /// 是否已经被取消。
            /// </summary>
            public bool IsCancel { get; set; }
            
            public void Clear()
            {
                TimerId = 0;
                TimerInterval = 0f;
                TimerRepeatTimes = 0;
                TimerCallback = null;
                UserData = null;
                InvokeTimes = 0;
                PreviousInvokeTime = 0f;
                NextInvokeTime = 0f;
                IsCancel = false;
            }

            public static TimerInfo Create(int timerId, float timerInterval, int timerRepeatTimes, TimerCallback timerCallback, object userData)
            {
                var timerInfo = ReferencePool.Acquire<TimerInfo>();
                timerInfo.TimerId = timerId;
                timerInfo.TimerInterval = timerInterval;
                timerInfo.TimerRepeatTimes = timerRepeatTimes;
                timerInfo.TimerCallback = timerCallback;
                timerInfo.UserData = userData;
                return timerInfo;
            }
        }
    }
}