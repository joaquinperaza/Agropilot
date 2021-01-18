using System;
using DotNetCoords.Datum;

namespace DotNetCoords
{
  /// <summary>
  /// ECEF (earth-centred, earth-fixed) Cartesian co-ordinates are used to define a
  /// point in three-dimensional space. ECEF co-ordinates are defined relative to
  /// an x-axis (the intersection of the equatorial plane and the plane defined by
  /// the prime meridian), a y-axis (at 90° to the x-axis and its intersection
  /// with the equator) and a z-axis (intersecting the North Pole). All the axes
  /// intersect at the point defined by the centre of mass of the Earth.
  /// </summary>
  public sealed class ECEFRef : CoordinateSystem 
  {
    /**
     * x co-ordinate in metres.
     */

    /**
     * y co-ordinate in metres.
     */

    /**
     * z co-ordinate in metres.
     */

    /// <summary>
    /// Create a new earth-centred, earth-fixed (ECEF) reference from the given
    /// parameters using the WGS84 reference ellipsoid.
    /// </summary>
    /// <param name="x">The x co-ordinate.</param>
    /// <param name="y">The y co-ordinate.</param>
    /// <param name="z">The z co-ordinate.</param>
    public ECEFRef(double x, double y, double z) : this(x, y, z, WGS84Datum.Instance)
    { 
    }

    /// <summary>
    /// Create a new earth-centred, earth-fixed (ECEF) reference from the given
    /// parameters and the given reference ellipsoid.
    /// </summary>
    /// <param name="x">The x co-ordinate.</param>
    /// <param name="y">The y co-ordinate.</param>
    /// <param name="z">The z co-ordinate.</param>
    /// <param name="datum">The datum.</param>
    public ECEFRef(double x, double y, double z, Datum.Datum datum) : base(datum)
    {
      X = x;
      Y = y;
      Z = z; 
    }


    /// <summary>
    /// Create a new earth-centred, earth-fixed reference from the given latitude
    /// and longitude.
    /// </summary>
    /// <param name="ll">The latitude and longitude.</param>
    public ECEFRef(LatLng ll) : base(ll.Datum)
    {
      var ellipsoid = Datum.ReferenceEllipsoid;

      var phi = Util.ToRadians(ll.Latitude);
      var lambda = Util.ToRadians(ll.Longitude);
      var h = ll.Height;
      var a = ellipsoid.SemiMajorAxis;
      var f = ellipsoid.Flattening;
      var eSquared = (2 * f) - (f * f);
      var nphi = a / Math.Sqrt(1 - eSquared * Util.SinSquared(phi));

      X = (nphi + h) * Math.Cos(phi) * Math.Cos(lambda);
      Y = (nphi + h) * Math.Cos(phi) * Math.Sin(lambda);
      Z = (nphi * (1 - eSquared) + h) * Math.Sin(phi);
    }

    /// <summary>
    /// Convert this ECEFRef object to a point represented
    /// by a latitude and longitude and a perpendicular height above (or below) a
    /// reference ellipsoid.
    /// </summary>
    /// <returns>
    /// The equivalent latitude and longitude.
    /// </returns>
    public override LatLng ToLatLng() 
    {
      var ellipsoid = Datum.ReferenceEllipsoid;
      
      var a = ellipsoid.SemiMajorAxis;
      var b = ellipsoid.SemiMinorAxis;
      var e2Squared = ((a * a) - (b * b)) / (b * b);
      var f = ellipsoid.Flattening;
      var eSquared = (2 * f) - (f * f);
      var p = Math.Sqrt((X * X) + (Y * Y));
      var theta = Math.Atan((Z * a) / (p * b));

      var phi = Math.Atan((Z + (e2Squared * b * Util.SinCubed(theta)))
          / (p - eSquared * a * Util.CosCubed(theta)));
      var lambda = Math.Atan2(Y, X);

      var nphi = a / Math.Sqrt(1 - eSquared * Util.SinSquared(phi));
      var h = (p / Math.Cos(phi)) - nphi;

      return new LatLng(Util.ToDegrees(phi), Util.ToDegrees(lambda), h,
          WGS84Datum.Instance);
    }

    /// <summary>
    /// Gets the x co-ordinate.
    /// </summary>
    /// <value>The x co-ordinate.</value>
    public double X { get; }

    /// <summary>
    /// Gets the y co-ordinate.
    /// </summary>
    /// <value>The y co-ordinate.</value>
    public double Y { get; }


    /// <summary>
    /// Gets the z co-ordinate.
    /// </summary>
    /// <value>The the z co-ordinate.</value>
    public double Z { get; }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current ECEF reference.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current ECEF reference.
    /// </returns>
    public override string ToString() 
    {
      return "(" + X + "," + Y + "," + Z + ")";
    }
  }
}
