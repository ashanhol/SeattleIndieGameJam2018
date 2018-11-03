using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScript : MonoBehaviour {

  void OnMouseDown () {
    SceneManager.LoadScene ("LevelStartScene", LoadSceneMode.Additive);
  }

}
