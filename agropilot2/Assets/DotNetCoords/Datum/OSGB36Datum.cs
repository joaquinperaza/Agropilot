using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
  /// <summary>
  /// Class representing the Ordnance Survey of Great Britain 1936 (OSGB36) datum.
  /// </summary>
  public sealed class OSGB36Datum : Datum<OSGB36Datum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="OSGB36Datum"/> class.
    /// </summary>
    public OSGB36Datum()
    {
      Name = "Ordnance Survey of Great Britain 1936 (OSGB36)";
      ReferenceEllipsoid = Airy1830Ellipsoid.Instance;
      DX = 446.448;
      DY = -125.157;
      DZ = 542.06;
      DS = -20.4894;
      RX = 0.1502;
      RY = 0.2470;
      RZ = 0.8421;
    }
  }
}
