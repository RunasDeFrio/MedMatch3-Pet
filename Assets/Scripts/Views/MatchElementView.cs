using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Визуализатор элемента сетки.
    /// </summary>
    public class MatchElementView : MonoBehaviour, IDisposable
    {
        [SerializeField] private float moveTime = 0.5f;

        [SerializeField] private float scaleTime = 0.5f;


        [SerializeField] private ObservablePointerClickTrigger clickTrigger;

        [SerializeField] private RectTransform rectTransform;

        private MatchElement _matchElement;

        private IDisposable _updateDisposable;

        public RectTransform RectTransform => rectTransform;

        public void Construct(MatchElement matchElement)
        {
            _matchElement = matchElement;
            clickTrigger.OnPointerClickAsObservable().Subscribe(_ => _matchElement.Touch());
        }

        /// <summary>
        /// Установить движение в точку за определенное время.
        /// </summary>
        /// <param name="destinationPoint"></param>
        public void MoveTo(Vector2 destinationPoint)
        {
            _updateDisposable?.Dispose();
            IAnimationStrategy strategy = null;
            _updateDisposable = Observable.EveryUpdate().Subscribe(_ => strategy.DoUpdate(this));
            strategy = new Moving(moveTime, destinationPoint, RectTransform.anchoredPosition,
                _updateDisposable.Dispose);
        }

        /// <summary>
        /// Начало анимации уничтожения после чего обект уничтожается.
        /// </summary>
        /// <param name="scaleFactor"></param>
        /// <param name="action"></param>
        public void ScaleTo(float scaleFactor, Action action)
        {
            _updateDisposable?.Dispose();
            var strategy = new ScaleStrategy(scaleTime, scaleFactor * transform.localScale, transform.localScale,
                () =>
                {
                    _updateDisposable.Dispose();
                    action?.Invoke();
                });
            _updateDisposable = Observable.EveryUpdate().Subscribe(_ => strategy.DoUpdate(this));
        }
        
        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}