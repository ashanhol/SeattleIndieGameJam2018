using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartScript : MonoBehaviour {

  void OnMouseDown () {
    SceneManager.LoadScene ("LevelScene", LoadSceneMode.Additive);
  }

}
