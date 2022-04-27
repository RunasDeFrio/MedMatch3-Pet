using UniRx;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///     Класс для представления кол-ва оставшихся ходов.
/// </summary>
public class MovesViewer : MonoBehaviour
{
    [SerializeField] private Text text;

    
    public void Construct(GameState gameState)
    {
        gameState.Moves.Subscribe(ViewMoves).AddTo(this);
    }

    private void ViewMoves(int gp)
    {
        gp = gp > 0 ? gp : 0;
        text.text = $"Осталось ходов: {gp}";
    }
}