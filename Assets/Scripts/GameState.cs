using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  private static int totalScore = 4;
  private static int timeForLevel = 3;
  private static int currentLevelLoop = 0;

  private static GameStatePlot[] plots;
  private static int currentPlotIndex = 0;
  private static int totalPlotCount = 4;
  private static int totalPlotCountLastRound = 0;

  public static void GenerateLevel () {
    plots = new GameStatePlot[totalPlotCount];
    totalPlotCountLastRound = totalPlotCount;
    totalPlotCount = totalPlotCount + 4;
  }

  public static void AdvanceLoop () {
    currentPlotIndex = 0;
    ++currentLevelLoop;
  }

  public static void doAction () { }

  public static GameStatePlot[] Plots {
    get {
      return plots;
    }
  }

  public static int TotalScore {
    get {
      int _totalScore = 0;
      for (int i = 0; i < totalPlotCount; i++) {
        _totalScore += plots[currentPlotIndex].Score;
      }
      return _totalScore;
    }
  }

  public static int TimeForLevel {
    get {
      return timeForLevel;
    }
    set {
      timeForLevel = value;
    }
  }

  public static GameStatePlot CurrentPlot {
    get {
      return plots[currentPlotIndex];
    }
  }

  public static int CurrentPlotIndex {
    set {
      currentPlotIndex = value;
    }
  }

}

public class GameStatePlot {
  public int Score { get; set; }
}
