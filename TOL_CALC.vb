' NX 11.0.2.7
' Journal created by ievgenz on Tue Aug 22 09:30:21 2017 Eastern Daylight Time
'
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpen.Annotations

Module NXJournal
'set precision > 0 for dimensions to avoid values like x.x999999999
	CONST PRECISION = 6	
	
	Dim theSession As Session = Session.GetSession()
	Dim workPart As Part = theSession.Parts.Work
	Dim displayPart As Part = theSession.Parts.Display
	Dim lw As ListingWindow = theSession.ListingWindow
	Dim selAnnotation As Annotation
	Dim noteDim As Dimension	
	
	Sub Main (ByVal args() As String) 


		'Dim parallelDimension1 As NXOpen.Annotations.ParallelDimension = CType(workPart.FindObject("HANDLE R-1970957"), NXOpen.Annotations.ParallelDimension)
		'Dim dimBuilder As NXOpen.Annotations.LinearDimensionBuilder = Nothing
		'dimBuilder = workPart.Dimensions.CreateLinearDimensionBuilder(parallelDimension1)

		ProcessDimensions
		
		'msgbox(parallelDimension1.ComputedSize)

		' SETTING TOLERANCE VALUES
		'dimBuilder.Style.DimensionStyle.ToleranceType = NXOpen.Annotations.ToleranceType.LimitTwoLines
		'dimBuilder.Style.DimensionStyle.UpperToleranceEnglish = 0.01
		'dimBuilder.Style.DimensionStyle.LowerToleranceEnglish = -0.01

		'

		'Dim nXObject1 As NXOpen.NXObject = Nothing
		'nXObject1 = dimBuilder.Commit()
		'dimBuilder.Destroy()
		


		' ----------------------------------------------
		'   Menu: Tools->Journal->Stop Recording
		' ----------------------------------------------
	End Sub

	Sub ProcessDimensions()
		While selectDimension("Select Dimension", selAnnotation) = Selection.Response.Ok
			
		Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
		markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
			
		Dim noteDim As NXOpen.Annotations.Dimension = CType(selAnnotation, NXOpen.Annotations.Dimension)
			
			'noteDim.SetDimensionPreferences(IsInspectionDimension = true)
				Dim dimPrefs As Annotations.DimensionPreferences
				dimPrefs = noteDim.GetDimensionPreferences()		

			

			
			Dim upperValue As Double
			Dim tolerance As Double
			Dim nomValue As Double	
			if (PRECISION>=0) then
				nomValue = Math.Round(noteDim.ComputedSize,PRECISION)
			else
				nomValue = noteDim.ComputedSize
			end if
			upperValue = getDouble ("Enter upper limit", nomValue)
			'msgbox(upperValue)
			tolerance = upperValue - nomValue
			'msgbox(tolerance)
			noteDim.ToleranceType = 2 '2-lines limits
			noteDim.UpperToleranceValue = tolerance
			noteDim.LowerToleranceValue = -tolerance			
			'noteDim.InspectionDimensionFlag = true
			noteDim.RedisplayObject
			'msgBox(noteDim.InspectionDimensionFlag)
			'msgbox(noteDim.ComputedSize)
			'msgbox(noteDim.ToleranceType)			
			

			' SETTING TOLERANCE VALUES
			'dimBuilder.Style.DimensionStyle.ToleranceType = NXOpen.Annotations.ToleranceType.LimitTwoLines
			'dimBuilder.Style.DimensionStyle.UpperToleranceEnglish = 0.01
			'dimBuilder.Style.DimensionStyle.LowerToleranceEnglish = -0.01
			
			
		theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
		theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
		
		End While


	End Sub

	Function getDouble(prompt as String, initialVal As Double) As Double
			Dim current as Double
			Try
				' if number then format it.
				current = CDbl(InputBox(prompt, "Enter a value", initialVal))
				'current = Math.Round(current, d)
				
				'MsgBox(getDouble)
				Return current
				
			Catch ex As System.InvalidCastException
					' item is not a number, do not format... leave as a string
					'msgbox(ex.StackTrace)
				Return initialVal
			End Try
	End function
	
	
	
	Function selectDimension(ByVal prompt As String, ByRef obj As Annotation)
			Dim ui As UI = GetUI()
			
			Dim mask(5) As Selection.MaskTriple
			With mask(0)
					.Type = UFConstants.UF_dimension_type			
					.Subtype = 0
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


End Module


