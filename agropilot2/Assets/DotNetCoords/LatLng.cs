using DotNetCoords.Datum;
using DotNetCoords.Ellipsoid;
using System;

namespace DotNetCoords
{
  /// <summary>
  /// Enumerated type defining whether a latitude is North or South of the equator
  /// </summary>
  public enum NorthSouth
  {
    /// <summary>
    /// Latitude is north of the equator.
    /// </summary>
    North=1,
    /// <summary>
    /// Latitude is south of the equator.
    /// </summary>
    South=-1
  }

  /// <summary>
  /// Enumerated type defining whether a longitude is east or west of the prime meridian
  /// </summary>
  public enum EastWest
  {
    /// <summary>
    /// Longitude is east of the prime meridian
    /// </summary>
    East = 1,
    /// <summary>
    /// Longitude is west of the prime meridian
    /// </summary>
    West = -1
  }

  /// <summary>
  /// Class to represent a latitude/longitude pair based on a particular datum.
  /// </summary>
  public sealed class LatLng 
  {
    /**
     * Latitude in degrees.
     */

    /**
     * Longitude in degrees.
     */

    /**
     * Height.
     */

    /**
     * Datum of this reference.
     */

    /// <summary>
    /// Initializes a new instance of the <see cref="LatLng"/> class based on another <see cref="LatLng"/> instance.
    /// </summary>
    /// <param name="original">The original <see cref="LatLng"/> instance.</param>
    public LatLng(LatLng original) : this(original.Latitude, original.Longitude, original.Height, original.Datum)
    {
    }

    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// WGS84 datum.
    /// </summary>
    /// <param name="latitude">The latitude in degrees. Must be between -90.0 and 90.0 inclusive.
    /// -90.0 and 90.0 are effectively equivalent.</param>
    /// <param name="longitude">The longitude in degrees. Must be between -180.0 and 180.0
    /// inclusive. -180.0 and 180.0 are effectively equivalent.</param>
    /// <exception cref="ArgumentException">If either the given latitude or the given longitude are invalid.</exception>
    public LatLng(double latitude, double longitude) : this(latitude, longitude, 0, WGS84Datum.Instance)
    {
    }

    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// WGS84 datum.
    /// </summary>
    /// <param name="latitude">The latitude in degrees. Must be between -90.0 and 90.0 inclusive.
    /// -90.0 and 90.0 are effectively equivalent.</param>
    /// <param name="longitude">The longitude in degrees. Must be between -180.0 and 180.0
    /// inclusive. -180.0 and 180.0 are effectively equivalent.</param>
    /// <param name="height">The perpendicular height above the reference ellipsoid.</param>
    /// <exception cref="ArgumentException">If either the given latitude or the given longitude are invalid.</exception>
    public LatLng(double latitude, double longitude, double height) : 
      this(latitude, longitude, height, WGS84Datum.Instance)
    {
    }

    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// WGS84 datum.
    /// </summary>
    /// <param name="latitudeDegrees">The degrees part of the latitude. Must be 0 &lt;= latitudeDegrees &lt;=
    /// 90.0.</param>
    /// <param name="latitudeMinutes">The minutes part of the latitude. Must be 0 &lt;= latitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="latitudeSeconds">The seconds part of the latitude. Must be 0 &lt;= latitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="northSouth">Whether the latitude is north or south of the equator.</param>
    /// <param name="longitudeDegrees">The degrees part of the longitude. Must be 0 &lt;= longitudeDegrees &lt;=
    /// 180.0.</param>
    /// <param name="longitudeMinutes">The minutes part of the longitude. Must be 0 &lt;= longitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="longitudeSeconds">The seconds part of the longitude. Must be 0 &lt;= longitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="eastWest">Whether the longitude is east or west of the prime meridian.</param>
    /// <exception cref="ArgumentException">If any of the parameters are invalid.</exception>
    public LatLng(int latitudeDegrees, int latitudeMinutes,
        double latitudeSeconds, NorthSouth northSouth, int longitudeDegrees,
        int longitudeMinutes, double longitudeSeconds, EastWest eastWest) :
      this(latitudeDegrees, latitudeMinutes, latitudeSeconds, northSouth,
          longitudeDegrees, longitudeMinutes, longitudeSeconds, eastWest, 0.0,
          WGS84Datum.Instance)
    {
    }

    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// WGS84 datum.
    /// </summary>
    /// <param name="latitudeDegrees">The degrees part of the latitude. Must be 0 &lt;= latitudeDegrees &lt;=
    /// 90.0.</param>
    /// <param name="latitudeMinutes">The minutes part of the latitude. Must be 0 &lt;= latitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="latitudeSeconds">The seconds part of the latitude. Must be 0 &lt;= latitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="northSouth">Whether the latitude is north or south of the equator.</param>
    /// <param name="longitudeDegrees">The degrees part of the longitude. Must be 0 &lt;= longitudeDegrees &lt;=
    /// 180.0.</param>
    /// <param name="longitudeMinutes">The minutes part of the longitude. Must be 0 &lt;= longitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="longitudeSeconds">The seconds part of the longitude. Must be 0 &lt;= longitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="eastWest">Whether the longitude is east or west of the prime meridian.</param>
    /// <param name="height">The perpendicular height above the reference ellipsoid.</param>
    /// <exception cref="ArgumentException">if any of the parameters are invalid.</exception>
    public LatLng(int latitudeDegrees, int latitudeMinutes,
        double latitudeSeconds, NorthSouth northSouth, int longitudeDegrees,
        int longitudeMinutes, double longitudeSeconds, EastWest eastWest, double height) :
      this(latitudeDegrees, latitudeMinutes, latitudeSeconds, northSouth,
          longitudeDegrees, longitudeMinutes, longitudeSeconds, eastWest, height,
          WGS84Datum.Instance)
    {
    }


    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// specified datum.
    /// </summary>
    /// <param name="latitudeDegrees">The degrees part of the latitude. Must be 0 &lt;= latitudeDegrees &lt;=
    /// 90.0.</param>
    /// <param name="latitudeMinutes">The minutes part of the latitude. Must be 0 &lt;= latitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="latitudeSeconds">The seconds part of the latitude. Must be 0 &lt;= latitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="northSouth">Whether the latitude is north or south of the equator.</param>
    /// <param name="longitudeDegrees">The degrees part of the longitude. Must be 0 &lt;= longitudeDegrees &lt;=
    /// 180.0.</param>
    /// <param name="longitudeMinutes">The minutes part of the longitude. Must be 0 &lt;= longitudeMinutes &lt;
    /// 60.0.</param>
    /// <param name="longitudeSeconds">The seconds part of the longitude. Must be 0 &lt;= longitudeSeconds &lt;
    /// 60.0.</param>
    /// <param name="eastWest">Whether the longitude is east or west of the prime meridian.</param>
    /// <param name="height">The perpendicular height above the reference ellipsoid.</param>
    /// <param name="datum">The datum that this reference is based on.</param>
    /// <exception cref="ArgumentException">if any of the parameters are invalid.</exception>
    public LatLng(int latitudeDegrees, int latitudeMinutes,
        double latitudeSeconds, NorthSouth northSouth, int longitudeDegrees,
        int longitudeMinutes, double longitudeSeconds, EastWest eastWest,
        double height, Datum.Datum datum)
    {

      if (latitudeDegrees < 0.0 || latitudeDegrees > 90.0
          || latitudeMinutes < 0.0 || latitudeMinutes >= 60.0
          || latitudeSeconds < 0.0 || latitudeSeconds >= 60.0)
      {
        throw new ArgumentException("Invalid latitude");
      }

      if (longitudeDegrees < 0.0 || longitudeDegrees > 180.0
          || longitudeMinutes < 0.0 || longitudeMinutes >= 60.0
          || longitudeSeconds < 0.0 || longitudeSeconds >= 60.0)
      {
        throw new ArgumentException("Invalid longitude");
      }

      Latitude = (int)northSouth
          * (latitudeDegrees + (latitudeMinutes / 60.0) + (latitudeSeconds / 3600.0));
      Longitude = (int)eastWest
          * (longitudeDegrees + (longitudeMinutes / 60.0) + (longitudeSeconds / 3600.0));
      Height = height;
      Datum = datum;
    }

