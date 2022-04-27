using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Стратегия проверки 4 соседей вируса.
/// </summary>
class TouchesFourNeighbors : ITouchStrategy
{
    private ReactiveGreed<MatchElement> _greed;
    
    public bool CheckPossibleMoves(Vector2Int pos)
    {
        int type = _greed[pos.x, pos.y].Id;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                var virus = _greed[pos.x + i, pos.y + j];
                if (i != j && i != -j && virus != null)
                    if (virus.Id == type)
                        return true;
            }
        return false;
    }

    public void Touch(Vector2Int position)
    {
        
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != j && i != -j)
                {
                    var virus = _greed[position.x + i, position.y + j];
                    if (virus != null && virus.Id == _greed[position.x, position.y].Id)
                    {
                        _greed.Remove(new Vector2Int(position.x + i, position.y + j));
                    }
                }
        _greed.Remove(position);
    }
}
