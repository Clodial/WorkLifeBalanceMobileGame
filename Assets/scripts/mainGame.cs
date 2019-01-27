using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class mainGame : MonoBehaviour {

    private int score;
    public GameObject actionLine;
    public Transform actionLinePlace;
    public Transform canvas;

    public GameObject logo;
    public GameObject vMark;
    public GameObject xMark;
    public GameObject chImage;
    public GameObject chText;
    public GameObject chapter;

    public Image chapterImage;
    public Image chMainImage;
    public Image chapterText;

    private int misses;
    ArrayList coords;
    int stage;
    int storyStage;
    int storyPage;
    bool storyTime;
    public AudioSource audioPlayer;
    public AudioClip audioMenu;
    public AudioClip amusedMusic;
    public int stage6Timer;

    public Sprite chapter1Image;
    public Sprite chapter2Image;
    public Sprite chapter3Image;
    public Sprite chapter4Image;
    public Sprite chapter5Image;
    public Sprite chapter6Image;

    public Sprite chapter1Title;
    public Sprite chapter2Title;
    public Sprite chapter3Title;
    public Sprite chapter4Title;
    public Sprite chapter5Title;
    public Sprite chapter6Title;

    public Sprite chapter1chunk1;
    public Sprite chapter1chunk2;
    public Sprite chapter2chunk1;
    public Sprite chapter3chunk1;
    public Sprite chapter3chunk2;
    public Sprite chapter4chunk1;
    public Sprite chapter5chunk1;
    public Sprite chapter5chunk2;
    public Sprite chapter6chunk1;

    public int lane1Rate;   // school loans
    public int lane2Rate;   // dating costs
    public int lane3Rate;   // housing lease
    public int lane4Rate;   // car lease
    public int lane5Rate;   // car repairs
    public int lane6Rate;   // utilities
    public int lane7Rate;   // cat medical bills
    public int lane8Rate;   // medical bills

    int lane1Timer;
    int lane2Timer;
    int lane3Timer;
    int lane4Timer;
    int lane5Timer;
    int lane6Timer;
    int lane7Timer;
    int lane8Timer;

    public int lane1Pos;
    public int lane2Pos;
    public int lane3Pos;
    public int lane4Pos;
    public int lane5Pos;
    public int lane6Pos;
    public int lane7Pos;
    public int lane8Pos;

    public GameObject lane1Obj;
    public GameObject lane2Obj;
    public GameObject lane3Obj;
    public GameObject lane4Obj;
    public GameObject lane5Obj;
    public GameObject lane6Obj;
    public GameObject lane7Obj;
    public GameObject lane8Obj;

    // Use this for initialization
    void Start () {
        audioPlayer = GetComponent<AudioSource>();
        stage = 0;
        storyPage = 0;
        storyStage = 0;
        storyTime = false;
        score = 0;
        misses = 0;
        stage6Timer = 1000;
        audioPlayer.clip = audioMenu;
        audioPlayer.volume = 1f;
        audioPlayer.loop = true;
        audioPlayer.Play();
        lane1Timer = lane1Rate;
        lane2Timer = lane2Rate;
        lane3Timer = lane3Rate;
        lane4Timer = lane4Rate;
        lane5Timer = lane5Rate;
        lane6Timer = lane6Rate;
        lane7Timer = lane7Rate;
        lane8Timer = lane8Rate;
        coords = new ArrayList();
        coords.Add(new Coordinate(lane1Rate, lane1Timer, lane1Pos, lane1Obj,0, true));
        coords.Add(new Coordinate(lane2Rate, lane2Timer, lane2Pos, lane2Obj,3, false));
        coords.Add(new Coordinate(lane3Rate, lane3Timer, lane3Pos, lane3Obj,0, false));
        coords.Add(new Coordinate(lane4Rate, lane4Timer, lane4Pos, lane4Obj,1, false));
        coords.Add(new Coordinate(lane5Rate, lane5Timer, lane5Pos, lane5Obj,1, true));
        coords.Add(new Coordinate(lane6Rate, lane6Timer, lane6Pos, lane6Obj,0, false));
        coords.Add(new Coordinate(lane7Rate, lane7Timer, lane7Pos, lane7Obj,2, false));
        coords.Add(new Coordinate(lane8Rate, lane8Timer, lane8Pos, lane8Obj,0, true));
    }
	
	// Update is called once per frame
	void Update () {       
        bool gameStatus = actionLine.GetComponent<LineMove>().getGameRun();
        float gameLinePos = actionLine.transform.localPosition.y;
        Debug.Log(misses);
        if (misses >= 10)
        {
            Debug.Log("fail");
            SceneManager.LoadScene(2);
        }
        if (gameStatus)
        {
            chImage.SetActive(false);
            chText.SetActive(false);
            chapter.SetActive(false);
            chapter.SetActive(false);
            chImage.SetActive(false);
            chText.SetActive(false);
            xMark.SetActive(false);
            vMark.SetActive(false);
            if (audioPlayer.isPlaying)
            {
                audioPlayer.Stop();
            }
            foreach(Coordinate coord in coords)
            {
                if (coord.getStage() <= stage)
                {
                    coord.setTimer();
                    if (coord.getTimer() == 0)
                    {
                        GameObject coordObject = coord.getType();
                        GameObject newObject = Instantiate(coordObject, new Vector3(0, 0, 0), Quaternion.identity);
                        newObject.transform.parent = canvas.gameObject.transform;
                        newObject.transform.localPosition = new Vector3(coord.getPosition(), gameLinePos, 0);
                        newObject.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                        if (stage == 5 && coord.getSpecial())
                        {
                            coord.resetTimer(true);
                        }
                        else
                        {
                            coord.resetTimer(false);
                        }
                    }
                }
            }
            if(score >= 5)
            {
                stage++;
                score = 0;
                misses = 0;
                actionLine.GetComponent<LineMove>().setGameRun(false); // this is how we get back into storyTime
                storyTime = true;
            }
        }
        else if(storyTime)
        {
            if (!audioPlayer.isPlaying || audioPlayer.clip == audioMenu)
            {
                audioPlayer.Stop();
                audioPlayer.loop = true;
                audioPlayer.clip = amusedMusic;
                audioPlayer.volume = 1f;
                audioPlayer.Play();
            }
            chapter.SetActive(true);
            chImage.SetActive(true);
            chText.SetActive(true);
            xMark.SetActive(true);
            logo.SetActive(false);
            actionLine.SetActive(false);
            // Story Time stuff
            if (stage == 0) //chapter 1
            {
                chImage.GetComponent<Image>().sprite = chapter1Image;
                chapter.GetComponent<Image>().sprite = chapter1Title;
                chImage.transform.localScale = new Vector3(1, 1, 1);
                // has 2 parts
                if (storyPage == 0)
                {
                    vMark.SetActive(true);
                    chText.GetComponent<Image>().sprite = chapter1chunk1;
                }
                else
                {
                    vMark.SetActive(false);
                    chText.GetComponent<Image>().sprite = chapter1chunk2;
                }
                
            }
            else if (stage == 1) //chapter 2
            {
                chImage.GetComponent<Image>().sprite = chapter2Image;
                chapter.GetComponent<Image>().sprite = chapter2Title;
                chText.GetComponent<Image>().sprite = chapter2chunk1;
                chImage.transform.localScale = new Vector3(1,.5f,1);
            }
            else if (stage == 2) // chapter 3
            {
                chImage.GetComponent<Image>().sprite = chapter3Image;
                chapter.GetComponent<Image>().sprite = chapter3Title;
                chImage.transform.localScale = new Vector3(1, 1, 1);
                // has 2 parts
                if (storyPage == 0)
                {
                    vMark.SetActive(true);
                    chText.GetComponent<Image>().sprite = chapter3chunk1;
                }
                else
                {
                    vMark.SetActive(false);
                    chText.GetComponent<Image>().sprite = chapter3chunk2;
                }
            }
            else if (stage == 3) // chapter 4
            {
                chImage.GetComponent<Image>().sprite = chapter4Image;
                chapter.GetComponent<Image>().sprite = chapter4Title;
                chText.GetComponent<Image>().sprite = chapter4chunk1;
                chImage.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (stage == 4) // chapter 5
            {
                chImage.GetComponent<Image>().sprite = chapter5Image;
                chapter.GetComponent<Image>().sprite = chapter5Title;
                chImage.transform.localScale = new Vector3(1, 1, 1);
                // has 2 parts
                if (storyPage == 0)
                {
                    vMark.SetActive(true);
                    chText.GetComponent<Image>().sprite = chapter5chunk1;
                }
                else
                {
                    vMark.SetActive(false);
                    chText.GetComponent<Image>().sprite = chapter5chunk2;
                }
            }
            else if (stage == 5) // chapter 6
            {
                stage6Timer--;
                if(stage6Timer <= 0)
                {
                    SceneManager.LoadScene(3); // win condition
                }
                chImage.GetComponent<Image>().sprite = chapter6Image;
                chapter.GetComponent<Image>().sprite = chapter6Title;
                chText.GetComponent<Image>().sprite = chapter6chunk1;
                chImage.transform.localScale = new Vector3(1, 1, 1);
                xMark.SetActive(false);
            }
        }
        else
        {
            actionLine.SetActive(false);
            chImage.SetActive(false);
            chText.SetActive(false);
            chapter.SetActive(false);
            chapter.SetActive(false);
            chImage.SetActive(false);
            chText.SetActive(false);
            xMark.SetActive(false);
            vMark.SetActive(false);
        }
	}

    public void playAudioOneShot(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,0), 1f);
    }

    public int getScore()
    {
        return score;
    }

    public int getStage()
    {
        return stage;
    }

    public bool getStoryTime()
    {
        return storyTime;
    }

    public int setScore()
    {
        score = score + 1;
        return score;
    }

    public void setMisses()
    {
        misses = misses + 1;
    }

    public void setStoryPage()
    {
        storyPage++;
    }

    public bool setStoryTime()
    {
        if (storyTime)
        {
            storyTime = false;
            return false;
        }
        else
        {
            storyTime = true;
            return true;
        }
    }
}

public class Coordinate {

    int rate;
    int timer;
    int position;
    int stage;
    GameObject type;
    bool special;

    public Coordinate(int rate, int timer, int position, GameObject type, int stage, bool special)
    {
        this.rate = rate;
        this.timer = timer;
        this.position = position;
        this.type = type;
        this.stage = stage;
        this.special = special;
    }

    public int getPosition()
    {
        return this.position;
    }

    public int getRate()
    {
        return this.rate;
    }

    public int getTimer()
    {
        return this.timer;
    }

    public int getStage()
    {
        return this.stage;
    }

    public bool getSpecial()
    {
        return this.special;
    }

    public GameObject getType()
    {
        return this.type;
    }

    public void setTimer()
    {
        this.timer--;
    }

    public void resetTimer(bool useSpecial)
    {
        if (useSpecial)
        {
            this.timer = (rate / 4) * 3;
        }
        else
        {
            this.timer = rate;
        }
    }

    public void setRate(int newRate)
    {
        this.rate = newRate;
    }
}