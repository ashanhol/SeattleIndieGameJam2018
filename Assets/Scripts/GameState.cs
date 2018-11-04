using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  public static List<int> PlantScore { get; set; }
  public static Dictionary<int, List<int>> PlantActions { get; set; }
  public static int TotalPlotCount { get; set; }
  public static int TotalPlotCountLastRound { get; set; }
  public static int PlotsRemoved { get; set; }
  public static int LapsRunThroughLoop { get; set; }

  // a reference for the plot visual element
  public static Queue<GameObject> onScreenPlot_ { get; set; }
  // a mapping for pointing the plot visual element to the backend data
  public static Queue<int> onScreenPlotBackendIndex_ { get; set; }

  //Maximum number of loops this game is gonna run through
  public static int MaxLoops { get; set; }

  //Has the player done a plot (at this index) for this loop?
  public static List<bool> HasActionBeenDoneThisLoop { get; set; }

}