    /// <summary>
    /// Create a new LatLng object to represent a latitude/longitude pair using the
    /// specified datum.
    /// </summary>
    /// <param name="latitude">The latitude in degrees. Must be between -90.0 and 90.0 inclusive.
    /// -90.0 and 90.0 are effectively equivalent.</param>
    /// <param name="longitude">The longitude in degrees. Must be between -180.0 and 180.0
    /// inclusive. -180.0 and 180.0 are effectively equivalent.</param>
    /// <param name="height">The perpendicular height above the reference ellipsoid.</param>
    /// <param name="datum">The datum that this reference is based on.</param>
    /// <exception cref="ArgumentException">If either the given latitude or the given longitude are invalid.</exception>
    public LatLng(double latitude, double longitude, double height, Datum.Datum datum)
    {
      if (!IsValidLatitude(latitude))
      {
        throw new ArgumentException("Latitude (" + latitude
            + ") is invalid. Must be between -90.0 and 90.0 inclusive.");
      }

      if (!IsValidLongitude(longitude))
      {
        throw new ArgumentException("Longitude (" + longitude
            + ") is invalid. Must be between -180.0 and 180.0 inclusive.");
      }

      Latitude = latitude;
      Longitude = longitude;
      Height = height;
      Datum = datum;
    }

