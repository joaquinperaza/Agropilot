namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Hayford 1910 reference ellipsoid.
  /// </summary>
  public sealed class Hayford1910Ellipsoid : Ellipsoid<Hayford1910Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Hayford1910Ellipsoid"/> class.
    /// </summary>
    public Hayford1910Ellipsoid() : base(6378388.0, 6356911.946)
    {
    }
  }
}