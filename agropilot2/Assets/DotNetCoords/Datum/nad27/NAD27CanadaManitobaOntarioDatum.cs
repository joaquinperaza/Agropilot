using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canada Manitoba/Ontario) datum.
  /// </summary>
  public sealed class NAD27CanadaManitobaOntarioDatum : Datum<NAD27CanadaManitobaOntarioDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanadaManitobaOntarioDatum"/> class.
    /// </summary>
    public NAD27CanadaManitobaOntarioDatum()
    {
      Name = "North American Datum 1927 (NAD27) - Canada Manitoba/Ontario";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -9.0;
      DY = 157.0;
      DZ = 184.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
