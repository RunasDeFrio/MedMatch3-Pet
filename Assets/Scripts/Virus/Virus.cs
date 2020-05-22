using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Основной класс для работы с вирусом.
/// </summary>
public class Virus : MonoBehaviour
{
    /// <summary>
    /// Стратегия поведения. Вызывается каждый Update.
    /// </summary>
    private AnimationStrategy strategy;

    /// <summary>
    /// Кол-во очков которое получает игрок при уничтожении данного вируса.
    /// </summary>
    [SerializeField]
    private int _gamePoints;

    public int Type { get; set; }
    public int GamePoints { get => _gamePoints; set => _gamePoints = value; }

    public event UnityAction<Virus> NeedDestroy;

    void Update()
    {
        if (strategy != null)
            strategy.DoUpdate(this);
    }

    /// <summary>
    /// Установить движение в точку за определенное время.
    /// </summary>
    /// <param name="destinationPoint"></param>
    /// <param name="moveTime"></param>
    public void SetMovingDestinationPoint(Vector2 destinationPoint, float moveTime)
    {
        strategy = new Moving(moveTime, destinationPoint, transform.position);
    }

    /// <summary>
    /// Начало анимации уничтожения после чего обект уничтожается.
    /// </summary>
    /// <param name="timeDestroy"></param>
    /// <param name="scaleFactor"></param>
    public void StartDestroy(float timeDestroy, float scaleFactor)
    {
        strategy = new Destroying(timeDestroy, scaleFactor * transform.localScale, transform.localScale);
    }

    public void DestroyVirus() => NeedDestroy.Invoke(this);

}
