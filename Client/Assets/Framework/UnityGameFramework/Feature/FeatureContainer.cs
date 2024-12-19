using System;
using System.Collections.Generic;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能容器，用于统一管理子功能的生命周期。
    /// </summary>
    public class FeatureContainer
    {
        /// <summary>
        /// 功能持有者。
        /// </summary>
        public object Owner { get; }

        private readonly bool m_ShutdownRemoveFeatures;
        private readonly Dictionary<Type, IFeature> m_FeatureDict = new Dictionary<Type, IFeature>();
        private readonly List<IFeature> m_FeatureList = new List<IFeature>();
        private bool m_IsAwake;

        /// <summary>
        /// 构造功能容器。
        /// </summary>
        /// <param name="owner">功能持有者实例。</param>
        /// <param name="shutdownRemoveFeatures">在shutdown时是否移除所有功能。</param>
        public FeatureContainer(object owner, bool shutdownRemoveFeatures = false)
        {
            Owner = owner;
            m_ShutdownRemoveFeatures = shutdownRemoveFeatures;
            m_IsAwake = true;
        }

        /// <summary>
        /// 唤起已注册的功能。
        /// </summary>
        public void Awake()
        {
            if (m_IsAwake)
            {
                return;
            }

            m_IsAwake = true;
            
            for (var index = 0; index < m_FeatureList.Count; index++)
            {
                var feature = m_FeatureList[index];
                try
                {
                    feature.Awake(Owner, this);
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                    // 触发异常时移除该功能。
                    m_FeatureList.RemoveAt(index--);
                    m_FeatureDict.Remove(feature.GetType());
                }
            }
        }

        /// <summary>
        /// 销毁容器。
        /// </summary>
        public void Shutdown()
        {
            if (!m_IsAwake)
            {
                return;
            }

            m_IsAwake = false;
            
            for (var index = m_FeatureList.Count - 1; index >= 0; index--)
            {
                var feature = m_FeatureList[index];
                try
                {
                    feature.Shutdown();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                }
            }

            if (!m_ShutdownRemoveFeatures)
            {
                return;
            }
            
            m_FeatureList.Clear();
            m_FeatureDict.Clear();
        }

        /// <summary>
        /// 添加子功能。
        /// </summary>
        public T AddFeature<T>() where T : class, IFeature, new()
        {
            if (!m_IsAwake)
            {
                // 目前添加子功能只允许在容器Awake后进行。
                throw new GameFrameworkException("Feature container is not awake.");
            }
            
            var featureType = typeof(T);
            if (m_FeatureDict.TryGetValue(featureType, out var existingFeature))
            {
                return (T)existingFeature;
            }

            var feature = new T();
            feature.Awake(Owner, this);

            m_FeatureDict.Add(featureType, feature);
            m_FeatureList.Add(feature);

            return feature;
        }

        /// <summary>
        /// 获取指定类型的子功能。
        /// </summary>
        public T GetFeature<T>() where T : class, IFeature
        {
            return m_FeatureDict.TryGetValue(typeof(T), out var feature) ? (T)feature : null;
        }

        /// <summary>
        /// 判断是否有指定的子功能
        /// </summary>
        public bool HasFeature<T>() where T : class, IFeature
        {
            return m_FeatureDict.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 移除指定子功能。
        /// </summary>
        public void RemoveFeature<T>() where T : class, IFeature
        {
            if (!m_FeatureDict.TryGetValue(typeof(T), out var feature))
            {
                return;
            }

            if (m_IsAwake)
            {
                try
                {
                    feature.Shutdown();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                }
            }

            m_FeatureDict.Remove(typeof(T));
            m_FeatureList.Remove(feature);
        }
    }
}