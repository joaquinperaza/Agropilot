using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canada Yukon) datum.
  /// </summary>
  public sealed class NAD27CanadaYukonDatum : Datum<NAD27CanadaYukonDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanadaYukonDatum"/> class.
    /// </summary>
    public NAD27CanadaYukonDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Canada Yukon";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -7.0;
      DY = 139.0;
      DZ = 181.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
