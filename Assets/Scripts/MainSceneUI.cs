using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {

  private GameObject scoreDisplay;
  private GameObject minimap;

  void Awake () {
    scoreDisplay = GameObject.Find ("ScoreDisplayGameObject");
    minimap = GameObject.Find ("MinimapGameObject");
  }

  void Start () {
    Invoke ("showSummaryPage", GameState.TimeForLevel);
  }

  public void showLevelSummaryScene () {
    SceneManager.LoadScene ("LevelSummary", LoadSceneMode.Additive);
  }

}
