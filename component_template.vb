' NX 12.0.2.9
' Journal created by Ievgenz on Fri Feb  7 09:25:15 2020 Eastern Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Make Work Part")

Dim component1 As NXOpen.Assemblies.Component = CType(workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT TF3886-030-01 1"), NXOpen.Assemblies.Component)

Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
theSession.Parts.SetWorkComponent(component1, NXOpen.PartCollection.RefsetOption.Entire, NXOpen.PartCollection.WorkComponentOption.Visible, partLoadStatus1)

workPart = theSession.Parts.Work ' TF3886-030-01
partLoadStatus1.Dispose()
theSession.SetUndoMarkName(markId1, "Make Work Part")

' ----------------------------------------------
'   Menu: Insert->Sketch...
' ----------------------------------------------
Dim rotMatrix1 As NXOpen.Matrix3x3 = Nothing
rotMatrix1.Xx = 1.0
rotMatrix1.Xy = 0.0
rotMatrix1.Xz = 0.0
rotMatrix1.Yx = 0.0
rotMatrix1.Yy = 1.0
rotMatrix1.Yz = 0.0
rotMatrix1.Zx = 0.0
rotMatrix1.Zy = 0.0
rotMatrix1.Zz = 1.0
Dim translation1 As NXOpen.Point3d = New NXOpen.Point3d(-0.088128454137423679, 0.23620234045612032, -0.0)
displayPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix1, translation1, 2.1837704875262456)

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim nullNXOpen_Sketch As NXOpen.Sketch = Nothing

Dim sketchInPlaceBuilder1 As NXOpen.SketchInPlaceBuilder = Nothing
sketchInPlaceBuilder1 = workPart.Sketches.CreateSketchInPlaceBuilder2(nullNXOpen_Sketch)

Dim origin1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
Dim normal1 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 0.0, 1.0)
Dim plane1 As NXOpen.Plane = Nothing
plane1 = workPart.Planes.CreatePlane(origin1, normal1, NXOpen.SmartObject.UpdateOption.WithinModeling)

sketchInPlaceBuilder1.PlaneReference = plane1

Dim unit1 As NXOpen.Unit = CType(workPart.UnitCollection.FindObject("Inch"), NXOpen.Unit)

Dim expression1 As NXOpen.Expression = Nothing
expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

Dim expression2 As NXOpen.Expression = Nothing
expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

Dim sketchAlongPathBuilder1 As NXOpen.SketchAlongPathBuilder = Nothing
sketchAlongPathBuilder1 = workPart.Sketches.CreateSketchAlongPathBuilder(nullNXOpen_Sketch)

sketchInPlaceBuilder1.OriginOptionInfer = NXOpen.OriginMethod.WorkPartOrigin

sketchAlongPathBuilder1.PlaneLocation.Expression.RightHandSide = "0"

theSession.SetUndoMarkName(markId2, "Create Sketch Dialog")

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch")

theSession.DeleteUndoMark(markId3, Nothing)

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch")

theSession.Preferences.Sketch.CreateInferredConstraints = True

theSession.Preferences.Sketch.ContinuousAutoDimensioning = True

theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression

theSession.Preferences.Sketch.TextSizeFixed = True

theSession.Preferences.Sketch.FixedTextSize = 0.12

theSession.Preferences.Sketch.DisplayParenthesesOnReferenceDimensions = True

theSession.Preferences.Sketch.DisplayReferenceGeometry = False

theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0

theSession.Preferences.Sketch.DisplayObjectColor = False

theSession.Preferences.Sketch.DisplayObjectName = True

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = sketchInPlaceBuilder1.Commit()

Dim sketch1 As NXOpen.Sketch = CType(nXObject1, NXOpen.Sketch)

Dim feature1 As NXOpen.Features.Feature = Nothing
feature1 = sketch1.Feature

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "update")

Dim nErrs1 As Integer = Nothing
nErrs1 = theSession.UpdateManager.DoUpdate(markId5)

sketch1.Activate(NXOpen.Sketch.ViewReorient.True)

theSession.DeleteUndoMark(markId4, Nothing)

theSession.SetUndoMarkName(markId2, "Create Sketch")

sketchInPlaceBuilder1.Destroy()

sketchAlongPathBuilder1.Destroy()

Try
  ' Expression is still in use.
  workPart.Expressions.Delete(expression2)
Catch ex As NXException
  ex.AssertErrorCode(1050029)
End Try

Try
  ' Expression is still in use.
  workPart.Expressions.Delete(expression1)
Catch ex As NXException
  ex.AssertErrorCode(1050029)
End Try

plane1.DestroyPlane()

' ----------------------------------------------
'   Menu: Insert->Sketch Curve->Rectangle...
' ----------------------------------------------
Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list")

Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Rectangle")

theSession.SetUndoMarkVisibility(markId7, "Create Rectangle", NXOpen.Session.MarkVisibility.Visible)

' ----------------------------------------------
' Creating rectangle using From Center method 
' ----------------------------------------------
Dim startPoint1 As NXOpen.Point3d = New NXOpen.Point3d(-0.6999999999999994, -0.63918499737328938, 0.0)
Dim endPoint1 As NXOpen.Point3d = New NXOpen.Point3d(0.70000000000000073, -0.63918499737328938, 0.0)
Dim line1 As NXOpen.Line = Nothing
line1 = workPart.Curves.CreateLine(startPoint1, endPoint1)

Dim startPoint2 As NXOpen.Point3d = New NXOpen.Point3d(0.70000000000000073, -0.63918499737328938, 0.0)
Dim endPoint2 As NXOpen.Point3d = New NXOpen.Point3d(0.69999999999999929, 0.63918499737328938, 0.0)
Dim line2 As NXOpen.Line = Nothing
line2 = workPart.Curves.CreateLine(startPoint2, endPoint2)

