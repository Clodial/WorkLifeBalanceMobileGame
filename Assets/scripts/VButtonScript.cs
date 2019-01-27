using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VButtonScript : MonoBehaviour {

    public GameObject mainCamera;

	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { TurnPage((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void TurnPage(PointerEventData data)
    {
        mainCamera.GetComponent<mainGame>().setStoryPage();
    }
}
