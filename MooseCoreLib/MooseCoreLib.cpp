/////////////////////////////////////////////////////////////////////////////
// MooseCoreLib.cpp
//

#include "StdAfx.h"
#include "MooseCoreLib.h"

/////////////////////////////////////////////////////////////////////////////
// C++ exports

MOOSECORELIB_CPP_FUNCTION void MooseSdkPrint(const wchar_t* str)
{
  if (str && str[0])
    RhinoApp().Print(L"%s\n", str);
}

MOOSECORELIB_CPP_FUNCTION double MooseSdkSum(double a, double b)
{
  return a + b;
}

MOOSECORELIB_CPP_FUNCTION ON_UUID MooseSdkAddPoint(const ON_3dPoint& point)
{
  ON_UUID object_id = ON_nil_uuid;

  if (point.IsValid())
  {
    CRhinoDoc* doc = RhinoApp().ActiveDoc();
    if (doc)
    {
      CRhinoPointObject* point_obj = doc->AddPointObject(point);
      if (point_obj)
        object_id = point_obj->ModelObjectId();
    }
  }

  return object_id;
}


/////////////////////////////////////////////////////////////////////////////
// .NET Exports

MOOSECORELIB_C_FUNCTION void MoosePrint(const wchar_t* str)
{
  return MooseSdkPrint(str);
}

MOOSECORELIB_C_FUNCTION double MooseSum(double a, double b)
{
  return MooseSdkSum(a, b);
}

MOOSECORELIB_C_FUNCTION ON_UUID MooseAddPoint(ON_3DPOINT_STRUCT point)
{
  const ON_3dPoint* _point = (const ON_3dPoint*)&point;
  return MooseSdkAddPoint(*_point);
}

MOOSECORELIB_C_FUNCTION int MooseFunction(const ON_Brep* pConstBrep, int x, int y, ON_3dPointArray* pPoints, ON_SimpleArray<ON_Line>* pLines)
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

MOOSECORELIB_C_FUNCTION int MooseFunction2(const ON_Brep* pConstBrep, int x, int y, int point_count, /*ARRAY*/const ON_3dPoint* pConstPoints, ON_SimpleArray<ON_Line>* pLines)
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

MOOSECORELIB_C_FUNCTION ON_Brep* MooseFunction3()
{
  const double d = 10.0;
  ON_Circle circle(ON_Plane::World_xy, d);
  ON_Cylinder cylinder(circle, d);
  ON_Brep* brep = ON_BrepCylinder(cylinder, true, true);
  return brep;
}

MOOSECORELIB_C_FUNCTION int MoooseGetPolylines(ON_SimpleArray<ON_Polyline*>* pArray)
{
  int rc = 0;
  if (pArray)
  {
    ON_Polyline* p0 = new ON_Polyline();
    p0->Append(ON_3dPoint(0, 0, 0));
    p0->Append(ON_3dPoint(5, 0, 0));
    p0->Append(ON_3dPoint(5, 5, 0));
    p0->Append(ON_3dPoint(0, 5, 0));

    ON_Polyline* p1 = new ON_Polyline();
    p1->Append(ON_3dPoint(0, 0, 0));
    p1->Append(ON_3dPoint(15, 0, 0));
    p1->Append(ON_3dPoint(15, 15, 0));
    p1->Append(ON_3dPoint(0, 15, 0));

    ON_Polyline* p2 = new ON_Polyline();
    p2->Append(ON_3dPoint(0, 0, 0));
    p2->Append(ON_3dPoint(25, 0, 0));
    p2->Append(ON_3dPoint(25, 25, 0));
    p2->Append(ON_3dPoint(0, 25, 0));

    pArray->Append(p0);
    pArray->Append(p1);
    pArray->Append(p2);

    rc = pArray->Count();
  }
  return rc;
}

MOOSECORELIB_C_FUNCTION int MooseGetCurves(ON_SimpleArray<ON_Curve*>* pCurveArray)
{
  int rc = 0;
  if (pCurveArray)
  {
    ON_LineCurve* pLine0 = new ON_LineCurve(ON_3dPoint(0, 0, 0), ON_3dPoint(10, 0, 0));
    pCurveArray->Append(pLine0);

    ON_LineCurve* pLine1 = new ON_LineCurve(ON_3dPoint(10, 0, 0), ON_3dPoint(10, 10, 0));
    pCurveArray->Append(pLine1);

    ON_LineCurve* pLine2 = new ON_LineCurve(ON_3dPoint(10, 10, 0), ON_3dPoint(0, 10, 0));
    pCurveArray->Append(pLine2);

    ON_LineCurve* pLine3 = new ON_LineCurve(ON_3dPoint(0, 10, 0), ON_3dPoint(0, 0, 0));
    pCurveArray->Append(pLine3);

    rc = pCurveArray->Count();
  }
  return rc;
}


MOOSECORELIB_C_FUNCTION ON_SimpleArray<ON_Polyline*>* ON_PolylineArray_New()
{
  return new ON_SimpleArray<ON_Polyline*>(0);
}

MOOSECORELIB_C_FUNCTION int ON_PolylineArray_Count(ON_SimpleArray<ON_Polyline*>* pArray)
{
  int rc = 0;
  if (pArray)
    rc = pArray->Count();
  return rc;
}

