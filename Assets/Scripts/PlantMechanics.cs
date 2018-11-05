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
  public static int LoopPlotIncreaseCount = 5;

  // How many actions do you have to do on a plant, before it grows to adulthood
  public static int ActionsRequiredToGrowToAdulthood = 3;

  // the maximium score you can have for a plant, as of it growing into an adult
  public static int MaximumPossibleScoreForGrownPlant = 0;

  // setup all the initial state required to generate a new level
  // this mostly includes generating empty lists, and setting attributes to zero
  public static void GenerateLevel () {
    GameState.TotalPlotCountLastRound = GameState.TotalPlotCount;
    GameState.TotalPlotCount = GameState.TotalPlotCount + LoopPlotIncreaseCount;
    GameState.HasActionBeenDoneThisLoop = Enumerable.Repeat (false, GameState.TotalPlotCount).ToList ();
    GameState.PlantScore = Enumerable.Repeat (0, GameState.TotalPlotCount).ToList ();
    GameState.PlantActions = new Dictionary<int, List<int>> ();
    for (int i = 0; i < GameState.TotalPlotCount; i++) {
      GameState.PlantActions.Add (i, new List<int> (new int[0]));
    }
    GameState.PlotsRemoved = 0;
    GameState.LapsRunThroughLoop = 0;
    GameState.onScreenPlot_ = new Queue<GameObject> ();
    GameState.onScreenPlotBackendIndex_ = new Queue<int> ();
    GameState.MaxLoops = 2;
    MaximumPossibleScoreForGrownPlant = GetMaximumPossibleScoreForGrownPlant();
  }

  // called whenever a tile leaves the scene (which is also when tiles are added)
  public static void TileAdvance () {
    ++GameState.PlotsRemoved;

    // check it this tile advance action corresponds to completing a loop
    int _plotsToAdvanceLoop = GameState.TotalPlotCount * (GameState.LapsRunThroughLoop + 1);
    if (GameState.PlotsRemoved >= _plotsToAdvanceLoop) {
      // reset all the "have you done actions this loop" values to false
      GameState.HasActionBeenDoneThisLoop = Enumerable.Repeat (false, GameState.TotalPlotCount).ToList ();
      ++GameState.LapsRunThroughLoop;
      if (GameState.LapsRunThroughLoop > GameState.MaxLoops) {
        //TODO: END GAME
        SceneManager.LoadScene (2);
      }

    }
  }

  // get the score of a plot for a given plot index
  public static int PlotScoreOnIndex(int backendPlotIndex) {
    return GameState.PlantScore[backendPlotIndex];
  }

  // should we spawn a baby plant for the given plot index
  public static bool ShouldSpawnBabyPlantOnIndex(int backendPlotIndex) {
    List<int> _plotActions = GameState.PlantActions[backendPlotIndex];
    if (_plotActions.Count != 0 && _plotActions.Count < ActionsRequiredToGrowToAdulthood) {
      return true;
    }
    return false;
  }

  // should we spawn an adult plant for the given plot index
  public static bool ShouldSpawnAdultPlantOnIndex(int backendPlotIndex) {
    List<int> _plotActions = GameState.PlantActions[backendPlotIndex];
    if (_plotActions.Count >= ActionsRequiredToGrowToAdulthood) {
      return true;
    }
    return false;
  }

  // adds score and an action entry to the "current plot"
  // the "current plot" is the plot the player is currently on
  public static int doPlayerActionOnIndex (int PlayerActionValue, int backendPlotIndex) {
    // if an action *has not* been done on the current index, during this loop
    if (!GameState.HasActionBeenDoneThisLoop[backendPlotIndex]) {
      // update actions
      List<int> _currentPlotActions = GameState.PlantActions[backendPlotIndex];
      _currentPlotActions.Add (PlayerActionValue);
      GameState.PlantActions[backendPlotIndex] = _currentPlotActions;
      // update score
      int actionScore = PlayerActionValue * (GameState.LapsRunThroughLoop + 1);
      GameState.PlantScore[backendPlotIndex] += actionScore;
      GameState.HasActionBeenDoneThisLoop[backendPlotIndex] = true;
      Debug.Log ("player has chosen player action for " + actionScore);
      return actionScore;
    // if an action *has* been done on the current index, during this loop
    } else {
      Debug.Log ("action already done for this plot");
      return 0;
    }
  }

  // the maximium score you can have for a plant, as of it growing into an adult
  private static int GetMaximumPossibleScoreForGrownPlant() {
    int _maxScore = 0;
    for (int i = 0; i < GameState.MaxLoops + 1; i++) {
      _maxScore += (int)PlayerAction.Seed * (i + 2);
    }
    return _maxScore;
  }

  // find the total score of all plots, plus the non plant related score 
  public static int TotalScore {
    get {
      int _totalScore = 0;
      foreach (int _score in GameState.PlantScore) {
        _totalScore += _score;
      }
      _totalScore += GameState.PlayerScore;
      return _totalScore;
    }
  }

  public static int CurrentIndex {
    get {
      return GameState.currentClosestPlotIndex;
    }
    set {
      GameState.currentClosestPlotIndex = value;
    }
  }

  // given a score, return a plant number
  public static int GetPlantNumForScore(int score, GameObject[] PlantList)
  {
    int maxScore = PlantingMechanics.MaximumPossibleScoreForGrownPlant;
    int currentPlantnum = 0;
    int maxPlantNumIndex = PlantList.Count() - 1;
    if (score > 0)
    {
        currentPlantnum = (int) Math.Floor( (double) maxPlantNumIndex * score / maxScore);
    }
    return currentPlantnum;
  }

  public static int NextBackendPlotIndex(int lastPlotIndex) {
    int _nextPlotIndex = lastPlotIndex + 1;
    if (_nextPlotIndex == GameState.TotalPlotCount) {
      _nextPlotIndex = 0;
    }
    if (_nextPlotIndex > GameState.TotalPlotCount) {
      Debug.Log("error in NextBackendPlotIndex logic");
    }
    return _nextPlotIndex;
  }

}
