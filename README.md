Moose
=====

Moose is a Rhino 5 solution that demonstrates how to share a common C++ library between a Rhino C++ plug-in and a RhinoCommon .NET plug-in written in C#.

The solution contains the following projects:

* Moose        - Rhino 5 C++ plug-in
* MooseCoreLib - Rhino 5-dependent C++ DLL that is used by Moose.
* MooseCommon  - .NET assembly that exposes the functions exported from MooseCoreLib.
* MooseNet     - RhinoCommon plug-in that references MooseCommon.
* MooseGh      - Grasshopper component that references MooseCommon.

Building Sample
--------------------
To build the sample, you are going to need:

* Rhinoceros 5 (http://www.rhino3d.com)
* Rhinoceros 5 C++ SDK (http://wiki.mcneel.com/developer/sdksamples/rhino5)
* Microsoft Visual C++ 2010
* Microsoft Visual C# 2010

Legal Stuff
-----------
Copyright © 2013 Robert McNeel & Associates. All Rights Reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
Software.

THIS SOFTWARE IS PROVIDED "AS IS" WITHOUT EXPRESS OR IMPLIED WARRANTY. ALL IMPLIED
WARRANTIES OF FITNESS FOR ANY PARTICULAR PURPOSE AND OF MERCHANTABILITY ARE HEREBY
DISCLAIMED.

Rhinoceros is a registered trademark of Robert McNeel & Associates.
