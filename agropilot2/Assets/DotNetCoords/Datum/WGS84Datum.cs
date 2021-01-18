using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
  /// <summary>
  /// Class representing the World Geodetic System 1984 (WGS84) datum.
  /// </summary>
  public sealed class WGS84Datum : Datum<WGS84Datum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WGS84Datum"/> class.
    /// </summary>
    public WGS84Datum() 
    {
      Name = "World Geodetic System 1984 (WGS84)";
      ReferenceEllipsoid = WGS84Ellipsoid.Instance;
      DX = 0.0;
      DY = 0.0;
      DZ = 0.0;
      DS = 1.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
