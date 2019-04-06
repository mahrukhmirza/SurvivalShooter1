
/// <summary>
/// mahrukh sameen mirza
/// april 6,2019
/// this file has the code for camera settings
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target; // the position that camera will be following
    public float smoothing = 5f; // the speed with which the camera will be following 
    Vector3 offset;
    private void Start()
    {
        // calculate the initial offset
        offset = transform.position - target.position;
    }
    void FixedUpdate()
    {
        // create a position the camera is aiming forbased on the offset from the target
        Vector3 targetCamPos = target.position + offset;
        // smoothly interpolate between the camera's current position and its target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
