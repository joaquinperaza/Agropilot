namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the GRS67 reference ellipsoid.
  /// </summary>
  public sealed class GRS67Ellipsoid : Ellipsoid<GRS67Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="GRS67Ellipsoid"/> class.
    /// </summary>
    public GRS67Ellipsoid() : base(6378160.0, 6356774.51609) 
    {
    }
  }
}