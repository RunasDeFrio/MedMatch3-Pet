using System.Collections.Generic;
using Elements;
using Sirenix.OdinInspector;
using UnityEngine;


namespace DefaultNamespace
{
    /// <summary>
    /// Корень сборки приложения
    /// </summary>
    public class Root : SerializedMonoBehaviour
    {
        [SerializeField, Tooltip("Размер сетки поля")] private Vector2Int matchGreedSize;
        [SerializeField, Tooltip("Начальные параметры игры")] private GameState.GameStateData gameStateData;
        [SerializeField, Tooltip("Данные элеменетов")] private List<ElementData> _elementData;

        [SerializeField] private MovesViewer movesViewer;
        [SerializeField] private GamePointsViewer gamePointsViewer;
        [SerializeField] private GameOverViewer gameOverViewer;
        [SerializeField] private MatchGreedView greedView;
        
        private void Awake()
        {
            ReactiveGreed<MatchElement> reactiveGreed = new ReactiveGreed<MatchElement>(matchGreedSize);
            
            MatchGreed greed = new MatchGreed(reactiveGreed, new MatchElementFactory(_elementData, reactiveGreed));
            GameState gameState = new GameState(gameStateData, greed);
            
            gameState.Restart();
            
            movesViewer.Construct(gameState);
            gamePointsViewer.Construct(gameState);
            gameOverViewer.Construct(gameState);
            greedView.Counstruct(greed);
        }
    }
}