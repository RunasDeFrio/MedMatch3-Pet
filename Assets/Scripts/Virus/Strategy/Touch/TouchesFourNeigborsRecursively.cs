/// <summary>
/// Стратегия рекурсивного прохода по соседям.
/// </summary>
class TouchesFourNeighborsRecursively : ITouchStrategy
{
    public bool CheckPossibleMoves(int x, int y, VirusGreed greed)
    {
        int type = greed.GetVirus(x, y).Type;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                var virus = greed.GetVirus(x + i, y + j);
                if (i != j && i != -j && virus != null)
                    if (virus.Type == type)
                        return true;
            }
        return false;
    }

    public int Touch(int x, int y, VirusGreed greed, out int gamePoints)
    {
        gamePoints = greed.GetVirus(x, y).GamePoints;
        int type = greed.GetVirus(x, y).Type;
        greed.DestroyVirus(x, y);
        int newMoves = 0;

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != j && i != -j)
                {
                    var virus = greed.GetVirus(x + i, y + j);
                    if (virus != null && virus.Type == type)
                    {
                        newMoves++;
                        newMoves += Touch(x + i, y + j, greed, out int newGamePoints);
                        gamePoints += newGamePoints;
                    }
                }

        return newMoves;
    }
}
