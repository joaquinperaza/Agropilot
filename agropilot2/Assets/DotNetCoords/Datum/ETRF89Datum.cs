using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
  /// <summary>
  /// Class representing the European Terrestrial Reference Frame (ETRF89) datum.
  /// </summary>
  public sealed class ETRF89Datum : Datum<ETRF89Datum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ETRF89Datum"/> class.
    /// </summary>
    public ETRF89Datum() 
    {
      Name = "European Terrestrial Reference Frame (ETRF89)";
      ReferenceEllipsoid = WGS84Ellipsoid.Instance;
      DX = 0.0;
      DY = 0.0;
      DZ = 0.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
