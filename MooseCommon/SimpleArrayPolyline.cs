using System;
using Rhino.Geometry;

namespace MooseCommon
{
  public class SimpleArrayPolyline : IDisposable
  {
    private IntPtr m_ptr; // ON_SimpleArray<ON_Polyline*>*

    /// <summary>
    /// Gets the non-const pointer (for modification) of this array.
    /// </summary>
    /// <returns>The non-const pointer.</returns>
    public IntPtr NonConstPointer()
    {
      return m_ptr;
    }

    /// <summary>
    /// Initializes a new <see cref="SimpleArrayPolyline"/> instance.
    /// </summary>
    public SimpleArrayPolyline()
    {
      m_ptr = UnsafeNativeMethods.ON_PolylineArray_New();
    }

    /// <summary>
    /// Gets the amount of polylines in this array.
    /// </summary>
    public int Count
    {
      get { return UnsafeNativeMethods.ON_PolylineArray_Count(m_ptr); }
    }

    /// <summary>
    /// Retrieves a Polyline at a given array index.
    /// </summary>
    /// <param name="index">THe index</param>
    /// <returns>The Polyline if successful, null otherwise.</returns>
    public Polyline Get(int index)
    {
      if (index < 0 || index >= Count)
        return null;

      var points_array = new Rhino.Runtime.InteropWrappers.SimpleArrayPoint3d();
      var ptr_points_array = points_array.NonConstPointer();
      var cnt = UnsafeNativeMethods.ON_PolylineArray_Get(m_ptr, index, ptr_points_array);

      Polyline rc = null;
      if (cnt > 0)
        rc = new Polyline(points_array.ToArray());

      points_array.Dispose();

      return rc;
    }

    /// <summary>
    /// Passively reclaims unmanaged resources when the class user did not explicitly call Dispose().
    /// </summary>
    ~SimpleArrayPolyline()
    {
      InternalDispose();
    }

    /// <summary>
    /// Actively reclaims unmanaged resources that this instance uses.
    /// </summary>
    public void Dispose()
    {
      InternalDispose();
      GC.SuppressFinalize(this);
    }

    private void InternalDispose()
    {
      if (IntPtr.Zero != m_ptr)
      {
        UnsafeNativeMethods.ON_PolylineArray_Delete(m_ptr);
        m_ptr = IntPtr.Zero;
      }
    }
  }
}
