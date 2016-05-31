using System;
using Grasshopper.Kernel;
using Rhino;

namespace MooseGh
{
  public class MooseGhComponent : GH_Component
  {
    public MooseGhComponent()
      : base("MooseGh", "Moose", "Adds two numbers.", "Maths", "Operators")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_InputParamManager pManager)
    {
      pManager.AddNumberParameter("X Value", "X", "First number to add", GH_ParamAccess.item, 0.0);
      pManager.AddNumberParameter("Y Value", "Y", "second number to add", GH_ParamAccess.item, 0.0);
    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_OutputParamManager pManager)
    {
      pManager.AddNumberParameter("Sum", "S", "Sum", GH_ParamAccess.item);
    }

    protected override void SolveInstance(IGH_DataAccess da)
    {
      var x = 0.0;
      if (!da.GetData(0, ref x)) return;
      if (!RhinoMath.IsValidDouble(x))
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "X Value is invalid");
        return;
      }

      var y = 0.0;
      if (!da.GetData(1, ref y)) return;
      if (!RhinoMath.IsValidDouble(y))
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Y Value is invalid");
        return;
      }

      var sum = MooseCommon.Utility.Sum(x, y);

      da.SetData(0, sum);
    }

    public override GH_Exposure Exposure
    {
      get { return GH_Exposure.primary; }
    }

    protected override System.Drawing.Bitmap Icon
    {
      get { return Properties.Resources.Add; }
    }

    public override Guid ComponentGuid
    {
      get { return new Guid("{f454047f-28e5-4df5-9bba-e0fdffc4ccb1}"); }
    }
  }
}
