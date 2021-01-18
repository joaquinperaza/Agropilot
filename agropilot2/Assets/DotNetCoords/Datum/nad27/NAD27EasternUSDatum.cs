using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Eastern US) datum.
  /// </summary>
  public sealed class NAD27EasternUSDatum : Datum<NAD27EasternUSDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27EasternUSDatum"/> class.
    /// </summary>
    public NAD27EasternUSDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Eastern US";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -9.0;
      DY = 161.0;
      DZ = 179.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
