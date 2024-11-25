using System;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 子功能接口。
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// 初始化子功能，由框架调度进行。
        /// </summary>
        void Awake(IFeatureOwner featureOwner);

        /// <summary>
        /// 销毁子功能，由框架调度进行。
        /// </summary>
        void Shutdown();
    }
    
    /// <summary>
    /// 子功能基类。
    /// </summary>
    public abstract class Feature : IFeature
    {
        /// <summary>
        /// 功能持有者。
        /// </summary>
        public IFeatureOwner Owner { get; private set; }
        
        /// <summary>
        /// 初始化子功能，由框架调度进行。
        /// </summary>
        public virtual void Awake(IFeatureOwner featureOwner)
        {
            Owner = featureOwner;
        }

        /// <summary>
        /// 销毁子功能，由框架调度进行。
        /// </summary>
        public abstract void Shutdown();
    }

    /// <summary>
    /// 子功能基类，对持有者有类型约束，若类型不匹配会直接触发异常。
    /// </summary>
    public abstract class Feature<T> : IFeature where T : IFeatureOwner
    {
        /// <summary>
        /// 功能持有者。
        /// </summary>
        public T Owner { get; private set; }
        
        /// <summary>
        /// 初始化子功能，由框架调度进行。
        /// </summary>
        public virtual void Awake(IFeatureOwner featureOwner)
        {
            if (!(featureOwner is T distFeatureOwner))
            {
                throw new ArgumentNullException(nameof(featureOwner));
            }
            
            Owner = distFeatureOwner;
        }

        /// <summary>
        /// 销毁子功能，由框架调度进行。
        /// </summary>
        public abstract void Shutdown();
    }
}