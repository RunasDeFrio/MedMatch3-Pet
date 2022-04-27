using System;
using RunasDev.Core.Enabled.Adapters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RunasDev.Core.Factories
{
    [Serializable]
    public class GameObjectFactory : AbstractGameObjectFactory<EnabledGameObjectAdapter>
    {
        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        [SerializeField]
        private GameObject _prefab;

        public GameObjectFactory()
        {
        }

        public GameObjectFactory(GameObject prefab, Transform owner) : base(owner)
        {
            _prefab = prefab;
        }

        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        public override GameObject Prefab => _prefab;

        public override EnabledGameObjectAdapter Create()
        {
            return Object.Instantiate(Prefab, owner);
        }
    }
}