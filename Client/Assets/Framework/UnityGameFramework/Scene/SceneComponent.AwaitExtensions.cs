using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 场景组件 异步任务await拓展。
    /// </summary>
    public partial class SceneComponent
    {
        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源名称。</param>
        public UniTask<bool> LoadSceneAsync(string sceneAssetName)
        {
            return LoadSceneAsync(sceneAssetName, DefaultPriority);
        }
        
        /// <summary>
        /// 加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源名称。</param>
        /// <param name="priority">加载场景资源的优先级。</param>
        public UniTask<bool> LoadSceneAsync(string sceneAssetName, int priority)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return UniTask.FromResult(false);
            }

            var task = AutoResetUniTaskCompletionSource<bool>.Create();
            m_SceneManager.LoadScene(sceneAssetName, priority, task);
            return task.Task;
        }

        /// <summary>
        /// 卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源名称。</param>
        public UniTask<bool> UnloadSceneAsync(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return UniTask.FromResult(false);
            }

            var task = AutoResetUniTaskCompletionSource<bool>.Create();
            m_SceneManager.UnloadScene(sceneAssetName, task);
            m_SceneOrder.Remove(sceneAssetName);
            return task.Task;
        }

        /// <summary>
        /// 检查是否是Await拓展模块传入的UserData。
        /// </summary>
        private void CheckAwaitUserData(GameFramework.Scene.LoadSceneSuccessEventArgs eventArgs)
        {
            if (eventArgs.UserData is AutoResetUniTaskCompletionSource<bool> task)
            {
                task.TrySetResult(true);
            }
        }

        /// <summary>
        /// 检查是否是Await拓展模块传入的UserData。
        /// </summary>
        private void CheckAwaitUserData(GameFramework.Scene.LoadSceneFailureEventArgs eventArgs)
        {
            if (eventArgs.UserData is AutoResetUniTaskCompletionSource<bool> task)
            {
                task.TrySetResult(false);
            }
        }

        /// <summary>
        /// 检查是否是Await拓展模块传入的UserData。
        /// </summary>
        private void CheckAwaitUserData(GameFramework.Scene.UnloadSceneSuccessEventArgs eventArgs)
        {
            if (eventArgs.UserData is AutoResetUniTaskCompletionSource<bool> task)
            {
                task.TrySetResult(true);
            }
        }

        /// <summary>
        /// 检查是否是Await拓展模块传入的UserData。
        /// </summary>
        private void CheckAwaitUserData(GameFramework.Scene.UnloadSceneFailureEventArgs eventArgs)
        {
            if (eventArgs.UserData is AutoResetUniTaskCompletionSource<bool> task)
            {
                task.TrySetResult(false);
            }
        }
    }
}