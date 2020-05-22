using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Класс для представления кол-ва оставшихся ходов.
/// </summary>
public class MovesViewer : MonoBehaviour
{
    private Text text;

    public void ViewMoves(int gp)
    {
        gp = gp > 0 ? gp : 0;
        text.text = $"Осталось ходов: {gp}";
    }

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();

        GameManager gm = FindObjectOfType<GameManager>();
        gm.MovesChange += ViewMoves;
    }
}
