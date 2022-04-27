using System;
using DefaultNamespace;
using UnityEngine;


/// <summary>
/// Стратегия движения. Двигает объект каждый Update.
/// </summary>
public class Moving : IAnimationStrategy
{
    private Action _onComplete;
    private float _moveTime = 1.0f;
    private float _startTime;
    
    private Vector3 _destinationPoint;
    private Vector3 _startPoint;

    public Moving(float moveTime, Vector3 destinationPoint, Vector3 startPoint, Action onComplete)
    {
        _moveTime = moveTime;
        _destinationPoint = destinationPoint;
        _startPoint = startPoint;
        _startTime = Time.time;
        _onComplete = onComplete;
    }

    public void DoUpdate(MatchElementView matchElement)
    {
        float t = (Time.time - _startTime) / _moveTime;

        if (t <= 1.0f)
        {
            matchElement.RectTransform.anchoredPosition = Vector3.Lerp(_startPoint, _destinationPoint, t);
        }
        else
        {
            matchElement.RectTransform.anchoredPosition = _destinationPoint;
            _onComplete?.Invoke();
        }
    }

}
