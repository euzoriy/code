' NX 11.0.2.7
' Journal created by ievgenz on Wed Mar 28 16:40:31 2018 Eastern Daylight Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: File->Save As...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim stepCreator1 As NXOpen.StepCreator = Nothing
stepCreator1 = theSession.DexManager.CreateStepCreator()

stepCreator1.ExportAs = NXOpen.StepCreator.ExportAsOption.Ap214

stepCreator1.BsplineTol = 0.001

stepCreator1.OutputFile = "L:\Tooling Fixtures\TF3470\MODELING\TF3470-020-01.stp"

stepCreator1.SettingsFile = "C:\Program Files\Siemens\NX 11.0\step214ug\ugstep214.def"

stepCreator1.ObjectTypes.Solids = True

theSession.SetUndoMarkName(markId1, "Save As STEP File Options Dialog")

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Save As STEP File Options")

theSession.DeleteUndoMark(markId2, Nothing)

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Save As STEP File Options")

stepCreator1.FileSaveFlag = False

stepCreator1.FileSaveFlag = True

stepCreator1.LayerMask = "1-256"

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = stepCreator1.Commit()

theSession.DeleteUndoMark(markId3, Nothing)

theSession.SetUndoMarkName(markId1, "Save As STEP File Options")

stepCreator1.Destroy()

Dim partSaveStatus1 As NXOpen.PartSaveStatus = Nothing
partSaveStatus1 = workPart.SaveAs("L:\Tooling Fixtures\TF3470\MODELING\TF3470-020-01.stp")

partSaveStatus1.Dispose()
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module