using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  public static PlantObject[] Plots { get; set; }
  public static int TotalPlotCount { get; set; }
  public static int TotalPlotCountLastRound { get; set; }
  public static int CurrentPlotIndex { get; set; }
  public static int LapsRunThroughLoop { get; set; }

  // TODO: use this!
  public static int TimeForLevel { get; set; }

}
