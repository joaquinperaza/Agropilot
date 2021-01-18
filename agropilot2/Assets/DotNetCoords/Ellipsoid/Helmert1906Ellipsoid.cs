namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Helmert 1906 reference ellipsoid.
  /// </summary>
  public sealed class Helmert1906Ellipsoid : Ellipsoid<Helmert1906Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Helmert1906Ellipsoid"/> class.
    /// </summary>
    public Helmert1906Ellipsoid() : base(6378200.0, 6356818.17)
    {
    }
  }
}