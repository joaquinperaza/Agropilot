namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class defining the Modified Airy reference ellipsoid.
  /// </summary>
  public sealed class ModifiedAiryEllipsoid : Ellipsoid<ModifiedAiryEllipsoid>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ModifiedAiryEllipsoid" /> class.
    /// </summary>
    public ModifiedAiryEllipsoid() : base(6377340.189, double.NaN, 0.00667054015)
    {
    }
  }
}
