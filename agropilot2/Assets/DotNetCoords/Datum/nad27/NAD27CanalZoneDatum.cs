using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
  /// <summary>
  /// Class representing the NAD27 (Canal Zone) datum.
  /// </summary>
  public sealed class NAD27CanalZoneDatum : Datum<NAD27CanalZoneDatum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NAD27CanalZoneDatum"/> class.
    /// </summary>
    public NAD27CanalZoneDatum() 
    {
      Name = "North American Datum 1927 (NAD27) - Canal Zone";
      ReferenceEllipsoid = Clarke1866Ellipsoid.Instance;
      DX = 0.0;
      DY = 125.0;
      DZ = 201.0;
      DS = 0.0;
      RX = 0.0;
      RY = 0.0;
      RZ = 0.0;
    }
  }
}
