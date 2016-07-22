using System;
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
  }
}
