using System;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input.Custom;

namespace MooseNet
{
  [System.Runtime.InteropServices.Guid("ef1ada21-a544-4a85-8cfe-822b6201e1cc")]
  public class MooseNetCommand : Command
  {
    public override string EnglishName
    {
      get { return "MooseNet"; }
    }

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
      //  var str = string.Format("Identifier of point object is \"{0}\"", uuid_str );

      //  MooseCommon.Utility.Print(str);
      //}

      var sphere = new Sphere(Point3d.Origin, 5);

      var brep = sphere.ToBrep();
      const int x = 1;
      const int y = 2;
      Point3d[] points;
      Line[] lines;
      var rc = MooseCommon.Utility.ExampleFunction(brep, x, y, out points, out lines);
      if (rc > 0)
      {
        foreach (var p in points)
          doc.Objects.AddPoint(p);
        foreach (var l in lines)
          doc.Objects.AddLine(l);
        doc.Views.Redraw();
      }

      return Result.Success;
    }
  }
}
