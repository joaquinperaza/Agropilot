using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Mexico) datum.
  /// </summary>
  public sealed class NAD27MexicoDatum : Datum<NAD27MexicoDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27MexicoDatum"/> class.
    /// </summary>
    public NAD27MexicoDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Mexico";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -12.0;
      DY = 130.0;
      DZ = 190.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