Dim startPoint3 As NXOpen.Point3d = New NXOpen.Point3d(0.69999999999999929, 0.63918499737328938, 0.0)
Dim endPoint3 As NXOpen.Point3d = New NXOpen.Point3d(-0.70000000000000084, 0.63918499737328938, 0.0)
Dim line3 As NXOpen.Line = Nothing
line3 = workPart.Curves.CreateLine(startPoint3, endPoint3)

Dim startPoint4 As NXOpen.Point3d = New NXOpen.Point3d(-0.70000000000000084, 0.63918499737328938, 0.0)
Dim endPoint4 As NXOpen.Point3d = New NXOpen.Point3d(-0.6999999999999994, -0.63918499737328938, 0.0)
Dim line4 As NXOpen.Line = Nothing
line4 = workPart.Curves.CreateLine(startPoint4, endPoint4)

theSession.ActiveSketch.AddGeometry(line1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints)

theSession.ActiveSketch.AddGeometry(line2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints)

theSession.ActiveSketch.AddGeometry(line3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints)

theSession.ActiveSketch.AddGeometry(line4, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints)

Dim geom1_1 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1_1.Geometry = line1
geom1_1.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex
geom1_1.SplineDefiningPointIndex = 0
Dim geom2_1 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2_1.Geometry = line2
geom2_1.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex
geom2_1.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint1 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint1 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_1, geom2_1)

Dim geom1_2 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1_2.Geometry = line2
geom1_2.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex
geom1_2.SplineDefiningPointIndex = 0
Dim geom2_2 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2_2.Geometry = line3
geom2_2.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex
geom2_2.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint2 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint2 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_2, geom2_2)

Dim geom1_3 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1_3.Geometry = line3
geom1_3.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex
geom1_3.SplineDefiningPointIndex = 0
Dim geom2_3 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2_3.Geometry = line4
geom2_3.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex
geom2_3.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint3 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint3 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_3, geom2_3)

Dim geom1_4 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1_4.Geometry = line4
geom1_4.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex
geom1_4.SplineDefiningPointIndex = 0
Dim geom2_4 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2_4.Geometry = line1
geom2_4.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex
geom2_4.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint4 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint4 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_4, geom2_4)

Dim geom1 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1.Geometry = line1
geom1.PointType = NXOpen.Sketch.ConstraintPointType.None
geom1.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint5 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint5 = theSession.ActiveSketch.CreateHorizontalConstraint(geom1)

Dim conGeom1_1 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom1_1.Geometry = line1
conGeom1_1.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_1.SplineDefiningPointIndex = 0
Dim conGeom2_1 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_1.Geometry = line2
conGeom2_1.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_1.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint6 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint6 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_1, conGeom2_1)

Dim conGeom1_2 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom1_2.Geometry = line2
conGeom1_2.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_2.SplineDefiningPointIndex = 0
Dim conGeom2_2 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_2.Geometry = line3
conGeom2_2.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_2.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint7 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint7 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_2, conGeom2_2)

Dim conGeom1_3 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom1_3.Geometry = line3
conGeom1_3.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_3.SplineDefiningPointIndex = 0
Dim conGeom2_3 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_3.Geometry = line4
conGeom2_3.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_3.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint8 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint8 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_3, conGeom2_3)

Dim conGeom1_4 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom1_4.Geometry = line4
conGeom1_4.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_4.SplineDefiningPointIndex = 0
Dim conGeom2_4 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_4.Geometry = line1
conGeom2_4.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_4.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint9 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint9 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_4, conGeom2_4)

Dim conGeom1_5 As NXOpen.Sketch.ConstraintGeometry = Nothing
Dim datumCsys1 As NXOpen.Features.DatumCsys = CType(workPart.Features.FindObject("SKETCH(1:1B)"), NXOpen.Features.DatumCsys)

Dim point1 As NXOpen.Point = CType(datumCsys1.FindObject("POINT 1"), NXOpen.Point)

conGeom1_5.Geometry = point1
conGeom1_5.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_5.SplineDefiningPointIndex = 0
Dim conGeom2_5 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_5.Geometry = line1
conGeom2_5.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_5.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint10 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint10 = theSession.ActiveSketch.CreateMidpointConstraint(conGeom1_5, conGeom2_5)

Dim conGeom1_6 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom1_6.Geometry = point1
conGeom1_6.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom1_6.SplineDefiningPointIndex = 0
Dim conGeom2_6 As NXOpen.Sketch.ConstraintGeometry = Nothing
conGeom2_6.Geometry = line2
conGeom2_6.PointType = NXOpen.Sketch.ConstraintPointType.None
conGeom2_6.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint11 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint11 = theSession.ActiveSketch.CreateMidpointConstraint(conGeom1_6, conGeom2_6)

Dim geom2 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2.Geometry = line1
geom2.PointType = NXOpen.Sketch.ConstraintPointType.None
geom2.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint12 As NXOpen.SketchGeometricConstraint = Nothing
Try
  ' Constraint already exists.
  sketchGeometricConstraint12 = theSession.ActiveSketch.CreateHorizontalConstraint(geom2)
Catch ex As NXException
  ex.AssertErrorCode(910023)
End Try

Dim dimObject1_1 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject1_1.Geometry = line1
dimObject1_1.AssocType = NXOpen.Sketch.AssocType.StartPoint
dimObject1_1.AssocValue = 0
dimObject1_1.HelpPoint.X = 0.0
dimObject1_1.HelpPoint.Y = 0.0
dimObject1_1.HelpPoint.Z = 0.0
Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing

