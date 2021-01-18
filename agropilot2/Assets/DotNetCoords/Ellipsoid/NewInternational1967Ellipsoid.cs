namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the New International 1967 reference ellipsoid.
  /// </summary>
  public sealed class NewInternational1967Ellipsoid : Ellipsoid<NewInternational1967Ellipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewInternational1967Ellipsoid"/> class.
    /// </summary>
    public NewInternational1967Ellipsoid() : base(6378157.5, 6356772.2)
    {
    }
  }
}