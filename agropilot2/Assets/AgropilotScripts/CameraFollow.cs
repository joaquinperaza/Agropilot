using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour { 
    public Transform target;
    public float distance = 1.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;
    private bool threeD=true;
    public void ZoomIn() {
        
        if (threeD)
        {
            distance -= 2;
            height = distance / 3;
        }
            
        else {
            distance = distance / 0.1f;
            distance -= 2;
            height = distance;
            distance = distance * 0.1f;
        }
            
    }
    public void ZoomOut()
    {
        
        if (threeD)
        {
            distance += 2;
            height = distance / 3f;
        }
        else
        {
            distance = distance / 0.1f;
            distance += 2;
            height = distance;
            distance = distance * 0.1f;
        }
    }
    public void top()
    {
        height = distance;
        distance = distance * 0.1f;
        threeD = false;
    }
    public void back()
    {
        distance = distance / 0.1f;
        height = distance / 3f;
        threeD = true;


    }

    private void Start()
    {
        ZoomIn();
    }


    void Update()
{
    Vector3 wantedPosition;
    if (followBehind)
        wantedPosition = target.TransformPoint(0, height, -distance);
    else
        wantedPosition = target.TransformPoint(0, height, distance);

    transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

    if (smoothRotation)
    {
        Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
    }
    else transform.LookAt(target, target.up);
}
}