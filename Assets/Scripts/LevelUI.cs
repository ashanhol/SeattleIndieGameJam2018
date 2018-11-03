using UnityEngine;

public class LevelUI : MonoBehaviour {

  private GameObject summaryPage;
  private int timeForLevel = 10;

  void Awake () {
    summaryPage = GameObject.Find ("SummaryPage");
    this.hideSummaryPage ();
  }

  void Start () {
    Invoke ("showSummaryPage", this.timeForLevel);
  }

  public void showSummaryPage () {
    summaryPage.SetActive (true);
  }

  public void hideSummaryPage () {
    summaryPage.SetActive (false);
  }

}
