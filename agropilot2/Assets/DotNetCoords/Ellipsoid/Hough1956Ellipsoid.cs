namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Hough 1956 reference ellipsoid.
  /// </summary>
  public sealed class Hough1956Ellipsoid : Ellipsoid<Hough1956Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Hough1956Ellipsoid"/> class.
    /// </summary>
    public Hough1956Ellipsoid() : base(6378270.0, 6356794.34)
    {
    }
  }
}