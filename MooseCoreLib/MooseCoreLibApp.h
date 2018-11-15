// MooseCoreLib.h : main header file for the MooseCoreLib DLL.
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols

// CMooseCoreLibApp
// See MooseCoreLibApp.cpp for the implementation of this class
//

class CMooseCoreLibApp : public CWinApp
{
public:
	CMooseCoreLibApp() = default;

// Overrides
public:
  // CRITICAL: DO NOT CALL RHINO SDK FUNCTIONS HERE!
  // Only standard MFC DLL instance construction belongs here. 
  BOOL InitInstance() override;
	
  // CRITICAL: DO NOT CALL RHINO SDK FUNCTIONS HERE!
  // Only standard MFC DLL instance clean up belongs here. 
  int ExitInstance() override;
	
  DECLARE_MESSAGE_MAP()
};
