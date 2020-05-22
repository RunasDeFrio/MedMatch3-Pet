using UnityEngine;

/// <summary>
/// Стратегия уничтожения. Расширяет объект и затем вызывает его уничтожение.
/// </summary>
public class Destroying : AnimationStrategy
{
    private float _destroyTime = 1.0f;
    private float _startTime;
    private Vector2 _destinationScale;
    private Vector2 _startScale;

    public Destroying(float destroyTime, Vector2 destinationScale, Vector2 startScale)
    {
        _destroyTime = destroyTime;
        _destinationScale = destinationScale;
        _startScale = startScale;

        _startTime = Time.time;
    }

    public void DoUpdate(Virus virus)
    {
        float t = (Time.time - _startTime) / _destroyTime;
        if (t <= 1.0f)
            virus.transform.localScale = Vector2.Lerp(_startScale, _destinationScale, t);
        else
        {
            virus.transform.localScale = _startScale;
            virus.DestroyVirus();
        }
    }
}
