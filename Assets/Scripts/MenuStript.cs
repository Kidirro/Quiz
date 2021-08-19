using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStript : MonoBehaviour
{
    [SerializeField]private Text _goalText;


    [SerializeField] private List<GameObject> _menu;

    [SerializeField] private ImageFade _fade;

    private void Start()
    {
        GlobalEvent.Subscribe(TypesEvent.NewValue, UpdateText);
        GlobalEvent.Subscribe(TypesEvent.GameOver,ShowRestart);
        ShowAnyMenu(0);
    }

    void ShowRestart()
    {
        ShowAnyMenu(2);
    }

    void ShowAnyMenu(int idMenu)
    {
        for (int i = 0; i < _menu.Count; i++)
        {
            _menu[i].SetActive(i == idMenu);
        }

    }

    public void StartGame()
    {
       _fade.StartFade();

        ShowAnyMenu(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void  UpdateText()
    {
        _goalText.text = "Find " + Goal.CurrentCoal;
    }
}
