using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public GameObject puff;
    public int NumCloudsOnscreen;
    public int ScrollSpeed = 15;

    private Queue<GameObject> onScreenCloud_;

    //last object queued to calculate where next should spawn
    GameObject temp = null;

    void Start()
    {
        // puff = Instantiate(puff);

    }

    void FixedUpdate()
    {
        // MoveOncomingCloudLeft();
    }

    //Move all clouds in onScreenCloud_
    void MoveOncomingCloudLeft()
    {

            // if (puff)
            // {
            //     puff.transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed);
            // }

    }

    //Function to spawn the next plot in the queue offscreen
    void SpawnNextCloud()
    {
        //Spawn where the last object ends if not first object
        // Vector3 nextPos = new Vector3((temp.transform.position.x + puff.GetComponentInChildren<Renderer>().bounds.extents.x * 2), temp.transform.position.y, temp.transform.position.z);

        // temp = Instantiate(puff, nextPos, Quaternion.identity);

        //Add next plot to onScreenPlot_
        // onScreenCloud_.Enqueue(temp = Instantiate(puff, nextPos, Quaternion.identity));

    }

    //Function to remove first element from queue since it's offscreen
    //Should be called when cloud has hit offscreen boundry
    public void RemoveOffscreenCloud(GameObject toBeRemoved)
    {
        // Destroy(toBeRemoved);
        // SpawnNextCloud();
        //onScreenCloud_.Dequeue();
    }

}
