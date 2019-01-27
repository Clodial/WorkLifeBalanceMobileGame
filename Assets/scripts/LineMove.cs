using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour {

    public Transform actionLine;
    public float speed;
    private float trueSpeed;
    private bool goUp;
    private bool gameRun;
    private bool gameStart;
    private int gameState;

	// Use this for initialization
	void Start () {
        float updatedSpeed = speed * 1f;
        goUp = true;
        trueSpeed = updatedSpeed /60;
        gameRun = false;
        gameStart = false;
        gameState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameRun)
        {
            actionLine.Translate(0f, trueSpeed, 0f);
            CheckLocal(goUp, trueSpeed, 250f / 60);
        }
        if (gameStart)
        {
            actionLine.SetPositionAndRotation(new Vector3(0, 0, 300),new Quaternion());
            gameRun = false;
            gameState = 0;
            GameObject[] tags = GameObject.FindGameObjectsWithTag("tapZone");
            for (int i = 0; i < tags.Length; i++)
            {
                Destroy(tags[i]);
            }
            gameStart = false;
        }
	}

    void CheckLocal(bool direction, float curSpeed, float bounds)
    {
        float updatedSpeed = speed * 1f;
        if (direction)
        {
            if (actionLine.position.y < bounds)
            {
                this.trueSpeed = updatedSpeed / 60;
                this.goUp = direction;
            }
            else
            {
                this.trueSpeed = -updatedSpeed / 60;
                this.goUp = false;
            }
        }
        else
        {
            if (actionLine.position.y > -bounds)
            {
                this.trueSpeed = -updatedSpeed / 60;
                this.goUp = false;
            }
            else
            {
                this.trueSpeed = updatedSpeed / 60;
                this.goUp = true;
            }
        }
    }

    public void setGameRun(bool state)
    {
        gameRun = state;
        if (!gameRun)
        {
            gameStart = true;
        }
        else
        {
            gameStart = false;
            actionLine.SetPositionAndRotation(new Vector3(0, 0, 300), new Quaternion());
        }
    }

    public bool getGameRun()
    {
        return this.gameRun;
    }

    public int getGameState()
    {
        return this.gameState;
    }
}
