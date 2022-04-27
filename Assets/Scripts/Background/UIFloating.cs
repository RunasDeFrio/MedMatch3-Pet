using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Анимация "плавания" объекта на основе гармонических колебаний.
/// </summary>
public class UIFloating : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    /// <summary>
    /// Флаг случайной генерации смещения
    /// </summary>
    [SerializeField] private bool randomOffset;
    
    /// <summary>
    /// Смещение по времени.
    /// </summary>
    [SerializeField, HideIf("@randomOffset")]
    private float timeOffset;

    /// <summary>
    /// Частоты с которыми будет происходить вращение по осям.
    /// </summary>
    [SerializeField] private Vector2 frequency = new Vector2(6, 1);

    /// <summary>
    /// Амплитуда колебаний.
    /// </summary>
    [SerializeField] private Vector2 intensity = new Vector2(0.1f, 1);

    private Vector3 _startPosition;

    private void Start()
    {
        if (randomOffset)
            timeOffset = Random.Range(0, 2 * Mathf.PI);
        _startPosition = _rectTransform.anchoredPosition;
    }

    void Update()
    {
        _rectTransform.anchoredPosition = _startPosition + new Vector3(intensity.x * Mathf.Cos(frequency.x * Time.time + timeOffset),
            intensity.y * Mathf.Sin(frequency.y * Time.time + timeOffset));
    }
}