    /// <summary>
    /// Determines whether the specified latitude is valid.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <returns>
    /// 	<c>true</c> if the latitude is valid; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidLatitude(double latitude)
    {
      return (latitude >= -90.0 && latitude <= 90.0);
    }

    /// <summary>
    /// Determines whether the specified longitude is valid longitude.
    /// </summary>
    /// <param name="longitude">The longitude.</param>
    /// <returns>
    /// 	<c>true</c> if the longitude is valid; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidLongitude(double longitude)
    {
      return (longitude >= -180.0 && longitude <= 180.0);
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current LatLng object.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current LatLng object.
    /// </returns>
    public override string ToString() 
    {
      return "(" + Latitude + ", " + Longitude + ")";
    }

    /// <summary>
    /// Return a String representation of this LatLng object in
    /// degrees-minutes-seconds format. The returned format will be like this: DD
    /// MM SS.SSS N DD MM SS.SSS E where DD is the number of degrees, MM is the
    /// number of minutes, SS.SSS is the number of seconds, N is either N or S to
    /// indicate north or south of the equator and E is either E or W to indicate
    /// east or west of the prime meridian.
    /// </summary>
    /// <returns>A string representation of this LatLng object in DMS format.</returns>
    public string ToDMSString() 
    {
      return FormatLatitude() + " " + FormatLongitude();
    }

    private string FormatLatitude() 
    {
      var ns = Latitude >= 0 ? "N" : "S";
      return Math.Abs(LatitudeDegrees) + " " + LatitudeMinutes + " "
          + LatitudeSeconds + " " + ns;
    }

    private string FormatLongitude() 
    {
      var ew = Longitude >= 0 ? "E" : "W";
      return Math.Abs(LongitudeDegrees) + " " + LongitudeMinutes + " "
          + LongitudeSeconds + " " + ew;
    }

    /// <summary>
    /// Convert this latitude and longitude into an OSGB (Ordnance Survey of Great
    /// Britain) grid reference.
    /// </summary>
    /// <returns>The converted OSGB grid reference.</returns>
    public OSRef ToOSRef() 
    {
      var airy1830 = Airy1830Ellipsoid.Instance;
      const double osgbF0 = 0.9996012717;
      const double n0 = -100000.0;
      const double e0 = 400000.0;
      var phi0 = Util.ToRadians(49.0);
      var lambda0 = Util.ToRadians(-2.0);
      var a = airy1830.SemiMajorAxis;
      var b = airy1830.SemiMinorAxis;
      var eSquared = airy1830.EccentricitySquared;
      var phi = Util.ToRadians(Latitude);
      var lambda = Util.ToRadians(Longitude);
      var n = (a - b) / (a + b);
      var v = a * osgbF0
          * Math.Pow(1.0 - eSquared * Util.SinSquared(phi), -0.5);
      var rho = a * osgbF0 * (1.0 - eSquared)
          * Math.Pow(1.0 - eSquared * Util.SinSquared(phi), -1.5);
      var etaSquared = (v / rho) - 1.0;
      var m = (b * osgbF0)
          * (((1 + n + ((5.0 / 4.0) * n * n) + ((5.0 / 4.0) * n * n * n)) * (phi - phi0))
              - (((3 * n) + (3 * n * n) + ((21.0 / 8.0) * n * n * n))
                  * Math.Sin(phi - phi0) * Math.Cos(phi + phi0))
              + ((((15.0 / 8.0) * n * n) + ((15.0 / 8.0) * n * n * n))
                  * Math.Sin(2.0 * (phi - phi0)) * Math.Cos(2.0 * (phi + phi0))) - (((35.0 / 24.0)
              * n * n * n)
              * Math.Sin(3.0 * (phi - phi0)) * Math.Cos(3.0 * (phi + phi0))));
      var I = m + n0;
      var ii = (v / 2.0) * Math.Sin(phi) * Math.Cos(phi);
      var iii = (v / 24.0) * Math.Sin(phi) * Math.Pow(Math.Cos(phi), 3.0)
          * (5.0 - Util.TanSquared(phi) + (9.0 * etaSquared));
      var iiia = (v / 720.0) * Math.Sin(phi) * Math.Pow(Math.Cos(phi), 5.0)
          * (61.0 - (58.0 * Util.TanSquared(phi)) + Math.Pow(Math.Tan(phi), 4.0));
      var iv = v * Math.Cos(phi);
      var V = (v / 6.0) * Math.Pow(Math.Cos(phi), 3.0)
          * ((v / rho) - Util.TanSquared(phi));
      var vi = (v / 120.0)
          * Math.Pow(Math.Cos(phi), 5.0)
          * (5.0 - (18.0 * Util.TanSquared(phi)) + (Math.Pow(Math.Tan(phi), 4.0))
              + (14 * etaSquared) - (58 * Util.TanSquared(phi) * etaSquared));

      var N = I + (ii * Math.Pow(lambda - lambda0, 2.0))
                 + (iii * Math.Pow(lambda - lambda0, 4.0))
                 + (iiia * Math.Pow(lambda - lambda0, 6.0));
      var e = e0 + (iv * (lambda - lambda0)) + (V * Math.Pow(lambda - lambda0, 3.0))
                 + (vi * Math.Pow(lambda - lambda0, 5.0));

      return new OSRef(e, N);
    }

    /// <summary>
    /// Convert this latitude and longitude to a UTM reference.
    /// </summary>
    /// <returns>The converted UTM reference.</returns>
    /// <param name="forceLongitudeZone">Force a longitude zone for the UTM Reference</param>
    /// <exception cref="NotDefinedOnUtmGridException">
    /// If an attempt is made to convert a LatLng that falls outside the
    /// area covered by the UTM grid. The UTM grid is only defined for
    /// latitudes south of 84°N and north of 80°S.</exception>
    public UTMRef ToUtmRef(int forceLongitudeZone = 0) 
    {
      if (Latitude < -80 || Latitude > 84) {
        throw new NotDefinedOnUtmGridException("Latitude (" + Latitude
            + ") falls outside the UTM grid.");
      }

      if (Longitude == 180.0) {
        Longitude = -180.0;
      }

      const double utmF0 = 0.9996;
      var a = Datum.ReferenceEllipsoid.SemiMajorAxis;
      var eSquared = Datum.ReferenceEllipsoid.EccentricitySquared;
      var longitude = Longitude;
      var latitude = Latitude;

      var latitudeRad = latitude * (Math.PI / 180.0);
      var longitudeRad = longitude * (Math.PI / 180.0);
      var longitudeZone = (int) Math.Floor((longitude + 180.0) / 6.0) + 1;
      if (forceLongitudeZone != 0)
        longitudeZone = forceLongitudeZone;

      // Special zone for Norway
      if (latitude >= 56.0 && latitude < 64.0 && longitude >= 3.0
          && longitude < 12.0) {
        longitudeZone = 32;
      }

      // Special zones for Svalbard
      if (latitude >= 72.0 && latitude < 84.0) {
        if (longitude >= 0.0 && longitude < 9.0) {
          longitudeZone = 31;
        } else if (longitude >= 9.0 && longitude < 21.0) {
          longitudeZone = 33;
        } else if (longitude >= 21.0 && longitude < 33.0) {
          longitudeZone = 35;
        } else if (longitude >= 33.0 && longitude < 42.0) {
          longitudeZone = 37;
        }
      }

      double longitudeOrigin = (longitudeZone - 1) * 6 - 180 + 3;
      var longitudeOriginRad = longitudeOrigin * (Math.PI / 180.0);

      var utmZone = UTMRef.GetUTMLatitudeZoneLetter(latitude);

      var ePrimeSquared = (eSquared) / (1 - eSquared);

      var n = a
          / Math.Sqrt(1 - eSquared * Math.Sin(latitudeRad)
              * Math.Sin(latitudeRad));
      var t = Math.Tan(latitudeRad) * Math.Tan(latitudeRad);
      var c = ePrimeSquared * Math.Cos(latitudeRad) * Math.Cos(latitudeRad);
      var A = Math.Cos(latitudeRad) * (longitudeRad - longitudeOriginRad);

      var m = a
          * ((1 - eSquared / 4 - 3 * eSquared * eSquared / 64 - 5 * eSquared
              * eSquared * eSquared / 256)
              * latitudeRad
              - (3 * eSquared / 8 + 3 * eSquared * eSquared / 32 + 45 * eSquared
                  * eSquared * eSquared / 1024)
              * Math.Sin(2 * latitudeRad)
              + (15 * eSquared * eSquared / 256 + 45 * eSquared * eSquared
                  * eSquared / 1024) * Math.Sin(4 * latitudeRad) - (35 * eSquared
              * eSquared * eSquared / 3072)
              * Math.Sin(6 * latitudeRad));

      var utmEasting = (utmF0
          * n
          * (A + (1 - t + c) * Math.Pow(A, 3.0) / 6 + (5 - 18 * t + t * t + 72
              * c - 58 * ePrimeSquared)
              * Math.Pow(A, 5.0) / 120) + 500000.0);

      var utmNorthing = (utmF0 * (m + n
          * Math.Tan(latitudeRad)
          * (A * A / 2 + (5 - t + (9 * c) + (4 * c * c)) * Math.Pow(A, 4.0) / 24 + (61
              - (58 * t) + (t * t) + (600 * c) - (330 * ePrimeSquared))
              * Math.Pow(A, 6.0) / 720)));

      // Adjust for the southern hemisphere
      if (latitude < 0) {
        utmNorthing += 10000000.0;
      }

      return new UTMRef(longitudeZone, utmZone, utmEasting, utmNorthing, Datum);
    }

    /// <summary>
    /// Convert this latitude and longitude to an MGRS reference.
    /// </summary>
    /// <returns>The converted MGRS reference</returns>
    public MGRSRef ToMGRSRef() 
    {
      var utm = ToUtmRef();
      return new MGRSRef(utm);
    }

    /// <summary>
    /// Convert this LatLng from the OSGB36 datum to the WGS84 datum using an
    /// approximate Helmert transformation.
    /// </summary>   
    public void ToWGS84() 
    {
      ToDatum(WGS84Datum.Instance);
    }

    /// <summary>
    /// Converts this LatLng to another datum.
    /// </summary>
    /// <param name="d">The datum.</param>
    public void ToDatum(Datum.Datum d) 
    {
      // first convert to WGS84 if needed
      if (!(Datum is WGS84Datum)) 
      {
        InternalToDatum(WGS84Datum.Instance, true);
      } 

      if (d is WGS84Datum) 
      {
        // Don't do anything if datum and d are both WGS84.
        return;
      }

      InternalToDatum(d, false);
    }

    private void InternalToDatum(Datum.Datum d, bool toWgs)
    {
      var a = Datum.ReferenceEllipsoid.SemiMajorAxis;
      var eSquared = Datum.ReferenceEllipsoid.EccentricitySquared;
      var phi = Util.ToRadians(Latitude);
      var lambda = Util.ToRadians(Longitude);
      var v = a / (Math.Sqrt(1 - eSquared * Util.SinSquared(phi)));
      var h = Height; // height
      var x = (v + h) * Math.Cos(phi) * Math.Cos(lambda);
      var y = (v + h) * Math.Cos(phi) * Math.Sin(lambda);
      var z = ((1 - eSquared) * v + h) * Math.Sin(phi);

      double invert = -1;
      var referenceDatum = d;
      if (toWgs)
      {
        invert = 1;
        referenceDatum = Datum;
      }

      var dx = invert * referenceDatum.DX;
      var dy = invert * referenceDatum.DY;
      var dz = invert * referenceDatum.DZ;
      var ds = invert * referenceDatum.DS / 1000000.0;
      var rx = invert * Util.ToRadians(referenceDatum.RX / 3600.0);
      var ry = invert * Util.ToRadians(referenceDatum.RY / 3600.0);
      var rz = invert * Util.ToRadians(referenceDatum.RZ / 3600.0);

      var sc = 1 + ds;
      var xB = dx + (x * sc) + ((-rx * y) * sc) + ((ry * z) * sc);
      var yB = dy + ((rz * x) * sc) + (y * sc) + ((-rx * z) * sc);
      var zB = dz + ((-ry * x) * sc) + ((rx * y) * sc) + (z * sc);

      a = d.ReferenceEllipsoid.SemiMajorAxis;
      eSquared = d.ReferenceEllipsoid.EccentricitySquared;

      var lambdaB = Util.ToDegrees(Math.Atan(yB / xB));
      var p = Math.Sqrt((xB * xB) + (yB * yB));
      var phiN = Math.Atan(zB / (p * (1 - eSquared)));
      for (var i = 1; i < 10; i++) {
        v = a / (Math.Sqrt(1 - eSquared * Util.SinSquared(phiN)));
        var phiN1 = Math.Atan((zB + (eSquared * v * Math.Sin(phiN))) / p);
        phiN = phiN1;
      }

      var phiB = Util.ToDegrees(phiN);

      Latitude = phiB;
      Longitude = lambdaB;

      Datum = d;
    }


    /// <summary>
    /// Convert this LatLng from the WGS84 datum to the OSGB36 datum using an
    /// approximate Helmert transformation.
    /// </summary>
    public void ToOSGB36() 
    {
      ToDatum(OSGB36Datum.Instance);
    }

    /// <summary>
    /// Calculate the surface distance in kilometres from this LatLng to the given
    /// LatLng.
    /// </summary>
    /// <param name="ll">The LatLng object to measure the distance to.</param>
    /// <returns>The surface distance in kilometres.</returns>
    public double Distance(LatLng ll) 
    {
      const double er = 6366.707;

      var latFrom = Util.ToRadians(Latitude);
      var latTo = Util.ToRadians(ll.Latitude);
      var lngFrom = Util.ToRadians(Longitude);
      var lngTo = Util.ToRadians(ll.Longitude);

      return Math.Acos(Math.Sin(latFrom) * Math.Sin(latTo)
          + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Cos(lngTo - lngFrom))
          * er;
    }

    /// <summary>
    /// Calculate the surface distance in miles from this LatLng to the given
    /// LatLng.
    /// </summary>
    /// <param name="ll">The LatLng object to measure the distance to.</param>
    /// <returns>The surface distance in miles.</returns>
    public double DistanceMiles(LatLng ll) 
    {
      return Distance(ll) / 1.609344;
    }


    /// <summary>
    /// Gets the bearing in degrees between two LatLng objects
    /// </summary>
    /// <param name="ll">The other LatLng object to measure the bearing to</param>
    /// <returns>The bearing in degrees</returns>
    public double Bearing(LatLng ll)
    {
      var y = Math.Sin(Util.ToRadians(ll.Longitude - Longitude)) * Math.Cos(Util.ToRadians(ll.Latitude));
      var x = Math.Cos(Util.ToRadians(Latitude)) * Math.Sin(Util.ToRadians(ll.Latitude)) -
                                                            Math.Sin(Util.ToRadians(Latitude)) * Math.Cos(Util.ToRadians(ll.Latitude)) * Math.Cos(Util.ToRadians(ll.Longitude - Longitude));
      return Util.ToDegrees(Math.Atan2(y, x));
    }

    /// <summary>
    /// Gets the latitude in degrees.
    /// </summary>
    /// <value>The latitude in degrees.</value>
    public double Latitude { get; private set; }

    /// <summary>
    /// Gets the latitude degrees.
    /// </summary>
    /// <value>The latitude degrees.</value>
    public int LatitudeDegrees 
    {
      get
      {
        var ll = Latitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          deg++;
        }
        return deg;
      }
    }

