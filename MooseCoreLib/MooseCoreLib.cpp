/////////////////////////////////////////////////////////////////////////////
// MooseCoreLib.cpp
//

#include "StdAfx.h"
#include "MooseCoreLib.h"

/////////////////////////////////////////////////////////////////////////////
// C++ exports

MOOSECORELIB_CPP_FUNCTION
void MooseSdkPrint( const wchar_t* str )
{
  if( str && str[0] )
    RhinoApp().Print( L"%s\n", str );
}

MOOSECORELIB_CPP_FUNCTION
double MooseSdkSum( double a, double b )
{
  return a + b;
}

MOOSECORELIB_CPP_FUNCTION
ON_UUID MooseSdkAddPoint( const ON_3dPoint& point )
{
  ON_UUID object_id = ON_nil_uuid;
  
  if( point.IsValid() )
  {
    CRhinoDoc* doc = RhinoApp().ActiveDoc();
    if( doc )
    {
      CRhinoPointObject* point_obj = doc->AddPointObject( point );
      if( point_obj )
        object_id = point_obj->ModelObjectId();
    }
  }

  return object_id;
}

/////////////////////////////////////////////////////////////////////////////
// .NET Exports

MOOSECORELIB_C_FUNCTION
void MoosePrint( const wchar_t* str )
{
  return MooseSdkPrint( str );
}

MOOSECORELIB_C_FUNCTION
double MooseSum( double a, double b )
{
  return MooseSdkSum( a, b );
}

MOOSECORELIB_C_FUNCTION
ON_UUID MooseAddPoint( ON_3DPOINT_STRUCT point )
{
  const ON_3dPoint* _point = (const ON_3dPoint*)&point;
  return MooseSdkAddPoint( *_point );
}

MOOSECORELIB_C_FUNCTION
int MooseFunction(const ON_Brep* pConstBrep, int x, int y, ON_3dPointArray* pPoints, ON_SimpleArray<ON_Line>* pLines)
{
  if (pConstBrep && pPoints && pLines)
  {
    // Append some sample data...

    ON_3dPoint p0(0, 0, 0);
    ON_3dPoint p1(5, 0, 0);
    ON_3dPoint p2(5, 5, 0);
    ON_3dPoint p3(0, 5, 0);

    pPoints->Append(p0);
    pPoints->Append(p1);
    pPoints->Append(p2);
    pPoints->Append(p3);

    pLines->Append(ON_Line(p0, p1));
    pLines->Append(ON_Line(p1, p2));
    pLines->Append(ON_Line(p2, p3));
    pLines->Append(ON_Line(p3, p0));

    return pPoints->Count();
  }

  return 0;
}

int MooseFunction2(const ON_Brep* pConstBrep, int x, int y, int point_count, /*ARRAY*/const ON_3dPoint* pConstPoints, ON_SimpleArray<ON_Line>* pLines)
{  
  if (pConstBrep && pConstPoints && pLines)
  {
    ON_3dPointArray points(point_count);
    points.Append(point_count, pConstPoints);
    // TODO: Do someting with points

    // Append some sample data...

    ON_3dPoint p0(0, 0, 0);
    ON_3dPoint p1(5, 0, 0);
    ON_3dPoint p2(5, 5, 0);
    ON_3dPoint p3(0, 5, 0);

    pLines->Append(ON_Line(p0, p1));
    pLines->Append(ON_Line(p1, p2));
    pLines->Append(ON_Line(p2, p3));
    pLines->Append(ON_Line(p3, p0));

    return pLines->Count();
  }

  return 0;

}
