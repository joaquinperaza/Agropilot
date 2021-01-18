namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the WGS66 reference ellipsoid.
  /// </summary>
  public sealed class WGS66Ellipsoid : Ellipsoid<WGS66Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WGS66Ellipsoid"/> class.
    /// </summary>
    public WGS66Ellipsoid() : base(6378145.0, 6356759.770)
    {
    }
  }
}