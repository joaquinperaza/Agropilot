using DotNetCoords.Datum;
using System;

namespace DotNetCoords
{
  /// <summary>
  /// Class to represent a Universal Transverse Mercator (UTM) reference.
  /// </summary>
  public sealed class UTMRef : CoordinateSystem 
  {
    /// <summary>
    /// Create a new UTM reference object using the WGS84 datum. Checks are made to make sure that the
    /// given parameters are roughly valid, but the checks are not exhaustive with
    /// regards to the easting value. Catching a NotDefinedOnUTMGridException does
    /// not necessarily mean that the UTM reference is well-formed. This is because
    /// that valid values for the easting vary depending on the latitude.
    /// </summary>
    /// <param name="lngZone">The longitude zone number.</param>
    /// <param name="latZone">The latitude zone character.</param>
    /// <param name="easting">The easting in metres.</param>
    /// <param name="northing">The northing in metres.</param>
    /// <exception cref="NotDefinedOnUtmGridException">
    /// If any of the parameters are invalid. Be careful that a valid
    /// value for the easting does not necessarily mean that the UTM
    /// reference is well-formed. The current checks do not take into
    /// account the varying range of valid values for the easting for
    /// different latitudes.</exception>
    public UTMRef(int lngZone, char latZone, double easting, double northing) : 
      this(lngZone, latZone, easting, northing, WGS84Datum.Instance)
    {

    }

    /// <summary>
    /// Create a new UTM reference object. Checks are made to make sure that the
    /// given parameters are roughly valid, but the checks are not exhaustive with
    /// regards to the easting value. Catching a NotDefinedOnUTMGridException does
    /// not necessarily mean that the UTM reference is well-formed. This is because
    /// that valid values for the easting vary depending on the latitude.
    /// </summary>
    /// <param name="lngZone">The longitude zone number.</param>
    /// <param name="latZone">The latitude zone character.</param>
    /// <param name="easting">The easting in metres.</param>
    /// <param name="northing">The northing in metres.</param>
    /// <param name="datum">The datum of the UTM reference</param>
    /// <exception cref="NotDefinedOnUtmGridException">
    /// If any of the parameters are invalid. Be careful that a valid
    /// value for the easting does not necessarily mean that the UTM
    /// reference is well-formed. The current checks do not take into
    /// account the varying range of valid values for the easting for
    /// different latitudes.</exception>
    public UTMRef(int lngZone, char latZone, double easting, double northing, 
      Datum.Datum datum) : base(datum)
    {
      if (lngZone < 1 || lngZone > 60) {
        throw new NotDefinedOnUtmGridException("Longitude zone (" + lngZone
            + ") is not defined on the UTM grid.");
      }

      if (latZone < 'C' || latZone > 'X') {
        throw new NotDefinedOnUtmGridException("Latitude zone (" + latZone
            + ") is not defined on the UTM grid.");
      }

      if (easting < 0.0 || easting > 1000000.0) {
        throw new NotDefinedOnUtmGridException("Easting (" + easting
            + ") is not defined on the UTM grid.");
      }

      if (northing < 0.0 || northing > 10000000.0) {
        throw new NotDefinedOnUtmGridException("Northing (" + northing
            + ") is not defined on the UTM grid.");
      }

      Easting = easting;
      Northing = northing;
      LatZone = latZone;
      LngZone = lngZone;
    }

    /// <summary>
    /// Convert this UTM reference to a latitude and longitude.
    /// </summary>
    /// <returns>
    /// The converted latitude and longitude.
    /// </returns>
    public override LatLng ToLatLng() 
    {
      const double utmF0 = 0.9996;
      var a = Datum.ReferenceEllipsoid.SemiMajorAxis;
      var eSquared = Datum.ReferenceEllipsoid.EccentricitySquared;
      var ePrimeSquared = eSquared / (1.0 - eSquared);
      var e1 = (1 - Math.Sqrt(1 - eSquared)) / (1 + Math.Sqrt(1 - eSquared));
      var x = Easting - 500000.0;
      var y = Northing;
      var zoneNumber = LngZone;
      var zoneLetter = LatZone;

      var longitudeOrigin = (zoneNumber - 1.0) * 6.0 - 180.0 + 3.0;

      // Correct y for southern hemisphere
      if ((zoneLetter - 'N') < 0) {
        y -= 10000000.0;
      }

      var m = y / utmF0;
      var mu = m
          / (a * (1.0 - eSquared / 4.0 - 3.0 * eSquared * eSquared / 64.0 - 5.0 * Math
              .Pow(eSquared, 3.0) / 256.0));

      var phi1Rad = mu + (3.0 * e1 / 2.0 - 27.0 * Math.Pow(e1, 3.0) / 32.0)
          * Math.Sin(2.0 * mu)
          + (21.0 * e1 * e1 / 16.0 - 55.0 * Math.Pow(e1, 4.0) / 32.0)
          * Math.Sin(4.0 * mu) + (151.0 * Math.Pow(e1, 3.0) / 96.0)
          * Math.Sin(6.0 * mu);

      var n = a
          / Math.Sqrt(1.0 - eSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad));
      var t = Math.Tan(phi1Rad) * Math.Tan(phi1Rad);
      var c = ePrimeSquared * Math.Cos(phi1Rad) * Math.Cos(phi1Rad);
      var r = a * (1.0 - eSquared)
          / Math.Pow(1.0 - eSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad), 1.5);
      var d = x / (n * utmF0);

