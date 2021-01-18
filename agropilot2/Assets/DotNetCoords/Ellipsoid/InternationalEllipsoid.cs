namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the International 1909 reference ellipsoid.
  /// </summary>
  public sealed class InternationalEllipsoid : Ellipsoid<InternationalEllipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InternationalEllipsoid"/> class.
    /// </summary>
    public InternationalEllipsoid() : base(6378388, 6356911.9462)
    {
    }
  }
}