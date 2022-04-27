using System;
using System.Collections.Generic;
using RunasDev.Core.Enabled.Adapters;
using Sirenix.Serialization;
using UnityEngine;

namespace RunasDev.Core.Pooling.Storages
{
    [Serializable]
    public class DictionaryComponentStorage<TKey, TData> where TData : Behaviour
    {
        [OdinSerialize]
        private Dictionary<TKey, Storage<EnabledGameObjectAsBehaviourAdapter<TData>>> _dictionaryStorages;
        
        public DictionaryComponentStorage()
        {
            _dictionaryStorages = new Dictionary<TKey, Storage<EnabledGameObjectAsBehaviourAdapter<TData>>>();
        }

        public bool HaveStorage(TKey key) => _dictionaryStorages.ContainsKey(key);

        public Storage<EnabledGameObjectAsBehaviourAdapter<TData>> GetOrCreateStorage(TKey key)
        {
            if (_dictionaryStorages.ContainsKey(key))
            {
                return _dictionaryStorages[key];
            }
            else
            {
                Storage<EnabledGameObjectAsBehaviourAdapter<TData>> storage = new Storage<EnabledGameObjectAsBehaviourAdapter<TData>>();
                _dictionaryStorages.Add(key, storage);
                return storage;
            }
        }
    }
}