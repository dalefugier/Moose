Moose
=====

Moose is a Rhino 7 solution that demonstrates how to share a common C++ library between a [Rhino C++](https://developer.rhino3d.com/guides/cpp/) plug-in, a [RhinoCommon](https://developer.rhino3d.com/guides/rhinocommon/) plug-in written in C#, and a [Grasshopper](https://developer.rhino3d.com/guides/grasshopper/) component written in C#.

The solution contains the following projects:

* **MooseCoreLib** — Rhino dependent C++ assembly (DLL).
* **MooseCommon** — .NET assembly that exposes the functions exported from MooseCoreLib.
* **Moose** — Rhino C++ plug-in that references MooseCoreLib.
* **MooseNet** — RhinoCommon plug-in that references MooseCommon.
* **MooseGh** — Grasshopper component that references MooseCommon.

Building Sample
--------------------
To build the sample, you are going to need:

* Rhino 7 (http://www.rhino3d.com)
* Rhino 7 C/C++ SDK (https://developer.rhino3d.com/)
* Microsoft Visual C++ 2019
* Microsoft Visual C# 2019

Legal Stuff
-----------
Copyright © 2012-2022 Robert McNeel & Associates. All Rights Reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
Software.

THIS SOFTWARE IS PROVIDED "AS IS" WITHOUT EXPRESS OR IMPLIED WARRANTY. ALL IMPLIED
WARRANTIES OF FITNESS FOR ANY PARTICULAR PURPOSE AND OF MERCHANTABILITY ARE HEREBY
DISCLAIMED.

Rhinoceros is a registered trademark of Robert McNeel & Associates.
