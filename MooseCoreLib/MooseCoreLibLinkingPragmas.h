/////////////////////////////////////////////////////////////////////////////
// MooseCoreLibLinkingPragmas.h

#pragma once

#if !defined(MOOSECORELIB_DLL_EXPORTS)

#if defined(WIN64)

#if defined(NDEBUG)

#if defined(RHINO_DEBUG_PLUGIN)

// PseudoDebug Win64 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib PseudoDebug Win64" )
#pragma comment( lib, "../Bin/PseudoDebug/MooseCoreLib_x64.lib" )

#else

// Release Win64 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib Release Win64" )
#pragma comment( lib, "../Bin/Release/MooseCoreLib_x64.lib" )

#endif // RHINO_DEBUG_PLUGIN

#else // _DEBUG

// Debug Win64 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib Debug Win64" )
#pragma comment( lib, "../Bin/Debug/MooseCoreLib_x64.lib" )

#endif // NDEBUG else _DEBUG

#else // WIN32

#if defined(NDEBUG) 

#if defined(RHINO_DEBUG_PLUGIN)

// PseudoDebug Win32 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib PseudoDebug Win32" )
#pragma comment( lib, "../Bin/PseudoDebug/MooseCoreLib.lib" )

#else

// Release Win32 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib Release Win32" )
#pragma comment( lib, "../Bin/Release/MooseCoreLib.lib" )

#endif // RHINO_DEBUG_PLUGIN

#else // _DEBUG

// Debug Win32 libraries
#pragma message( " --- Plug-in linking with MooseCoreLib Debug Win32" )
#pragma comment( lib, "../Bin/Debug/MooseCoreLib.lib" )

#endif // NDEBUG else _DEBUG

#endif // WIN64 else WIN32

#endif // MOOSECORELIB_DLL_EXPORTS