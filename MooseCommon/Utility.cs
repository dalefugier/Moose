using Rhino.Geometry;
using Rhino.Runtime;
using System;
using System.Collections.Generic;

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
      IntPtr const_ptr_brep = Interop.NativeGeometryConstPointer(brep);

      // Creates a ON_3dPointArray wrapper class instance
      Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d points_array = new Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d();
      // Get a non-const point to this class
      IntPtr ptr_points_array = points_array.NonConstPointer();

      // Creates a ON_SimpleArray<ON_Line> wrapper class instance
      Rhino.Runtime.InteropWrappers.SimpleArrayLine lines_array = new Rhino.Runtime.InteropWrappers.SimpleArrayLine();
      // Get a non-const point to this class
      IntPtr ptr_lines_array = lines_array.NonConstPointer();

      int rc = UnsafeNativeMethods.MooseFunction(const_ptr_brep, x, y, ptr_points_array, ptr_lines_array);
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
      IntPtr const_ptr_brep = Interop.NativeGeometryConstPointer(brep);

      List<Point3d> pts = new List<Point3d>(points);
      Point3d[] ptarray = pts.ToArray();

      // Creates a ON_SimpleArray<ON_Line> wrapper class instance
      Rhino.Runtime.InteropWrappers.SimpleArrayLine lines_array = new Rhino.Runtime.InteropWrappers.SimpleArrayLine();
      // Get a non-const point to this class
      IntPtr ptr_lines_array = lines_array.NonConstPointer();

      int rc = UnsafeNativeMethods.MooseFunction2(const_ptr_brep, x, y, ptarray.Length, ptarray, ptr_lines_array);
      lines = rc > 0 ? lines_array.ToArray() : new Line[0];
      lines_array.Dispose();

      return rc;
    }

    /// <summary>
    /// Creates a solid cylinder
    /// </summary>
    public static Brep ExampleFunction3()
    {
      IntPtr ptr = UnsafeNativeMethods.MooseFunction3();
      if (ptr == IntPtr.Zero)
        return null;

      GeometryBase geometry = Interop.CreateFromNativePointer(ptr);
      return geometry as Brep;
    }

    /// <summary>
    /// Demonstrates using a custom inteop helper class
    /// </summary>
    public static Polyline[] ExampleGetPolylines()
    {
      SimpleArrayPolyline array = new SimpleArrayPolyline();
      IntPtr ptr_array = array.NonConstPointer();

      int count = UnsafeNativeMethods.MoooseGetPolylines(ptr_array);
      if (count == 0)
      {
        array.Dispose();
        return new Polyline[0];
      }

      List<Polyline> list = new List<Polyline>(count);
      for (int i = 0; i < count; i++)
        list.Add(array.Get(i));

      array.Dispose();

      return list.ToArray();
    }

    /// <summary>
    /// Demonstrates getting a bunch of curves from C++.
    /// </summary>
    public static Curve[] ExampleGetCurves()
    {
      using (Rhino.Runtime.InteropWrappers.SimpleArrayCurvePointer curves = new Rhino.Runtime.InteropWrappers.SimpleArrayCurvePointer())
      {
        IntPtr ptr_curves = curves.NonConstPointer();
        int count = UnsafeNativeMethods.MooseGetCurves(ptr_curves);
        Curve[] rc = (count == 0)
          ? new Curve[0]
          : curves.ToNonConstArray();
        return rc;
      }
    }

    /// <summary>
    /// Demonstrates setting a bunch of curves to C++.
    /// </summary>
    public static int ExampleSetCurves(IEnumerable<Curve> curves)
    {
      using (Rhino.Runtime.InteropWrappers.SimpleArrayCurvePointer curve_array = new Rhino.Runtime.InteropWrappers.SimpleArrayCurvePointer(curves))
      {
        IntPtr ptr_curve_array = curve_array.ConstPointer();
        return UnsafeNativeMethods.MooseSetCurves(ptr_curve_array);
      }
    }

    /// <summary>
    /// Intersects a line with a mesh.
    /// </summary>
    /// <param name="mesh">The mesh.</param>
    /// <param name="line">The line.</param>
    /// <returns>
    /// An array of intersection points of successful, 
    /// or an empty array if not successful.
    /// </returns>
    public static Point3d[] MeshLineIntersection(Mesh mesh, Line line)
    {
      if (null == mesh)
        throw new ArgumentNullException(nameof(mesh));

      using (Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d points_array = new Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d())
      {
        IntPtr ptr_points = points_array.NonConstPointer();
        IntPtr ptr_const_mesh = Interop.NativeGeometryConstPointer(mesh);
        bool rc = UnsafeNativeMethods.ON_MeshTree_IntersectLine(ptr_const_mesh, ref line, ptr_points);
        if (rc)
          return points_array.ToArray();
      }
      return new Point3d[0];
    }

    /// <summary>
    /// Returns the number of vertices in a Brep
    /// </summary>
    public static int BrepVertexCount(Brep brep)
    {
      if (null == brep)
        throw new ArgumentNullException(nameof(brep));

      // Get the native ON_Brep pointer
      IntPtr const_ptr_brep = Interop.NativeGeometryConstPointer(brep);
      return UnsafeNativeMethods.ON_Brep_VertexCount(const_ptr_brep);
    }

    /// <summary>
    /// Inspects a NURBS curve
    /// </summary>
    public static bool NurbsCurveInspect(NurbsCurve curve, out int pointCount, out int knotCount)
    {
      pointCount = 0;
      knotCount = 0;

      if (null == curve)
        throw new ArgumentNullException(nameof(curve));

      // Get the native ON_NurbsCurve pointer
      IntPtr const_ptr_curve = Interop.NativeGeometryConstPointer(curve);
      return UnsafeNativeMethods.ON_NurbsCurve_Inspect(const_ptr_curve, ref pointCount, ref knotCount);
    }

    /// <summary>
    /// Creates a mesh.
    /// </summary>
    /// <returns>The mesh if successfull, null on failure.</returns>
    public static Mesh CreateMesh()
    {
      IntPtr ptr = UnsafeNativeMethods.MooseCreateMesh();
      if (ptr == IntPtr.Zero)
        return null;

      GeometryBase geometry = Interop.CreateFromNativePointer(ptr);
      return geometry as Mesh;
    }
  }
}
