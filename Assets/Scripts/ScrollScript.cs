﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    public GameObject plotPrefab;
    public int ScrollSpeed = 15;

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

    }

    //Function to remove first element from queue since it's offscreen
    //Should be called when plot has hit offscreen boundry
    public void RemoveOffscreenPlot (GameObject toBeRemoved) {
        Destroy (toBeRemoved);
        SpawnNextPlot();
        PlantingMechanics.TileAdvance();
    }

}
