using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Cuba) datum.
  /// </summary>
  public sealed class NAD27CubaDatum : Datum<NAD27CubaDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CubaDatum"/> class.
    /// </summary>
    public NAD27CubaDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Cuba";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -9.0;
      DY = 152.0;
      DZ = 178.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
