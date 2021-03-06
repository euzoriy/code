' NX 11.0.0.33
' Journal created by ievgenz on Thu Mar 16 08:41:42 2017 Eastern Daylight Time
'
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI
Imports System.Windows.Forms

Module journal
    Dim theSession As Session = Session.GetSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
    Dim selobj As NXObject
    Dim lw As ListingWindow = theSession.ListingWindow
	
  Sub Main
    Dim type As Integer
    Dim subtype As Integer
    Dim theUI As UI = ui.GetUI
    Dim numsel As Integer = theUI.SelectionManager.GetNumSelectedObjects()
    lw.Open()
	

''Type(text): NXOpen.Annotations.IdSymbol
''Type: 25
''Subtype: 3
	
	startMoving()
	
    lw.WriteLine("Selected Objects: " & numsel.ToString())
    For inx As Integer = 0 To numsel-1
      selobj = theUI.SelectionManager.GetSelectedObject(inx)
      ufs.Obj.AskTypeAndSubtype(selobj.Tag, type, subtype)
      lw.WriteLine("Object: " & selobj.ToString())
      lw.WriteLine(" Tag: " & selobj.Tag.ToString())
      lw.WriteLine(" Type: " & type.ToString())
	  lw.WriteLine(" Type(text): " & selobj.GetType().ToString())
      lw.WriteLine(" Subtype: " & subtype.ToString())
      lw.WriteLine("")
    Next
    End Sub
''**************************************************************************************************
	Sub moveSymbol (obj As NXOpen.Annotations.IdSymbol, dX as Integer, dY as Integer)
		Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
		markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
		Dim idSymbol1 As NXOpen.Annotations.IdSymbol = obj''CType(workPart.FindObject("HANDLE R-2492193"), NXOpen.Annotations.IdSymbol)
		Dim idSymbolBuilder1 As NXOpen.Annotations.IdSymbolBuilder = Nothing
		idSymbolBuilder1 = workPart.Annotations.IdSymbols.CreateIdSymbolBuilder(idSymbol1)
		''idSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
		theSession.SetUndoMarkName(markId1, "Balloon Dialog")
		''idSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
		''Dim leaderData1 As NXOpen.Annotations.LeaderData = Nothing
		''leaderData1 = workPart.Annotations.CreateLeaderData()
		''leaderData1.StubSize = 0.125
		''leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow
		''leaderData1.VerticalAttachment = NXOpen.Annotations.LeaderVerticalAttachment.Center
		''idSymbolBuilder1.Leader.Leaders.Append(leaderData1)
		''leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred
		''idSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
		''idSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
		''Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
		''markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Balloon")
		' ----------------------------------------------
		'   Dialog Begin Balloon
		' ----------------------------------------------
		''theSession.DeleteUndoMark(markId2, Nothing)
		''Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
		''markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Balloon")
		''theSession.DeleteUndoMark(markId3, Nothing)
		Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
		markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Balloon")
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
		Dim horizontalDimension1 As NXOpen.Annotations.HorizontalDimension = CType(workPart.FindObject("HANDLE R-2119496"), NXOpen.Annotations.HorizontalDimension)
		assocOrigin1.OffsetAnnotation = horizontalDimension1
		assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
	''**********************************************************************************************
		assocOrigin1.XOffsetFactor = assocOrigin1.XOffsetFactor + dX
		assocOrigin1.YOffsetFactor = assocOrigin1.YOffsetFactor + dY
	''**********************************************************************************************
		assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
		idSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)
		Dim point1 As NXOpen.Point3d = New NXOpen.Point3d(22.940111610815642, 17.071188042204213, 0.0)
		idSymbolBuilder1.Origin.Origin.SetValue(Nothing, nullNXOpen_View, point1)
		idSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
		theSession.SetUndoMarkName(markId4, "Balloon - Specify Location")
		theSession.SetUndoMarkVisibility(markId4, Nothing, NXOpen.Session.MarkVisibility.Visible)
		theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
		Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
		markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Balloon")
		Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
		markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Balloon")
		Dim nXObject1 As NXOpen.NXObject = Nothing
		nXObject1 = idSymbolBuilder1.Commit()
		theSession.DeleteUndoMark(markId6, Nothing)
		theSession.SetUndoMarkName(markId1, "Balloon")
		idSymbolBuilder1.Destroy()
		theSession.DeleteUndoMark(markId5, Nothing)
		theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
		theSession.DeleteUndoMark(markId4, Nothing)
		' ----------------------------------------------
		'   Menu: Tools->Journal->Stop Recording
		' ----------------------------------------------
	End Sub
''**************************************************************************************************
	
		Sub startMoving()
			'plain, untitled message box with OK button
			MessageBox.Show("Press OK then finished.")
		End Sub
		
	
End Module