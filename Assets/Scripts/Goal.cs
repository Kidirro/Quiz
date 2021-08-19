using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static private  string _currentCoal;
    static public string CurrentCoal
    {
        get { return _currentCoal; }
    }
    
    private static List<string> _listOfLetters = new List<string>();
    private static List<string> _listOfNumbers = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

    private static List<string> _listOfValue = new List<string>();
    private static List<string> _listOfUsedGoals = new List<string>();

    private void Awake()
    {
        _listOfLetters = GenerateListOfLetters(25, 65);
        GlobalEvent.Subscribe(TypesEvent.GameStart,ClearUsedList);
    }

    private List<string> GenerateListOfLetters(int size,int startPosition = 0)
    {
        List<string> resultList = new List<string>();

        for (int i = startPosition; i <= startPosition + size; i++)
        {
            resultList.Add(((char)i).ToString());
        }

        return resultList;
    }

    public static void NewGoal(cellType type,int size) 
    {
        List<string> _remain = new List<string>();
        switch (type)
        {
            case cellType.numbers:
                _remain.AddRange(_listOfNumbers);
                break; 
            case cellType.letters:
                _remain.AddRange(_listOfLetters);
                break;  
            case cellType.any:
                _remain.AddRange(_listOfNumbers);
                _remain.AddRange(_listOfLetters);
                break;
        }
        RemoveUsedFromList(_remain, _listOfUsedGoals);

        for (int i = 0; i < size; i++)
        {
            int randomValue = Random.Range(0, _remain.Count);
            _listOfValue.Add(_remain[randomValue]);
            _remain.Remove(_remain[randomValue]);
            
        }
        _currentCoal = _listOfValue[ Random.Range(0, _listOfValue.Count)];
        _listOfUsedGoals.Add(_currentCoal);
    }

    private static List<string> RemoveUsedFromList(List<string> mainList, List<string> usedValue)
    {
        foreach (string item in usedValue)
        {
            if (mainList.Contains(item))
            {
                mainList.Remove(item);
            }
        }
        return mainList;
    }

    private static void ClearUsedList()
    {
        _listOfUsedGoals.Clear();
    }

    public static string GetValue()
    {
        int randomValue = Random.Range(0, _listOfValue.Count);
        string resultString = string.Copy(_listOfValue[randomValue]);
        _listOfValue.Remove(resultString);
        return resultString;
    }
}
