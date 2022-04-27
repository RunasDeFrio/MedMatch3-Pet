using System;
using System.Collections.Generic;
using RunasDev.Core.Enabled.Adapters;
using RunasDev.Core.Factories;
using UnityEngine;

namespace RunasDev.Core.Pooling.ComponentPools
{
    [Serializable]
    public class ComponentWithGameObjectDictionaryPool<TKey, TData> where TData : Behaviour
    {
        [SerializeField]
        private Dictionary<TKey, TData> _prefabDictionary = new Dictionary<TKey, TData>();

        private Dictionary<TKey, SetPool<EnabledGameObjectAsBehaviourAdapter<TData>>> _dictionaryPools;

        public ComponentWithGameObjectDictionaryPool()
        {
            _dictionaryPools = new Dictionary<TKey, SetPool<EnabledGameObjectAsBehaviourAdapter<TData>>>(_prefabDictionary.Count);
        }

        public void SetPrefabs(Dictionary<TKey, TData> _datas)
        {
            _prefabDictionary = _datas;
        }
        
        public TData Spawn(TKey key, Transform transform)
        {
            if (_dictionaryPools == null)
                _dictionaryPools = new Dictionary<TKey, SetPool<EnabledGameObjectAsBehaviourAdapter<TData>>>(_prefabDictionary.Count);
            
            SetPool<EnabledGameObjectAsBehaviourAdapter<TData>> pool;
            if (_dictionaryPools.ContainsKey(key))
            {
                pool = _dictionaryPools[key];
            }
            else
            {
                if (_prefabDictionary.ContainsKey(key))
                {
                    var fabric = new GameObjectAsBehaviourFactory<TData>(_prefabDictionary[key], transform);
                    
                    pool = new SetPool<EnabledGameObjectAsBehaviourAdapter<TData>>(fabric);
                    _dictionaryPools[key] = pool;
                }
                else
                {
                    return default;
                }
            }

            TData data = pool.Spawn();
            return data;
        }

        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="go">Созданный прежде, с помощью Spawn, объект.</param>
        public void DeSpawn(TKey key, TData component)
        {
            if (_dictionaryPools.ContainsKey(key))
            {
                _dictionaryPools[key].DeSpawn(component);
            }
        }

        /// <summary>
        /// Вернуть объекты в пулл и выключить их.
        /// </summary>
        public void DeSpawnAll()
        {
            foreach (var kvp in _dictionaryPools)
            {
                kvp.Value.DeSpawnAll();
            }
        }
    }
}