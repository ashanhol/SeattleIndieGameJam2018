using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    public GameObject plotPrefab;
    public int NumPlotsOnscreen;

    private Queue<GameObject> onScreenPlot_;

    // Use this for initialization
    void Start () {
        //No more than NumPlotsOnScreen at once, plus the one going on/off screen
        onScreenPlot_ = new Queue<GameObject> (NumPlotsOnscreen + 1);
        GameState.GenerateLevel ();
        GameStatePlot[] plots = GameState.Plots;
    }

    // Update is called once per frame
    void Update () {

    }

    //Move all plots in onScreenPlot_
    void MoveOncomingPlotLeft () {

    }

    //Function to spawn the next plot in the queue offscreen
    void SpawnNextPlot () {
        //Add next plot to onScreenPlot_

    }

    //Function to remove first element from queue since it's offscreen
    void RemoveOffscreenPlot (GameObject toBeRemoved) {

    }

}
