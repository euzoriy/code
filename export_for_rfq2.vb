' NX 12.0.2.9
' Journal created by ievgenz on Tue Jul 16 11:27:07 2019 Eastern Daylight Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: File->Export->STEP...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim stepCreator1 As NXOpen.StepCreator = Nothing
stepCreator1 = theSession.DexManager.CreateStepCreator()

stepCreator1.ExportAs = NXOpen.StepCreator.ExportAsOption.Ap214

stepCreator1.BsplineTol = 0.001

stepCreator1.OutputFile = "L:\Tooling Fixtures\TF3273\RFQ\TF1142-100-01.stp"

stepCreator1.SettingsFile = "C:\Program Files\Siemens\NX 12.0\step214ug\ugstep214.def"

stepCreator1.BsplineTol = 0.001

stepCreator1.InputFile = "L:\Tooling Fixtures\TF1142\MODELING\TF1142-100-01.prt"

theSession.SetUndoMarkName(markId1, "Export to STEP Options Dialog")

stepCreator1.ObjectTypes.Solids = False

stepCreator1.ObjectTypes.Curves = True

stepCreator1.ObjectTypes.Surfaces = True

stepCreator1.ObjectTypes.Csys = True

stepCreator1.ObjectTypes.ProductData = True

stepCreator1.ObjectTypes.PmiData = True

stepCreator1.ObjectTypes.Solids = True

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to STEP Options")

theSession.DeleteUndoMark(markId2, Nothing)

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to STEP Options")

stepCreator1.FileSaveFlag = False

stepCreator1.LayerMask = "1-256"

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = stepCreator1.Commit()

theSession.DeleteUndoMark(markId3, Nothing)

theSession.SetUndoMarkName(markId1, "Export to STEP Options")

stepCreator1.Destroy()

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module