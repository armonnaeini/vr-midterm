﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformCorrection : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cameraProxy;
    public float lerpSpeed; // speed at which to interpolate between the proxy and main camera
    // speed to move camera
    public float moveSpeed=1.0f;
    // Start is called before the first frame update

    private float cameraHeight = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the difference in rotation between the proxy rotation and current main camera rotation
        Quaternion correction = cameraProxy.transform.rotation * Quaternion.Inverse(mainCamera.transform.localRotation);
        // Set the rotation value of this camera to be a portion of the offset, over time this will correct slowly
        transform.rotation = Quaternion.Lerp(transform.rotation, correction, lerpSpeed);
        // copy the transform position
        transform.position = new Vector3(cameraProxy.transform.position.x * moveSpeed, 
            cameraProxy.transform.position.y, 
            cameraProxy.transform.position.z * moveSpeed
            );
    }
}
