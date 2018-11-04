using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    PlayerAction pa = PlayerAction.Seed;
    public GameObject gameController;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Use this for initialization
    void Start ()
    {
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
            Text scoreTextTopRight = GameObject.Find("ScoreText").GetComponentInChildren<Text>();
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            ScrollScript scrollScript = gameController.GetComponent<ScrollScript> ();
            int _actionScore = scrollScript.doPlayerActionOnCurrentPlot((int)pa);
            int _totalScore = PlantingMechanics.TotalScore;
            scoreTextTopRight.text = _totalScore.ToString();
            scrollScript.CheckPlantGrowth ();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


    PlayerAction CycleThroughActions(string cycleDirection, PlayerAction CurPa)
    {
        GameState.PlayerScore += 10;
        //TODO: VISUAL UI OF +10 SCORE ABOVE FARMER

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
