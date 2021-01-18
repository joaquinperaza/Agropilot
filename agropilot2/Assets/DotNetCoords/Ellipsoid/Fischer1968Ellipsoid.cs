namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Fischer 1968 reference ellipsoid.
  /// </summary>
  public sealed class Fischer1968Ellipsoid : Ellipsoid<Fischer1968Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Fischer1968Ellipsoid"/> class.
    /// </summary>
    public Fischer1968Ellipsoid() : base(6378150.0, 6356768.337)
    {
    }
  }
}