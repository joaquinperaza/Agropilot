namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Everest 1830 reference ellipsoid.
  /// </summary>
  public sealed class EverestEllipsoid : Ellipsoid<EverestEllipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="EverestEllipsoid"/> class.
    /// </summary>
    public EverestEllipsoid() : base(6377276.34518, 6356075.41511) 
    {
    }
  }
}