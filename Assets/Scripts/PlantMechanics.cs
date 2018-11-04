using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum PlayerAction : int {
  Seed = 20,
  Water = 10,
  Fertilize = 5,
}

//Should only have one of these classes initalized at once
public class PlantingMechanics {
  public static int LoopPlotIncreaseCount = 4;

  public static void GenerateLevel () {
    GameState.TotalPlotCountLastRound = GameState.TotalPlotCount;
    GameState.TotalPlotCount = GameState.TotalPlotCount + LoopPlotIncreaseCount;
    GameState.PlantScore = new List<int>(new int[GameState.TotalPlotCount]);
    GameState.PlotsRemoved = 0;
    GameState.LapsRunThroughLoop = 0;
    GameState.onScreenPlot_ = new Queue<GameObject> ();
  }

  public static void AdvanceLoop () {
    ++GameState.LapsRunThroughLoop;
  }

  public static int doPlayerAction (int PlayerActionValue) {
    int actionScore = PlayerActionValue * GameState.LapsRunThroughLoop;
    GameState.PlantScore[CurrentPlotIndex] += actionScore;
    return actionScore;
  }

  public static int TotalScore {
    get {
      int _totalScore = 0;
      foreach (var _score in GameState.PlantScore) {
        _totalScore += _score;
      }
      return _totalScore;
    }
  }

  public static int CurrentPlotIndex {
    get {
      int _plotMidpointOffset = (int)Math.Floor((double) GameState.TotalPlotCount / 2);
      int _midpointPlusRemoved = GameState.PlotsRemoved + _plotMidpointOffset;
      int _currentPlotIndex = _midpointPlusRemoved % GameState.TotalPlotCount;
      return _currentPlotIndex;
    }
  }

}
