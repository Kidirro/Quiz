using System.Collections.Generic;
using UnityEngine;

public class MatrixBuilder : MonoBehaviour
{
    static private GameObject _cellParent;

    static private List<GameObject> _matrixData = new List<GameObject>();

    static private Vector2Int _matrixSize;

    static private Vector2 _startVector;

    static private Vector2 _worldPointRightUp => Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

    static private Vector2 _worldPointLeftDown => Camera.main.ScreenToWorldPoint(Vector2.zero);

    static public void BuildNewMatrix(GameObject prefab, Vector2Int size, cellType type = cellType.any)
    {
        if (_cellParent == null)
        {
            _cellParent = new GameObject();
            _cellParent.name = "Cells";
        }

        if (_matrixData.Count > 0)
        {
            foreach(GameObject cell in _matrixData)
            {
                Destroy(cell);
            }
            _matrixData.Clear();
        }

        float StartPointX = CalculateLengthToBorder(_worldPointRightUp.x, _worldPointLeftDown.x, prefab.transform.localScale.x, size.x);
        float StartPointY = CalculateLengthToBorder(_worldPointRightUp.y, _worldPointLeftDown.y, prefab.transform.localScale.y, size.y);

        _startVector = new Vector2(StartPointX,StartPointY);


        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                _matrixData.Add(NewCell(prefab, new Vector2(StartPointX + i * prefab.transform.localScale.x, StartPointY + j * prefab.transform.localScale.y)));
            }
        }
        GiveName(_matrixData);
        _matrixSize = size;
    }
    public static void AddLineToMatrix(GameObject prefab, int countLine)
    {
        float NewStartPointY = CalculateLengthToBorder(_worldPointRightUp.y, _worldPointLeftDown.y, prefab.transform.localScale.y,_matrixSize.y+countLine);
        float difference = _startVector.y - NewStartPointY;
        foreach(GameObject cell in _matrixData)
        {
            cell.transform.position = new Vector2(cell.transform.position.x, cell.transform.position.y+difference);
        }
        _startVector.y = NewStartPointY;

        for (int i = 0; i < _matrixSize.x; i++)
        {

            _matrixData.Add(NewCell(prefab, new Vector2(_startVector.x + i * prefab.transform.localScale.x, _startVector.y)));
        }
        _matrixSize.y = _matrixSize.y + countLine;
        GiveName(_matrixData);

    }
    
    private static void GiveName(List<GameObject> cells)
    {
        foreach (GameObject cell in cells)
        {
            cell.name = Goal.GetValue();
        }

        GlobalEvent.EventTrigger(TypesEvent.NewValue);
    }

    private static GameObject NewCell(GameObject prefab,Vector2 position)
    {
        GameObject CellObject = Instantiate(prefab, position, Quaternion.identity);
        CellObject.transform.SetParent(_cellParent.transform);
        return CellObject;
    }


    static private float CalculateLengthToBorder(float biggestPoint,float smallestPoint,float scaleCell,float countCell)
    {
        return (biggestPoint + smallestPoint - scaleCell * countCell) / 2+scaleCell/2;
    }
}

public enum cellType
{
    numbers,
    letters,
    any
}
