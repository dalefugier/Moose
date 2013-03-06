using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Rhino;
using Rhino.Geometry;

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
  }
}
