' NX 11.0.0.33
' Journal created by ievgenz on Fri Dec 09 14:02:03 2016 Eastern Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Edit->Settings...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim viewlabels1(0) As NXOpen.DisplayableObject
Dim note1 As NXOpen.Annotations.Note = CType(workPart.FindObject("HANDLE R-2197776"), NXOpen.Annotations.Note)

viewlabels1(0) = note1
Dim editViewLabelSettingsBuilder1 As NXOpen.Drawings.EditViewLabelSettingsBuilder = Nothing
editViewLabelSettingsBuilder1 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels1)

theSession.SetUndoMarkName(markId1, "Settings Dialog")

Dim editsettingsbuilders1(0) As NXOpen.Drafting.BaseEditSettingsBuilder
editsettingsbuilders1(0) = editViewLabelSettingsBuilder1
workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders1)

editViewLabelSettingsBuilder1.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Note

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = editViewLabelSettingsBuilder1.Commit()

theSession.DeleteUndoMark(markId2, Nothing)

theSession.SetUndoMarkName(markId1, "Settings")

editViewLabelSettingsBuilder1.Destroy()

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim viewlabels2(0) As NXOpen.DisplayableObject
viewlabels2(0) = note1
Dim editViewLabelSettingsBuilder2 As NXOpen.Drawings.EditViewLabelSettingsBuilder = Nothing
editViewLabelSettingsBuilder2 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels2)

theSession.SetUndoMarkName(markId3, "Settings Dialog")

Dim editsettingsbuilders2(0) As NXOpen.Drafting.BaseEditSettingsBuilder
editsettingsbuilders2(0) = editViewLabelSettingsBuilder2
workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders2)

' ----------------------------------------------
'   Dialog Begin Settings
' ----------------------------------------------
Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")

theSession.DeleteUndoMark(markId4, Nothing)

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")

Dim nXObject2 As NXOpen.NXObject = Nothing
nXObject2 = editViewLabelSettingsBuilder2.Commit()

theSession.DeleteUndoMark(markId5, Nothing)

theSession.SetUndoMarkName(markId3, "Settings")

editViewLabelSettingsBuilder2.Destroy()

Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Edit Object Origin")

Dim label1 As NXOpen.Annotations.Label = CType(workPart.FindObject("HANDLE R-2197806"), NXOpen.Annotations.Label)

label1.LeaderOrientation = NXOpen.Annotations.LeaderOrientation.FromLeft

Dim origin1 As NXOpen.Point3d = New NXOpen.Point3d(5.766230114615718, 15.235885551775372, 1.2811071428607501)
label1.AnnotationOrigin = origin1

Dim nErrs1 As Integer = Nothing
nErrs1 = theSession.UpdateManager.DoUpdate(markId6)

' ----------------------------------------------
'   Menu: Edit->Settings...
' ----------------------------------------------
Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim viewlabels3(0) As NXOpen.DisplayableObject
viewlabels3(0) = note1
Dim editViewLabelSettingsBuilder3 As NXOpen.Drawings.EditViewLabelSettingsBuilder = Nothing
editViewLabelSettingsBuilder3 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels3)

theSession.SetUndoMarkName(markId7, "Settings Dialog")

Dim editsettingsbuilders3(0) As NXOpen.Drafting.BaseEditSettingsBuilder
editsettingsbuilders3(0) = editViewLabelSettingsBuilder3
workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders3)

editViewLabelSettingsBuilder3.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Label

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")

theSession.DeleteUndoMark(markId8, Nothing)

Dim markId9 As NXOpen.Session.UndoMarkId = Nothing
markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")

Dim nXObject3 As NXOpen.NXObject = Nothing
nXObject3 = editViewLabelSettingsBuilder3.Commit()

theSession.DeleteUndoMark(markId9, Nothing)

theSession.SetUndoMarkName(markId7, "Settings")

editViewLabelSettingsBuilder3.Destroy()

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module