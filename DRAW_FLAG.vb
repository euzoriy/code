' NX 11.0.2.7
' Journal created by ievgenz on Wed Sep 26 11:02:16 2018 Eastern Daylight Time
'
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpen.Annotations
Module NXJournal

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
Dim selAnnotation As Annotation
CONST scaleFactor = 2

Sub Main (ByVal args() As String) 
	AddFlag
End Sub

Sub AddFlag()
	While selectDimension("Select Dimension", selAnnotation) = Selection.Response.Ok
		Dim nullNXOpen_Annotations_SimpleDraftingAid As NXOpen.Annotations.SimpleDraftingAid = Nothing
		Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
		draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullNXOpen_Annotations_SimpleDraftingAid)
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
		draftingNoteBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter
		Dim text1(0) As String
		text1(0) = "<C" & scaleFactor & "><&2><C>"
		draftingNoteBuilder1.Text.TextBlock.SetText(text1)
		draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)

		Dim symbolscale1 As Double = Nothing
		symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale
		Dim symbolaspectratio1 As Double = Nothing
		symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
		' ----------------------------------------------
		'   Dialog Begin Origin Tool
		' ----------------------------------------------
		Dim assocOrigin1 As NXOpen.Annotations.Annotation.AssociativeOriginData = Nothing
		assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.OffsetFromText
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

		'Dim note1 As NXOpen.Annotations.Note = CType(workPart.FindObject("ENTITY 25 1 1"), NXOpen.Annotations.Note)
		
		assocOrigin1.OffsetAnnotation = selAnnotation
		assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.MidCenter
		assocOrigin1.XOffsetFactor = 0.0
		assocOrigin1.YOffsetFactor = 0.0
		assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
		draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)

		REM Dim point1 As NXOpen.Point3d = New NXOpen.Point3d(5.0823348110342996, 8.2189874906049845, 0.0)
		REM draftingNoteBuilder1.Origin.Origin.SetValue(Nothing, nullNXOpen_View, point1)
		
		draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
		Dim nXObject1 As NXOpen.NXObject = Nothing
		nXObject1 = draftingNoteBuilder1.Commit()
		draftingNoteBuilder1.Destroy()
	End While
End Sub

	Function selectDimension(ByVal prompt As String, ByRef obj As Annotation)
			Dim ui As UI = GetUI()
			
			Dim mask(3) As Selection.MaskTriple
			
			With mask(0) 'dimensions
					.Type = UFConstants.UF_dimension_type			
					.Subtype = 0
					.SolidBodySubtype = 0
			End With
			
			With mask(1) 'notes
				.Type = 25
				.Subtype = 1
				.SolidBodySubtype = 0			
			End With
			
			With mask(2) 'labels
				.Type = 25
				.Subtype = 2
				.SolidBodySubtype = 0			
			End With			
			
			Dim cursor As Point3d = Nothing
			Dim resp As Selection.Response = _
			ui.SelectionManager.SelectObject(prompt, prompt, _
					Selection.SelectionScope.AnyInAssembly, _
					Selection.SelectionAction.ClearAndEnableSpecific, _
					False, False, mask, obj, cursor)
			If resp = Selection.Response.ObjectSelected Or _
			resp = Selection.Response.ObjectSelectedByName Then
					Return Selection.Response.Ok
			Else
					Return Selection.Response.Cancel
			End If
	End Function    'selectNoteDimension
	Function sReverse(ByVal value As String) As String
		' Convert to char array.
		Dim arr() As Char = value.ToCharArray()
		' Use Array.Reverse function.
		Array.Reverse(arr)
		' Construct new string.
		Return New String(arr)
	End Function


End Module