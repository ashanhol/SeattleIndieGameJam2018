using UnityEngine;

//Should only have one of these classes initalized at once
public class GameState {
  public static int totalScore = 4;
  public static int timeForLevel = 3;

  public static int TotalScore {
    get {
      return totalScore;
    }
    set {
      totalScore = value;
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

}
