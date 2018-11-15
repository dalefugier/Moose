using System;
using System.Drawing;
using System.Reflection;
using Grasshopper.Kernel;

namespace MooseGh
{
  public class MooseGhInfo : GH_AssemblyInfo
  {
    public override string Name => "MooseGh";

    public override Bitmap Icon
    {
      get
      {
        const string resource = "MooseGh.Resources.MooseGh.ico";
        var size = new Size(24, 24);
        var assembly = Assembly.GetExecutingAssembly();
        var icon = Rhino.UI.DrawingUtilities.IconFromResource(resource, size, assembly);
        return icon.ToBitmap();
      }
    }

    public override string Description => "MooseGh component for Grasshopper";
    public override Guid Id => new Guid("22ea6797-5721-4f9f-a110-8991bd8b5362");
    public override string AuthorName => "Robert McNeel & Associates";
    public override string AuthorContact => "https://github.com/dalefugier/Moose";
  }
}
