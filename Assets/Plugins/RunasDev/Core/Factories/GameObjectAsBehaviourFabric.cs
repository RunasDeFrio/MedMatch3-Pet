using System;
using RunasDev.Core.Enabled.Adapters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RunasDev.Core.Factories
{
    [Serializable]
    public class GameObjectAsBehaviourFactory<T> : AbstractGameObjectFactory<EnabledGameObjectAsBehaviourAdapter<T>> where T: Behaviour
    {
        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        [SerializeField]
        private Behaviour _behaviour;

        public GameObjectAsBehaviourFactory()
        {
        }

        public GameObjectAsBehaviourFactory(Behaviour behaviour, Transform owner) : base(owner)
        {
            _behaviour = behaviour;
        }
        
        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        public Behaviour Behaviour => _behaviour;
        
        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        public override GameObject Prefab => _behaviour.gameObject;
        
        public override EnabledGameObjectAsBehaviourAdapter<T> Create()
        {
            return Object.Instantiate(Prefab, owner).GetComponent<T>();
        }
    }
}