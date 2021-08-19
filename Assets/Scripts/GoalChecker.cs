using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    static public void CheckСorrectnessOfPressing(GameObject clickedObject) 
    {
       if (clickedObject.name == Goal.CurrentCoal)
        {
            GlobalEvent.EventTrigger(TypesEvent.NewLevel);
        }
        else
        {
            clickedObject.transform.GetChild(0).GetComponent<CellAnimation>().StartErrorShaking();
        }
    }
}
