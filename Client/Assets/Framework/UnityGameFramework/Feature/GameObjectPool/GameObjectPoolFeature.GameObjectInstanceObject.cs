using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    public partial class GameObjectPoolFeature
    {
        /// <summary>
        /// GameObject实例对象。
        /// </summary>
        private sealed class GameObjectInstanceObject : ObjectBase
        {
            private GameObject m_Asset;
            private GameObjectPoolFeature m_Feature;

            protected override void OnSpawn()
            {
                base.OnSpawn();

                var gameObject = (GameObject)Target;
                gameObject.transform.SetParent(null);
            }
            
            protected override void OnUnspawn()
            {
                base.OnUnspawn();
                
                var gameObject = (GameObject)Target;
                gameObject.transform.SetParent(m_Feature.m_InstanceRoot);
            }

            protected override void Release(bool isShutdown)
            {
                // 销毁对象即可。
                Object.Destroy((GameObject)Target);
            }

            public static GameObjectInstanceObject Create(string assetName, GameObject asset, GameObjectPoolFeature feature)
            {
                var gameObject = Object.Instantiate(asset);
                
                var gameObjectInstanceObject = ReferencePool.Acquire<GameObjectInstanceObject>();
                gameObjectInstanceObject.Initialize(assetName, gameObject);
                gameObjectInstanceObject.m_Asset = asset;
                gameObjectInstanceObject.m_Feature = feature;
                
                return gameObjectInstanceObject;
            }
        }
    }
}