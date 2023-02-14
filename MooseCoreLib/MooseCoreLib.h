// MooseCoreLib.h : main header file for the MooseCoreLib API.

#pragma once

#if defined(MOOSECORELIB_DLL_EXPORTS)

/* Compiling MooseCoreLib as a Windows DLL - export classes, functions, and globals */
#define MOOSECORELIB_CPP_CLASS __declspec(dllexport)
#define MOOSECORELIB_CPP_FUNCTION __declspec(dllexport)
#define MOOSECORELIB_CPP_DATA __declspec(dllexport)

#define MOOSECORELIB_C_FUNCTION extern "C" __declspec(dllexport)

#else

/* Using MooseCoreLib as a Windows DLL - import classes, functions, and globals */
#define MOOSECORELIB_CPP_CLASS __declspec(dllimport)
#define MOOSECORELIB_CPP_FUNCTION __declspec(dllimport)
#define MOOSECORELIB_CPP_DATA __declspec(dllimport)

#define MOOSECORELIB_C_FUNCTION extern "C" __declspec(dllimport)

#include "MooseCoreLibLinkingPragmas.h"

#endif


/////////////////////////////////////////////////////////////////////////////
// C++ exports

MOOSECORELIB_CPP_FUNCTION
void MooseSdkPrint(const wchar_t* str);

MOOSECORELIB_CPP_FUNCTION
double MooseSdkSum(double a, double b);

MOOSECORELIB_CPP_FUNCTION
ON_UUID MooseSdkAddPoint(const ON_3dPoint& point);


/////////////////////////////////////////////////////////////////////////////
// .NET Exports

struct ON_3DPOINT_STRUCT
{
  double val[3];
};


MOOSECORELIB_C_FUNCTION
void MoosePrint(const wchar_t* str);

MOOSECORELIB_C_FUNCTION
double MooseSum(double a, double b);

MOOSECORELIB_C_FUNCTION
ON_UUID MooseAddPoint(ON_3DPOINT_STRUCT point);

MOOSECORELIB_C_FUNCTION
int MooseFunction(const ON_Brep* pConstBrep, int x, int y, ON_3dPointArray* pPoints, ON_SimpleArray<ON_Line>* pLines);

MOOSECORELIB_C_FUNCTION
int MooseFunction2(const ON_Brep* pConstBrep, int x, int y, int point_count, /*ARRAY*/const ON_3dPoint* pConstPoints, ON_SimpleArray<ON_Line>* pLines);

MOOSECORELIB_C_FUNCTION
ON_Brep* MooseFunction3();

MOOSECORELIB_C_FUNCTION
int MoooseGetPolylines(ON_SimpleArray<ON_Polyline*>* pArray);


MOOSECORELIB_C_FUNCTION
ON_SimpleArray<ON_Polyline*>* ON_PolylineArray_New();

MOOSECORELIB_C_FUNCTION
int ON_PolylineArray_Count(ON_SimpleArray<ON_Polyline*>* pArray);

MOOSECORELIB_C_FUNCTION
int ON_PolylineArray_Get(ON_SimpleArray<ON_Polyline*>* pArray, int index, ON_3dPointArray* pPoints);

MOOSECORELIB_C_FUNCTION
void ON_PolylineArray_Delete(ON_SimpleArray<ON_Polyline*>* pArray);


MOOSECORELIB_C_FUNCTION
bool ON_MeshTree_IntersectLine(const ON_Mesh* pMesh, const ON_Line* pLine, ON_3dPointArray* pPoints);

MOOSECORELIB_C_FUNCTION
int ON_Brep_VertexCount(const ON_Brep* pConstBrep);

MOOSECORELIB_C_FUNCTION
bool ON_NurbsCurve_Inspect(const ON_NurbsCurve* pConstCurve, int* pPointCount, int* pKnotCount);
