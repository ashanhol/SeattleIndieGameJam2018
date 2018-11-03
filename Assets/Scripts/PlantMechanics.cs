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

  public static void GenerateLevel () {
    GameState.Plots = new PlantObject[GameState.TotalPlotCount];
    GameState.TotalPlotCountLastRound = GameState.TotalPlotCount;
    GameState.TotalPlotCount = GameState.TotalPlotCount + 4;
    GameState.LapsRunThroughLoop = 0;
  }

  public static void AdvanceLoop () {
    GameState.CurrentPlotIndex = 0;
    ++GameState.LapsRunThroughLoop;
  }

  //Need function for matching score with plant
  public static void doPlayerAction (int PlayerActionValue) {
    CurrentPlot.Actions.Add(PlayerActionValue);
    CurrentPlot.Score += PlayerActionValue * GameState.LapsRunThroughLoop;
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