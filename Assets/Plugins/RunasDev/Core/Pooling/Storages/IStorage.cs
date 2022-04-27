using System.Collections.Generic;
using RunasDev.Core.Enabled;

namespace RunasDev.Core.Pooling.Storages
{
    public interface IStorage<T> where T : IEnabled
    {
        /// <summary>
        /// Выключенные объекты
        /// </summary>
        IEnumerable<T> DeActiveObjects { get; }

        /// <summary>
        /// Активные объекты
        /// </summary>
        IEnumerable<T> ActiveObjects { get; }

        /// <summary>
        /// Вытянутый из пулла объект.
        /// </summary>
        /// <returns>Вытянутый объект </returns>
        T Pop();

        bool Pop(out T component);

        /// <summary>
        /// Добавить к данному хранилищу объект.
        /// </summary>
        /// <param name="component"></param>
        void Join(T component);

        /// <summary>
        /// Вернуть объект в пулл и выключить его.
        /// </summary>
        /// <param name="component">Созданный прежде, с помощью Spawn, объект.</param>
        void Push(T component);

        void PushAll();

        void Clear();
    }
}