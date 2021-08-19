using UnityEngine;
using UnityEngine.EventSystems;

public class CellClickRegistrator : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GoalChecker.CheckСorrectnessOfPressing(this.gameObject);
    }
}
