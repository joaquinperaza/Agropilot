using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Caribbean) datum.
  /// </summary>
  public sealed class NAD27CaribbeanDatum : Datum<NAD27CaribbeanDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CaribbeanDatum"/> class.
    /// </summary>
    public NAD27CaribbeanDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Caribbean";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -3.0;
      DY = 142.0;
      DZ = 183.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
