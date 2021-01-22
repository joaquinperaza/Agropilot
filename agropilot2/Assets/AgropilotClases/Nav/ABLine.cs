using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ABLine : Path
{
    private Vector3 A;
    private Vector3 B;

    public override Path getNext(int width) {
        Vector3 heading = B - A;
        Vector3 cross=Vector3.Cross(heading, Vector3.up).normalized;
        return new ABLine(A,B);
    }
    public override Path getPrev(int width) { return new ABLine(A,B); }
    public ABLine(Vector3 A, Vector3 B)
    {
        path.SetPositions(new Vector3[2]{ this.A,this.B});
    }
}
