using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
  /// <summary>
  /// Class representing the Ireland 1965 datum.
  /// </summary>
  public sealed class Ireland1965Datum : Datum<Ireland1965Datum>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Ireland1965Datum"/> class.
    /// </summary>
    public Ireland1965Datum()
    {
      Name = "Ireland 1965";
      ReferenceEllipsoid = ModifiedAiryEllipsoid.Instance;
      DX = 482.53;
      DY = -130.596;
      DZ = 564.557;
      DS = 8.15;
      RX = -1.042;
      RY = -0.214;
      RZ = -0.631;
    }
  }
}
