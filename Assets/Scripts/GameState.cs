using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  public static List<int> PlantScore { get; set; }
  public static List<List<int>> PlantActions { get; set; }
  public static int TotalPlotCount { get; set; }
  public static int TotalPlotCountLastRound { get; set; }
  public static int PlotsRemoved { get; set; }
  public static int PlotsRemovedAtLastLap { get; set; }
  public static int LapsRunThroughLoop { get; set; }
  public static Queue<GameObject> onScreenPlot_ { get; set; }

  //Maximum number of loops this game is gonna run through
  public static int MaxLoops { get; set; }
   
}
