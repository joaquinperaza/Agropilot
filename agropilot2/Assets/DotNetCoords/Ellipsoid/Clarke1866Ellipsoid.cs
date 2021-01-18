namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Clarke 1866 reference ellipsoid.
  /// </summary>
  public sealed class Clarke1866Ellipsoid : Ellipsoid<Clarke1866Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Clarke1866Ellipsoid"/> class.
    /// </summary>
    public Clarke1866Ellipsoid() : base(6378206.4, 6356583.8)
    {
    }
  }
}