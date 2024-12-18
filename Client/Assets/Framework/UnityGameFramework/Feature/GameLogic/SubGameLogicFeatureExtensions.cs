using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 子功能模块拓展方法。
    /// </summary>
    public static class SubGameLogicFeatureExtensions
    {
        /// <summary>
        /// 添加子游戏逻辑，在自己被销毁时，子逻辑也会跟着被销毁。
        /// </summary>
        public static void AddSubGameLogic(this FeatureContainer container, IGameLogic gameLogic)
        {
            container.AddFeature<SubGameLogicFeature>().AddGameLogic(gameLogic);
        }

        /// <summary>
        /// 添加子游戏逻辑，在自己被销毁时，子逻辑也会跟着被销毁。
        /// </summary>
        public static void AddSubGameLogic<T>(this FeatureContainer container) where T : IGameLogic, new()
        {
            container.AddFeature<SubGameLogicFeature>().AddGameLogic<T>();
        }

        /// <summary>
        /// 移除子游戏逻辑。
        /// </summary>
        public static void RemoveGameLogic(this FeatureContainer container, IGameLogic gameLogic)
        {
            var feature = container.GetFeature<SubGameLogicFeature>();
            if (feature == null)
            {
                return;
            }
            
            feature.RemoveGameLogic(gameLogic);
        }

        /// <summary>
        /// 添加子游戏逻辑，在自己被销毁时，子逻辑也会跟着被销毁。
        /// </summary>
        public static void AddSubGameLogic(this IFeatureContainerOwner owner, IGameLogic gameLogic)
        {
            owner.FeatureContainer.AddSubGameLogic(gameLogic);
        }
        
        /// <summary>
        /// 添加子游戏逻辑，在自己被销毁时，子逻辑也会跟着被销毁。
        /// </summary>
        public static void AddSubGameLogic<T>(this IFeatureContainerOwner owner) where T : IGameLogic, new()
        {
            owner.FeatureContainer.AddSubGameLogic<T>();
        }

        /// <summary>
        /// 移除子游戏逻辑。
        /// </summary>
        public static void RemoveGameLogic(this IFeatureContainerOwner owner, IGameLogic gameLogic)
        {
            owner.FeatureContainer.RemoveGameLogic(gameLogic);
        }
    }
}