using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XButtonScript : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject actionLine;

    // Use this for initialization
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { RunGameLine((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void RunGameLine(PointerEventData data)
    {
        mainCamera.GetComponent<mainGame>().setStoryTime();
        actionLine.SetActive(true);
        actionLine.GetComponent<LineMove>().setGameRun(true);
    }

}
