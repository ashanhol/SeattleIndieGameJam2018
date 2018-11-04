﻿using System.Collections;
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
        for (int i = 0; i < GameState.TotalPlotCount; i++) {
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

        CheckPlantRerendering();
    }

    //Function to remove first element from queue since it's offscreen
    //Should be called when plot has hit offscreen boundry
    public void RemoveOffscreenPlot (GameObject toBeRemoved) {
        Destroy (toBeRemoved);
        SpawnNextPlot ();
        PlantingMechanics.TileAdvance ();
        GameState.onScreenPlot_.Dequeue ();
    }

    // check if a plant should be grown on the current plot
    public void CheckPlantGrowth () {
        if (PlantingMechanics.ShouldSpawnBabyPlantOnCurrentIndex ()) {
            Instantiate (babyPlantPrefab, CurrentPlot.transform.GetChild (0));
        }
        if (PlantingMechanics.ShouldSpawnAdultPlantOnCurrentIndex ()) {
            int score = PlantingMechanics.LastPlotScore;
            if (score <= 0)
            {

            }
            else
            {

            }
            //Figure out what plant to spawn
            // Instantiate(PlantList[plantnum], CurrentPlot.transform.GetChild(0));
        }
    }

    // check if a plant should be re-rendered (because it was grown last loop) on a plot
    void CheckPlantRerendering () {
        if (PlantingMechanics.ShouldSpawnBabyPlantOnJustAddedIndex ()) {
            Instantiate(babyPlantPrefab, temp.transform.GetChild(0));
        }
        if (PlantingMechanics.ShouldSpawnAdultPlantOnJustAddedIndex ()) {
            int score = PlantingMechanics.LastPlotScore;
            if (score <= 0)
            {

            }
            else
            {

            }
            //Figure out what plant to spawn
            // Instantiate(PlantList[plantnum], temp.transform.GetChild(0));
        }
    }

    static GameObject CurrentPlot {
        get {
            int _currentPlotIndex = (int) Math.Floor ((double) GameState.onScreenPlot_.Count / 2);
            GameObject _currentPlot = GameState.onScreenPlot_.ElementAt (_currentPlotIndex);
            return _currentPlot;
        }
    }

}
