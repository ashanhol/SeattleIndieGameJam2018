using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Tires : MonoBehaviour {

    public float rotation_speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Rotate(0,0, rotation_speed);	
	}
}