dimObject1_1.View = nullNXOpen_NXObject
Dim dimObject2_1 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject2_1.Geometry = line1
dimObject2_1.AssocType = NXOpen.Sketch.AssocType.EndPoint
dimObject2_1.AssocValue = 0
dimObject2_1.HelpPoint.X = 0.0
dimObject2_1.HelpPoint.Y = 0.0
dimObject2_1.HelpPoint.Z = 0.0
dimObject2_1.View = nullNXOpen_NXObject
Dim dimOrigin1 As NXOpen.Point3d = New NXOpen.Point3d(6.6613381477509392e-16, -0.80403748624807247, 0.0)
Dim nullNXOpen_Expression As NXOpen.Expression = Nothing

Dim sketchDimensionalConstraint1 As NXOpen.SketchDimensionalConstraint = Nothing
sketchDimensionalConstraint1 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_1, dimObject2_1, dimOrigin1, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic)

Dim sketchHelpedDimensionalConstraint1 As NXOpen.SketchHelpedDimensionalConstraint = CType(sketchDimensionalConstraint1, NXOpen.SketchHelpedDimensionalConstraint)

Dim dimension1 As NXOpen.Annotations.Dimension = Nothing
dimension1 = sketchHelpedDimensionalConstraint1.AssociatedDimension

Dim expression3 As NXOpen.Expression = Nothing
expression3 = sketchHelpedDimensionalConstraint1.AssociatedExpression

Dim dimObject1_2 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject1_2.Geometry = line2
dimObject1_2.AssocType = NXOpen.Sketch.AssocType.StartPoint
dimObject1_2.AssocValue = 0
dimObject1_2.HelpPoint.X = 0.0
dimObject1_2.HelpPoint.Y = 0.0
dimObject1_2.HelpPoint.Z = 0.0
dimObject1_2.View = nullNXOpen_NXObject
Dim dimObject2_2 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject2_2.Geometry = line2
dimObject2_2.AssocType = NXOpen.Sketch.AssocType.EndPoint
dimObject2_2.AssocValue = 0
dimObject2_2.HelpPoint.X = 0.0
dimObject2_2.HelpPoint.Y = 0.0
dimObject2_2.HelpPoint.Z = 0.0
dimObject2_2.View = nullNXOpen_NXObject
Dim dimOrigin2 As NXOpen.Point3d = New NXOpen.Point3d(0.86485248887478305, 1.861197763073103e-16, 0.0)
Dim sketchDimensionalConstraint2 As NXOpen.SketchDimensionalConstraint = Nothing
sketchDimensionalConstraint2 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_2, dimObject2_2, dimOrigin2, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic)

Dim sketchHelpedDimensionalConstraint2 As NXOpen.SketchHelpedDimensionalConstraint = CType(sketchDimensionalConstraint2, NXOpen.SketchHelpedDimensionalConstraint)

Dim dimension2 As NXOpen.Annotations.Dimension = Nothing
dimension2 = sketchHelpedDimensionalConstraint2.AssociatedDimension

Dim expression4 As NXOpen.Expression = Nothing
expression4 = sketchHelpedDimensionalConstraint2.AssociatedExpression

theSession.Preferences.Sketch.AutoDimensionsToArcCenter = False

theSession.ActiveSketch.Update()

theSession.Preferences.Sketch.AutoDimensionsToArcCenter = True

Dim geoms1(3) As NXOpen.SmartObject
geoms1(0) = line1
geoms1(1) = line2
geoms1(2) = line3
geoms1(3) = line4
theSession.ActiveSketch.UpdateConstraintDisplay(geoms1)

Dim geoms2(3) As NXOpen.SmartObject
geoms2(0) = line1
geoms2(1) = line2
geoms2(2) = line3
geoms2(3) = line4
theSession.ActiveSketch.UpdateDimensionDisplay(geoms2)

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim parallelDimension1 As NXOpen.Annotations.ParallelDimension = CType(dimension2, NXOpen.Annotations.ParallelDimension)

Dim sketchLinearDimensionBuilder1 As NXOpen.SketchLinearDimensionBuilder = Nothing
sketchLinearDimensionBuilder1 = workPart.Sketches.CreateLinearDimensionBuilder(parallelDimension1)

sketchLinearDimensionBuilder1.Driving.DrivingMethod = NXOpen.Annotations.DrivingValueBuilder.DrivingValueMethod.Inferred

sketchLinearDimensionBuilder1.Driving.DrivingMethod = NXOpen.Annotations.DrivingValueBuilder.DrivingValueMethod.Driving

theSession.SetUndoMarkName(markId8, "Linear Dimension Dialog")

sketchLinearDimensionBuilder1.Origin.SetInferRelativeToGeometry(True)

sketchLinearDimensionBuilder1.Driving.ExpressionName = "p6"

sketchLinearDimensionBuilder1.Style.OrdinateStyle.DoglegCreationOption = NXOpen.Annotations.OrdinateDoglegCreationOption.No

sketchLinearDimensionBuilder1.Origin.SetInferRelativeToGeometry(True)

sketchLinearDimensionBuilder1.Origin.SetInferRelativeToGeometry(True)

Dim nullNXOpen_Direction As NXOpen.Direction = Nothing

sketchLinearDimensionBuilder1.Measurement.Direction = nullNXOpen_Direction

Dim nullNXOpen_View As NXOpen.View = Nothing

sketchLinearDimensionBuilder1.Measurement.DirectionView = nullNXOpen_View

sketchLinearDimensionBuilder1.Origin.SetInferRelativeToGeometry(True)

' ----------------------------------------------
'   Dialog Begin Linear Dimension
' ----------------------------------------------
Dim markId9 As NXOpen.Session.UndoMarkId = Nothing
markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

theSession.DeleteUndoMark(markId9, Nothing)

Dim markId10 As NXOpen.Session.UndoMarkId = Nothing
markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

sketchLinearDimensionBuilder1.Driving.ExpressionValue.RightHandSide = "1"

sketchLinearDimensionBuilder1.Driving.ExpressionMode = NXOpen.Annotations.DrivingValueBuilder.DrivingExpressionMode.KeepExpression

