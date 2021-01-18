using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Greenland) datum.
  /// </summary>
  public sealed class NAD27GreenlandDatum : Datum<NAD27GreenlandDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27GreenlandDatum"/> class.
    /// </summary>
    public NAD27GreenlandDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Greenland";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 11.0;
      DY = 114.0;
      DZ = 195.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
