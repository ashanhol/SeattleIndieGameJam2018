using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    public GameObject plotPrefab;
    public GameObject babyPlantPrefab;
    public int ScrollSpeed;// = 15;

    public GameObject[] PlantList;

    void Start ()
    {
        PlantingMechanics.GenerateLevel();
        //Populate queue with starting plots
        for (int i = 0; i < GameState.TotalPlotCount; i++) {
            //Spawn in default area if first object
            if (i == 0)
            {
                GameObject _thisPlot = Instantiate(plotPrefab);
                AddNewPlot(_thisPlot);
            }
            else
            {
                SpawnNextPlot();
            }
        }
    }

    void FixedUpdate () {
        MoveOncomingPlotLeft ();
    }

    //Move all plots in onScreenPlot_
    void MoveOncomingPlotLeft () {
        //Foreach gameobject in queue, move left
        foreach (var plot in GameState.onScreenPlot_) {
            if (plot) {
                plot.transform.Translate (Vector3.left * Time.deltaTime * ScrollSpeed);
            }
        }
    }

    //Function to spawn the next plot in the queue offscreen
    void SpawnNextPlot()
    {
        //Spawn where the last object ends if not first object

        GameObject _lastPlot = LastAddedPlot;
        PlotData _lastPlotData = _lastPlot.GetComponentInChildren(typeof(PlotData)) as PlotData;
        int _lastPlotIndex = _lastPlotData.BackendPlotIndex;

        float nextXPos = _lastPlot.transform.position.x +
            plotPrefab.GetComponentInChildren<Renderer>().bounds.extents.x * 2;
        Vector3 nextPos = new Vector3(
            nextXPos,
            _lastPlot.transform.position.y,
            _lastPlot.transform.position.z);

        //Add next plot to onScreenPlot_
        GameObject _thisPlot = Instantiate(plotPrefab, nextPos, Quaternion.identity);
        AddNewPlot(_thisPlot, _lastPlotIndex);
        CheckPlantRerendering();
    }

    //Function to remove first element from queue since it's offscreen
    //Should be called when plot has hit offscreen boundry
    public void RemoveOffscreenPlot (GameObject toBeRemoved) {
        Destroy (toBeRemoved);
        SpawnNextPlot ();
        PlantingMechanics.TileAdvance ();
        GameState.onScreenPlot_.Dequeue ();
        GameState.onScreenPlotBackendIndex_.Dequeue ();
    }

    public int doPlayerActionOnCurrentPlot (int PlayerActionValue) {
        PlotData plotData = CurrentPlot.GetComponentInChildren(typeof(PlotData)) as PlotData;
        int BackendPlotIndex = plotData.BackendPlotIndex;
        return PlantingMechanics.doPlayerActionOnIndex(PlayerActionValue, BackendPlotIndex);
    }

    // check if a plant should be spawned on "the current plot"
    public void CheckPlantGrowth () {
        _CheckIfPlantShouldBeSpawned(CurrentPlot);
    }

    // check if a plant should be spawned on "a newly rendered plot"
    void CheckPlantRerendering () {
        _CheckIfPlantShouldBeSpawned(LastAddedPlot);
    }

    // check if a plant should be spawned on "a given plot"
    private void _CheckIfPlantShouldBeSpawned(GameObject plot) {
        PlotData plotData = plot.GetComponentInChildren(typeof(PlotData)) as PlotData;
        int BackendPlotIndex = plotData.BackendPlotIndex;
        Transform plantLocation = plot.transform.GetChild(0);
        if (PlantingMechanics.ShouldSpawnBabyPlantOnIndex(BackendPlotIndex)) {
            GameObject plantToSpawn = babyPlantPrefab;
            Instantiate(plantToSpawn, plantLocation);
        }
        if (PlantingMechanics.ShouldSpawnAdultPlantOnIndex(BackendPlotIndex)) {
            int _score = PlantingMechanics.PlotScoreOnIndex(BackendPlotIndex);
            int currentPlantnum = PlantingMechanics.GetPlantNumForScore(_score, PlantList);
            GameObject plantToSpawn = PlantList[currentPlantnum];
            Instantiate(plantToSpawn, plantLocation);
        }
    }

    // adds a new plot and plot index to the backend data
    public void AddNewPlot (GameObject thisPlot, int lastPlotIndex = -1) {
        PlotData thisPlotData = thisPlot.AddComponent(typeof(PlotData)) as PlotData;
        int _thisBackendPlotIndex = PlantingMechanics.NextBackendPlotIndex(lastPlotIndex);
        thisPlotData.BackendPlotIndex = _thisBackendPlotIndex;
        GameState.onScreenPlot_.Enqueue(thisPlot);
        GameState.onScreenPlotBackendIndex_.Enqueue(_thisBackendPlotIndex);
    }

    public void SetGivenPlotAsCurrentPlot (GameObject plot){
        PlotData plotData = plot.GetComponentInChildren(typeof(PlotData)) as PlotData;
        int backendPlotIndex = plotData.BackendPlotIndex;
        PlantingMechanics.CurrentIndex = backendPlotIndex;
    }

    // find the plot the was added most recently
    static GameObject LastAddedPlot {
        get {
            GameObject[] _queueArray = GameState.onScreenPlot_.ToArray();
            GameObject _lastAddedPlot = _queueArray[GameState.onScreenPlot_.Count - 1];
            return _lastAddedPlot;
        }
    }

    // find the plot that is closest to the player
    public GameObject CurrentPlot {
        get {
            // look for a plot that matches the current plot index
            int currentClosestIndex = PlantingMechanics.CurrentIndex;
            foreach (var plot in GameState.onScreenPlot_) {
                PlotData plotData = plot.GetComponentInChildren(typeof(PlotData)) as PlotData;
                int thisBackendPlotIndex = plotData.BackendPlotIndex;
                if (thisBackendPlotIndex == currentClosestIndex) {
                    return plot;
                }
            }
            // otherwise get the first plot (we should never be hitting this case)
            return GameState.onScreenPlot_.ToArray()[0];
        }
    }

}
