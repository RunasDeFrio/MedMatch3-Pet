using System;

/// <summary>
/// Табличные значения для вывода.
/// </summary>
struct TableInfo
{
    public DateTime date;
    public int gamePoint;

    public TableInfo(DateTime date, int gamePoint)
    {
        this.date = date;
        this.gamePoint = gamePoint;
    }
}