sketchLinearDimensionBuilder1.Origin.SetInferRelativeToGeometry(True)

Dim nXObject2 As NXOpen.NXObject = Nothing
nXObject2 = sketchLinearDimensionBuilder1.Commit()

Dim point1_1 As NXOpen.Point3d = New NXOpen.Point3d(0.54757230134840096, -0.4999999999986619, 0.0)
Dim point2_1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
sketchLinearDimensionBuilder1.FirstAssociativity.SetValue(NXOpen.InferSnapType.SnapType.Start, line2, nullNXOpen_View, point1_1, Nothing, nullNXOpen_View, point2_1)

Dim point1_2 As NXOpen.Point3d = New NXOpen.Point3d(0.54757230134839985, 0.4999999999986619, 0.0)
Dim point2_2 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
sketchLinearDimensionBuilder1.SecondAssociativity.SetValue(NXOpen.InferSnapType.SnapType.End, line2, nullNXOpen_View, point1_2, Nothing, nullNXOpen_View, point2_2)

sketchLinearDimensionBuilder1.Driving.ExpressionValue.RightHandSide = "1"

theSession.SetUndoMarkName(markId10, "Linear Dimension - =")

theSession.SetUndoMarkVisibility(markId10, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.SetUndoMarkVisibility(markId8, Nothing, NXOpen.Session.MarkVisibility.Invisible)

Dim markId11 As NXOpen.Session.UndoMarkId = Nothing
markId11 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

Dim markId12 As NXOpen.Session.UndoMarkId = Nothing
markId12 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

Dim nXObject3 As NXOpen.NXObject = Nothing
nXObject3 = sketchLinearDimensionBuilder1.Commit()

theSession.DeleteUndoMark(markId12, Nothing)

theSession.SetUndoMarkName(markId8, "Linear Dimension")

Dim expression5 As NXOpen.Expression = sketchLinearDimensionBuilder1.Driving.ExpressionValue

sketchLinearDimensionBuilder1.Destroy()

theSession.DeleteUndoMark(markId11, Nothing)

theSession.SetUndoMarkVisibility(markId8, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.DeleteUndoMark(markId10, Nothing)

Dim markId13 As NXOpen.Session.UndoMarkId = Nothing
markId13 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim parallelDimension2 As NXOpen.Annotations.ParallelDimension = CType(dimension1, NXOpen.Annotations.ParallelDimension)

Dim sketchLinearDimensionBuilder2 As NXOpen.SketchLinearDimensionBuilder = Nothing
sketchLinearDimensionBuilder2 = workPart.Sketches.CreateLinearDimensionBuilder(parallelDimension2)

sketchLinearDimensionBuilder2.Driving.DrivingMethod = NXOpen.Annotations.DrivingValueBuilder.DrivingValueMethod.Inferred

sketchLinearDimensionBuilder2.Driving.DrivingMethod = NXOpen.Annotations.DrivingValueBuilder.DrivingValueMethod.Driving

theSession.SetUndoMarkName(markId13, "Linear Dimension Dialog")

sketchLinearDimensionBuilder2.Origin.SetInferRelativeToGeometry(True)

sketchLinearDimensionBuilder2.Driving.ExpressionName = "p7"

sketchLinearDimensionBuilder2.Style.OrdinateStyle.DoglegCreationOption = NXOpen.Annotations.OrdinateDoglegCreationOption.No

sketchLinearDimensionBuilder2.Origin.SetInferRelativeToGeometry(True)

sketchLinearDimensionBuilder2.Origin.SetInferRelativeToGeometry(True)

sketchLinearDimensionBuilder2.Measurement.Direction = nullNXOpen_Direction

sketchLinearDimensionBuilder2.Measurement.DirectionView = nullNXOpen_View

sketchLinearDimensionBuilder2.Origin.SetInferRelativeToGeometry(True)

' ----------------------------------------------
'   Dialog Begin Linear Dimension
' ----------------------------------------------
Dim markId14 As NXOpen.Session.UndoMarkId = Nothing
markId14 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

theSession.DeleteUndoMark(markId14, Nothing)

Dim markId15 As NXOpen.Session.UndoMarkId = Nothing
markId15 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

sketchLinearDimensionBuilder2.Driving.ExpressionValue.RightHandSide = "1"

sketchLinearDimensionBuilder2.Driving.ExpressionMode = NXOpen.Annotations.DrivingValueBuilder.DrivingExpressionMode.KeepExpression

sketchLinearDimensionBuilder2.Origin.SetInferRelativeToGeometry(True)

Dim nXObject4 As NXOpen.NXObject = Nothing
nXObject4 = sketchLinearDimensionBuilder2.Commit()

Dim point1_3 As NXOpen.Point3d = New NXOpen.Point3d(-0.5, -0.49999999999999994, 0.0)
Dim point2_3 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
sketchLinearDimensionBuilder2.FirstAssociativity.SetValue(NXOpen.InferSnapType.SnapType.Start, line1, nullNXOpen_View, point1_3, Nothing, nullNXOpen_View, point2_3)

Dim point1_4 As NXOpen.Point3d = New NXOpen.Point3d(0.5, -0.49999999999999994, 0.0)
Dim point2_4 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
sketchLinearDimensionBuilder2.SecondAssociativity.SetValue(NXOpen.InferSnapType.SnapType.End, line1, nullNXOpen_View, point1_4, Nothing, nullNXOpen_View, point2_4)

sketchLinearDimensionBuilder2.Driving.ExpressionValue.RightHandSide = "1"

theSession.SetUndoMarkName(markId15, "Linear Dimension - =")

theSession.SetUndoMarkVisibility(markId15, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.SetUndoMarkVisibility(markId13, Nothing, NXOpen.Session.MarkVisibility.Invisible)

Dim markId16 As NXOpen.Session.UndoMarkId = Nothing
markId16 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

Dim markId17 As NXOpen.Session.UndoMarkId = Nothing
markId17 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Linear Dimension")

Dim nXObject5 As NXOpen.NXObject = Nothing
nXObject5 = sketchLinearDimensionBuilder2.Commit()

theSession.DeleteUndoMark(markId17, Nothing)

theSession.SetUndoMarkName(markId13, "Linear Dimension")

Dim expression6 As NXOpen.Expression = sketchLinearDimensionBuilder2.Driving.ExpressionValue

sketchLinearDimensionBuilder2.Destroy()

theSession.DeleteUndoMark(markId16, Nothing)

theSession.SetUndoMarkVisibility(markId13, Nothing, NXOpen.Session.MarkVisibility.Visible)

theSession.DeleteUndoMark(markId15, Nothing)

' ----------------------------------------------
'   Menu: File->Finish Sketch
' ----------------------------------------------
Dim sketch2 As NXOpen.Sketch = Nothing
sketch2 = theSession.ActiveSketch

Dim markId18 As NXOpen.Session.UndoMarkId = Nothing
markId18 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch")

theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.True, NXOpen.Sketch.UpdateLevel.Model)

' ----------------------------------------------
'   Menu: Insert->Design Feature->Extrude...
' ----------------------------------------------
Dim markId19 As NXOpen.Session.UndoMarkId = Nothing
markId19 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim nullNXOpen_Features_Feature As NXOpen.Features.Feature = Nothing

Dim extrudeBuilder1 As NXOpen.Features.ExtrudeBuilder = Nothing
extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature)

Dim section1 As NXOpen.Section = Nothing
section1 = workPart.Sections.CreateSection(0.00038000000000000002, 0.00040000000000000002, 0.5)

extrudeBuilder1.Section = section1

extrudeBuilder1.AllowSelfIntersectingSection(True)

Dim unit2 As NXOpen.Unit = Nothing
unit2 = extrudeBuilder1.Draft.FrontDraftAngle.Units

Dim expression7 As NXOpen.Expression = Nothing
expression7 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2)

