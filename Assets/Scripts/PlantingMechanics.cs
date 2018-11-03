using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should only have one of these classes initalized at once
public class PlantingMechanics
{
    private static int numTotalPlots_ = 4;

    //Data structure for keeping track of farm plots
    public static PlantObject[] plots = new PlantObject[numTotalPlots_];


    //TODO: Need function for matching score with plant



}

public class PlantObject
{
    //Score 
    int Score { get; set; }

    //TODO: Function for keeping track of Actions taken on which turn




}