    /// <summary>
    /// Gets the latitude minutes.
    /// </summary>
    /// <value>The latitude minutes.</value>
    public int LatitudeMinutes 
    {
      get
      {
        var ll = Latitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          minx = 1 - minx;
        }
        var min = (int)Math.Floor(minx * 60);
        return min;
      }
    }

    /// <summary>
    /// Gets the latitude seconds.
    /// </summary>
    /// <value>The latitude seconds.</value>
    public double LatitudeSeconds 
    {
      get
      {
        var ll = Latitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          minx = 1 - minx;
        }
        var min = (int)Math.Floor(minx * 60);
        var sec = ((minx * 60) - min) * 60;
        return sec;
      }
    }

    /// <summary>
    /// Gets the longitude in degrees.
    /// </summary>
    /// <value>The longitude in degrees.</value>
    public double Longitude { get; private set; }

    /// <summary>
    /// Gets the longitude degrees.
    /// </summary>
    /// <value>The longitude degrees.</value>
    public int LongitudeDegrees 
    {
      get
      {
        var ll = Longitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          deg++;
        }
        return deg;
      }
    }

    /// <summary>
    /// Gets the longitude minutes.
    /// </summary>
    /// <value>The longitude minutes.</value>
    public int LongitudeMinutes 
    {
      get
      {
        var ll = Longitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          minx = 1 - minx;
        }
        var min = (int)Math.Floor(minx * 60);
        return min;
      }
    }

    /// <summary>
    /// Gets the longitude seconds.
    /// </summary>
    /// <value>The longitude seconds.</value>
    public double LongitudeSeconds 
    {
      get
      {
        var ll = Longitude;
        var deg = (int)Math.Floor(ll);
        var minx = ll - deg;
        if (ll < 0 && minx != 0.0)
        {
          minx = 1 - minx;
        }
        var min = (int)Math.Floor(minx * 60);
        var sec = ((minx * 60) - min) * 60;
        return sec;
      }
    }

    /// <summary>
    /// Gets the height.
    /// </summary>
    /// <value>The height.</value>
    public double Height { get; }

    /// <summary>
    /// Gets the datum.
    /// </summary>
    /// <value>The datum.</value>
    public Datum.Datum Datum { get; private set; } = WGS84Datum.Instance;
  }
}