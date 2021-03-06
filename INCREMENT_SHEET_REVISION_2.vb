' NX 12.0.2.9
' Journal created by Ievgenz on Fri Jun 12 15:22:31 2020 Eastern Daylight Time
'
Imports System
Imports NXOpen
Module NXJournal
Sub Main (ByVal args() As String) 
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
Dim draftingDrawingSheet1 As NXOpen.Drawings.DraftingDrawingSheet = CType(workPart.DraftingDrawingSheets.FindObject("Sheet 2"), NXOpen.Drawings.DraftingDrawingSheet)
Dim draftingDrawingSheetBuilder1 As NXOpen.Drawings.DraftingDrawingSheetBuilder = Nothing
draftingDrawingSheetBuilder1 = workPart.DraftingDrawingSheets.CreateDraftingDrawingSheetBuilder(draftingDrawingSheet1)
draftingDrawingSheetBuilder1.Revision = "c"
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = draftingDrawingSheetBuilder1.Commit()
draftingDrawingSheetBuilder1.Destroy()
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module