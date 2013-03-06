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