extrudeBuilder1.DistanceTolerance = 0.00040000000000000002

extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create

Dim targetBodies1(0) As NXOpen.Body
Dim nullNXOpen_Body As NXOpen.Body = Nothing

targetBodies1(0) = nullNXOpen_Body
extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies1)

extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0"

extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "1"

extrudeBuilder1.Offset.StartOffset.RightHandSide = "0"

extrudeBuilder1.Offset.EndOffset.RightHandSide = "0.25"

extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "1.5"

extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "4.3"

extrudeBuilder1.Draft.FrontDraftAngle.RightHandSide = "7"

extrudeBuilder1.Draft.BackDraftAngle.RightHandSide = "2"

extrudeBuilder1.Offset.StartOffset.RightHandSide = "0"

extrudeBuilder1.Offset.EndOffset.RightHandSide = "0.1"

Dim smartVolumeProfileBuilder1 As NXOpen.GeometricUtilities.SmartVolumeProfileBuilder = Nothing
smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile

smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = False

smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci

theSession.SetUndoMarkName(markId19, "Extrude Dialog")

section1.DistanceTolerance = 0.00040000000000000002

section1.ChainingTolerance = 0.00038000000000000002

section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves)

Dim markId20 As NXOpen.Session.UndoMarkId = Nothing
markId20 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark")

Dim markId21 As NXOpen.Session.UndoMarkId = Nothing
markId21 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, Nothing)

Dim features1(0) As NXOpen.Features.Feature
Dim sketchFeature1 As NXOpen.Features.SketchFeature = CType(feature1, NXOpen.Features.SketchFeature)

features1(0) = sketchFeature1
Dim curveFeatureRule1 As NXOpen.CurveFeatureRule = Nothing
curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1, component1)

section1.AllowSelfIntersection(True)

Dim rules1(0) As NXOpen.SelectionIntentRule
rules1(0) = curveFeatureRule1
Dim helpPoint1 As NXOpen.Point3d = New NXOpen.Point3d(-0.40318538712709473, 0.5, 0.0)
section1.AddToSection(rules1, line3, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint1, NXOpen.Section.Mode.Create, False)

theSession.DeleteUndoMark(markId21, Nothing)

Dim direction1 As NXOpen.Direction = Nothing
direction1 = workPart.Directions.CreateDirection(sketch2, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling)

extrudeBuilder1.Direction = direction1

Dim expression8 As NXOpen.Expression = Nothing
expression8 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

theSession.DeleteUndoMark(markId20, Nothing)

Dim rotMatrix2 As NXOpen.Matrix3x3 = Nothing
rotMatrix2.Xx = 0.66977109926389744
rotMatrix2.Xy = -0.099991916584706395
rotMatrix2.Xz = 0.73580451969836869
rotMatrix2.Yx = -0.021179591258351262
rotMatrix2.Yy = 0.98791675012889624
rotMatrix2.Yz = 0.15353149425733167
rotMatrix2.Zx = -0.74226551819747033
rotMatrix2.Zy = -0.11841499665362074
rotMatrix2.Zz = 0.65956029979264508
Dim translation2 As NXOpen.Point3d = New NXOpen.Point3d(-2.2219615612626931, -0.20903899289014155, 0.98727513060132921)
displayPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix2, translation2, 2.1837704875262451)

extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0"

extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "1"

Dim success1 As Boolean = Nothing
success1 = direction1.ReverseDirection()

extrudeBuilder1.Direction = direction1

Dim scaleAboutPoint1 As NXOpen.Point3d = New NXOpen.Point3d(0.90630708582782693, 0.96354753335376542, 0.0)
Dim viewCenter1 As NXOpen.Point3d = New NXOpen.Point3d(-0.90630708582777442, -0.9635475333537673, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint1, viewCenter1)

