using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerAction pa = PlayerAction.Seed;



    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Get Input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("was " + pa.ToString());
            pa = CycleThroughActions("Left", pa);
            Debug.Log("now " + pa.ToString());
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("was " + pa.ToString());
            pa = CycleThroughActions("Right", pa);
            Debug.Log("now " + pa.ToString());
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO: PAUSE MENU
        }
    }

    PlayerAction CycleThroughActions(string cycleDirection, PlayerAction CurPa)
    {
        switch (CurPa)
        {
            case PlayerAction.Seed:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Fertilize;
                }
                else
                {
                    return PlayerAction.Water;
                }
            case PlayerAction.Water:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Seed;
                }
                else
                {
                    return PlayerAction.Fertilize;
                }
            case PlayerAction.Fertilize:
                if (cycleDirection == "Left")
                {
                    return PlayerAction.Water;
                }
                else
                {
                    return PlayerAction.Seed;
                }
            default:
                return PlayerAction.Fertilize;
        }
    }
}
