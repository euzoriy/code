' NX 11.0.1.11
' Journal created by ievgenz on Mon Apr 10 10:59:04 2017 Eastern Daylight Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Assemblies->Components->Create New Component...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create New Component")

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim fileNew1 As NXOpen.FileNew = Nothing
fileNew1 = theSession.Parts.FileNew()

theSession.SetUndoMarkName(markId2, "New Component File Dialog")

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New Component File")

theSession.DeleteUndoMark(markId3, Nothing)

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New Component File")

fileNew1.TemplateFileName = "LEESTA_MODEL_INCH.prt"

fileNew1.UseBlankTemplate = False

fileNew1.ApplicationName = "ModelTemplate"

fileNew1.Units = NXOpen.Part.Units.Inches

fileNew1.RelationType = ""

fileNew1.UsesMasterModel = "No"

fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item

fileNew1.TemplatePresentationName = "Model"

fileNew1.ItemType = ""

fileNew1.Specialization = ""

fileNew1.SetCanCreateAltrep(False)

fileNew1.NewFileName = "L:\Tooling Fixtures\TF3202\MODELING\TF3202-030-01.prt"

fileNew1.MasterFileName = ""

fileNew1.MakeDisplayedPart = False

theSession.DeleteUndoMark(markId4, Nothing)

theSession.SetUndoMarkName(markId2, "New Component File")

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim createNewComponentBuilder1 As NXOpen.Assemblies.CreateNewComponentBuilder = Nothing
createNewComponentBuilder1 = workPart.AssemblyManager.CreateNewComponentBuilder()

createNewComponentBuilder1.NewComponentName = "TF3202-020-01"

createNewComponentBuilder1.ReferenceSetName = "Entire Part"

theSession.SetUndoMarkName(markId5, "Create New Component Dialog")

createNewComponentBuilder1.NewComponentName = "TF3202-030-01"

' ----------------------------------------------
'   Dialog Begin Create New Component
' ----------------------------------------------
Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create New Component")

theSession.DeleteUndoMark(markId6, Nothing)

Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create New Component")

createNewComponentBuilder1.NewFile = fileNew1

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create New component")

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = createNewComponentBuilder1.Commit()

theSession.DeleteUndoMark(markId7, Nothing)

theSession.SetUndoMarkName(markId5, "Create New Component")

createNewComponentBuilder1.Destroy()

theSession.DeleteUndoMark(markId8, Nothing)

theSession.DeleteUndoMarksUpToMark(markId2, Nothing, False)

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module