Dim scaleAboutPoint2 As NXOpen.Point3d = New NXOpen.Point3d(1.1984718700749459, 1.1448089505193253, 0.0)
Dim viewCenter2 As NXOpen.Point3d = New NXOpen.Point3d(-1.1984718700748949, -1.1448089505193273, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint2, viewCenter2)

Dim scaleAboutPoint3 As NXOpen.Point3d = New NXOpen.Point3d(1.6993257859271518, 1.2744943394453423, 0.0)
Dim viewCenter3 As NXOpen.Point3d = New NXOpen.Point3d(-1.6993257859271003, -1.2744943394453441, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint3, viewCenter3)

Dim scaleAboutPoint4 As NXOpen.Point3d = New NXOpen.Point3d(2.1800561069460094, 1.3322565098003207, 0.0)
Dim viewCenter4 As NXOpen.Point3d = New NXOpen.Point3d(-2.1800561069459579, -1.332256509800323, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint4, viewCenter4)

Dim scaleAboutPoint5 As NXOpen.Point3d = New NXOpen.Point3d(2.7250701336825065, 1.362535066841237, 0.0)
Dim viewCenter5 As NXOpen.Point3d = New NXOpen.Point3d(-2.7250701336824541, -1.362535066841239, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint5, viewCenter5)

Dim scaleAboutPoint6 As NXOpen.Point3d = New NXOpen.Point3d(0.96076190610602841, 1.3974718634269112, 0.0)
Dim viewCenter6 As NXOpen.Point3d = New NXOpen.Point3d(-0.96076190610597578, -1.3974718634269112, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint6, viewCenter6)

Dim scaleAboutPoint7 As NXOpen.Point3d = New NXOpen.Point3d(1.2227878804985752, 1.0015215021226183, 0.0)
Dim viewCenter7 As NXOpen.Point3d = New NXOpen.Point3d(-1.2227878804985222, -1.00152150212262, 0.0)
displayPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint7, viewCenter7)

Dim markId22 As NXOpen.Session.UndoMarkId = Nothing
markId22 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude")

theSession.DeleteUndoMark(markId22, Nothing)

Dim markId23 As NXOpen.Session.UndoMarkId = Nothing
markId23 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude")

extrudeBuilder1.ParentFeatureInternal = False

Dim feature2 As NXOpen.Features.Feature = Nothing
feature2 = extrudeBuilder1.CommitFeature()

theSession.DeleteUndoMark(markId23, Nothing)

theSession.SetUndoMarkName(markId19, "Extrude")

Dim expression9 As NXOpen.Expression = extrudeBuilder1.Limits.StartExtend.Value

Dim expression10 As NXOpen.Expression = extrudeBuilder1.Limits.EndExtend.Value

extrudeBuilder1.Destroy()

workPart.Expressions.Delete(expression7)

workPart.Expressions.Delete(expression8)

' ----------------------------------------------
'   Menu: Insert->Design Feature->Revolve...
' ----------------------------------------------
Dim markId24 As NXOpen.Session.UndoMarkId = Nothing
markId24 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim revolveBuilder1 As NXOpen.Features.RevolveBuilder = Nothing
revolveBuilder1 = workPart.Features.CreateRevolveBuilder(nullNXOpen_Features_Feature)

revolveBuilder1.Limits.StartExtend.Value.RightHandSide = "0"

revolveBuilder1.Limits.EndExtend.Value.RightHandSide = "360"

revolveBuilder1.Limits.StartExtend.Value.RightHandSide = "0"

revolveBuilder1.Limits.EndExtend.Value.RightHandSide = "360"

revolveBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create

Dim targetBodies2(0) As NXOpen.Body
targetBodies2(0) = nullNXOpen_Body
revolveBuilder1.BooleanOperation.SetTargetBodies(targetBodies2)

revolveBuilder1.Offset.StartOffset.RightHandSide = "0"

revolveBuilder1.Offset.EndOffset.RightHandSide = "0.05"

revolveBuilder1.Tolerance = 0.00040000000000000002

Dim section2 As NXOpen.Section = Nothing
section2 = workPart.Sections.CreateSection(0.00038000000000000002, 0.00040000000000000002, 0.5)

revolveBuilder1.Section = section2

Dim smartVolumeProfileBuilder2 As NXOpen.GeometricUtilities.SmartVolumeProfileBuilder = Nothing
smartVolumeProfileBuilder2 = revolveBuilder1.SmartVolumeProfile

smartVolumeProfileBuilder2.OpenProfileSmartVolumeOption = False

smartVolumeProfileBuilder2.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci

theSession.SetUndoMarkName(markId24, "Revolve Dialog")

section2.DistanceTolerance = 0.00040000000000000002

section2.ChainingTolerance = 0.00038000000000000002

Dim starthelperpoint1(2) As Double
starthelperpoint1(0) = 0.0
starthelperpoint1(1) = 0.0
starthelperpoint1(2) = 0.0
revolveBuilder1.SetStartLimitHelperPoint(starthelperpoint1)

Dim endhelperpoint1(2) As Double
endhelperpoint1(0) = 0.0
endhelperpoint1(1) = 0.0
endhelperpoint1(2) = 0.0
revolveBuilder1.SetEndLimitHelperPoint(endhelperpoint1)

section2.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves)

Dim datumAxis1 As NXOpen.DatumAxis = CType(workPart.Datums.FindObject("DATUM_CSYS(0) X axis"), NXOpen.DatumAxis)

Dim direction2 As NXOpen.Direction = Nothing
direction2 = workPart.Directions.CreateDirection(datumAxis1, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling)

Dim datumPlane1 As NXOpen.DatumPlane = CType(workPart.Datums.FindObject("DATUM_CSYS(0) XY plane"), NXOpen.DatumPlane)

Dim datumCsys2 As NXOpen.Features.DatumCsys = CType(workPart.Features.FindObject("DATUM_CSYS(0)"), NXOpen.Features.DatumCsys)

