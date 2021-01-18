using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DotNetCoords;

public class Converter
{
    private UTMRef zerozone;
    private LatLng zero;


    public UTMRef LatLngToUTM(LatLng pos)
    {
        return pos.ToUtmRef(forceLongitudeZone : zerozone.LngZone);
    }

    public Vector2 WorldFromLatLng(LatLng pos)
    {
        UTMRef utm = LatLngToUTM(pos);
        Vector2 r=new Vector2((float)(utm.Easting - zerozone.Easting), (float)(utm.Northing - zerozone.Northing));
        return r;
    }

    public LatLng LatLngFromWorld(Vector2 pos)
    {
        Vector2 utm = new Vector2(pos.x + (float)zerozone.Easting, pos.y + (float)zerozone.Northing);
        return new UTMRef(zerozone.LngZone, zerozone.LatZone, utm.x, utm.y).ToLatLng();
    }

    public Converter(LatLng z) {
        zero = z;
        zerozone = z.ToUtmRef();
    }
}
