using RunasDev.Core.Enabled;
using RunasDev.Core.Factories;
using UnityEngine;

namespace RunasDev.Core.Pooling.ComponentPools
{
    public class ComponentWithGameObjectPool<T> : IPool<T> where T : Behaviour, IEnabled
    {
        private readonly SetPool<T> _pool;

        public ComponentWithGameObjectPool(IFactory<T> factory)
        {
            _pool = new SetPool<T>(factory);
        }

        /// <summary>
        /// Создать или вытянуть из пулла копию префаба.
        /// </summary>
        /// <returns>Созданная или вытянутая копия префаба, прикреплённая к владельцу </returns>
        public T Spawn()
        {
            return _pool.Spawn();
        }

        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="obj">Созданный прежде, с помощью Spawn, объект.</param>
        public void DeSpawn(T obj)
        {
            _pool.DeSpawn(obj);
        }

        public void DeSpawnAll()
        {
            _pool.DeSpawnAll();
        }

        public T Spawn(Transform owner)
        {
            T spawn = Spawn();
            spawn.transform.SetParent(owner);
            return spawn;
        }
    }
}