using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Класс менеджера для работы с проверками и событиями окончания игры.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _gamePoinst = 0;
    [SerializeField]
    private int _moves = 5;

    [SerializeField]
    private GameObject gameStatePrefab;
    private GameState _state;

    public event UnityAction GameOver;
    public event UnityAction NewRecord;

    public event UnityAction<int> MovesChange;
    public event UnityAction<int> GamePointsChange;

    // Start is called before the first frame update
    void Start()
    {
        VirusGreed greed = FindObjectOfType<VirusGreed>();
        _state = FindObjectOfType<GameState>();
        if (!_state)
            _state = Instantiate(gameStatePrefab).GetComponent<GameState>();

        greed.NewGamePoints += AddGamePoints;
        greed.NewMoves += AddMoves;
        greed.NotPossibleMoves += SetNotPossibleMoves;
        
        Time.timeScale = 1.0f;

        GamePointsChange?.Invoke(_gamePoinst);
        MovesChange?.Invoke(_moves);
    }
    /// <summary>
    /// Объявить рекорд или объявить GameOver.
    /// </summary>
    private void SetGameOver()
    {
        if (_state.CheckNewRecord(_gamePoinst))
            NewRecord?.Invoke();
        else
            GameOver?.Invoke();

    }

    /// <summary>
    /// Добавить очки к счёту.
    /// </summary>
    /// <param name="gp"></param>
    public void AddGamePoints(int gp)
    {
        _gamePoinst += gp;
        GamePointsChange?.Invoke(_gamePoinst);
    }

    /// <summary>
    /// Добавить или вычесть ходы.
    /// </summary>
    /// <param name="gp"></param>
    public void AddMoves(int gp)
    {
        _moves += gp;
        MovesChange?.Invoke(_moves);
        if (_moves < 1)
            SetGameOver();
    }

    /// <summary>
    /// Проверить возможно ли играть дальше.
    /// </summary>
    public void SetNotPossibleMoves()
    {
        if (_moves < 2)
            SetGameOver();
    }


}