      var latitude = (phi1Rad - (n * Math.Tan(phi1Rad) / r)
          * (d
              * d
              / 2.0
              - (5.0 + (3.0 * t) + (10.0 * c) - (4.0 * c * c) - (9.0 * ePrimeSquared))
              * Math.Pow(d, 4.0) / 24.0 + (61.0 + (90.0 * t) + (298.0 * c)
              + (45.0 * t * t) - (252.0 * ePrimeSquared) - (3.0 * c * c))
              * Math.Pow(d, 6.0) / 720.0))
          * (180.0 / Math.PI);

      var longitude = longitudeOrigin
          + ((d - (1.0 + 2.0 * t + c) * Math.Pow(d, 3.0) / 6.0 + (5.0 - (2.0 * c)
              + (28.0 * t) - (3.0 * c * c) + (8.0 * ePrimeSquared) + (24.0 * t * t))
              * Math.Pow(d, 5.0) / 120.0) / Math.Cos(phi1Rad))
          * (180.0 / Math.PI);

      return new LatLng(latitude, longitude);
    }

    /// <summary>
    /// Work out the UTM latitude zone from the latitude.
    /// </summary>
    /// <param name="latitude">The latitude to find the UTM latitude zone for.</param>
    /// <returns>The UTM latitude zone for the given latitude.</returns>
    public static char GetUTMLatitudeZoneLetter(double latitude)
    {
      if ((84 >= latitude) && (latitude >= 72))
        return 'X';
      if ((72 > latitude) && (latitude >= 64))
        return 'W';
      if ((64 > latitude) && (latitude >= 56))
        return 'V';
      if ((56 > latitude) && (latitude >= 48))
        return 'U';
      if ((48 > latitude) && (latitude >= 40))
        return 'T';
      if ((40 > latitude) && (latitude >= 32))
        return 'S';
      if ((32 > latitude) && (latitude >= 24))
        return 'R';
      if ((24 > latitude) && (latitude >= 16))
        return 'Q';
      if ((16 > latitude) && (latitude >= 8))
        return 'P';
      if ((8 > latitude) && (latitude >= 0))
        return 'N';
      if ((0 > latitude) && (latitude >= -8))
        return 'M';
      if ((-8 > latitude) && (latitude >= -16))
        return 'L';
      if ((-16 > latitude) && (latitude >= -24))
        return 'K';
      if ((-24 > latitude) && (latitude >= -32))
        return 'J';
      if ((-32 > latitude) && (latitude >= -40))
        return 'H';
      if ((-40 > latitude) && (latitude >= -48))
        return 'G';
      if ((-48 > latitude) && (latitude >= -56))
        return 'F';
      if ((-56 > latitude) && (latitude >= -64))
        return 'E';
      if ((-64 > latitude) && (latitude >= -72))
        return 'D';
      if ((-72 > latitude) && (latitude >= -80))
        return 'C';
      return 'Z';
    }

    /// <summary>
    /// Convert this UTM reference to a String representation for printing out.
    /// </summary>
    /// <returns>A string representation of this UTM reference.</returns>
    public override string ToString() 
    {
      return LngZone + LatZone.ToString() + " " + (int)Easting + " " + (int)Northing;
    }

    /// <summary>
    /// Gets the easting.
    /// </summary>
    /// <value>The easting.</value>
    public double Easting { get; }

    /// <summary>
    /// Gets the northing.
    /// </summary>
    /// <value>The northing.</value>
    public double Northing { get; }

    /// <summary>
    /// Gets the latitude zone character.
    /// </summary>
    /// <value>The latitude zone character.</value>
    public char LatZone { get; }

    /// <summary>
    /// Get the longitude zone number.
    /// </summary>
    /// <value>The longitude zone number.</value>
    public int LngZone { get; }
  }
}
