'Not finished
' NX 11.0.1.11
' Journal created by ievgenz on Mon May 08 08:54:45 2017 Eastern Daylight Time
'
Imports System
Imports NXOpen
Module NXJournal
Sub Main (ByVal args() As String) 
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
' ----------------------------------------------
'   Menu: File->&Edit...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
Dim note1 As NXOpen.Annotations.Note = CType(workPart.FindObject("HANDLE R-1635087"), NXOpen.Annotations.Note)
Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(note1)
draftingNoteBuilder1.Text.TextBlock.CustomSymbolScale = 1.0
draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
theSession.SetUndoMarkName(markId1, "Note Dialog")
draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
Dim leaderData1 As NXOpen.Annotations.LeaderData = Nothing
leaderData1 = workPart.Annotations.CreateLeaderData()
leaderData1.StubSize = 0.125
leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow
leaderData1.VerticalAttachment = NXOpen.Annotations.LeaderVerticalAttachment.Center
draftingNoteBuilder1.Leader.Leaders.Append(leaderData1)
leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred
Dim symbolscale1 As Double = Nothing
symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale
Dim symbolaspectratio1 As Double = Nothing
symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio
draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
'Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
'markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
'theSession.DeleteUndoMark(markId2, Nothing)
'Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
'markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
'theSession.DeleteUndoMark(markId3, Nothing)
	Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
	markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
		Dim text1(13) As String
		text1(0) = "NOTES:"
		text1(1) = ""
		text1(2) = " 1. ALL DIMENSIONS ARE IN INCHES UOS"
		text1(3) = " 2. CORNERS MUST HAVE FILLETS .005 - .020 UOS"
		text1(4) = " 3. BREAK SHARP EDGES .003 - .015 UOS"
		text1(5) = " 4. CHAMFERS ARE .020 - .040 UOS"
		text1(6) = " 5. DWG INTERPRETATIONS PER ASME Y14.5-2009"
		text1(7) = " 6. UOS ANGULAR TOLERANCES ARE: <$t>5<$s>"
		text1(8) = " 7. UOS LINEAR TOLERANCES ARE:.XXX <$t>.005,.XX <$t>.01"
		text1(9) = " 8. ALL MACHINED SURFACES ARE 125 RMS UOS"
		text1(10) = " 9. ASSOCIATED COMPUTER DATA FILES ARE: "
		text1(11) = "           NX DWG        <W@$SH_PART_NAME>.PRT"
		text1(12) = "           NX MODEL      <W@$SH_MASTER_PART_NAME>.PRT. "
		text1(13) = "10. UOS ALL <O> <&70><+><&10><+><O>.010<+>A<+>B<+>C<+><&90>"
		draftingNoteBuilder1.Text.TextBlock.SetText(text1)
		Dim text2(14) As String
		text2(0) = "NOTES:"
		text2(1) = ""
		text2(2) = " 1. ALL DIMENSIONS ARE IN INCHES UOS"
		text2(3) = " 2. CORNERS MUST HAVE FILLETS .005 - .020 UOS"
		text2(4) = " 3. BREAK SHARP EDGES .003 - .015 UOS"
		text2(5) = " 4. CHAMFERS ARE .020 - .040 UOS"
		text2(6) = " 5. DWG INTERPRETATIONS PER ASME Y14.5-2009"
		text2(7) = " 6. UOS ANGULAR TOLERANCES ARE: <$t>5<$s>"
		text2(8) = " 7. UOS LINEAR TOLERANCES ARE:.XXX <$t>.005,.XX <$t>.01"
		text2(9) = " 8. ALL MACHINED SURFACES ARE 125 RMS UOS"
		text2(10) = " 9. ASSOCIATED COMPUTER DATA FILES ARE: "
		text2(11) = "           NX DWG        <W@$SH_PART_NAME>.PRT"
		text2(12) = "           NX MODEL      <W@$SH_MASTER_PART_NAME>.PRT. "
		text2(13) = "10. UOS ALL <O> <&70><+><&10><+><O>.010<+>A<+>B<+>C<+><&90>23  THREAD PER SPEC PWA 355."
		text2(14) = ""
		draftingNoteBuilder1.Text.TextBlock.SetText(text2)
	theSession.SetUndoMarkName(markId4, "Note - Insert Text from File")
	theSession.SetUndoMarkVisibility(markId4, Nothing, NXOpen.Session.MarkVisibility.Visible)
theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
theSession.DeleteUndoMark(markId5, Nothing)
'Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
'markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note")
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = draftingNoteBuilder1.Commit()
theSession.DeleteUndoMark(markId7, Nothing)
theSession.SetUndoMarkName(markId1, "Note")
draftingNoteBuilder1.Destroy()
'theSession.DeleteUndoMark(markId6, Nothing)
theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
	theSession.DeleteUndoMark(markId4, Nothing)
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module