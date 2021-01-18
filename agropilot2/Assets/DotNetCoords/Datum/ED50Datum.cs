using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
  /// <summary>
  /// Class representing the European 1950 (ED50) datum.
  /// </summary>
  public sealed class ED50Datum : Datum<ED50Datum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ED50Datum"/> class.
    /// </summary>
    public ED50Datum()
    {
      Name = "European Datum 1950";
      ReferenceEllipsoid = InternationalEllipsoid.Instance;
      DX = -87;
      DY = -98;
      DZ = -121;
    }
  }
}
