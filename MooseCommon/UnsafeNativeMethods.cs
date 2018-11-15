using System;
using System.Runtime.InteropServices;
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
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void MoosePrint([MarshalAs(UnmanagedType.LPWStr)]string str);

    /// <summary>
    /// Sums two numbers
    /// </summary>
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern double MooseSum(double a, double b);

    /// <summary>
    /// Adds a point to the Rhino document
    /// </summary>
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern Guid MooseAddPoint(Point3d point);

    /// <summary>
    /// Calls a more complicated, exported function
    /// </summary>
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MooseFunction(IntPtr pConstBrep, int x, int y, IntPtr pPoints, IntPtr pLines);

    /// <summary>
    /// Calls another more complicated, exported function
    /// </summary>
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MooseFunction2(IntPtr pConstBrep, int x, int y, int pointCount, Point3d[] pPoints, IntPtr pLines);

    /// <summary>
    /// Demonstrates using a custom inteop helper class
    /// </summary>
    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MoooseGetPolylines(IntPtr pArray);

    #region SimpleArrayPolyline helpers

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr ON_PolylineArray_New();

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ON_PolylineArray_Count(IntPtr pArray);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ON_PolylineArray_Get(IntPtr pArray, int index, IntPtr pPoints);

    [DllImport("MooseCoreLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ON_PolylineArray_Delete(IntPtr pArray);

    #endregion
  }
}
