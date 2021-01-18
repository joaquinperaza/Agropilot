using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (San Salvador) datum.
  /// </summary>
  public sealed class NAD27SanSalvadorDatum : Datum<NAD27SanSalvadorDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27SanSalvadorDatum"/> class.
    /// </summary>
    public NAD27SanSalvadorDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - San Salvador";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 1.0;
      DY = 140.0;
      DZ = 165.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
