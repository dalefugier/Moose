using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace MooseGh
{
  public class MooseGhInfo : GH_AssemblyInfo
  {
    public override string Name
    {
      get { return "MooseGh"; }
    }

    public override Bitmap Icon
    {
      get { return Properties.Resources.Icon; }
    }

    public override string Description
    {
      get { return "Sample 'Moose' Grasshopper component";
      }
    }

    public override Guid Id
    {
      get { return new Guid("e87661ba-2061-4853-90b5-dd67837143a1"); }
    }

    public override string AuthorName
    {
      get { return "Robert McNeel & Associates"; }
    }

    public override string AuthorContact
    {
      get { return "devsupport@mcneel.com"; }
    }
  }
}
