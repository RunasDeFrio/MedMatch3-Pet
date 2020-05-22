using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

/// <summary>
/// Класс состояния игры для хранения рекордов.
/// </summary>
public class GameState : MonoBehaviour
{
    private List<TableInfo> _table;
    public int NewRecordIndex { get; set; }
    internal List<TableInfo> Table { get => _table; set => _table = value; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ReadCSV();
    }

    private void ReadCSV()
    {
        //читаем csv
        TextAsset textAsset = Resources.Load<TextAsset>("RecordsTable");
        var lines = textAsset.text.Split(new[] { "\r\n" }, StringSplitOptions.None);
        //раскидываем строчки и параметры по полям структуры
        Table = (from info in (from line in lines
                               select (line.Split('\t')))
                 where info.Length > 1
                 let gp = Int32.Parse(info[1])
                 let date = DateTime.ParseExact(info[0], "dd.MM.yyyy", CultureInfo.InvariantCulture)
                 orderby gp
                 select new TableInfo(date, gp)).ToList();

    }

    public bool CheckNewRecord(int gamePoints)
    {
        if (Table != null && Table.Count > 0)
        {
            if (gamePoints < Table[0].gamePoint)
            {
                NewRecordIndex = -1;
                return false;
            }
            for (int i = 1; i < Table.Count; i++)
                if (gamePoints < Table[i].gamePoint)
                {
                    NewRecordIndex = i - 1;
                    Table[NewRecordIndex] = new TableInfo(DateTime.Now, gamePoints);
                    return true;
                }
            NewRecordIndex = Table.Count - 1;
            Table[NewRecordIndex] = new TableInfo(DateTime.Now, gamePoints);
            return true;
        }
        else
        {
            NewRecordIndex = -1;
            return false;
        }
    }
}
