using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameButtonClass : MonoBehaviour {

    public GameObject actionLine;
    public GameObject mainCamera;

	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { RunGameLine((PointerEventData) data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { EndGameLine((PointerEventData)data); });
        trigger.triggers.Add(exitEntry);

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { StartGameLine((PointerEventData)data); });
        trigger.triggers.Add(enterEntry);
    }

    public void StartGameLine(PointerEventData data)
    {
        mainCamera.GetComponent<mainGame>().setStoryTime();
    }

    public void RunGameLine(PointerEventData data)
    {
        bool storyTime = mainCamera.GetComponent<mainGame>().getStoryTime();

        if (!storyTime)
        {
            actionLine.GetComponent<LineMove>().setGameRun(true);
        }
    }

    public void EndGameLine(PointerEventData data)
    {
        SceneManager.LoadScene(1);
    }

}
