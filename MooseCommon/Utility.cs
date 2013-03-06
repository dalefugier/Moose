using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooseCommon
{
  public class Utility
  {
    /// <summary>
    /// Prints a message to the Rhino command line.
    /// </summary>
    public static void Print(string str)
    {
      UnsafeNativeMethods.MoosePrint(str);
    }

    /// <summary>
    /// Sums two numbers
    /// </summary>
    public static double Sum(double a, double b)
    {
      return UnsafeNativeMethods.MooseSum(a, b);
    }

    /// <summary>
    /// Adds a point to the Rhino document
    /// </summary>
    public static Guid AddPoint(Rhino.Geometry.Point3d point)
    {
      return UnsafeNativeMethods.MooseAddPoint(point);
    }
  }
}
