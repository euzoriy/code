' NX 12.0.2.9
' Journal created by ievgenz on Tue Oct 29 13:25:17 2019 Eastern Daylight Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Edit->Show and Hide->Show and Hide...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

theSession.SetUndoMarkName(markId1, "Show and Hide Dialog")

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Hide All")

Dim numberHidden1 As Integer = Nothing
numberHidden1 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_ALL", NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

Dim nErrs1 As Integer = Nothing
nErrs1 = theSession.UpdateManager.DoUpdate(markId2)

workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly)

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Show Solid Bodies")

Dim numberShown1 As Integer = Nothing
numberShown1 = theSession.DisplayManager.ShowByType("SHOW_HIDE_TYPE_SOLID_BODIES", NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

Dim nErrs2 As Integer = Nothing
nErrs2 = theSession.UpdateManager.DoUpdate(markId3)

workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.ShowOnly)

theSession.SetUndoMarkName(markId1, "Show and Hide")

theSession.DeleteUndoMark(markId1, Nothing)

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module