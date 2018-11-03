using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction : int {
  Seed = 20,
  Water = 10,
  Fertilize = 5,
}

//Should only have one of these classes initalized at once
public class PlantingMechanics {

  public static PlantObject[] GenerateLevel () {
    GameState.Plots = new PlantObject[GameState.TotalPlotCount];
    GameState.TotalPlotCountLastRound = GameState.TotalPlotCount;
    GameState.TotalPlotCount = GameState.TotalPlotCount + 4;
    GameState.LapsRunThroughLoop = 0;
    return GameState.Plots;
  }

  public static PlantObject[] AdvanceLoop () {
    GameState.CurrentPlotIndex = 0;
    ++GameState.LapsRunThroughLoop;
    return GameState.Plots;
  }

  //Need function for matching score with plant
  public static int doPlayerAction (int PlayerActionValue) {
    CurrentPlot.Actions.Add(PlayerActionValue);
    int actionScore = PlayerActionValue * GameState.LapsRunThroughLoop;
    CurrentPlot.Score += actionScore;
    return actionScore;
  }

  public static int TotalScore {
    get {
      int _totalScore = 0;
      for (int i = 0; i < GameState.TotalPlotCount; i++) {
        _totalScore += CurrentPlot.Score;
      }
      return _totalScore;
    }
  }

  public static PlantObject CurrentPlot {
    get {
      return GameState.Plots[GameState.CurrentPlotIndex];
    }
  }

}

public class PlantObject {
  //Score
  public int Score { get; set; }
  //For keeping track of Actions taken on which turn
  public List<int> Actions { get; set; }
}
