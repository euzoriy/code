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
theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.iNVisible)
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
editTitleBlockBuilder1.SetCellValueForLabel("MATERIAL", "<WRef1*0@PARTMATL>")
theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = editTitleBlockBuilder1.Commit()
Dim objects1() As NXOpen.NXObject
objects1 = editTitleBlockBuilder1.GetCommittedObjects()
theSession.SetUndoMarkName(markId1, "Populate Title Block")
editTitleBlockBuilder1.Destroy()
theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module