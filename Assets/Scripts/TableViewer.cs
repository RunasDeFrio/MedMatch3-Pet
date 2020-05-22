using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для управления представления данных таблицы.
/// </summary>
class TableViewer : MonoBehaviour
{
    [SerializeField]
    private GameObject gameStatePrefab;
    [SerializeField]
    private GameObject cellPrefab;

    private GameState _state;

    private void Start()
    {
        _state = FindObjectOfType<GameState>();
        if (!_state)
            _state = Instantiate(gameStatePrefab).GetComponent<GameState>();
        CreateTable();
    }

    private void CreateTable()    
    {
        var date = Instantiate(cellPrefab, transform).GetComponent<Text>();
        var gp = Instantiate(cellPrefab, transform).GetComponent<Text>();

        date.text = "Дата";
        gp.text   = "Очки";

        for (int i = _state.Table.Count -1 ; i >= 0; i--)
        {
            date = Instantiate(cellPrefab, transform).GetComponent<Text>();
            gp = Instantiate(cellPrefab, transform).GetComponent<Text>();

            date.text = _state.Table[i].date.ToString();
            gp.text = _state.Table[i].gamePoint.ToString();
            
            if(_state.NewRecordIndex == i)
            {
                date.color = Color.green;
                gp.color = Color.green;
                _state.NewRecordIndex = -1;
            }
        }
    }
}
