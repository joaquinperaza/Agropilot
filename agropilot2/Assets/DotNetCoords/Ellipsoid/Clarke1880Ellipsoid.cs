namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Clarke 1880 reference ellipsoid.
  /// </summary>
  public sealed class Clarke1880Ellipsoid : Ellipsoid<Clarke1880Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Clarke1880Ellipsoid"/> class.
    /// </summary>
    public Clarke1880Ellipsoid() : base(6378249.145, 6356514.8696)
    {
    }
  }
}