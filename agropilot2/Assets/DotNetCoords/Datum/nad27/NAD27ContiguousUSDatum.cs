using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Contiguous United States) datum.
  /// </summary>
  public sealed class NAD27ContiguousUSDatum : Datum<NAD27ContiguousUSDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27ContiguousUSDatum"/> class.
    /// </summary>
    public NAD27ContiguousUSDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Contiguous United States";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -8.0;
      DY = 160.0;
      DZ = 176.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
