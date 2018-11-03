using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {

  private static int totalScore = 4;
  private static int timeForLevel = 3;

  private static GameStatePlot[] plots;
  private static int plotCurrentIndex = 0;
  private static int plotTotalCount = 4;

  public static void GeneratePlots () {
    plots = new GameStatePlot[plotTotalCount];
  }

  public static int TotalScore {
    get {
      int _totalScore = 0;
      for (int i = 0; i < plotTotalCount; i++) {
        _totalScore += plots[plotCurrentIndex].Score;
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

  public static GameStatePlot PlotCurrent {
    get {
      return plots[plotCurrentIndex];
    }
  }

  public static int PlotCurrentIndex {
    set {
      plotCurrentIndex = value;
    }
  }

}

public class GameStatePlot {
  public int Score { get; set; }
}
