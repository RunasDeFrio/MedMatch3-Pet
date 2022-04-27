using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// Класс для представления конца игры.
/// </summary>
public class GameOverViewer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Button _restartButton;

    public void Construct(GameState gameState)
    {
        gameState.IsGameActive.Subscribe(value => target.SetActive(!value)).AddTo(this);
        _restartButton.OnClickAsObservable().Subscribe(_ => gameState.Restart()).AddTo(this);
    }

}