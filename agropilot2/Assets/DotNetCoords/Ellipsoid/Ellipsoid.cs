using System;

namespace DotNetCoords.Ellipsoid
{
  /// <summary>
  /// Class to represent a reference ellipsoid.
  /// </summary>
  public abstract class Ellipsoid
  {

    /**
     * Semi major axis.
     */

    /**
     * Semi minor axis.
     */

    /**
     * Eccentricity squared.
     */

    /**
     * Flattening.
     */


    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipsoid"/> class.
    /// </summary>
    /// <param name="semiMajorAxis">The semi major axis.</param>
    /// <param name="semiMinorAxis">The semi minor axis.</param>
    protected Ellipsoid(double semiMajorAxis, double semiMinorAxis) 
    {
      SemiMajorAxis = semiMajorAxis;
      SemiMinorAxis = semiMinorAxis;
      var semiMajorAxisSquared = semiMajorAxis * semiMajorAxis;
      var semiMinorAxisSquared = semiMinorAxis * semiMinorAxis;
      Flattening = (semiMajorAxis - semiMinorAxis) / semiMajorAxis;
      EccentricitySquared = (semiMajorAxisSquared - semiMinorAxisSquared)
          / semiMajorAxisSquared;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipsoid"/> class. If either the
    /// semiMinorAxis or the eccentricitySquared are Double.NaN, then that value is
    /// calculated from the other two parameters. An ArgumentException is
    /// thrown if both the semiMinorAxis and the eccentricitySquared are
    /// Double.NaN.
    /// </summary>
    /// <param name="semiMajorAxis">The semi major axis.</param>
    /// <param name="semiMinorAxis">The semi minor axis.</param>
    /// <param name="eccentricitySquared">The eccentricity squared.</param>
    /// <exception cref="ArgumentException" />
    protected Ellipsoid(double semiMajorAxis, double semiMinorAxis,
        double eccentricitySquared) 
    {
      if (double.IsNaN(semiMinorAxis) && double.IsNaN(eccentricitySquared)) {
        throw new ArgumentException(
            "At least one of semiMinorAxis and eccentricitySquared must be defined");
      }

      SemiMajorAxis = semiMajorAxis;
      var semiMajorAxisSquared = semiMajorAxis * semiMajorAxis;

      if (double.IsNaN(semiMinorAxis)) {
        SemiMinorAxis = Math.Sqrt(semiMajorAxisSquared
            * (1 - eccentricitySquared));
      } else {
        SemiMinorAxis = semiMinorAxis;
      }

      var semiMinorAxisSquared = SemiMinorAxis * SemiMinorAxis;

      Flattening = (SemiMajorAxis - SemiMinorAxis) / SemiMajorAxis;

      if (double.IsNaN(eccentricitySquared)) {
        EccentricitySquared = (semiMajorAxisSquared - semiMinorAxisSquared)
            / semiMajorAxisSquared;
      } else {
        EccentricitySquared = eccentricitySquared;
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current Ellipsoid.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current Ellipsoid.
    /// </returns>
    public override string ToString() 
    {
      return "[semi-major axis = " + SemiMajorAxis + ", semi-minor axis = "
          + SemiMinorAxis + "]";
    }

    /// <summary>
    /// Gets the eccentricity squared.
    /// </summary>
    /// <value>The eccentricity squared.</value>
    public double EccentricitySquared { get; }

    /// <summary>
    /// Gets the flattening.
    /// </summary>
    /// <value>The flattening.</value>
    public double Flattening { get; }

    /// <summary>
    /// Gets the semi major axis.
    /// </summary>
    /// <value>The semi major axis.</value>
    public double SemiMajorAxis { get; }

    /// <summary>
    /// Gets the semi minor axis.
    /// </summary>
    /// <value>The semi minor axis.</value>
    public double SemiMinorAxis { get; }
  }

  /// <summary>
  /// Generic class to represent a reference ellipsoid.
  /// </summary>
  /// <typeparam name="T">The type of the ellipsoid</typeparam>
  public abstract class Ellipsoid<T> : Ellipsoid where T : Ellipsoid, new()
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipsoid&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="semiMajorAxis">The semi major axis.</param>
    /// <param name="semiMinorAxis">The semi minor axis.</param>
    protected Ellipsoid(double semiMajorAxis, double semiMinorAxis) : base(semiMajorAxis, semiMinorAxis)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipsoid&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="semiMajorAxis">The semi major axis.</param>
    /// <param name="semiMinorAxis">The semi minor axis.</param>
    /// <param name="eccentricitySquared">The eccentricity squared.</param>
    protected Ellipsoid(double semiMajorAxis, double semiMinorAxis,
        double eccentricitySquared)
      : base(semiMajorAxis, semiMinorAxis, eccentricitySquared)
    {
    }

    private static T _reference;

    /// <summary>
    /// Get the static instance of this ellipsoid.
    /// </summary>
    /// <value>A reference to the static instance of this ellipsoid.</value>
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