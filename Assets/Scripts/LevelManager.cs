using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _level = 0;

    [SerializeField] private int _lineSize;
    [SerializeField] private GameObject _cellPrefab;

    private void Start()
    {
        GlobalEvent.Subscribe(TypesEvent.GameStart, StartNewGame);
        GlobalEvent.Subscribe(TypesEvent.NewLevel, StartNewLevel);
    }


    public void StartNewGame()
    {
        _level = 1;

        Goal.NewGoal(cellType.any, _lineSize * _level);
        MatrixBuilder.BuildNewMatrix(_cellPrefab, new Vector2Int(_lineSize, _level));
        GlobalEvent.EventTrigger(TypesEvent.NewValue);
    }

    public void StartNewLevel()
    {
        if (_level != 3)
        {
            _level += 1;
            Goal.NewGoal(cellType.any, _lineSize * _level);
            MatrixBuilder.AddLineToMatrix(_cellPrefab, 1);
        }
        else GlobalEvent.EventTrigger(TypesEvent.GameOver);
    }
}
