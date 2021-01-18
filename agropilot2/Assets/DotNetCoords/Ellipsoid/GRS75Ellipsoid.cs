namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the GRS75 reference ellipsoid.
  /// </summary>
  public sealed class GRS75Ellipsoid : Ellipsoid<GRS75Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="GRS75Ellipsoid"/> class.
    /// </summary>
    public GRS75Ellipsoid() : base(6378140.0, 6356755.288)
    {
    }
  }
}