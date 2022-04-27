using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Основной класс для работы с вирусом.
/// </summary>
public class MatchElement
{
    private readonly ITouchStrategy _strategy;

    public Vector2Int Position { get; set; }
    
    public int Id { get; }

    /// <summary>
    /// Кол-во очков которое получает игрок.
    /// </summary>
    public int GamePoints { get; }

    public MatchElement(int gamePoints, int id, ITouchStrategy strategy)
    {
        GamePoints = gamePoints;
        Id = id;
     
        _strategy = strategy;
    }


    public void Touch()
    {
        _strategy.Touch(Position);
    }
}