using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Фабрика для создания вирусов.
/// </summary>
public class VirusFactory : MonoBehaviour
{    
    /// <summary>
    /// Массив префабов для шариков.
    /// </summary>
    [SerializeField]
    private List<GameObject> virusPrefabs;

    /// <summary>
    /// Сохраннёные копии.
    /// </summary>
    private List<Stack<Virus>> _store;

    public void Awake()
    {
        _store = new List<Stack<Virus>>(virusPrefabs.Count);

        for(int i = 0; i < virusPrefabs.Count; i++)
            _store.Add(new Stack<Virus>(10));
    }

    /// <summary>
    /// Изымает из хранилища вирус. Если хранилище пусто, то создаёт объект.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="pos"></param>
    /// <param name="ScaleFactor"></param>
    /// <returns></returns>
    public Virus Get(Transform parent, Vector2 pos, Vector2 ScaleFactor)
    {
        Virus virus;
        int type = UnityEngine.Random.Range(0, virusPrefabs.Count - 1);
        if (_store[type].Count > 0)
        {
            virus = _store[type].Pop();
            virus.transform.position = pos;
            virus.transform.SetParent(parent);
            virus.gameObject.SetActive(true);
        }
        else
        {
            virus = Instantiate(virusPrefabs[type], pos, Quaternion.identity, transform).GetComponent<Virus>();
            virus.Type = type;
            virus.NeedDestroy += Pull;
        }
        virus.transform.localScale *= ScaleFactor;
        return virus;
    }

    /// <summary>
    /// Поместить объект обратно в хранилище. Осуществляется самостоятельно после вызова NeedDestroy.
    /// </summary>
    /// <param name="virus"></param>
    public void Pull(Virus virus)
    {
        if (virus != null)
        {
            virus.gameObject.SetActive(false);
            _store[virus.Type].Push(virus);
        }
    }
}