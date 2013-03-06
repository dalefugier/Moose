/////////////////////////////////////////////////////////////////////////////
// MooseCoreLibApp.h
//

#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "Resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMooseCoreLibApp
//

class CMooseCoreLibApp : public CWinApp
{
public:
	CMooseCoreLibApp();

// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	DECLARE_MESSAGE_MAP()
};
