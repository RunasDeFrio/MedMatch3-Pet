using RunasDev.Core.Enabled;
using UnityEngine;

namespace RunasDev.Core.Pooling
{
    public interface IPool<T> where T : IEnabled
    {
        /// <summary>
        /// Создать или вытянуть из пулла копию префаба.
        /// </summary>
        /// <param name="owner">Трансформ будущего владельца префаба для прикрепления объекта к нему</param>
        /// <returns>Созданная или вытянутая копия префаба, прикреплённая к владельцу </returns>
        T Spawn();

        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="go">Созданный прежде, с помощью Spawn, объект.</param>
        void DeSpawn(T obj);

        /// <summary>
        /// Вернуть объекты в пулл и выключить их.
        /// </summary>
        void DeSpawnAll();
    }
}