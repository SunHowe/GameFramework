using System;
using System.Collections.Generic;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 可批量调用IDisposable.Dispose()的容器。
    /// </summary>
    public sealed class DisposableGroup : IDisposable
    {
        private readonly List<IDisposable> m_Disposables = new List<IDisposable>();

        public void Add(IDisposable disposable)
        {
            if (disposable == null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }

            m_Disposables.Add(disposable);
        }

        public void Dispose()
        {
            for (var index = m_Disposables.Count - 1; index >= 0; index--)
            {
                var disposable = m_Disposables[index];
                try
                {
                    disposable.Dispose();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                }
            }
            
            m_Disposables.Clear();
        }
    }
}