using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canada NW Territory) datum.
  /// </summary>
  public sealed class NAD27CanadaNWTerritoryDatum : Datum<NAD27CanadaNWTerritoryDatum> 
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanadaNWTerritoryDatum"/> class.
    /// </summary>
    public NAD27CanadaNWTerritoryDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Canada NW Territory";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 4.0;
      DY = 159.0;
      DZ = 188.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
