using System;
using Rhino.Geometry;

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
    public static Guid AddPoint(Point3d point)
    {
      return UnsafeNativeMethods.MooseAddPoint(point);
    }

    /// <summary>
    /// Example function
    /// </summary>
    public static int ExampleFunction(Brep brep, int x, int y, out Point3d[] points, out Line[] lines)
    {
      return UnsafeNativeMethods.MooseFunction(brep, x, y, out points, out lines);
    }

    /// <summary>
    /// Another example function
    /// </summary>
    public static Polyline[] ExampleGetPolylines()
    {
      return UnsafeNativeMethods.MooseGetPolylines();
    }
  }
}
