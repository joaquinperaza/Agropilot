namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the GRS80 reference ellipsoid.
  /// </summary>
  public sealed class GRS80Ellipsoid : Ellipsoid<GRS80Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="GRS80Ellipsoid"/> class.
    /// </summary>
    public GRS80Ellipsoid() : base(6378137, 6356752.3141)
    {
    }
  }
}