Dim point2 As NXOpen.Point = CType(datumCsys2.FindObject("POINT 1"), NXOpen.Point)

Dim xform1 As NXOpen.Xform = Nothing
xform1 = workPart.Xforms.CreateXformByPlaneXDirPoint(datumPlane1, direction2, point2, NXOpen.SmartObject.UpdateOption.WithinModeling, 0.625, False, True)

Dim cartesianCoordinateSystem1 As NXOpen.CartesianCoordinateSystem = Nothing
cartesianCoordinateSystem1 = workPart.CoordinateSystems.CreateCoordinateSystem(xform1, NXOpen.SmartObject.UpdateOption.WithinModeling)

Dim datumCsysBuilder1 As NXOpen.Features.DatumCsysBuilder = Nothing
datumCsysBuilder1 = workPart.Features.CreateDatumCsysBuilder(nullNXOpen_Features_Feature)

datumCsysBuilder1.Csys = cartesianCoordinateSystem1

datumCsysBuilder1.DisplayScaleFactor = 1.25

Dim feature3 As NXOpen.Features.Feature = Nothing
feature3 = datumCsysBuilder1.CommitFeature()

datumCsysBuilder1.Destroy()

Dim markId25 As NXOpen.Session.UndoMarkId = Nothing
markId25 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Sketch")

theSession.BeginTaskEnvironment()

Dim sketchInPlaceBuilder2 As NXOpen.SketchInPlaceBuilder = Nothing
sketchInPlaceBuilder2 = workPart.Sketches.CreateSketchInPlaceBuilder2(nullNXOpen_Sketch)

sketchInPlaceBuilder2.Csystem = cartesianCoordinateSystem1

sketchInPlaceBuilder2.PlaneOption = NXOpen.Sketch.PlaneOption.Inferred

theSession.Preferences.Sketch.CreateInferredConstraints = True

theSession.Preferences.Sketch.ContinuousAutoDimensioning = True

theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression

theSession.Preferences.Sketch.TextSizeFixed = True

theSession.Preferences.Sketch.FixedTextSize = 0.12

theSession.Preferences.Sketch.DisplayParenthesesOnReferenceDimensions = True

theSession.Preferences.Sketch.DisplayReferenceGeometry = False

theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0

theSession.Preferences.Sketch.DisplayObjectColor = False

theSession.Preferences.Sketch.DisplayObjectName = True

Dim nXObject6 As NXOpen.NXObject = Nothing
nXObject6 = sketchInPlaceBuilder2.Commit()

sketchInPlaceBuilder2.Destroy()

Dim sketch3 As NXOpen.Sketch = CType(nXObject6, NXOpen.Sketch)

sketch3.Activate(NXOpen.Sketch.ViewReorient.True)

Dim markId26 As NXOpen.Session.UndoMarkId = Nothing
markId26 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Open Sketch")

theSession.DeleteUndoMarksUpToMark(markId26, Nothing, True)

theSession.DeleteUndoMarksUpToMark(markId26, Nothing, False)

Dim markId27 As NXOpen.Session.UndoMarkId = Nothing
markId27 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Open Sketch")

theSession.ActiveSketch.SetName("SKETCH_001")

Dim markId28 As NXOpen.Session.UndoMarkId = Nothing
markId28 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list")

' ----------------------------------------------
'   Dialog Begin Profile
' ----------------------------------------------
' ----------------------------------------------
'   Menu: Insert->Curve->Line...
' ----------------------------------------------
theSession.DeleteUndoMark(markId28, "Curve")

Dim markId29 As NXOpen.Session.UndoMarkId = Nothing
markId29 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list")

Dim markId30 As NXOpen.Session.UndoMarkId = Nothing
markId30 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve")

Dim expression11 As NXOpen.Expression = Nothing
expression11 = workPart.Expressions.CreateSystemExpression("1")

theSession.SetUndoMarkVisibility(markId30, "Curve", NXOpen.Session.MarkVisibility.Visible)

Dim startPoint5 As NXOpen.Point3d = New NXOpen.Point3d(0.5, 0.0, 0.0)
Dim endPoint5 As NXOpen.Point3d = New NXOpen.Point3d(0.50000000000000056, 0.0, 1.0)
Dim line5 As NXOpen.Line = Nothing
line5 = workPart.Curves.CreateLine(startPoint5, endPoint5)

theSession.ActiveSketch.AddGeometry(line5, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints)

Dim geom1_5 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom1_5.Geometry = line5
geom1_5.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex
geom1_5.SplineDefiningPointIndex = 0
Dim geom2_5 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom2_5.Geometry = line1
geom2_5.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex
geom2_5.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint13 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint13 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_5, geom2_5)

Dim geom3 As NXOpen.Sketch.ConstraintGeometry = Nothing
geom3.Geometry = line5
geom3.PointType = NXOpen.Sketch.ConstraintPointType.None
geom3.SplineDefiningPointIndex = 0
Dim sketchGeometricConstraint14 As NXOpen.SketchGeometricConstraint = Nothing
sketchGeometricConstraint14 = theSession.ActiveSketch.CreateVerticalConstraint(geom3)

