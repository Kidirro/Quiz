using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFade : MonoBehaviour
{

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void StartFade()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            _image.color = new Color(1, 1, 1, i);
            yield return null;
        }

        GlobalEvent.EventTrigger(TypesEvent.GameStart);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            _image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}