MOOSECORELIB_C_FUNCTION int ON_PolylineArray_Get(ON_SimpleArray<ON_Polyline*>* pArray, int index, ON_3dPointArray* pPoints)
{
  int rc = 0;
  if (pArray && pPoints && 0 <= index && index < pArray->Count())
  {
    ON_Polyline* pPolyline = (*pArray)[index];
    if (pPolyline)
    {
      // Copy point values
      pPoints->Append(pPolyline->Count(), pPolyline->Array());
      rc = pPoints->Count();
    }
  }
  return rc;
}

MOOSECORELIB_C_FUNCTION void ON_PolylineArray_Delete(ON_SimpleArray<ON_Polyline*>* pArray)
{
  if (pArray)
  {
    for (int index = 0; index < pArray->Count(); index++)
    {
      ON_Polyline* pPolyline = (*pArray)[index];
      if (pPolyline)
        delete pPolyline;
    }
    delete pArray;
  }
}

MOOSECORELIB_C_FUNCTION
bool ON_MeshTree_IntersectLine(
  const ON_Mesh* pMesh,
  const ON_Line* pLine,
  ON_3dPointArray* pPoints
)
{
  bool rc = false;
  if (pMesh && pLine && pPoints)
  {
    const int points_count = pPoints->Count();
    const ON_MeshTree* pMeshTree = pMesh->MeshTree(true);
    if (pMeshTree)
    {
      ON_SimpleArray<ON_CMX_EVENT> cmx_events;
      const int cmx_count = pMeshTree->IntersectLine(*pLine, cmx_events);
      for (int i = 0; i < cmx_count; i++)
      {
        const ON_CMX_EVENT& cmx = cmx_events[i];
        if (cmx.m_type == ON_CMX_EVENT::cmx_point)
          pPoints->Append(cmx.m_M[0].m_P);
      }
    }
    rc = pPoints->Count() > points_count;
  }
  return rc;
}

MOOSECORELIB_C_FUNCTION
int ON_Brep_VertexCount(
  const ON_Brep* pConstBrep
)
{
  int count = 0;
  if (pConstBrep) // always validate...
    count = pConstBrep->m_V.Count();
  return count;
}

MOOSECORELIB_C_FUNCTION
bool ON_NurbsCurve_Inspect(
  const ON_NurbsCurve* pConstCurve,
  int* pPointCount,
  int* pKnotCount
)
{
  bool rc = false;
  if (pConstCurve && pPointCount && pKnotCount)
  {
    *pPointCount = pConstCurve->CVCount();
    *pKnotCount = pConstCurve->KnotCount();
    rc = true;
  }
  return rc;
}

MOOSECORELIB_C_FUNCTION
ON_Mesh* MooseCreateMesh()
{
  const int vertex_count = 12;
  const int face_count = 30;

  ON_Mesh* mesh = new ON_Mesh(vertex_count, face_count, false, false);

  const double c = (1 + sqrt(5)) / 4;

  int vi = 0;
  mesh->SetVertex(vi++, ON_3dPoint(0.5, 0.0, c));
  mesh->SetVertex(vi++, ON_3dPoint(0.5, 0.0, -c));
  mesh->SetVertex(vi++, ON_3dPoint(-0.5, 0.0, c));
  mesh->SetVertex(vi++, ON_3dPoint(-0.5, 0.0, -c));
  mesh->SetVertex(vi++, ON_3dPoint(c, 0.5, 0.0));
  mesh->SetVertex(vi++, ON_3dPoint(c, -0.5, 0.0));
  mesh->SetVertex(vi++, ON_3dPoint(-c, 0.5, 0.0));
  mesh->SetVertex(vi++, ON_3dPoint(-c, -0.5, 0.0));
  mesh->SetVertex(vi++, ON_3dPoint(0.0, c, 0.5));
  mesh->SetVertex(vi++, ON_3dPoint(0.0, c, -0.5));
  mesh->SetVertex(vi++, ON_3dPoint(0.0, -c, 0.5));
  mesh->SetVertex(vi++, ON_3dPoint(0.0, -c, -0.5));

  int fi = 0;
  mesh->SetTriangle(fi++, 0, 2, 10);
  mesh->SetTriangle(fi++, 0, 10, 5);
  mesh->SetTriangle(fi++, 0, 5, 4);
  mesh->SetTriangle(fi++, 0, 4, 8);
  mesh->SetTriangle(fi++, 0, 8, 2);
  mesh->SetTriangle(fi++, 3, 1, 11);
  mesh->SetTriangle(fi++, 3, 11, 7);
  mesh->SetTriangle(fi++, 3, 7, 6);
  mesh->SetTriangle(fi++, 3, 6, 9);
  mesh->SetTriangle(fi++, 3, 9, 1);
  mesh->SetTriangle(fi++, 2, 6, 7);
  mesh->SetTriangle(fi++, 2, 7, 10);
  mesh->SetTriangle(fi++, 10, 7, 11);
  mesh->SetTriangle(fi++, 10, 11, 5);
  mesh->SetTriangle(fi++, 5, 11, 1);
  mesh->SetTriangle(fi++, 5, 1, 4);
  mesh->SetTriangle(fi++, 4, 1, 9);
  mesh->SetTriangle(fi++, 4, 9, 8);
  mesh->SetTriangle(fi++, 8, 9, 6);
  mesh->SetTriangle(fi++, 8, 6, 2);

  mesh->ComputeVertexNormals();
  mesh->Compact();

  if (!mesh->IsValid())
  {
    delete mesh;
    mesh = nullptr;
  }

  return mesh;
}
