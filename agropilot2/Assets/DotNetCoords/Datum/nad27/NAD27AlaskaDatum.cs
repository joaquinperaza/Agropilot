using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Alaska) datum.
  /// </summary>
  public sealed class NAD27AlaskaDatum : Datum<NAD27AlaskaDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27AlaskaDatum"/> class.
    /// </summary>
    public NAD27AlaskaDatum()
    {
      Name = "North American Datum 1927 (NAD27) - Alaska";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -5.0;
      DY = 135.0;
      DZ = 172.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
