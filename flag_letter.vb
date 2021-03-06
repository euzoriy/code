' NX 11.0.2.7
' Journal created by ievgenz on Fri Dec 08 10:02:37 2017 Eastern Standard Time
'
Imports System
Imports NXOpen
Imports NXOpen.UI

Module NXJournal

	Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
	
Sub Main (ByVal args() As String) 
	'Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	'Dim workPart As NXOpen.Part = theSession.Parts.Work
	'Dim displayPart As NXOpen.Part = theSession.Parts.Display

	Dim nullNXOpen_Annotations_SimpleDraftingAid As NXOpen.Annotations.SimpleDraftingAid = Nothing
	Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
	draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullNXOpen_Annotations_SimpleDraftingAid)
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter
	Dim text1(0) As String
	text1(0) = ""
	draftingNoteBuilder1.Text.TextBlock.SetText(text1)
	draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)

	Dim symbolscale1 As Double = Nothing
	symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale
	Dim symbolaspectratio1 As Double = Nothing
	symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)

	Dim text7(0) As String
	text7(0) = "<C2.000><&2><C>"
	draftingNoteBuilder1.Text.TextBlock.SetText(text7)

	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
	Dim assocOrigin1 As NXOpen.Annotations.Annotation.AssociativeOriginData = Nothing
	assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag
	Dim nullNXOpen_View As NXOpen.View = Nothing
	assocOrigin1.View = nullNXOpen_View
	assocOrigin1.ViewOfGeometry = nullNXOpen_View
	Dim nullNXOpen_Point As NXOpen.Point = Nothing
	assocOrigin1.PointOnGeometry = nullNXOpen_Point
	Dim nullNXOpen_Annotations_Annotation As NXOpen.Annotations.Annotation = Nothing
	assocOrigin1.VertAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	assocOrigin1.HorizAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	assocOrigin1.AlignedAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin1.DimensionLine = 0
	assocOrigin1.AssociatedView = nullNXOpen_View
	assocOrigin1.AssociatedPoint = nullNXOpen_Point
	assocOrigin1.OffsetAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	assocOrigin1.XOffsetFactor = 0.0
	assocOrigin1.YOffsetFactor = 0.0
	assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
	draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)

	'Dim location As NXOpen.Point3d = New NXOpen.Point3d(11.469330616847763, 20.345419444286641, 0.0)
	'Dim location As Point3d = Nothing
	Dim location As Point3d
	If pickScreenPosition(location)=Selection.Response.Cancel Then
		exit sub
	End If

	draftingNoteBuilder1.Origin.Origin.SetValue(Nothing, nullNXOpen_View, location)
	draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
	Dim nXObject1 As NXOpen.NXObject = Nothing
	nXObject1 = draftingNoteBuilder1.Commit()
	draftingNoteBuilder1.Destroy()
	Dim draftingNoteBuilder2 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
	draftingNoteBuilder2 = workPart.Annotations.CreateDraftingNoteBuilder(nullNXOpen_Annotations_SimpleDraftingAid)
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder2.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter
	Dim textBlockCust(0) As String
	textBlockCust(0) = "<C2.000><&2><C>"
	draftingNoteBuilder2.Text.TextBlock.SetText(textBlockCust)
	draftingNoteBuilder2.Style.DimensionStyle.LimitFitDeviation = "RC"
	draftingNoteBuilder2.Style.DimensionStyle.LimitFitShaftDeviation = "RC"

	draftingNoteBuilder2.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)

	Dim symbolscale2 As Double = Nothing
	symbolscale2 = draftingNoteBuilder2.Text.TextBlock.SymbolScale
	Dim symbolaspectratio2 As Double = Nothing
	symbolaspectratio2 = draftingNoteBuilder2.Text.TextBlock.SymbolAspectRatio
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)
	Dim textLetter(0) As String
	textLetter(0) = "A"
	draftingNoteBuilder2.Text.TextBlock.SetText(textLetter)
	' ----------------------------------------------
	'   Dialog Begin Origin Tool
	' ----------------------------------------------
	Dim assocOrigin2 As NXOpen.Annotations.Annotation.AssociativeOriginData = Nothing
	assocOrigin2.OriginType = NXOpen.Annotations.AssociativeOriginType.OffsetFromText
	assocOrigin2.View = nullNXOpen_View
	assocOrigin2.ViewOfGeometry = nullNXOpen_View
	assocOrigin2.PointOnGeometry = nullNXOpen_Point
	assocOrigin2.VertAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin2.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	assocOrigin2.HorizAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin2.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	assocOrigin2.AlignedAnnotation = nullNXOpen_Annotations_Annotation
	assocOrigin2.DimensionLine = 0
	assocOrigin2.AssociatedView = nullNXOpen_View
	assocOrigin2.AssociatedPoint = nullNXOpen_Point
	Dim note1 As NXOpen.Annotations.Note = CType(nXObject1, NXOpen.Annotations.Note)
	assocOrigin2.OffsetAnnotation = note1
	assocOrigin2.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.MidCenter
	assocOrigin2.XOffsetFactor = 0.0
	assocOrigin2.YOffsetFactor = 0.0
	assocOrigin2.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above

	draftingNoteBuilder2.Origin.SetAssociativeOrigin(assocOrigin2)

	draftingNoteBuilder2.Origin.Origin.SetValue(Nothing, nullNXOpen_View, location)
	draftingNoteBuilder2.Origin.SetInferRelativeToGeometry(True)
	Dim nXObject2 As NXOpen.NXObject = Nothing
	nXObject2 = draftingNoteBuilder2.Commit()
	draftingNoteBuilder2.Destroy()
	Dim draftingNoteBuilder3 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
	draftingNoteBuilder3 = workPart.Annotations.CreateDraftingNoteBuilder(nullNXOpen_Annotations_SimpleDraftingAid)
	draftingNoteBuilder3.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder3.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder3.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter
	REM Dim textLetter(0) As String
	REM textLetter(0) = "A"
	REM draftingNoteBuilder3.Text.TextBlock.SetText(textLetter)
	draftingNoteBuilder3.Style.DimensionStyle.LimitFitDeviation = "RC"
	draftingNoteBuilder3.Style.DimensionStyle.LimitFitShaftDeviation = "RC"

	draftingNoteBuilder3.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane
	draftingNoteBuilder3.Origin.SetInferRelativeToGeometry(True)
	Dim leaderData3 As NXOpen.Annotations.LeaderData = Nothing
	leaderData3 = workPart.Annotations.CreateLeaderData()
	leaderData3.StubSize = 0.125
	leaderData3.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow
	leaderData3.VerticalAttachment = NXOpen.Annotations.LeaderVerticalAttachment.Center
	draftingNoteBuilder3.Leader.Leaders.Append(leaderData3)
	leaderData3.StubSide = NXOpen.Annotations.LeaderSide.Inferred
	Dim symbolscale3 As Double = Nothing
	symbolscale3 = draftingNoteBuilder3.Text.TextBlock.SymbolScale
	Dim symbolaspectratio3 As Double = Nothing
	symbolaspectratio3 = draftingNoteBuilder3.Text.TextBlock.SymbolAspectRatio
	draftingNoteBuilder3.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder3.Origin.SetInferRelativeToGeometry(True)
	draftingNoteBuilder3.Destroy()

End Sub

Function pickScreenPosition(ByRef location As Point3d) As Selection.Response
 
	Dim ui As UI = GetUI()
	Dim resp As Selection.DialogResponse = Selection.DialogResponse.None
	Dim localView As View
	resp = ui.SelectionManager.SelectScreenPosition("Select Screen Position", localView, location)
	If resp <> Selection.DialogResponse.Back And resp <> Selection.DialogResponse.Cancel Then
		Return Selection.Response.Ok
	Else
		Return Selection.Response.Cancel
	End If
 
End Function


End Module