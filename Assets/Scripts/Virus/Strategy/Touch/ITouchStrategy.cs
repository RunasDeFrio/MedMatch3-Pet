
/// <summary>
/// Стратегия которая вызывается при нажатии на вирус. Позволяет расширить возможности игры.
/// </summary>
public interface ITouchStrategy
{
    int Touch(int i, int j, VirusGreed greed, out int gamePoints);
    bool CheckPossibleMoves(int i, int j, VirusGreed greed);
}
