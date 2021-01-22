using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TractorStatus : MonoBehaviour
{
    public Vector3 position;
    public float wheelAngle;
    public float heading;
    private ImplementSettings implemet;

    void SetImplement(ImplementSettings i)
    {
        implemet = i;
    }
    public TractorStatus()
    {

    }
}
