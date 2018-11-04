using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerAction : int {
  Seed = 20,
  Water = 10,
  Fertilize = 5,
}

//Should only have one of these classes initalized at once
public class PlantingMechanics {
  //How many plots we're increasing per level
  public static int LoopPlotIncreaseCount = 4;

  public static void GenerateLevel () {
    GameState.TotalPlotCountLastRound = GameState.TotalPlotCount;
    GameState.TotalPlotCount = GameState.TotalPlotCount + LoopPlotIncreaseCount;
    GameState.HasActionBeenDoneThisLoop = Enumerable.Repeat (false, GameState.TotalPlotCount).ToList ();
    GameState.PlantScore = Enumerable.Repeat (0, GameState.TotalPlotCount).ToList ();
    GameState.PlantActions = new Dictionary<int, List<int>> ();
    for (int i = 0; i < GameState.TotalPlotCount + 1; i++) {
      GameState.PlantActions.Add (i, new List<int> (new int[0]));
    }
    GameState.PlotsRemoved = 0;
    GameState.LapsRunThroughLoop = 0;
    GameState.PlotsRemovedAtLastLap = 0;
    GameState.onScreenPlot_ = new Queue<GameObject> ();
    GameState.MaxLoops = 3;
  }

  public static void TileAdvance () {
    ++GameState.PlotsRemoved;

    int _plotsToAdvanceLoop = GameState.TotalPlotCount * (GameState.LapsRunThroughLoop + 1);
    if (GameState.PlotsRemoved >= _plotsToAdvanceLoop) {
      GameState.HasActionBeenDoneThisLoop = Enumerable.Repeat (false, GameState.TotalPlotCount).ToList ();
      GameState.PlotsRemovedAtLastLap = GameState.PlotsRemoved;
      ++GameState.LapsRunThroughLoop;
      if (GameState.LapsRunThroughLoop > GameState.MaxLoops) {
        //TODO: END GAME
        SceneManager.LoadScene (2);
      }

    }
  }

  public static bool ShouldSpawnBabyPlantOnJustAddedIndex() {
    int _justAddedPlotIndex = JustAddedPlotIndex;
    List<int> _justAddedPlotActions = GameState.PlantActions[_justAddedPlotIndex];
    if (_justAddedPlotActions.Count == 1) {
      return true;
    }
    return false;
  }

  public static bool ShouldSpawnAdultPlantOnJustAddedIndex() {
    int _justAddedPlotIndex = JustAddedPlotIndex;
    List<int> _justAddedPlotActions = GameState.PlantActions[_justAddedPlotIndex];
    if (_justAddedPlotActions.Count > 1) {
      return true;
    }
    return false;
  }

  public static bool ShouldSpawnBabyPlantOnLastIndex () {
    int _lastPlotIndex = LastPlotIndex;
    List<int> _lastPlotActions = GameState.PlantActions[_lastPlotIndex];
    if (_lastPlotActions.Count == 1) {
      return true;
    }
    return false;
  }

  public static bool ShouldSpawnAdultPlantOnLastIndex () {
    int _lastPlotIndex = LastPlotIndex;
    List<int> _lastPlotActions = GameState.PlantActions[_lastPlotIndex];
    if (_lastPlotActions.Count > 1) {
      return true;
    }
    return false;
  }

  public static int doPlayerAction (int PlayerActionValue) {
    int _currentPlotIndex = CurrentPlotIndex;
    if (!GameState.HasActionBeenDoneThisLoop[_currentPlotIndex]) {
      int actionScore = PlayerActionValue * (GameState.LapsRunThroughLoop + 1);
      List<int> _currentPlotActions = GameState.PlantActions[_currentPlotIndex];
      _currentPlotActions.Add (PlayerActionValue);
      GameState.PlantActions[_currentPlotIndex] = _currentPlotActions;
      GameState.PlantScore[_currentPlotIndex] += actionScore;
      GameState.HasActionBeenDoneThisLoop[_currentPlotIndex] = true;
      Debug.Log ("player has chosen player action for " + actionScore);
      return actionScore;
    } else {
      Debug.Log ("action already done for this plot");
      return 0;
    }
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

  public static int JustAddedPlotIndex {
    get {
      int _maxPlusRemoved = GameState.PlotsRemoved + GameState.TotalPlotCount;
      int _justAddedPlotIndex = (_maxPlusRemoved - 1) % GameState.TotalPlotCount;
      return _justAddedPlotIndex;
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
      int _plotMidpointOffset = (int) Math.Floor ((double) GameState.TotalPlotCount / 2);
      int _midpointPlusRemoved = GameState.PlotsRemoved + _plotMidpointOffset;
      int _currentPlotIndex = _midpointPlusRemoved % GameState.TotalPlotCount;
      return _currentPlotIndex;
    }
  }

}
