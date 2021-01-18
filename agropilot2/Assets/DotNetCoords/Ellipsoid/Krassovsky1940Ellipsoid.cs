namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Krassovsky 1940 reference ellipsoid.
  /// </summary>
  public sealed class Krassovsky1940Ellipsoid : Ellipsoid<Krassovsky1940Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Krassovsky1940Ellipsoid"/> class.
    /// </summary>
    public Krassovsky1940Ellipsoid() : base(6378245.0, 6356863.019)
    {
    }
  }
}