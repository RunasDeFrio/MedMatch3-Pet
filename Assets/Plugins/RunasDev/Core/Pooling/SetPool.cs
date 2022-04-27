using RunasDev.Core.Enabled;
using RunasDev.Core.Factories;
using RunasDev.Core.Pooling.Storages;

namespace RunasDev.Core.Pooling
{
    public class SetPool<T> : IPool<T> where T : IEnabled
    {
        private readonly IFactory<T> _factory;
        protected Storage<T> _storage = new Storage<T>();
        
        public SetPool(IFactory<T> factory)
        {
            _factory = factory;
            if (_storage == null)
                _storage = new Storage<T>();
            _storage.PushAll();
        }
        
        /// <summary>
        /// Создать или вытянуть из пулла копию префаба.
        /// </summary>
        /// <returns>Созданная или вытянутая копия префаба, прикреплённая к владельцу </returns>
        public T Spawn()
        {
            bool have = _storage.Pop(out var instance);
            if (!have)
            {
                instance = _factory.Create();
                instance.Enable();
                _storage.Join(instance);
                return instance;
            }
            else
                return instance;
        }
        
        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="obj">Созданный прежде, с помощью Spawn, объект.</param>
        public void DeSpawn(T obj)
        {
            _storage.Push(obj);
        }

        public void DeSpawnAll()
        {
            _storage.PushAll();
        }
    }
}