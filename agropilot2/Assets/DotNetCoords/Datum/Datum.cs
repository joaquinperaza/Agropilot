namespace DotNetCoords.Datum
{
  /// <summary>
  /// The Datum class represents a set of parameters for describing a particular
  /// datum, including a name, the reference ellipsoid used and the seven
  /// parameters required to translate co-ordinates in this datum to the WGS84
  /// datum.
  /// </summary>
  public abstract class Datum
  {
    /**
     * Translation along the x-axis for use in 7-parameter Helmert
     * transformations. This value should be used to convert a co-ordinate in a
     * given datum to the WGS84 datum.
     */

    /**
     * Translation along the y-axis for use in 7-parameter Helmert
     * transformations. This value should be used to convert a co-ordinate in a
     * given datum to the WGS84 datum.
     */

    /**
     * Translation along the z-axis for use in 7-parameter Helmert
     * transformations. This value should be used to convert a co-ordinate in a
     * given datum to the WGS84 datum.
     */

    /**
     * Scale factor for use in 7-parameter Helmert transformations. This value
     * should be used to convert a co-ordinate in a given datum to the WGS84
     * datum.
     */

    /**
     * Rotation about the x-axis for use in 7-parameter Helmert transformations.
     * This value should be used to convert a co-ordinate in a given datum to the
     * WGS84 datum.
     */

    /**
     * Rotation about the y-axis for use in 7-parameter Helmert transformations.
     * This value should be used to convert a co-ordinate in a given datum to the
     * WGS84 datum.
     */

    /**
     * Rotation about the z-axis for use in 7-parameter Helmert transformations.
     * This value should be used to convert a co-ordinate in a given datum to the
     * WGS84 datum.
     */


    /// <summary>
    /// Get the name of this Datum.
    /// </summary>
    /// <value>The name of this Datum.</value>
    public string Name { get; internal set; }

    /// <summary>
    /// Get the reference ellipsoid associated with this Datum.
    /// </summary>
    /// <value>The reference ellipsoid associated with this Datum.</value>
    public Ellipsoid.Ellipsoid ReferenceEllipsoid { get; internal set; }

    /// <summary>
    /// Gets the scaling factor used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The scaling factor.</value>
    public double DS { get; internal set; }

    /// <summary>
    /// Gets the x parameter of the translation vector used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The x parameter of the translation vector.</value>
    public double DX { get; internal set; }

    /// <summary>
    /// Gets the y parameter of the translation vector used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The y parameter of the translation vector.</value>
    public double DY { get; internal set; }

    /// <summary>
    /// Gets the z parameter of the translation vector used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The z parameter of the translation vector.</value>
    public double DZ { get; internal set; }

    /// <summary>
    /// Gets the x parameter of the rotation matrix used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The x parameter of the rotation matrix.</value>
    public double RX { get; internal set; }

    /// <summary>
    /// Gets the y parameter of the rotation matrix used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The y parameter of the rotation matrix.</value>
    public double RY { get; internal set; }

    /// <summary>
    /// Gets the z parameter of the rotation matrix used by the Helmert Transformation when converting between datums.
    /// </summary>
    /// <value>The z parameter of the rotation matrix.</value>
    public double RZ { get; internal set; }


    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the parameters of the current Datum object.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the parameters of the current Datum object.
    /// </returns>
    public override string ToString()
    {
      return Name + " " + ReferenceEllipsoid + " dx=" + DX + " dy=" + DY
          + " dz=" + DZ + " ds=" + DS + " rx=" + RX + " ry=" + RY + " rz=" + RZ;
    }
  }

  /// <summary>
  /// Generic datum class represents a set of parameters for describing a particular
  /// datum, including a name, the reference ellipsoid used and the seven
  /// parameters required to translate co-ordinates in this datum to the WGS84
  /// datum. 
  /// </summary>
  /// <typeparam name="T">The type of the datum</typeparam>
  public abstract class Datum<T> : Datum where T : Datum, new()
  {
    private static T _reference;

    /// <summary>
    /// Get the static instance of this datum.
    /// </summary>
    /// <value>A reference to the static instance of this datum.</value>
    public static T Instance
    {
      get
      {
        if (_reference == null)
        {
          _reference = new T();
        }
        return _reference;
      }
    }
  }
}
