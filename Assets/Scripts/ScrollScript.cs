using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    public GameObject plotPrefab;
    public GameObject babyPlantPrefab;
    public int ScrollSpeed = 15;

    public GameObject[] PlantList;

    //last object queued to calculate where next should spawn
    GameObject temp = null;

    void Start ()
    {
        PlantingMechanics.GenerateLevel();
        //Populate queue with starting plots
        for (int i = 0; i < GameState.TotalPlotCount + 1; i++) {
            //Spawn in default area if first object
            if (i == 0)
            {
                GameState.onScreenPlot_.Enqueue(temp = Instantiate(plotPrefab));
            }
            else
            {
                SpawnNextPlot();
            }
        }

        //PlantObject p = PlantingMechanics.plots[0];
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
        Vector3 nextPos = new Vector3((temp.transform.position.x + plotPrefab.GetComponentInChildren<Renderer>().bounds.extents.x * 2), temp.transform.position.y, temp.transform.position.z);

        //Add next plot to onScreenPlot_
        GameState.onScreenPlot_.Enqueue(temp = Instantiate(plotPrefab, nextPos, Quaternion.identity));

        if (PlantingMechanics.ShouldSpawnBabyPlantOnJustAddedIndex()) {
            // TODO Adina spawn a plant
            Instantiate(babyPlantPrefab, temp.transform.GetChild(0));
        }
        if (PlantingMechanics.ShouldSpawnAdultPlantOnJustAddedIndex()) {
            //Figure out what plant to spawn
            //Instantiate(PlantList[plantnum], LastPlot.transform.GetChild(0));
        }
    }

    //Function to remove first element from queue since it's offscreen
    //Should be called when plot has hit offscreen boundry
    public void RemoveOffscreenPlot (GameObject toBeRemoved) {
        Destroy (toBeRemoved);
        SpawnNextPlot();
        PlantingMechanics.TileAdvance();
        GameState.onScreenPlot_.Dequeue();
        if (PlantingMechanics.ShouldSpawnBabyPlantOnLastIndex()) {
            // TODO Adina spawn a plant
            Instantiate(babyPlantPrefab, LastPlot.transform.GetChild(0));
        }
        if (PlantingMechanics.ShouldSpawnAdultPlantOnLastIndex()) {
            //Figure out what plant to spawn


            // TODO Adina spawn a plant
            //Instantiate(PlantList[plantnum], LastPlot.transform.GetChild(0));
        }
    }

    public static GameObject LastPlot {
        get {
            int _previousPlotIndex = (int)Math.Floor((double) GameState.onScreenPlot_.Count / 2) - 1;
            GameObject previousPlot = GameState.onScreenPlot_.ElementAt(_previousPlotIndex);
            return previousPlot;
        }
    }

}
