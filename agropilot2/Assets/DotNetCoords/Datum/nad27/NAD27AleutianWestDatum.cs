using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Aleutian West) datum.
  /// </summary>
  public sealed class NAD27AleutianWestDatum : Datum<NAD27AleutianWestDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27AleutianWestDatum"/> class.
    /// </summary>
    public NAD27AleutianWestDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Aleutian West";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 2.0;
      DY = 204.0;
      DZ = 105.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
