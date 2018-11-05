using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryMenu : MonoBehaviour
{
    public GameObject summaryMenuUI;

    public void ReplayGame()
    {
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoToMainMenu()
    {
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}