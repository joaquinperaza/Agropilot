namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the IERS 1989 reference ellipsoid.
  /// </summary>
  public sealed class IERS1989Ellipsoid : Ellipsoid<IERS1989Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="IERS1989Ellipsoid"/> class.
    /// </summary>
    public IERS1989Ellipsoid() : base(6378136.0, 6356751.302)
    {
    }
  }
}