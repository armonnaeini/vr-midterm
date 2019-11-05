using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    // Global to hold object to be moved
    public GameObject MoveObj;
    // How much the object wiggles
    public float WiggleDistance = 0.01f;
    // Time that object moves again
    public float WiggleTime = 0.01f;
    // Holds the time to move object
    private double nextInterval;
    //sign switch
    static float sign;
    // Start is called before the first frame update
    void Start()
    {
        // Each interval is one sec
        nextInterval = Time.realtimeSinceStartup + WiggleTime;
        sign = WiggleDistance;
    }

    // Update is called once per frame
    void Update()
    {
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > nextInterval)
        {
            // update time to next interval
            nextInterval = timeNow + WiggleTime;
            sign *= -1.0f;

            // Reposition it to random but bounded location
            MoveObj.transform.position = new Vector3(
            MoveObj.transform.position.x + sign,
            MoveObj.transform.position.y,
            MoveObj.transform.position.z
            );
        }
            
    }
}
