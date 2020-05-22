using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс сетки, которая управляет логикой шариков.
/// </summary>
public class VirusGreed : MonoBehaviour
{
    [SerializeField]
    private float _virusMoveTime = 0.5f;

    [SerializeField]
    private float _virusDestroyTime = 0.5f;
    /// <summary>
    /// Размер сетки.
    /// </summary>
    [SerializeField]
    private Vector2 _cellSize = new Vector2(0, 0);
    
    /// <summary>
    /// Ширина сетки.
    /// </summary>
    [SerializeField]
    private int _width = 10;

    /// <summary>
    /// Высота сетки.
    /// </summary>
    [SerializeField]
    private int _height = 10;

    /// <summary>
    /// Массив всех шариков.
    /// </summary>
    private List<List<Virus>> _viruses;

    private bool _gameOver = false;

    private VirusFactory _spawner;

    private Camera _camera;

    private ITouchStrategy _touchStrategy = new TouchesFourNeighbors();

    private Transform _tr;

    public int Width { get => _width; set => _width = value; }
    public int Height { get => _height; set => _height = value; }

    public event UnityEngine.Events.UnityAction<int> NewGamePoints;
    public event UnityEngine.Events.UnityAction<int> NewMoves;
    public event UnityEngine.Events.UnityAction NotPossibleMoves;

    void Start()
    {
        _camera = Camera.main;
        _tr = transform;
        _spawner = GetComponent<VirusFactory>();
        _viruses = new List<List<Virus>>(Width);

        GameManager gm = FindObjectOfType<GameManager>();
        gm.GameOver += SetGameOver;


        for (int i = 0; i < Width; i++)
            _viruses.Add(new List<Virus>(Height));
        MakeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameOver && Input.GetMouseButtonDown(0) && Time.timeScale > float.Epsilon)
        {
            Vector3 posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            int x = Mathf.RoundToInt(posTouch.x / _cellSize.x);
            int y = Mathf.RoundToInt(posTouch.y / _cellSize.y);

            int newMoves = -1;
            int newGamePoints = 0;
            if (CheckBoundaries(x, y) && _viruses[x][y] != null)
            {
                newMoves += _touchStrategy.Touch(x, y, this, out newGamePoints);
                NewMoves?.Invoke(newMoves);
                NewGamePoints?.Invoke(newGamePoints);
                UpdateGreed();
                if (!CheckPossibleMoves())
                    NotPossibleMoves?.Invoke();
            }
        }
    }

    Vector2 IndexToLocalPosition(int x, int y) => new Vector2(x * _cellSize.x, y * _cellSize.y);
    Vector2 IndexToPosition(int x, int y) => _tr.TransformPoint(IndexToLocalPosition(x, y));

    bool CheckBoundaries(int x, int y) => x > -1 && x < Width && y > -1 && y < Height;
    public Virus GetVirus(int x, int y) => CheckBoundaries(x, y) ? _viruses[x][y] : null;

    void SetGameOver() => _gameOver = true;

    void CreateVirus(int x, int y)
    {
        Vector2 pos = IndexToPosition(x, y);
        Virus virus = _spawner.Get(transform, pos + Vector2.up * 2, _cellSize);
        virus.SetMovingDestinationPoint(pos, _virusMoveTime);
        _viruses[x][y] = virus;
    }

    public void DestroyVirus(int x, int y)
    {
        if(CheckBoundaries(x, y))
        _viruses[x][y].StartDestroy(_virusDestroyTime, 1.5f);
        _viruses[x][y] = null;
    }

    private void MakeGrid()
    {
        for (int j = 0; j < Height; j++)
            for (int i = 0; i < Width; i++)
            {
                if(j + 1 >= _viruses[i].Count) _viruses[i].Add(null);
                CreateVirus(i, j);
            }
    }

    private int CheckNeighborsRecursive(int x, int y, int newGamePoints)
    {
        newGamePoints++;
        int type = GetVirus(x, y).Type;
        DestroyVirus(x, y);
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != j && i != -j)
                {
                    var virus = GetVirus(x + i, y + j);
                    if(virus != null && virus.Type == type)
                        newGamePoints = CheckNeighborsRecursive(x + i, y + j, newGamePoints);
                }

        return newGamePoints;
    }

    private void UpdateGreed()
    {
        for (int i = 0; i < Width; i++)
        {
            bool columnNeedFilling = false;
            for (int j = 0; j < Height; j++)
            {
                if (!columnNeedFilling && _viruses[i][j] == null)
                    columnNeedFilling = !PullTopNeighbors(i, j);

                if (columnNeedFilling)
                    CreateVirus(i, j);
            }
        }
    }

    private bool PullTopNeighbors(int x, int y)
    {
        for (int k = 0; y + k < Height; k++)
            if (_viruses[x][y + k] != null)
            {
                _viruses[x][y] = _viruses[x][y + k];
                _viruses[x][y + k] = null;
                _viruses[x][y].SetMovingDestinationPoint(IndexToPosition(x, y), _virusMoveTime);
                return true;
            }
        return false;
    }

    private bool CheckPossibleMoves()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (_touchStrategy.CheckPossibleMoves(x, y, this))
                    return true;
        return false;
    }
}
