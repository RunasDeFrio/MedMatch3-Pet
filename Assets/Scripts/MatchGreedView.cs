using System;
using System.Collections.Generic;
using System.Drawing;
using RunasDev.Core.Pooling.ComponentPools;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchGreedView : SerializedMonoBehaviour, IDisposable
    {
        [OdinSerialize] private ComponentWithGameObjectDictionaryPool<int, MatchElementView> _pool;

        [SerializeField] private Vector2 cellSize;

        private CompositeDisposable _compositeDisposable;

        private Dictionary<MatchElement, MatchElementView> _elementViewsDictionary;

        public void Counstruct(MatchGreed greed)
        {
            _elementViewsDictionary = new Dictionary<MatchElement, MatchElementView>();
            _compositeDisposable = new CompositeDisposable();

            greed.Greed.OnElementAdd.Subscribe(((MatchElement item, Vector2Int pos) info) =>
            {
                OnElementAdd(info.item, info.pos);
            }).AddTo(_compositeDisposable);

            greed.Greed.OnElementRemove.Subscribe(((MatchElement item, Vector2Int pos) info) =>
            {
                var elementView = _elementViewsDictionary[info.item];
                _elementViewsDictionary.Remove(info.item);
                _pool.DeSpawn(info.item.Id, elementView);
                elementView.Dispose();
            }).AddTo(_compositeDisposable);

            greed.Greed.OnElementMove.Subscribe(((MatchElement item, Vector2Int from, Vector2Int to) info) =>
            {
                _elementViewsDictionary[info.item].MoveTo(PosFromIndex(info.to));
            }).AddTo(_compositeDisposable);

            for (int i = 0; i < greed.Greed.Size.x; i++)
            for (int j = 0; j < greed.Greed.Size.y; j++)
                if (greed.Greed[i, j] != null)
                    OnElementAdd(greed.Greed[i, j], new Vector2Int(i, j));
        }

        private void OnElementAdd(MatchElement item, Vector2Int pos)
        {
            var elementView = _pool.Spawn(item.Id, transform);
            _elementViewsDictionary.Add(item, elementView);

            elementView.Construct(item);

            elementView.RectTransform.sizeDelta = cellSize;
            
            elementView.RectTransform.anchorMin = Vector2.up;
            elementView.RectTransform.anchorMax = Vector2.up;
            
            elementView.RectTransform.anchoredPosition = PosFromIndex(new Vector2Int(pos.x, -1));
            elementView.MoveTo(PosFromIndex(pos));
        }

        public Vector2 PosFromIndex(Vector2Int pos) => (cellSize * pos + cellSize / 2) * new Vector2(1, -1);

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}