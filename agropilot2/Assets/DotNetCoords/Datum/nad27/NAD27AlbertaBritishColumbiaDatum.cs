using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Alberta and British Columbia) datum.
  /// </summary>
  public sealed class NAD27AlbertaBritishColumbiaDatum : Datum<NAD27AlbertaBritishColumbiaDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27AlbertaBritishColumbiaDatum"/> class.
    /// </summary>
    public NAD27AlbertaBritishColumbiaDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Alberta and British Columbia";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = -7.0;
      DY = 162.0;
      DZ = 188.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
