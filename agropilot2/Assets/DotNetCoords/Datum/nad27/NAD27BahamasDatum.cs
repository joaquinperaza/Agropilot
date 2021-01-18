using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Bahamas) datum.
  /// </summary>
  public sealed class NAD27BahamasDatum : Datum<NAD27BahamasDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27BahamasDatum"/> class.
    /// </summary>
    public NAD27BahamasDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Bahamas";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -4.0;
      DY = 154.0;
      DZ = 178.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
