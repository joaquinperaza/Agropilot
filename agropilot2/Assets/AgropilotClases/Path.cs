using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path
{
    public LineRenderer path;
    public abstract Path getNext(int width);
    public abstract Path getPrev(int width);
    public Path() {
        path = new LineRenderer();
    }
}
