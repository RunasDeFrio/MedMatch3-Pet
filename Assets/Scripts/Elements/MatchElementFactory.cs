using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;
using Elements;
using RunasDev.Core.Factories;
using RunasDev.Core.Pooling;

/// <summary>
/// Фабрика для создания элементов.
/// </summary>
public class MatchElementFactory : IFactory<MatchElement>
{
    private readonly List<ElementData> _elementData;

    private readonly ReactiveGreed<MatchElement> _greed;

    public MatchElementFactory(List<ElementData> elementData, ReactiveGreed<MatchElement> greed)
    {
        _elementData = elementData;
        _greed = greed;
    }

    public MatchElement Create()
    {
        int randIndex = Random.Range(0, _elementData.Count - 1);
        ElementData data = _elementData[randIndex]; 
        return new MatchElement(data.GamePoints, data.Id, new TouchesFourNeighborsRecursively(_greed));
    }
}