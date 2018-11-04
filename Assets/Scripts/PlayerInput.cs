using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    PlayerAction pa = PlayerAction.Seed;
    public GameObject gameController;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private Text ScoreTextTopRight;

    // Use this for initialization
    void Start ()
    {
        ScoreTextTopRight = GameObject.Find("ScoreText").GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update ()
    {
        //Get Input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("was " + pa.ToString());
            pa = CycleThroughActions("Left", pa);
            Debug.Log("now " + pa.ToString());
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("was " + pa.ToString());
            pa = CycleThroughActions("Right", pa);
            Debug.Log("now " + pa.ToString());
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            //TODO: PAUSE MENU
            if(GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
        else if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            int _actionScore = PlantingMechanics.doPlayerAction((int)pa);
            int _totalScore = PlantingMechanics.TotalScore;
            ScoreTextTopRight.text = _totalScore.ToString();
            gameController = GameObject.FindGameObjectWithTag("GameController");
            gameController.GetComponent<ScrollScript> ().CheckPlantGrowth ();
        }
    }

    // Resume Game
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Pause Game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
    }





    PlayerAction CycleThroughActions(string cycleDirection, PlayerAction CurPa)
    {
        switch (CurPa)
        {
            case PlayerAction.Seed:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Fertilize;
                }
                else
                {
                    return PlayerAction.Water;
                }
            case PlayerAction.Water:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Seed;
                }
                else
                {
                    return PlayerAction.Fertilize;
                }
            case PlayerAction.Fertilize:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Water;
                }
                else
                {
                    return PlayerAction.Seed;
                }
            default:
                return PlayerAction.Fertilize;
        }
    }
}
