using UnityEngine;


/// <summary>
/// Стратегия движения. Двигает объект каждый Update.
/// </summary>
public class Moving : AnimationStrategy
{
    private float _moveTime = 1.0f;
    private float _startTime;
    private bool _moving = false;
    private Vector2 _destinationPoint;
    private Vector2 _startPoint;

    public Moving(float moveTime, Vector2 destinationPoint, Vector2 startPoint)
    {
        _moveTime = moveTime;
        _destinationPoint = destinationPoint;
        _startPoint = startPoint;

        _moving = true;
        _startTime = Time.time;
    }

    public void DoUpdate(Virus virus)
    {
        if (_moving)
        {
            float t = (Time.time - _startTime) / _moveTime;

            if (t <= 1.0f)
                virus.transform.position = Vector2.Lerp(_startPoint, _destinationPoint, t);
            else
            {
                virus.transform.position = _destinationPoint;
                _moving = false;
            }
        }
    }

}
