using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Aleutian East) datum.
  /// </summary>
  public sealed class NAD27AleutianEastDatum : Datum<NAD27AleutianEastDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27AleutianEastDatum"/> class.
    /// </summary>
    public NAD27AleutianEastDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Aleutian East";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -2.0;
      DY = 152.0;
      DZ = 149.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
