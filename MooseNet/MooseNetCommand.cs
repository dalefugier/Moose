using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using MooseCommon;

namespace MooseNet
{
  [System.Runtime.InteropServices.Guid("ef1ada21-a544-4a85-8cfe-822b6201e1cc")]
  public class MooseNetCommand : Command
  {
    public MooseNetCommand()
    {
      // Rhino only creates one instance of each command class defined in a
      // plug-in, so it is safe to store a refence in a static property.
      Instance = this;
    }

    ///<summary>The only instance of this command.</summary>
    public static MooseNetCommand Instance
    {
      get;
      private set;
    }

    ///<returns>The command name as it appears on the Rhino command line.</returns>
    public override string EnglishName
    {
      get { return "MooseNet"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      GetPoint gp = new GetPoint();
      gp.SetCommandPrompt("Location of point object");
      gp.Get();
      if(gp.CommandResult() != Result.Success)
        return gp.CommandResult();

      Point3d point = gp.Point();

      Guid point_id = MooseCommon.Utility.AddPoint(point);
      if (!Guid.Equals(point_id, Guid.Empty))
      {
        doc.Views.Redraw();

        string uuid_str = point_id.ToString();
        string str = string.Format("Identifier of point object is \"{0}\"", uuid_str );

        MooseCommon.Utility.Print(str);
      }

      return Result.Success;
    }
  }
}
