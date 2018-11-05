using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryScreen : MonoBehaviour {

    public GameObject[] PlantList;

	// Use this for initialization
	void Start () {
        Debug.Log("GOT TO END");


        foreach(KeyValuePair<GameObject, int> kvp in CalculatePlots())
        {
            Debug.Log("IN DICT");
            Debug.Log("Plant " + kvp.Key + " was grown " + kvp.Value + "times.");
           //kvp.Value
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //Calculate plants based on each plots score

    Dictionary<GameObject, int> CalculatePlots()
    {
        //Keep map of each plant and how many times we grew em. Dict<Plant, num> 
        //Use PlantMechanics.GetPlantNumForScore to reference index in PlantList
        Dictionary<GameObject, int> PlantTotal = new Dictionary<GameObject, int>();

        foreach(var plant in PlantList)
        {
            PlantTotal.Add(plant, 0);
        }

        foreach(var score in GameState.PlantScore)
        {
            Debug.Log("PlantScore: " + GameState.PlantScore);
            PlantTotal[PlantList[PlantingMechanics.GetPlantNumForScore(score, PlantList)]]++;
        }

        return PlantTotal;
    }

}
