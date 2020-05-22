using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Анимация "плавания" объекта на основе гармонических колебаний.
/// </summary>
public class Floating : MonoBehaviour
{
    /// <summary>
    /// Генерация смещения будет случайной?
    /// </summary>
    [SerializeField]
    private bool RandomOffset;
    
    /// <summary>
    /// Смещение по времени. "Фаза"
    /// </summary>
    [SerializeField]
    private float timeOffset;

    /// <summary>
    /// Частоты с которыми будет происходить вращение по осям.
    /// </summary>
    [SerializeField]
    private Vector2 frequency = new Vector2(6, 1);

    /// <summary>
    /// Амплитуда колебаний.
    /// </summary>
    [SerializeField]
    private Vector2 intensity = new Vector2(0.1f, 1);

    private Vector3 _startPosition;
    
    private void Start()
    {
        if (RandomOffset)
            timeOffset = Random.Range(0, 2 * Mathf.PI);
        _startPosition = transform.position;
    }

    void Update()
    {
        transform.position = _startPosition + new Vector3(intensity.x * Mathf.Cos(frequency.x * Time.time + timeOffset), intensity.y * Mathf.Sin(frequency.y * Time.time + timeOffset));
    }
}
