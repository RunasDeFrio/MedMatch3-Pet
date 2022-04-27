using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using RunasDev.Core.Factories;
using RunasDev.Core.Pooling;
using UniRx;
using UnityEngine;

/// <summary>
/// Класс сетки, которая управляет логикой шариков.
/// </summary>
public class MatchGreed : IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;

    private readonly IFactory<MatchElement> _spawner;

    private readonly ReactiveGreed<MatchElement> _greed;

    public ReactiveGreed<MatchElement> Greed => _greed;

    public MatchGreed(ReactiveGreed<MatchElement> greed, MatchElementFactory spawner)
    {
        _compositeDisposable = new CompositeDisposable();
        _greed = greed;
        _greed.OnElementAdd.Subscribe(eventInfo => { eventInfo.item.Position = eventInfo.pos; })
            .AddTo(_compositeDisposable);

        _greed.OnElementMove.Subscribe(eventInfo => { eventInfo.item.Position = eventInfo.to; })
            .AddTo(_compositeDisposable);

        _greed.OnElementRemove.Subscribe(eventInfo =>
        {
            Observable.TimerFrame(1).Subscribe(_ => OnElementRemove(eventInfo));
        }).AddTo(_compositeDisposable);
        _spawner = spawner;
    }

    private void OnElementRemove((MatchElement item, Vector2Int pos) eventInfo)
    {
        var pos = eventInfo.pos;

        for (int i = _greed.Size.y; i >= 1; i--)
        {
            Vector2Int to = new Vector2Int(pos.x, i);
            if(_greed[to] == null)
            {
                _greed.Move(new Vector2Int(pos.x, i - 1), to);
            }
        }

        _greed.Add(_spawner.Create(), new Vector2Int(pos.x, 0));
    }

    public void RandomGenerate()
    {
        _greed.Clear();
        for (int i = 0; i < _greed.Size.x; i++)
        for (int j = 0; j < _greed.Size.y; j++)
            _greed.Add(_spawner.Create(), new Vector2Int(i, j));
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
    
}