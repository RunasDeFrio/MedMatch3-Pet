using System;
using DefaultNamespace;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;


/// <summary>
/// Состояние игры, ходов и очков.
/// </summary>
public class GameState : IDisposable
{
    private readonly GameStateData _gameStateData;
    private readonly MatchGreed _greed;

    [Serializable]
    public struct GameStateData
    {
        public int startMoves;
        public int startGamePoint;
    }

    private readonly ReactiveProperty<int> _gamePoints;

    private readonly ReactiveProperty<bool> _isGameActive;

    private readonly ReactiveProperty<int> _moves;

    private int removeInFrame = 0;

    private IDisposable frameTimer = null;

    public IReadOnlyReactiveProperty<bool> IsGameActive => _isGameActive;

    public IReadOnlyReactiveProperty<int> GamePoints => _gamePoints;

    public IReadOnlyReactiveProperty<int> Moves => _moves;


    public GameState(GameStateData gameStateData, MatchGreed greed)
    {
        _gameStateData = gameStateData;
        _greed = greed;
        
        _moves = new ReactiveProperty<int>();
        _isGameActive = new ReactiveProperty<bool>();
        _gamePoints = new ReactiveProperty<int>();


        greed.Greed.OnElementRemove.Subscribe(eventInfo =>
        {
            var (element, vector2Int) = eventInfo;
            _gamePoints.Value += element.GamePoints;

            if (removeInFrame > 1)
            {
                _moves.Value++;
            }
            else
            {
                _moves.Value--;
                removeInFrame++;
                if (removeInFrame > 1)
                {
                    _moves.Value += removeInFrame - 1;
                }

                if (frameTimer == null)
                    frameTimer = Observable.TimerFrame(1).Subscribe(_ =>
                    {
                        removeInFrame = 0;
                        if (_moves.Value == 0)
                            _isGameActive.Value = false;
                        frameTimer = null;
                    });
            }
        });
    }

    public void Dispose()
    {
        _gamePoints?.Dispose();
        _isGameActive?.Dispose();
        _moves?.Dispose();
        frameTimer?.Dispose();
    }

    public void Restart()
    {
        _greed.RandomGenerate();

        _moves.Value = _gameStateData.startMoves;
        _gamePoints.Value = _gameStateData.startGamePoint;

        _isGameActive.Value = true;
    }
}