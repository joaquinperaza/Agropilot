using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Western US) datum.
  /// </summary>
  public sealed class NAD27WesternUSDatum : Datum<NAD27WesternUSDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27WesternUSDatum"/> class.
    /// </summary>
    public NAD27WesternUSDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Western US";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -8.0;
      DY = 159.0;
      DZ = 175.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
