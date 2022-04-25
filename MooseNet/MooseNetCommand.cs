using System;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input.Custom;

namespace MooseNet
{
  public class MooseNetCommand : Command
  {
    public MooseNetCommand()
    {
      // Rhino only creates one instance of each command class defined in a
      // plug-in, so it is safe to store a refence in a static property.
      Instance = this;
    }

    /// <summary>
    /// The only instance of this command.
    /// </summary>
    public static MooseNetCommand Instance
    {
      get; private set;
    }

    /// <returns>
    /// The command name as it appears on the Rhino command line.
    /// </returns>
    public override string EnglishName => "MooseNet";

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      //var gp = new GetPoint();
      //gp.SetCommandPrompt("Location of point object");
      //gp.Get();
      //if (gp.CommandResult() != Result.Success)
      //  return gp.CommandResult();

      //var point = gp.Point();

      //var point_id = MooseCommon.Utility.AddPoint(point);
      //if (!Equals(point_id, Guid.Empty))
      //{
      //  doc.Views.Redraw();

      //  var uuid_str = point_id.ToString();
      //  var str = $"Identifier of point object is \"{uuid_str}\"";
      //  MooseCommon.Utility.Print(str);
      //}


      //var sphere = new Sphere(Point3d.Origin, 5);
      //var brep = sphere.ToBrep();
      //const int x = 1;
      //const int y = 2;
      //var rc = MooseCommon.Utility.ExampleFunction(brep, x, y, out var points, out var lines);
      //if (rc > 0)
      //{
      //  foreach (var p in points)
      //    doc.Objects.AddPoint(p);
      //  foreach (var l in lines)
      //    doc.Objects.AddLine(l);
      //}

      //var polylines = MooseCommon.Utility.ExampleGetPolylines();
      //foreach (var pline in polylines)
      //  doc.Objects.AddPolyline(pline);

      //var cylinder_brep = MooseCommon.Utility.ExampleFunction3();
      //if (null != cylinder_brep)
      //  doc.Objects.AddBrep(cylinder_brep);

      var sphere = new Sphere(Plane.WorldXY, 5.0);
      var mesh = Mesh.CreateQuadSphere(sphere, 4);

      var line = new Line(new Point3d(-10, 0, 0), new Point3d(10, 0, 0));

      var points = MooseCommon.Utility.MeshLineIntersection(mesh, line);
      foreach (var pt in points)
        doc.Objects.AddPoint(pt);


      doc.Views.Redraw();

      return Result.Success;
    }
  }
}
