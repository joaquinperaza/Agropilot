namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Bessel 1841 reference ellipsoid.
  /// </summary>
  public sealed class Bessel1841Ellipsoid : Ellipsoid<Bessel1841Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Bessel1841Ellipsoid"/> class.
    /// </summary>
    public Bessel1841Ellipsoid() : base(6377397.155, 6356078.9629)
    {
    }
  }
}