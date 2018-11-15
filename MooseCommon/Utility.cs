using System;
using System.Collections.Generic;
using Rhino.Geometry;
using Rhino.Runtime;

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
      if (null == brep)
        throw new ArgumentNullException(nameof(brep));

      // Get the native ON_Brep pointer
      var const_ptr_brep = Interop.NativeGeometryConstPointer(brep);

      // Creates a ON_3dPointArray wrapper class instance
      var points_array = new Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d();
      // Get a non-const point to this class
      var ptr_points_array = points_array.NonConstPointer();

      // Creates a ON_SimpleArray<ON_Line> wrapper class instance
      var lines_array = new Rhino.Runtime.InteropWrappers.SimpleArrayLine();
      // Get a non-const point to this class
      var ptr_lines_array = lines_array.NonConstPointer();

      var rc = UnsafeNativeMethods.MooseFunction(const_ptr_brep, x, y, ptr_points_array, ptr_lines_array);
      if (rc > 0)
      {
        points = points_array.ToArray();
        lines = lines_array.ToArray();
      }
      else
      {
        points = new Point3d[0];
        lines = new Line[0];
      }

      points_array.Dispose();
      lines_array.Dispose();

      return rc;
    }

    /// <summary>
    /// Calls another more complicated, exported function
    /// </summary>
    public static int ExampleFunction2(Brep brep, int x, int y, IEnumerable<Point3d> points, out Line[] lines)
    {
      if (null == brep)
        throw new ArgumentNullException(nameof(brep));

      // Get the native ON_Brep pointer
      var const_ptr_brep = Interop.NativeGeometryConstPointer(brep);

      var pts = new List<Point3d>(points);
      var ptarray = pts.ToArray();

      // Creates a ON_SimpleArray<ON_Line> wrapper class instance
      var lines_array = new Rhino.Runtime.InteropWrappers.SimpleArrayLine();
      // Get a non-const point to this class
      var ptr_lines_array = lines_array.NonConstPointer();

      var rc = UnsafeNativeMethods.MooseFunction2(const_ptr_brep, x, y, ptarray.Length, ptarray, ptr_lines_array);
      lines = rc > 0 ? lines_array.ToArray() : new Line[0];
      lines_array.Dispose();

      return rc;
    }

    /// <summary>
    /// Demonstrates using a custom inteop helper class
    /// </summary>
    public static Polyline[] ExampleGetPolylines()
    {
      var array = new SimpleArrayPolyline();
      var ptr_array = array.NonConstPointer();

      var count = UnsafeNativeMethods.MoooseGetPolylines(ptr_array);
      if (count == 0)
      {
        array.Dispose();
        return new Polyline[0];
      }

      var list = new List<Polyline>(count);
      for (var i = 0; i < count; i++)
        list.Add(array.Get(i));

      array.Dispose();

      return list.ToArray();
    }
  }
}
