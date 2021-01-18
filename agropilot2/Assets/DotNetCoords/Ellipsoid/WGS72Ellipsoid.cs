namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the WGS72 reference ellipsoid.
  /// </summary>
  public sealed class WGS72Ellipsoid : Ellipsoid<WGS72Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WGS72Ellipsoid"/> class.
    /// </summary>
    public WGS72Ellipsoid() : base(6378135, 6356750.5) 
    {
    }
  }
}