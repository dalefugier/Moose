/////////////////////////////////////////////////////////////////////////////
// MoosePlugIn.h : main header file for the Moose plug-in
//

#pragma once

/////////////////////////////////////////////////////////////////////////////
// CMoosePlugIn
// See MoosePlugIn.cpp for the implementation of this class
//

class CMoosePlugIn : public CRhinoUtilityPlugIn
{
public:
  CMoosePlugIn();
  ~CMoosePlugIn();

  // Required overrides
  const wchar_t* PlugInName() const;
  const wchar_t* PlugInVersion() const;
  GUID PlugInID() const;
  BOOL OnLoadPlugIn();
  void OnUnloadPlugIn();

  // Online help overrides
  BOOL AddToPlugInHelpMenu() const;
  BOOL OnDisplayPlugInHelp( HWND hWnd ) const;

private:
  ON_wString m_plugin_version;

  // TODO: Add additional class information here
};

CMoosePlugIn& MoosePlugIn();



