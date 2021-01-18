namespace DotNetCoords.Ellipsoid
{

  /// <summary>
  /// Class defining the Fischer 1960 reference ellipsoid.
  /// </summary>
  public sealed class Fischer1960Ellipsoid : Ellipsoid<Fischer1960Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Fischer1960Ellipsoid"/> class.
    /// </summary>
    public Fischer1960Ellipsoid() : base(6378166.0, 6356784.284)
    {
    }
  }
}