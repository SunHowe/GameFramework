using System;
using System.Collections.Generic;

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
        public IFeatureOwner Owner { get; }

        private readonly Dictionary<Type, IFeature> m_FeatureDict = new Dictionary<Type, IFeature>();
        private readonly List<IFeature> m_FeatureList = new List<IFeature>();

        public FeatureContainer(IFeatureOwner owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// 销毁容器，并销毁所有子功能。
        /// </summary>
        public void Shutdown()
        {
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

            m_FeatureDict.Clear();
            m_FeatureList.Clear();
        }

        /// <summary>
        /// 添加子功能。
        /// </summary>
        public T AddFeature<T>() where T : class, IFeature, new()
        {
            var featureType = typeof(T);
            if (m_FeatureDict.ContainsKey(featureType))
            {
                throw new Exception($"Feature {featureType} already added.");
            }

            var feature = new T();
            feature.Awake(Owner);

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

            try
            {
                feature.Shutdown();
            }
            catch (Exception e)
            {
                Log.Fatal(e.ToString());
            }
            
            m_FeatureDict.Remove(typeof(T));
            m_FeatureList.Remove(feature);
        }
    }
}