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
    GameState.PlantActions = new Dictionary<int, List<int>>();
    for (int i = 0; i < GameState.TotalPlotCount + 1; i++) {
      GameState.PlantActions.Add(i, new List<int>(new int[0]));
    }
    GameState.PlotsRemoved = 0;
    GameState.LapsRunThroughLoop = 0;
    GameState.PlotsRemovedAtLastLap = 0;
    GameState.onScreenPlot_ = new Queue<GameObject> ();
  }

  public static void TileAdvance() {
    ++GameState.PlotsRemoved;

    int _plotsToAdvanceLoop = GameState.TotalPlotCount * (GameState.LapsRunThroughLoop + 1);
    if (GameState.PlotsRemoved >= _plotsToAdvanceLoop) {
      ++GameState.LapsRunThroughLoop;
      GameState.PlotsRemovedAtLastLap = GameState.PlotsRemoved;
    }
  }

  public static bool ShouldSpawnBabyPlant() {
    int _lastPlotIndex = LastPlotIndex;
    List<int> _lastPlotActions = GameState.PlantActions[_lastPlotIndex];
    if (_lastPlotActions.Count == 1) {
      return true;
    }
    return false;
  }

  public static bool ShouldSpawnAdultPlant() {
    int _lastPlotIndex = LastPlotIndex;
    List<int> _lastPlotActions = GameState.PlantActions[_lastPlotIndex];
    if (_lastPlotActions.Count > 1) {
      return true;
    }
    return false;
  }

  public static int doPlayerAction (int PlayerActionValue) {
    int actionScore = PlayerActionValue * (GameState.LapsRunThroughLoop + 1);
    GameState.PlantScore[CurrentPlotIndex] += actionScore;
    Debug.Log("player has chosen player action for " + actionScore);
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

  public static int LastPlotScore {
    get {
      return GameState.PlantScore[LastPlotIndex];
    }
  }

  public static int LastPlotIndex {
    get {
      int _lastPlotIndex = CurrentPlotIndex - 1;
      if (_lastPlotIndex == -1) {
        _lastPlotIndex = GameState.TotalPlotCount;
      }
      return _lastPlotIndex;
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
