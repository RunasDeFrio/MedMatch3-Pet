using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для переключения между сценами и управления игрового меню в процессе игры.
/// </summary>
public class GameMenu : MainMenu
{
    private GameObject _target;

    void Awake()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.NewRecord += GoToTableRecord;

        //TODO: лучше заменить на ссылку указываемой в инспекторе
        _target = transform.GetChild(0).gameObject;
    }


    public void ShowMenu()
    {
        Time.timeScale = 0;
        _target.SetActive(true);
    }

    public void HideMenu()
    {
        Time.timeScale = 1;
        _target.SetActive(false);
    }

    public new void GoToMainMenu()
    {
        Time.timeScale = 1;
        base.GoToMainMenu();
    }
}
