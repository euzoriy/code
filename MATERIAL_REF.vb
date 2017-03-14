' NX 11.0.0.33
' Journal created by ievgenz on Wed Jan 11 13:48:07 2017 Eastern Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim titleblocks1(0) As NXOpen.Annotations.TitleBlock
Dim titleBlock1 As NXOpen.Annotations.TitleBlock = CType(workPart.FindObject("HANDLE R-1575235"), NXOpen.Annotations.TitleBlock)

titleblocks1(0) = titleBlock1
Dim editTitleBlockBuilder1 As NXOpen.Annotations.EditTitleBlockBuilder = Nothing
editTitleBlockBuilder1 = workPart.DraftingManager.TitleBlocks.CreateEditTitleBlockBuilder(titleblocks1)

Dim titleBlockCellBuilderList1 As NXOpen.Annotations.TitleBlockCellBuilderList = Nothing
titleBlockCellBuilderList1 = editTitleBlockBuilder1.Cells

theSession.SetUndoMarkName(markId1, "Populate Title Block Dialog")

Dim taggedObject1 As NXOpen.TaggedObject = Nothing
taggedObject1 = titleBlockCellBuilderList1.FindItem(0)

Dim titleBlockCellBuilder1 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject1, NXOpen.Annotations.TitleBlockCellBuilder)

Dim taggedObject2 As NXOpen.TaggedObject = Nothing
taggedObject2 = titleBlockCellBuilderList1.FindItem(1)

Dim titleBlockCellBuilder2 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject2, NXOpen.Annotations.TitleBlockCellBuilder)

Dim taggedObject3 As NXOpen.TaggedObject = Nothing
taggedObject3 = titleBlockCellBuilderList1.FindItem(2)

Dim titleBlockCellBuilder3 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject3, NXOpen.Annotations.TitleBlockCellBuilder)

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

' ----------------------------------------------
'   Dialog Begin Populate Title Block
' ----------------------------------------------
theSession.DeleteUndoMark(markId2, Nothing)

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

theSession.DeleteUndoMark(markId3, Nothing)

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

theSession.DeleteUndoMark(markId4, Nothing)

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start")

theSession.SetUndoMarkName(markId6, "Text Dialog")

' ----------------------------------------------
'   Dialog Begin Text
' ----------------------------------------------
Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Text")

theSession.DeleteUndoMark(markId7, Nothing)

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Text")

theSession.DeleteUndoMark(markId8, Nothing)

theSession.SetUndoMarkName(markId6, "Text")

theSession.DeleteUndoMark(markId6, Nothing)

editTitleBlockBuilder1.SetCellValueForLabel("MATERIAL", "<WRef1*0@PARTMATL>")

theSession.SetUndoMarkName(markId5, "Populate Title Block")

theSession.SetUndoMarkVisibility(markId5, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)

Dim markId9 As NXOpen.Session.UndoMarkId = Nothing
markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

Dim markId10 As NXOpen.Session.UndoMarkId = Nothing
markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = editTitleBlockBuilder1.Commit()

Dim objects1() As NXOpen.NXObject
objects1 = editTitleBlockBuilder1.GetCommittedObjects()

theSession.DeleteUndoMark(markId10, Nothing)

theSession.SetUndoMarkName(markId1, "Populate Title Block")

editTitleBlockBuilder1.Destroy()

theSession.DeleteUndoMark(markId9, Nothing)

theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.DeleteUndoMark(markId5, Nothing)

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module