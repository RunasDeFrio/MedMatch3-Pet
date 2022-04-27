using System;
using System.Collections.Generic;
using RunasDev.Core.Enabled;
using UnityEngine;

namespace RunasDev.Core.Pooling.Storages
{
    /// <summary>
    /// Хранилище объектов.
    /// Реализовывает обычный менеджмент состояния активности объектов.
    /// </summary>
    [Serializable]
    public class Storage<T> : IStorage<T> where T : IEnabled
    {
        /// <summary>
        /// Пулл удалённых объектов
        /// </summary>
        [SerializeField]
        private Stack<T> _despawnPool = new Stack<T>();

        /// <summary>
        /// Множество активных объектов.
        /// </summary>
        private HashSet<T> _setActiveObjects = new HashSet<T>();

        /// <summary>
        /// Выключенные объекты
        /// </summary>
        public IEnumerable<T> DeActiveObjects => _despawnPool;

        /// <summary>
        /// Активные объекты
        /// </summary>
        public IEnumerable<T> ActiveObjects => _setActiveObjects;

        /// <summary>
        /// Вытянутый из пулла объект.
        /// </summary>
        /// <returns>Вытянутый объект </returns>
        public T Pop()
        {
            Pop(out var go);
            return go;
        }

        public bool Pop(out T enabled)
        {
            if (_despawnPool.Count > 0)
            {
                enabled = _despawnPool.Pop();
                enabled.Enable();
                _setActiveObjects.Add(enabled);
                return true;
            }

            enabled = default;
            return false;
        }

        /// <summary>
        /// Добавить к данному хранилищу объект.
        /// </summary>
        /// <param name="enabled"></param>
        public void Join(T enabled)
        {
            if (enabled.IsEnable)
            {
                _setActiveObjects.Add(enabled);
            }
            else
            {
                _despawnPool.Push(enabled);
            }
        }
        
        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="enabled">Созданный прежде, с помощью Spawn, объект.</param>
        public void Push(T enabled)
        {
            if (_setActiveObjects.Contains(enabled))
            {
                _despawnPool.Push(enabled);
                _setActiveObjects.Remove(enabled);
                enabled.Disable();
            }
        }
        
        public void PushAll()
        {
            foreach (var obj in _setActiveObjects)
            {
                obj.Disable();
                _despawnPool.Push(obj);
            }
            _setActiveObjects.Clear();
        }

        public void Clear()
        {
            _despawnPool.Clear();
            _setActiveObjects.Clear();
        }
    }
}