namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the WGS84 reference ellipsoid.
  /// </summary>
  public sealed class WGS84Ellipsoid : Ellipsoid<WGS84Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WGS84Ellipsoid"/> class.
    /// </summary>
    public WGS84Ellipsoid() : base(6378137, 6356752.3142)
    {
    }
  }
}