using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Стратегия рекурсивного прохода по соседям.
/// </summary>
class TouchesFourNeighborsRecursively : ITouchStrategy
{
    private ReactiveGreed<MatchElement> _greed;

    public TouchesFourNeighborsRecursively(ReactiveGreed<MatchElement> greed)
    {
        this._greed = greed;
    }

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
        HashSet<Vector2Int> removes = new HashSet<Vector2Int>(3);
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        removes.Add(position);
        stack.Push(position);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            var currentElement = _greed[current];
            
            if (currentElement == null)
                continue;
            
            for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != j && i != -j)
                {
                    Vector2Int find = new Vector2Int(current.x + i, current.y + j);
                    var matchElement = _greed[find];
                    if (matchElement != null && matchElement.Id == currentElement.Id && !removes.Contains(find))
                    {
                        stack.Push(find);
                        removes.Add(find);
                    }
                }
        }

        foreach (var pos in removes)
        {
            _greed.Remove(pos);
        }
    }
}