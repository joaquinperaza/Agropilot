namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Modified Everest reference ellipsoid.
  /// </summary>
  public sealed class ModifiedEverestEllipsoid : Ellipsoid<ModifiedEverestEllipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ModifiedEverestEllipsoid"/> class.
    /// </summary>
    public ModifiedEverestEllipsoid() : base(6377304.063, 6356103.039)
    {
    }
  }
}