using System;

namespace DotNetCoords
{
  internal static class Util
  {
    /**
     * Calculate sin^2(x).
     * 
     * @param x
     *          x
     * @return sin^2(x)
     * @since 1.0
     */
    internal static double SinSquared(double x)
    {
      return Math.Sin(x) * Math.Sin(x);
    }

    /**
     * Calculate sin^3(x).
     * 
     * @param x
     *          x
     * @return sin^3(x)
     * @since 1.1
     */
    internal static double SinCubed(double x)
    {
      return SinSquared(x) * Math.Sin(x);
    }


    /**
     * Calculate cos^2(x).
     * 
     * @param x
     *          x
     * @return cos^2(x)
     * @since 1.0
     */

    private static double CosSquared(double x)
    {
      return Math.Cos(x) * Math.Cos(x);
    }


    /**
     * Calculate cos^3(x).
     * 
     * @param x
     *          x
     * @return cos^3(x)
     * @since 1.1
     */
    internal static double CosCubed(double x)
    {
      return CosSquared(x) * Math.Cos(x);
    }


    /**
     * Calculate tan^2(x).
     * 
     * @param x
     *          x
     * @return tan^2(x)
     * @since 1.0
     */
    internal static double TanSquared(double x)
    {
      return Math.Tan(x) * Math.Tan(x);
    }


    /**
     * Calculate sec(x).
     * 
     * @param x
     *          x
     * @return sec(x)
     * @since 1.0
     */
    internal static double Sec(double x)
    {
      return 1.0 / Math.Cos(x);
    }

    internal static double ToRadians(double val) 
    { 
      return val * (Math.PI / 180); 
    }

    internal static double ToDegrees(double val)
    {
      return val * (180 / Math.PI);
    }
  }
}
