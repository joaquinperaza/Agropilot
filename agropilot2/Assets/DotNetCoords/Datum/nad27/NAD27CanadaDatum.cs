using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canada) datum.
  /// </summary>
  public sealed class NAD27CanadaDatum : Datum<NAD27CanadaDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanadaDatum"/> class.
    /// </summary>
    public NAD27CanadaDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Canada";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -10.0;
      DY = 158.0;
      DZ = 187.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
