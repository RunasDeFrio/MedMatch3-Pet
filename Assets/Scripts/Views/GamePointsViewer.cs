using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Класс для представления кол-во очков набранных игроком.
/// </summary>
public class GamePointsViewer : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void ViewGamePoints(int gp)
    {
        text.text = $"Ваш счёт: {gp}";
    }

    
    public void Construct(GameState gameState)
    {
        gameState.GamePoints.Subscribe(ViewGamePoints).AddTo(this);
    }
}
