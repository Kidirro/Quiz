using System.Collections;
using UnityEngine;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private float _powerErrorShaking;
    [SerializeField] private float _powerSpawnShaking;
    private IEnumerator _shakingProcess;
    private Vector2 _anchorScale;

    private void Awake()
    {
        _shakingProcess = ShakingProcess(1,_powerErrorShaking);
        _anchorScale = transform.localScale;
        GlobalEvent.Subscribe(TypesEvent.NewValue, StartSpawnAnimation);
    }

    public void StartSpawnAnimation()
    {
        StartShaking(-1, _powerSpawnShaking);  
    }

    public void StartErrorShaking()
    {
        StartShaking(1, _powerErrorShaking);
    }

    public void StartShaking(int i, float power)
    {
        StopCoroutine(_shakingProcess);
        _shakingProcess = ShakingProcess(i, power);
        transform.localScale = _anchorScale;
        StartCoroutine(_shakingProcess);
    }



    IEnumerator ShakingProcess(int i,float power)
    {
        float time =1;
        float difference = EaseInBounce(time);
        float anchor = transform.localScale.x;
        while (difference>0)
        {
            transform.localScale = new Vector2(anchor-difference*power,anchor-i*difference*power);
            time += Time.deltaTime;
            difference = EaseInBounce(time);
            yield return null;
        }
    }

    private float EaseInBounce(float x)
    {
        return 1 - EaseOutBounce(1 - x);
    }

    private float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * ((x - 1.5f) / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * ((x - 2.25f) / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * ((x - 2.625f) / d1) * x + 0.984375f;
        }
    }

    private void OnDestroy()
    {
        GlobalEvent.Unsubscribe(TypesEvent.NewValue, StartSpawnAnimation);
    }
}
