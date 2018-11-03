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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "Boundary")
        {
            gameController.GetComponent<ScrollScript>().RemoveOffscreenPlot(gameObject);
        }
            
    }
}
