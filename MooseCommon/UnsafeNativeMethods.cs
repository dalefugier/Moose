using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Rhino.Geometry;
using Rhino.Runtime;

namespace MooseCommon
{
  /// <summary>
  /// Unsafe native methods
  /// </summary>
  internal static class UnsafeNativeMethods
  {
    /// <summary>
    /// Prints a message to the Rhino command line.
    /// </summary>
    public static void MoosePrint(string s)
    {
      if (Environment.Is64BitProcess)
        UnsafeNativeMethods64.MoosePrint(s);
      else
        UnsafeNativeMethods32.MoosePrint(s);
    }

    /// <summary>
    /// Sums two numbers
    /// </summary>
    public static double MooseSum(double a, double b)
    {
      if (Environment.Is64BitProcess)
        return UnsafeNativeMethods64.MooseSum(a, b);
      else
        return UnsafeNativeMethods32.MooseSum(a, b);
    }

    /// <summary>
    /// Adds a point to the Rhino document
    /// </summary>
    public static Guid MooseAddPoint(Point3d point)
    {
      if (Environment.Is64BitProcess)
        return UnsafeNativeMethods64.MooseAddPoint(point);
      else
        return UnsafeNativeMethods32.MooseAddPoint(point);
    }

    /// <summary>
    /// Calls a more complicated, exported function
    /// </summary>
    public static int MooseFunction(Brep brep, int x, int y, out Point3d[] points, out Line[] lines)
    {
      if (null == brep)
        throw new ArgumentNullException("brep");

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

      var rc = Environment.Is64BitProcess ?
        UnsafeNativeMethods64.MooseFunction(const_ptr_brep, x, y, ptr_points_array, ptr_lines_array) :
        UnsafeNativeMethods32.MooseFunction(const_ptr_brep, x, y, ptr_points_array, ptr_lines_array);

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
    public static int MooseFunction2(Brep brep, int x, int y, IEnumerable<Point3d> points, out Line[] lines)
    {
      if (null == brep)
        throw new ArgumentNullException("brep");

      // Get the native ON_Brep pointer
      var const_ptr_brep = Interop.NativeGeometryConstPointer(brep);

      var pts = new List<Point3d>(points);
      var ptarray = pts.ToArray();

      // Creates a ON_SimpleArray<ON_Line> wrapper class instance
      var lines_array = new Rhino.Runtime.InteropWrappers.SimpleArrayLine();
      // Get a non-const point to this class
      var ptr_lines_array = lines_array.NonConstPointer();

      var rc = Environment.Is64BitProcess ?
        UnsafeNativeMethods64.MooseFunction2(const_ptr_brep, x, y, ptarray.Length, ptarray, ptr_lines_array) :
        UnsafeNativeMethods32.MooseFunction2(const_ptr_brep, x, y, ptarray.Length, ptarray, ptr_lines_array);

      lines = rc > 0 ? lines_array.ToArray() : new Line[0];
      lines_array.Dispose();

      return rc;
    }

    /// <summary>
    /// Demonstrates using a custom inteop helper class
    /// </summary>
    /// <returns></returns>
    public static Polyline[] MooseGetPolylines()
    {
      var array = new SimpleArrayPolyline();
      var ptr_array = array.NonConstPointer();

      int count = Environment.Is64BitProcess ?
        UnsafeNativeMethods64.MoooseGetPolylines(ptr_array) :
        UnsafeNativeMethods32.MoooseGetPolylines(ptr_array);

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


    #region SimpleArrayPolyline helpers
    public static IntPtr ON_PolylineArray_New()
    {
      return Environment.Is64BitProcess ?
        UnsafeNativeMethods64.ON_PolylineArray_New() :
        UnsafeNativeMethods32.ON_PolylineArray_New();
    }

    public static int ON_PolylineArray_Count(IntPtr ptrArray)
    {
      return Environment.Is64BitProcess ?
        UnsafeNativeMethods64.ON_PolylineArray_Count(ptrArray) :
        UnsafeNativeMethods32.ON_PolylineArray_Count(ptrArray);
    }

    public static int ON_PolylineArray_Get(IntPtr ptrArray, int index, IntPtr ptrPoints)
    {
      return Environment.Is64BitProcess ?
        UnsafeNativeMethods64.ON_PolylineArray_Get(ptrArray, index, ptrPoints) :
        UnsafeNativeMethods32.ON_PolylineArray_Get(ptrArray, index, ptrPoints);
    }

    public static int ON_PolylineArray_Delete(IntPtr ptrArray)
    {
      return Environment.Is64BitProcess ?
        UnsafeNativeMethods64.ON_PolylineArray_Delete(ptrArray) :
        UnsafeNativeMethods32.ON_PolylineArray_Delete(ptrArray);
    }
    #endregion
  }

  /// <summary>
  /// 64-bit unsafe native methods
  /// </summary>
  internal static class UnsafeNativeMethods64
  {
    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MoosePrint([MarshalAs(UnmanagedType.LPWStr)]string str);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern double MooseSum(double a, double b);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern Guid MooseAddPoint(Point3d point);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MooseFunction(IntPtr pConstBrep, int x, int y, IntPtr pPoints, IntPtr pLines);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MooseFunction2(IntPtr pConstBrep, int x, int y, int pointCount, Point3d[] pPoints, IntPtr pLines);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MoooseGetPolylines(IntPtr pArray);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr ON_PolylineArray_New();

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Count(IntPtr pArray);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Get(IntPtr pArray, int index, IntPtr pPoints);

    [DllImport("MooseCoreLib_x64.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Delete(IntPtr pArray);
  }

  /// <summary>
  /// 32-bit unsafe native methods
  /// </summary>
  internal static class UnsafeNativeMethods32
  {
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MoosePrint([MarshalAs(UnmanagedType.LPWStr)]string str);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern double MooseSum(double a, double b);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern Guid MooseAddPoint(Point3d point);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MooseFunction(IntPtr pConstBrep, int x, int y, IntPtr pPoints, IntPtr pLines);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MooseFunction2(IntPtr pConstBrep, int x, int y, int pointCount, Point3d[] pPoints, IntPtr pLines);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MoooseGetPolylines(IntPtr pArray);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr ON_PolylineArray_New();

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Count(IntPtr pArray);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Get(IntPtr pArray, int index, IntPtr pPoints);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int ON_PolylineArray_Delete(IntPtr pArray);
  }
}
