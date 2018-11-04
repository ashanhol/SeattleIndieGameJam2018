using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  public static PlantObject[] Plots { get; set; }
  public static int TotalPlotCount { get; set; }
  public static int TotalPlotCountLastRound { get; set; }
  public static int CurrentPlotIndex { get; set; }
  public static int LapsRunThroughLoop { get; set; }
  public static Queue<GameObject> onScreenPlot_ { get; set; }

  // TODO: use this!
  public static int TimeForLevel { get; set; }

}
