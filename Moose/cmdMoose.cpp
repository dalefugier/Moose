// cmdMoose.cpp : command file

#include "StdAfx.h"
#include "MoosePlugIn.h"

////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
//
// BEGIN Moose command
//

#pragma region Moose command

// Do NOT put the definition of class CCommandMoose in a header
// file. There is only ONE instance of a CCommandMoose class
// and that instance is the static theMooseCommand that appears
// immediately below the class definition.

class CCommandMoose : public CRhinoCommand
{
public:
  // The one and only instance of CCommandMoose is created below.
  // No copy constructor or operator= is required.
  // Values of member variables persist for the duration of the application.

  // CCommandMoose::CCommandMoose()
  // is called exactly once when static theMooseCommand is created.
  CCommandMoose() = default;

  // CCommandMoose::~CCommandMoose()
  // is called exactly once when static theMooseCommand is destroyed.
  // The destructor should not make any calls to the Rhino SDK. 
  // If your command has persistent settings, then override 
  // CRhinoCommand::SaveProfile and CRhinoCommand::LoadProfile.
  ~CCommandMoose() = default;

  // Returns a unique UUID for this command.
  // If you try to use an id that is already being used, then
  // your command will not work. Use GUIDGEN.EXE to make unique UUID.
  UUID CommandUUID() override
  {
    // {823698C9-DF2A-47D4-9C52-F66EC8AE3046}
    static const GUID MooseCommand_UUID =
    { 0x823698C9, 0xDF2A, 0x47D4, { 0x9C, 0x52, 0xF6, 0x6E, 0xC8, 0xAE, 0x30, 0x46 } };
    return MooseCommand_UUID;
  }

  // Returns the English command name.
  // If you want to provide a localized command name, then override 
  // CRhinoCommand::LocalCommandName.
  const wchar_t* EnglishCommandName() override { return L"Moose"; }

  // Rhino calls RunCommand to run the command.
  CRhinoCommand::result RunCommand(const CRhinoCommandContext& context) override;
};

// The one and only CCommandMoose object
// Do NOT create any other instance of a CCommandMoose class.
static class CCommandMoose theMooseCommand;

CRhinoCommand::result CCommandMoose::RunCommand(const CRhinoCommandContext& context)
{
  //CRhinoGetPoint gp;
  //gp.SetCommandPrompt(L"Location of point object");
  //gp.GetPoint();
  //if (gp.CommandResult() != CRhinoCommand::success)
  //  return gp.CommandResult();

  //ON_3dPoint point = gp.Point();

  //ON_UUID point_id = MooseSdkAddPoint(point);
  //if (ON_UuidIsNotNil(point_id))
  //{
  //  context.m_doc.Redraw();

  //  ON_wString str, uuid_str;
  //  ON_UuidToString(point_id, uuid_str);
  //  str.Format(L"Identifier of point object is \"%s\"", static_cast<const wchar_t*>(uuid_str));

  //  MooseSdkPrint(static_cast<const wchar_t*>(str));
  //}

  ON_SimpleArray<ON_Curve*> curveArray;
  int curveCount = MooseGetCurves(&curveArray);
  for (int i = 0; i < curveCount; i++)
  {
    ON_Curve* pCurve = curveArray[i];
    if (pCurve)
    {
      CRhinoCurveObject* pCurveObject = new CRhinoCurveObject();
      pCurveObject->SetCurve(pCurve);
      curveArray[i] = nullptr;
      context.m_doc.AddObject(pCurveObject);
    }
  }
  context.m_doc.Redraw();

  return CRhinoCommand::success;
}

#pragma endregion

//
// END Moose command
//
////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
