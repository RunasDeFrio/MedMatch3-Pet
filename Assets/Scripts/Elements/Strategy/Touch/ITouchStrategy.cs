
using System.Numerics;
using UnityEngine;

/// <summary>
/// Стратегия которая вызывается при нажатии на вирус. Позволяет расширить возможности игры.
/// </summary>
public interface ITouchStrategy
{
    void Touch(Vector2Int position);
    
    bool CheckPossibleMoves(Vector2Int pos);
}
