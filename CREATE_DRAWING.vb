' NX 11.0.0.33
' Journal created by ievgenz on Fri Mar 10 14:19:14 2017 Eastern Standard Time
'
Imports System
Imports NXOpen
Module NXJournal
Sub Main (ByVal args() As String) 
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
' ----------------------------------------------
'   Menu: File->New...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
Dim fileNew1 As NXOpen.FileNew = Nothing
fileNew1 = theSession.Parts.FileNew()
theSession.SetUndoMarkName(markId1, "New Dialog")
Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New")
theSession.DeleteUndoMark(markId2, Nothing)
Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New")
fileNew1.TemplateFileName = "D_INCH.PRT"
fileNew1.UseBlankTemplate = False
fileNew1.ApplicationName = "DrawingTemplate"
fileNew1.Units = NXOpen.Part.Units.Inches
fileNew1.RelationType = ""
fileNew1.UsesMasterModel = "Yes"
fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item
fileNew1.TemplatePresentationName = "SIZE D - INCH, DRAFTING TEMPLATE"
fileNew1.ItemType = ""
fileNew1.Specialization = ""
fileNew1.SetCanCreateAltrep(False)
''TODO need to be modified*************
fileNew1.NewFileName = "L:\Tooling Fixtures\TF3094\DRAFTING\TF3094-020.prt"
fileNew1.MasterFileName = "TF3094-020-01"
''*************************************
fileNew1.MakeDisplayedPart = True
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = fileNew1.Commit()
workPart = theSession.Parts.Work ' TF3094-020
displayPart = theSession.Parts.Display ' TF3094-020
theSession.DeleteUndoMark(markId3, Nothing)
fileNew1.Destroy()
theSession.ApplicationSwitchImmediate("UG_APP_DRAFTING")
workPart.Drafting.EnterDraftingApplication()
Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Drafting")
workPart.Views.WorkView.UpdateCustomSymbols()
workPart.Drafting.SetTemplateInstantiationIsComplete(True)
Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
Dim titleblocks1(0) As NXOpen.Annotations.TitleBlock
Dim titleBlock1 As NXOpen.Annotations.TitleBlock = CType(workPart.FindObject("HANDLE R-1575235"), NXOpen.Annotations.TitleBlock)
titleblocks1(0) = titleBlock1
Dim editTitleBlockBuilder1 As NXOpen.Annotations.EditTitleBlockBuilder = Nothing
editTitleBlockBuilder1 = workPart.DraftingManager.TitleBlocks.CreateEditTitleBlockBuilder(titleblocks1)
Dim titleBlockCellBuilderList1 As NXOpen.Annotations.TitleBlockCellBuilderList = Nothing
titleBlockCellBuilderList1 = editTitleBlockBuilder1.Cells
theSession.SetUndoMarkName(markId5, "Populate Title Block Dialog")
Dim taggedObject1 As NXOpen.TaggedObject = Nothing
taggedObject1 = titleBlockCellBuilderList1.FindItem(0)
Dim titleBlockCellBuilder1 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject1, NXOpen.Annotations.TitleBlockCellBuilder)
Dim taggedObject2 As NXOpen.TaggedObject = Nothing
taggedObject2 = titleBlockCellBuilderList1.FindItem(1)
Dim titleBlockCellBuilder2 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject2, NXOpen.Annotations.TitleBlockCellBuilder)
Dim taggedObject3 As NXOpen.TaggedObject = Nothing
taggedObject3 = titleBlockCellBuilderList1.FindItem(2)
Dim titleBlockCellBuilder3 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject3, NXOpen.Annotations.TitleBlockCellBuilder)
Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")
' ----------------------------------------------
'   Dialog Begin Populate Title Block
' ----------------------------------------------
theSession.DeleteUndoMark(markId6, Nothing)
Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")
editTitleBlockBuilder1.Destroy()
theSession.UndoToMark(markId5, Nothing)
theSession.DeleteUndoMark(markId5, Nothing)
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module