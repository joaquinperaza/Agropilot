using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Central America) datum.
  /// </summary>
  public sealed class NAD27CentralAmericaDatum : Datum<NAD27CentralAmericaDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CentralAmericaDatum"/> class.
    /// </summary>
    public NAD27CentralAmericaDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Central America";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 0.0;
      DY = 125.0;
      DZ = 194.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
