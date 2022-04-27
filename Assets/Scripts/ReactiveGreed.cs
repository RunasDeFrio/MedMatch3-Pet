using System;
using RunasDev.Core.Extensions;
using UniRx;
using UnityEngine;


namespace DefaultNamespace
{
    public class ReactiveGreed<T> : IDisposable
    {
        /// <summary>
        /// Размер сетки.
        /// </summary>
        private readonly Vector2 _cellSize = new Vector2(0, 0);

        /// <summary>
        /// Массив всех элементов
        /// </summary>
        private readonly T[,] _elements;

        private readonly Subject<(T, Vector2Int)> _onElementAdd;
        private readonly Subject<(T, Vector2Int)> _onElementRemove;
        private readonly Subject<(T item, Vector2Int from, Vector2Int to)> _onElementMove;

        /// <summary>
        /// Размер сетки.
        /// </summary>
        public Vector2Int Size { get; }

        public IObservable<(T item, Vector2Int pos)> OnElementAdd => _onElementAdd;

        public IObservable<(T item, Vector2Int pos)> OnElementRemove => _onElementRemove;

        public IObservable<(T item, Vector2Int from, Vector2Int to)> OnElementMove => _onElementMove;


        
        public ReactiveGreed(Vector2Int size)
        {
            Size = size;
            _elements = new T[Size.x, Size.y];
            _onElementAdd = new Subject<(T, Vector2Int)>();
            _onElementRemove = new Subject<(T, Vector2Int)>();
            _onElementMove = new Subject<(T item, Vector2Int from, Vector2Int to)>();
        }

        public T this[Vector2Int pos] => this[pos.x, pos.y];

        public T this[int x, int y] => _elements.InRange(x, y) ? _elements[x, y] : default;


        public void Add(T element, Vector2Int pos)
        {
            if(_elements[pos.x, pos.y] != null)
                return;
            
            _elements[pos.x, pos.y] = element;
            _onElementAdd.OnNext((element, pos));
        }

        public void Remove(Vector2Int pos)
        {
            if (!_elements.InRange(pos) || this[pos] == null)
                return;

            var element = _elements[pos.x, pos.y];
            _elements[pos.x, pos.y] = default;
            _onElementRemove.OnNext((element, pos));
        }

        public void Move(Vector2Int from, Vector2Int to)
        {
            if (!_elements.InRange(from.x, from.y) || !_elements.InRange(to.x, to.y) ||
                _elements[from.x, from.y] == null || _elements[to.x, to.y] != null)
                return;

            var element = _elements[from.x, from.y];
            _elements[to.x, to.y] = element;
            _elements[from.x, from.y] = default;

            _onElementMove.OnNext((element, from, to));
        }

        public void Clear()
        {
            for (int i = 0; i < Size.x; i++)
            for (int j = 0; j < Size.y; j++)
            {
                Remove(new Vector2Int(i, j));
            }
        }

        public void Dispose()
        {
            _onElementAdd?.Dispose();
            _onElementRemove?.Dispose();
            _onElementMove?.Dispose();
        }
    }
}