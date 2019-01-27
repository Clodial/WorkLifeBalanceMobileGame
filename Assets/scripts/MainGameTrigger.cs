using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainGameTrigger : EventTrigger {

    public GameObject actionLine;

    public override void OnPointerDown(PointerEventData data)
    {
        actionLine.GetComponent<LineMove>().setGameRun(true);
    }

    public override void OnPointerExit(PointerEventData data)
    {
        actionLine.GetComponent<LineMove>().setGameRun(false);
    }

}
