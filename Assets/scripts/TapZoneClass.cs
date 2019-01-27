using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapZoneClass : MonoBehaviour {

    public GameObject mainCamera;
    public bool isHome;
    public int timeToLive;
    bool shrink;
    public AudioClip successClip;
    public AudioClip failClip;

	// Use this for initialization
	void Start () {
        shrink = false;
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) =>
        {
            mainCamera.GetComponent<mainGame>().playAudioOneShot(successClip, 1f);
            if(isHome)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mainGame>().setScore();
            }
            GameObject.Destroy(gameObject);
        });
        trigger.triggers.Add(entry);
        gameObject.transform.localScale = new Vector3(.65f, .65f, .65f);
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive--;
        if(timeToLive <= 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mainGame>().setMisses();
            mainCamera.GetComponent<mainGame>().playAudioOneShot(failClip, 1f);
            GameObject.Destroy(gameObject);
        }
        if(gameObject.transform.localScale.x < 1.25f && !shrink) {
            gameObject.transform.localScale += new Vector3(.01f, .01f, .01f);
        }
        else
        {
            shrink = true;
            gameObject.transform.localScale -= new Vector3(.01f, .01f, .01f);
        }
	}
}
