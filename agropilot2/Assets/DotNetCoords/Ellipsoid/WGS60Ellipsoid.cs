namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the WGS60 reference ellipsoid.
  /// </summary>
  public sealed class WGS60Ellipsoid : Ellipsoid<WGS60Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WGS60Ellipsoid"/> class.
    /// </summary>
    public WGS60Ellipsoid() : base(6378165.0, 6356783.287)
    {
    }
  }
}