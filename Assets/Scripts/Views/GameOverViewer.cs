using UnityEngine;
using System.Collections;

/// <summary>
/// Класс для представления конца игры.
/// </summary>
public class GameOverViewer : MonoBehaviour
{
    private GameObject _target;
    // Use this for initialization
    void Awake()
    {
        _target = transform.GetChild(0).gameObject;
        GameManager gm = FindObjectOfType<GameManager>();
        if(_target)
            gm.GameOver += () => _target.SetActive(true);
    }
}
