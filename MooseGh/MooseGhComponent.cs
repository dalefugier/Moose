using System;
using System.Drawing;
using System.Reflection;
using Grasshopper.Kernel;
using Rhino;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace MooseGh
{
  public class MooseGhComponent : GH_Component
  {
    /// <summary>
    /// Each implementation of GH_Component must provide a public 
    /// constructor without any arguments.
    /// Category represents the Tab in which the component will appear, 
    /// Subcategory the panel. If you use non-existing tab or panel names, 
    /// new tabs/panels will automatically be created.
    /// </summary>
    public MooseGhComponent()
      : base("MooseGh", "MooseGh",
          "Adds two numbers",
          "Maths", "Operators")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_InputParamManager input)
    {
      input.AddNumberParameter("X Value", "X", "First number to add", GH_ParamAccess.item, 0.0);
      input.AddNumberParameter("Y Value", "Y", "second number to add", GH_ParamAccess.item, 0.0);
    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_OutputParamManager output)
    {
      output.AddNumberParameter("Sum", "S", "Sum", GH_ParamAccess.item);
    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    protected override void SolveInstance(IGH_DataAccess data)
    {
      var x = 0.0;
      if (!data.GetData(0, ref x))
        return;

      if (!RhinoMath.IsValidDouble(x))
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "X Value is invalid");
        return;
      }

      var y = 0.0;
      if (!data.GetData(1, ref y))
        return;

      if (!RhinoMath.IsValidDouble(y))
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Y Value is invalid");
        return;
      }

      var sum = MooseCommon.Utility.Sum(x, y);

      data.SetData(0, sum);
    }

    public override GH_Exposure Exposure => GH_Exposure.primary;

    protected override Bitmap Icon
    {
      get
      {
        const string resource = "MooseGh.Resources.Add.ico";
        var size = new Size(24, 24);
        var assembly = Assembly.GetExecutingAssembly();
        var icon = Rhino.UI.DrawingUtilities.IconFromResource(resource, size, assembly);
        return icon.ToBitmap();
      }
    }

    public override Guid ComponentGuid => new Guid("9cc586db-becb-4abb-ba4c-e51e14b19680");
  }
}
