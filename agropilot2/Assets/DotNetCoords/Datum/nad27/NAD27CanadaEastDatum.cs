using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canada East) datum.
  /// </summary>
  public sealed class NAD27CanadaEastDatum : Datum<NAD27CanadaEastDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanadaEastDatum"/> class.
    /// </summary>
    public NAD27CanadaEastDatum()
    {
      Name = "North American Datum 1927 (NAD27) - Canada East";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -22.0;
      DY = 160.0;
      DZ = 190.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
