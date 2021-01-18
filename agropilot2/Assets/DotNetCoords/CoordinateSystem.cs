namespace DotNetCoords
{
  /// <summary>
  /// Base class for classes defining co-ordinate systems.
  /// </summary>
  public abstract class CoordinateSystem 
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CoordinateSystem"/> class.
    /// </summary>
    /// <param name="datum">The datum.</param>
    protected CoordinateSystem(Datum.Datum datum)
    {
      Datum = datum;
    }

    /// <summary>
    /// Convert a co-ordinate in the co-ordinate system to a point represented
    /// by a latitude and longitude and a perpendicular height above (or below) a
    /// reference ellipsoid.
    /// </summary>
    /// <returns>A LatLng representation of a point in a co-ordinate system.</returns>
    public abstract LatLng ToLatLng();
    
    /// <summary>
    /// Gets the datum.
    /// </summary>
    /// <value>The datum.</value>
    public Datum.Datum Datum { get; }
  }
}
