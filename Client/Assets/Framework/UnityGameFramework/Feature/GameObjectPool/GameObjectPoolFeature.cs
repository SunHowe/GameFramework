using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// GameObject对象池模块。用于提供局部实例化对象的对象池功能。当功能销毁时会将创建的实例化对象销毁。
    /// </summary>
    public sealed partial class GameObjectPoolFeature : Feature
    {
        public const int DEFAULT_CAPACITY = int.MaxValue;
        public const float DEFAULT_EXPIRE_TIME = float.MaxValue;
        public const int DEFAULT_PRIORITY = 0;
        public const float DEFAULT_AUTO_RELEASE_INTERVAL = DEFAULT_EXPIRE_TIME;

        /// <summary>
        /// 实例对象池容量。
        /// </summary>
        public int Capacity
        {
            get => m_Parent?.Capacity ?? m_Capacity;
            set
            {
                if (m_Parent != null)
                {
                    throw new Exception("It's not allow to set capacity when parent is not null.");
                }

                m_Capacity = value;

                if (m_ObjectPool != null)
                {
                    m_ObjectPool.Capacity = value;
                }
            }
        }

        /// <summary>
        /// 实例对象池对象过期秒数。
        /// </summary>
        public float ExpireTime
        {
            get => m_Parent?.ExpireTime ?? m_ExpireTime;
            set
            {
                if (m_Parent != null)
                {
                    throw new Exception("It's not allow to set expire time when parent is not null.");
                }

                m_ExpireTime = value;

                if (m_ObjectPool != null)
                {
                    m_ObjectPool.ExpireTime = value;
                }
            }
        }

        /// <summary>
        /// 实例对象池的优先级。
        /// </summary>
        public int Priority
        {
            get => m_Parent?.Priority ?? m_Priority;
            set
            {
                if (m_Parent != null)
                {
                    throw new Exception("It's not allow to set priority when parent is not null.");
                }

                m_Priority = value;
                if (m_ObjectPool != null)
                {
                    m_ObjectPool.Priority = value;
                }
            }
        }

        /// <summary>
        /// 实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float AutoReleaseInterval
        {
            get => m_Parent?.AutoReleaseInterval ?? m_AutoReleaseInterval;
            set
            {
                if (m_Parent != null)
                {
                    throw new Exception("It's not allow to set auto release interval when parent is not null.");
                }

                m_AutoReleaseInterval = value;
                if (m_ObjectPool != null)
                {
                    m_ObjectPool.AutoReleaseInterval = value;
                }
            }
        }

        /// <summary>
        /// 父级模块。当父级模块存在时，实际的操作由父级模块进行。
        /// </summary>
        private GameObjectPoolFeature m_Parent;

        /// <summary>
        /// 记录从父级模块获取到的实例化对象。
        /// </summary>
        private GameFrameworkLinkedList<GameObject> m_InstantiateFromParent;

        /// <summary>
        /// 对象池实例。
        /// </summary>
        private IObjectPool<GameObjectInstanceObject> m_ObjectPool;

        private int m_Capacity;
        private float m_ExpireTime;
        private int m_Priority;
        private float m_AutoReleaseInterval;
        private CancellationTokenSource m_CancellationTokenSource;
        private Transform m_InstanceRoot;
        private ObjectPoolComponent m_ObjectPoolComponent;
        
        private static int s_IdIncrease = 0;

        /// <summary>
        /// 同步实例化资源。仅支持在对象池已有缓存时使用。
        /// </summary>
        public GameObject Instantiate(string assetName)
        {
            if (m_Parent != null)
            {
                // 向父级获取。
                var gameObject = m_Parent.Instantiate(assetName);
                if (gameObject == null)
                {
                    return null;
                }

                m_InstantiateFromParent.AddLast(gameObject);
                return gameObject;
            }

            if (m_ObjectPool == null)
            {
                // 对象池未创建，不允许实例化。
                return null;
            }

            var gameObjectInstanceObject = m_ObjectPool.Spawn(assetName);
            if (gameObjectInstanceObject == null)
            {
                return null;
            }

            return (GameObject)gameObjectInstanceObject.Target;
        }

        /// <summary>
        /// 异步实例化资源。若资源在对象池中不存在则会加载资源并实例化新的对象。
        /// </summary>
        public async UniTask<GameObject> InstantiateAsync(string assetName)
        {
            if (m_Parent != null)
            {
                // 向父级获取。
                var token = (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;
                
                var gameObject = await m_Parent.InstantiateAsync(assetName);
                if (gameObject == null)
                {
                    return null;
                }

                if (token.IsCancellationRequested)
                {
                    // 取消加载时进行归还。
                    m_Parent.Destroy(gameObject);
                    return null;
                }

                m_InstantiateFromParent.AddLast(gameObject);
                return gameObject;
            }

            GameObjectInstanceObject gameObjectInstanceObject = null;
            // 若对象池未创建，则创建对象池实例。
            if (m_ObjectPool == null)
            {
                var name = Utility.Text.Format("GameObject Instance Pool ({0})", ++s_IdIncrease);
                m_InstanceRoot = m_ObjectPoolComponent.AcquireTransformRoot(name);
                m_ObjectPool = ObjectPoolComponent.Instance.CreateSingleSpawnObjectPool<GameObjectInstanceObject>(name, m_AutoReleaseInterval, m_Capacity, m_ExpireTime, m_Priority);
            }
            else
            {
                gameObjectInstanceObject = m_ObjectPool.Spawn(assetName);
            }

            if (gameObjectInstanceObject == null)
            {
                // 加载资源，然后再进行实例化。
                var token = (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;
                var asset = await FeatureContainer.LoadAssetAsync<GameObject>(assetName);
                if (asset == null)
                {
                    // 加载失败。
                    return null;
                }

                if (token.IsCancellationRequested)
                {
                    // 取消加载。目前资源模块不用归还资源。
                    return null;
                }
                
                gameObjectInstanceObject = GameObjectInstanceObject.Create(assetName, asset, this);
                m_ObjectPool.Register(gameObjectInstanceObject, true);
            }
            
            return (GameObject)gameObjectInstanceObject.Target;
        }

        /// <summary>
        /// 归还GameObject实例。
        /// </summary>
        public void Destroy(GameObject gameObject)
        {
            if (ReferenceEquals(gameObject, null))
            {
                // 引用为空的检测。这里不做GameObject是否被销毁的判断。
                return;
            }

            if (m_Parent != null)
            {
                // 向父级归还。
                m_InstantiateFromParent.Remove(gameObject);
                m_Parent.Destroy(gameObject);
                return;
            }

            if (gameObject == null)
            {
                // 错误的对象实例。
                return;
            }

            if (m_ObjectPool != null && m_ObjectPool.ReleaseObject(gameObject))
            {
                // 向对象池归还
                return;
            }

            // 异常情况。
            Object.Destroy(gameObject);
        }

        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);

            m_ObjectPoolComponent = ObjectPoolComponent.Instance;
            m_Priority = DEFAULT_PRIORITY;
            m_Capacity = DEFAULT_CAPACITY;
            m_ExpireTime = DEFAULT_EXPIRE_TIME;
            m_AutoReleaseInterval = DEFAULT_AUTO_RELEASE_INTERVAL;
        }

        public override void Shutdown()
        {
            if (m_CancellationTokenSource != null)
            {
                m_CancellationTokenSource.Cancel();
                m_CancellationTokenSource.Dispose();
                m_CancellationTokenSource = null;
            }
            
            if (m_ObjectPool != null)
            {
                if (m_ObjectPoolComponent == ObjectPoolComponent.Instance)
                {
                    m_ObjectPoolComponent.DestroyObjectPool(m_ObjectPool);
                }

                m_ObjectPool = null;
            }

            if (m_InstantiateFromParent != null)
            {
                if (m_Parent != null)
                {
                    // 向父级归还。
                    foreach (var gameObject in m_InstantiateFromParent)
                    {
                        m_Parent.Destroy(gameObject);
                    }
                }
                else
                {
                    // 异常情况。
                    foreach (var gameObject in m_InstantiateFromParent)
                    {
                        if (gameObject != null)
                        {
                            Object.Destroy(gameObject);
                        }
                    }
                }

                m_InstantiateFromParent.Clear();
            }

            if (m_InstanceRoot != null)
            {
                if (m_ObjectPoolComponent == ObjectPoolComponent.Instance)
                {
                    m_ObjectPoolComponent.ReleaseTransformRoot(m_InstanceRoot);
                }
                
                m_InstanceRoot = null;
            }

            m_Parent = null;
            m_ObjectPoolComponent = null;
        }
    }
}