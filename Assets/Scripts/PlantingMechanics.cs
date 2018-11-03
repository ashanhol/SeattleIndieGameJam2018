using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should only have one of these classes initalized at once
public class PlantingMechanics : MonoBehaviour
{
    //Data structure for keeping track of farm plots
    public static PlantObject[] plots = new PlantObject[4];


    //TODO: Need function for matching score with plant

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

public class PlantObject
{
    //Score 
    int Score { get; set; }

    //TODO: Function for keeping track of Actions taken on which turn



    
}