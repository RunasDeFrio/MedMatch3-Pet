using System;
using UnityEngine;

namespace RunasDev.Core.Factories
{
    [Serializable]
    public abstract class AbstractGameObjectFactory<T> : IFactory<T>
    {
        [SerializeField] protected Transform owner;
        
        protected AbstractGameObjectFactory(Transform owner)
        {
            this.owner = owner;
        }

        protected AbstractGameObjectFactory()
        {
        }

        /// <summary>
        /// Основной префаб для копирования.
        /// </summary>
        public abstract GameObject Prefab { get; }

        public abstract T Create();
        
    }
}