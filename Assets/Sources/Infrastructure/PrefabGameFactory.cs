using UnityEngine;
using Zenject;

namespace Assets.Sources.Infrastructure
{
    public class PrefabGameFactory<TComponent> : IFactory<string, TComponent>, IFactory<string, Vector3, TComponent>
    {
        private IInstantiator _instantiator;

        public PrefabGameFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public TComponent Create(string assetPath)
        {
            GameObject prefab = Resources.Load<GameObject>(assetPath);
            GameObject newObject = _instantiator.InstantiatePrefab(prefab);

            return newObject.GetComponent<TComponent>();
        }

        public TComponent Create(string assetPath, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(assetPath);
            GameObject newObject = _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity, null);

            return newObject.GetComponent<TComponent>();
        }
    }
}
