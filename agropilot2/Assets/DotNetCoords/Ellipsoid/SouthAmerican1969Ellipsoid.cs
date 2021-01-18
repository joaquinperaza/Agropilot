namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the South American 1969 reference ellipsoid.
  /// </summary>
  public sealed class SouthAmerican1969Ellipsoid : Ellipsoid<SouthAmerican1969Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SouthAmerican1969Ellipsoid"/> class.
    /// </summary>
    public SouthAmerican1969Ellipsoid()
      : base(6378160.0, 6356774.7192)
    {
    }
  }
}