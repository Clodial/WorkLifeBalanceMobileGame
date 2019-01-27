using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerClick;
        exitEntry.callback.AddListener((data) => { EndGameLine((PointerEventData)data); });
        trigger.triggers.Add(exitEntry);
    }

    public void EndGameLine(PointerEventData data)
    {
        SceneManager.LoadScene(0);
    }
}
