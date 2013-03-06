/////////////////////////////////////////////////////////////////////////////
// cmdMoose.cpp
//

#include "StdAfx.h"
#include "MoosePlugIn.h"

////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
//
// BEGIN Moose command
//

#pragma region Moose command

class CCommandMoose : public CRhinoCommand
{
public:
	CCommandMoose() {}
  ~CCommandMoose() {}
	UUID CommandUUID()
	{
		// {54828986-8CDE-4DCF-B9A5-C1113652A0A4}
    static const GUID MooseCommand_UUID =
    { 0x54828986, 0x8CDE, 0x4DCF, { 0xB9, 0xA5, 0xC1, 0x11, 0x36, 0x52, 0xA0, 0xA4 } };
    return MooseCommand_UUID;
	}
	const wchar_t* EnglishCommandName() { return L"Moose"; }
	const wchar_t* LocalCommandName() const { return L"Moose"; }
	CRhinoCommand::result RunCommand( const CRhinoCommandContext& );
};

// The one and only CCommandMoose object.  
static class CCommandMoose theMooseCommand;

CRhinoCommand::result CCommandMoose::RunCommand( const CRhinoCommandContext& context )
{
  CRhinoGetPoint gp;
  gp.SetCommandPrompt( L"Location of point object" );
  gp.GetPoint();
  if( gp.CommandResult() != CRhinoCommand::success )
    return gp.CommandResult();

  ON_3dPoint point = gp.Point();

  ON_UUID point_id = MooseSdkAddPoint( point );
  if( ON_UuidIsNotNil(point_id) )
  {
    context.m_doc.Redraw();

    ON_wString str, uuid_str;
    ON_UuidToString( point_id, uuid_str );
    str.Format( L"Identifier of point object is \"%s\"", uuid_str );

    MooseSdkPrint( str );
  }

  return CRhinoCommand::success;
}

#pragma endregion

//
// END Moose command
//
////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
