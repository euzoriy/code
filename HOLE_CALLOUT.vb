' NX 11.0.2.7
' Journal created by ievgenz on Mon May 28 16:11:33 2018 Eastern Daylight Time
'
Imports System
Imports NXOpen
Module NXJournal
    Sub Main(ByVal args() As String)
        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        Dim displayPart As NXOpen.Part = theSession.Parts.Display
        ' ----------------------------------------------
        '   Menu: Insert->Annotation->Note...
        ' ----------------------------------------------
        Dim nullNXOpen_Annotations_SimpleDraftingAid As NXOpen.Annotations.SimpleDraftingAid = Nothing
        Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
        draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullNXOpen_Annotations_SimpleDraftingAid)
        'draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
        'draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
        'draftingNoteBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter
        Dim text1(2) As String
        text1(0) = "THREAD "
        text1(1) = "TAP DRILL "
        draftingNoteBuilder1.Text.TextBlock.SetText(text1)
        draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane
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

        Dim baseView1 As NXOpen.Drawings.BaseView = CType(workPart.DraftingViews.FindObject("Top@38"), NXOpen.Drawings.BaseView)
        Dim draftingBody1 As NXOpen.Drawings.DraftingBody = CType(baseView1.DraftingBodies.FindObject("COMPONENT TF3204-010-03 1  HANDLE R-8048  0"), NXOpen.Drawings.DraftingBody)
        Dim draftingCurve1 As NXOpen.Drawings.DraftingCurve = CType(draftingBody1.DraftingCurves.FindObject("(Extracted Edge) HANDLE R-105961"), NXOpen.Drawings.DraftingCurve)

        Dim point1_1 As NXOpen.Point3d = New NXOpen.Point3d(-3.7109425451730429, 3.148970380522365, 2.5000000000000098)
        Dim nullNXOpen_View As NXOpen.View = Nothing
        Dim point2_1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
        leaderData1.Leader.SetValue(NXOpen.InferSnapType.SnapType.Center, draftingCurve1, baseView1, point1_1, Nothing, nullNXOpen_View, point2_1)
        Dim point1 As NXOpen.Point = Nothing
        point1 = workPart.Points.CreatePoint(draftingCurve1, NXOpen.SmartObject.UpdateOption.AfterModeling)
        Dim point2 As NXOpen.Point3d = New NXOpen.Point3d(-3.7109425451730429, 3.148970380522365, 2.5000000000000098)
        leaderData1.Leader.SetValue(point1, baseView1, point2)
        Dim leaderData2 As NXOpen.Annotations.LeaderData = Nothing
        leaderData2 = workPart.Annotations.CreateLeaderData()
        leaderData2.StubSize = 0.125
        leaderData2.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow
        leaderData2.VerticalAttachment = NXOpen.Annotations.LeaderVerticalAttachment.Center
        draftingNoteBuilder1.Leader.Leaders.Append(leaderData2)
        leaderData2.StubSide = NXOpen.Annotations.LeaderSide.Inferred
        Dim point3 As NXOpen.Point3d = New NXOpen.Point3d(-3.7109425451730429, 3.148970380522365, 2.5000000000000098)
        leaderData1.Leader.SetValue(draftingCurve1, baseView1, point3)
        ' Jog position relocated or jog removed
        leaderData1.Jogs.Clear()
        Dim assocOrigin1 As NXOpen.Annotations.Annotation.AssociativeOriginData = Nothing
        assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag
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
        Dim point4 As NXOpen.Point3d = New NXOpen.Point3d(2.42257982366471, 13.454416188193937, 0.0)
        draftingNoteBuilder1.Origin.Origin.SetValue(Nothing, nullNXOpen_View, point4)
        draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
        leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Right
        Dim point5 As NXOpen.Point3d = New NXOpen.Point3d(-3.7109425451730429, 3.148970380522365, 2.5000000000000098)
        leaderData1.Leader.SetValue(draftingCurve1, baseView1, point5)
        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = draftingNoteBuilder1.Commit()
        Dim point6 As NXOpen.Point3d = New NXOpen.Point3d(-3.7109425451730429, 3.148970380522365, 2.5000000000000098)
        leaderData1.Leader.SetValue(draftingCurve1, baseView1, point6)
        ' Jog position relocated or jog removed
        leaderData1.Jogs.Clear()
        draftingNoteBuilder1.Destroy()
    End Sub
End Module