using System;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Стратегия скейла.
/// </summary>
public class ScaleStrategy : IAnimationStrategy
{
    private Action _onComplete;
    private float _endTime = 1.0f;
    private float _startTime;
    private Vector2 _destinationScale;
    private Vector2 _startScale;

    public ScaleStrategy(float endTime, Vector2 destinationScale, Vector2 startScale, Action onComplete)
    {
        _endTime = endTime;
        _destinationScale = destinationScale;
        _startScale = startScale;

        _startTime = Time.time;
        _onComplete = onComplete;
    }

    public void DoUpdate(MatchElementView matchElement)
    {
        float t = (Time.time - _startTime) / _endTime;
        if (t <= 1.0f)
            matchElement.transform.localScale = Vector2.Lerp(_startScale, _destinationScale, t);
        else
        {
            matchElement.transform.localScale = _startScale;
            _onComplete?.Invoke();
        }
    }
}
