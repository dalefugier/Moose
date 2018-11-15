// MooseCoreLibLinkingPragmas.h

#pragma once

#if !defined(MOOSECORELIB_DLL_EXPORTS)
#pragma message( " --- Plug-in linking with MooseCoreLib Release Win32" )
#if defined(RHINO_DEBUG_PLUGIN)
#pragma comment( lib, "../Bin/Debug/MooseCoreLib.lib" )
#else
#pragma comment( lib, "../Bin/Release/MooseCoreLib.lib" )
#endif // RHINO_DEBUG_PLUGIN
#endif // MOOSECORELIB_DLL_EXPORTS