Dim dimObject1_3 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject1_3.Geometry = line5
dimObject1_3.AssocType = NXOpen.Sketch.AssocType.StartPoint
dimObject1_3.AssocValue = 0
dimObject1_3.HelpPoint.X = 0.0
dimObject1_3.HelpPoint.Y = 0.0
dimObject1_3.HelpPoint.Z = 0.0
dimObject1_3.View = nullNXOpen_NXObject
Dim dimObject2_3 As NXOpen.Sketch.DimensionGeometry = Nothing
dimObject2_3.Geometry = line5
dimObject2_3.AssocType = NXOpen.Sketch.AssocType.EndPoint
dimObject2_3.AssocValue = 0
dimObject2_3.HelpPoint.X = 0.0
dimObject2_3.HelpPoint.Y = 0.0
dimObject2_3.HelpPoint.Z = 0.0
dimObject2_3.View = nullNXOpen_NXObject
Dim dimOrigin3 As NXOpen.Point3d = New NXOpen.Point3d(0.77514795687647686, 0.0, 0.49999999999999983)
Dim sketchDimensionalConstraint3 As NXOpen.SketchDimensionalConstraint = Nothing
sketchDimensionalConstraint3 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_3, dimObject2_3, dimOrigin3, expression11, NXOpen.Sketch.DimensionOption.CreateAsDriving)

Dim sketchHelpedDimensionalConstraint3 As NXOpen.SketchHelpedDimensionalConstraint = CType(sketchDimensionalConstraint3, NXOpen.SketchHelpedDimensionalConstraint)

Dim dimension3 As NXOpen.Annotations.Dimension = Nothing
dimension3 = sketchHelpedDimensionalConstraint3.AssociatedDimension

theSession.Preferences.Sketch.AutoDimensionsToArcCenter = False

theSession.ActiveSketch.Update()

theSession.Preferences.Sketch.AutoDimensionsToArcCenter = True

' ----------------------------------------------
'   Dialog Begin Line
' ----------------------------------------------
' ----------------------------------------------
'   Menu: Task->Finish Sketch
' ----------------------------------------------
Dim markId31 As NXOpen.Session.UndoMarkId = Nothing
markId31 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch")

theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.True, NXOpen.Sketch.UpdateLevel.SketchOnly)

theSession.DeleteUndoMarksSetInTaskEnvironment()

theSession.EndTaskEnvironment()

theSession.DeleteUndoMark(markId25, Nothing)

section2.DistanceTolerance = 0.00040000000000000002

section2.ChainingTolerance = 0.00038000000000000002

Dim starthelperpoint2(2) As Double
starthelperpoint2(0) = 0.0
starthelperpoint2(1) = 0.0
starthelperpoint2(2) = 0.0
revolveBuilder1.SetStartLimitHelperPoint(starthelperpoint2)

Dim endhelperpoint2(2) As Double
endhelperpoint2(0) = 0.0
endhelperpoint2(1) = 0.0
endhelperpoint2(2) = 0.0
revolveBuilder1.SetEndLimitHelperPoint(endhelperpoint2)

Dim markId32 As NXOpen.Session.UndoMarkId = Nothing
markId32 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, Nothing)

Dim features2(0) As NXOpen.Features.Feature
Dim sketchFeature2 As NXOpen.Features.SketchFeature = CType(workPart.Features.FindObject("SKETCH(3)"), NXOpen.Features.SketchFeature)

features2(0) = sketchFeature2
Dim curveFeatureRule2 As NXOpen.CurveFeatureRule = Nothing
curveFeatureRule2 = workPart.ScRuleFactory.CreateRuleCurveFeature(features2)

section2.AllowSelfIntersection(False)

Dim rules2(0) As NXOpen.SelectionIntentRule
rules2(0) = curveFeatureRule2
Dim helpPoint2 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
section2.AddToSection(rules2, nullNXOpen_NXObject, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint2, NXOpen.Section.Mode.Create, False)

theSession.DeleteUndoMark(markId32, Nothing)

Dim refs1() As NXOpen.NXObject
section2.EvaluateAndAskOutputEntities(refs1)

revolveBuilder1.Section = section2

Dim expression12 As NXOpen.Expression = Nothing
expression12 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

Dim datumAxis2 As NXOpen.DatumAxis = CType(workPart.Datums.FindObject("DATUM_CSYS(0) Z axis"), NXOpen.DatumAxis)

Dim direction3 As NXOpen.Direction = Nothing
direction3 = workPart.Directions.CreateDirection(datumAxis2, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling)

Dim nullNXOpen_Point As NXOpen.Point = Nothing

Dim axis1 As NXOpen.Axis = Nothing
axis1 = workPart.Axes.CreateAxis(nullNXOpen_Point, direction3, NXOpen.SmartObject.UpdateOption.WithinModeling)

axis1.Point = nullNXOpen_Point

axis1.Evaluate()

revolveBuilder1.Axis = axis1

Dim markId33 As NXOpen.Session.UndoMarkId = Nothing
markId33 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Revolve")

theSession.DeleteUndoMark(markId33, Nothing)

Dim markId34 As NXOpen.Session.UndoMarkId = Nothing
markId34 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Revolve")

revolveBuilder1.ParentFeatureInternal = True

Dim feature4 As NXOpen.Features.Feature = Nothing
feature4 = revolveBuilder1.CommitFeature()

theSession.DeleteUndoMark(markId34, Nothing)

theSession.SetUndoMarkName(markId24, "Revolve")

Dim expression13 As NXOpen.Expression = revolveBuilder1.Limits.StartExtend.Value

Dim expression14 As NXOpen.Expression = revolveBuilder1.Limits.EndExtend.Value

revolveBuilder1.Destroy()

workPart.Expressions.Delete(expression12)

' ----------------------------------------------
'   Menu: Assemblies->Context Control->Work on Displayed Assembly
' ----------------------------------------------
Dim markId35 As NXOpen.Session.UndoMarkId = Nothing
markId35 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Reset Work Part")

Dim nullNXOpen_Assemblies_Component As NXOpen.Assemblies.Component = Nothing

Dim partLoadStatus2 As NXOpen.PartLoadStatus = Nothing
theSession.Parts.SetWorkComponent(nullNXOpen_Assemblies_Component, NXOpen.PartCollection.RefsetOption.Entire, NXOpen.PartCollection.WorkComponentOption.Visible, partLoadStatus2)

workPart = theSession.Parts.Work ' TF3886-01
partLoadStatus2.Dispose()
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module