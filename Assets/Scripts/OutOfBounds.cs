using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    public GameObject gameController;
    private ScrollScript scrollScript;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        scrollScript = gameController.GetComponent<ScrollScript> ();
    }

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PlayerCollide");
            Debug.Log(gameObject.GetComponentInChildren<Light>().enabled = true);
            scrollScript.SetGivenPlotAsCurrentPlot(gameObject);
        }
    }


    private void OnTriggerExit (Collider other) {

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Exit PlayerCollide");

            if (gameController.GetComponent<ScrollScript>().CurrentPlot != gameObject)
            {
                Debug.Log(gameObject.GetComponentInChildren<Light>().enabled = false);
            }
        }


        else if (other.gameObject.tag == "Boundary")
        {
            // TODO: re-add removing the clouds from the scene
            //  and also make sure that you're removing both the cloud
            //  amd the plot (possibly theyre in an array?)
            gameController.GetComponent<ScrollScript>().RemoveOffscreenPlot(gameObject);
        }

    }
}
