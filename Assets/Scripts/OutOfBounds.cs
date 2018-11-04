using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    public GameObject gameController;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Boundary")
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
            if (gameObject.tag == "Plot")
            {
                gameController.GetComponent<ScrollScript>().RemoveOffscreenPlot(gameObject);
            }
            else if (gameObject.tag == "Cloud")
            {
                Debug.Log("CloudExit");
                gameController.GetComponent<CloudScript>().RemoveOffscreenCloud(gameObject);
            }
        }

    }
}
