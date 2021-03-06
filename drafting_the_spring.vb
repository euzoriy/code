' NX 11.0.2.7
' Journal created by ievgenz on Mon Dec 04 15:19:37 2017 Eastern Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Tools->Expressions...
' ----------------------------------------------
theSession.Preferences.Modeling.UpdatePending = False

Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

theSession.SetUndoMarkName(markId1, "Expressions Dialog")

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Expressions")

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Make Up to Date")

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Expression")

Dim unit1 As NXOpen.Unit = CType(workPart.UnitCollection.FindObject("Inch"), NXOpen.Unit)

Dim expression1 As NXOpen.Expression = Nothing
expression1 = workPart.Expressions.CreateWithUnits("OL=1.25", unit1)

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Check Circular")

Dim objects1(0) As NXOpen.NXObject
objects1(0) = expression1
theSession.UpdateManager.MakeUpToDate(objects1, markId5)

expression1.EditComment("")

Dim objects2(0) As NXOpen.NXObject
objects2(0) = expression1
theSession.UpdateManager.MakeUpToDate(objects2, markId3)

Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "NX update")

Dim nErrs1 As Integer = Nothing
nErrs1 = theSession.UpdateManager.DoUpdate(markId6)

theSession.DeleteUndoMark(markId6, "NX update")

theSession.DeleteUndoMark(markId3, Nothing)

theSession.DeleteUndoMark(markId2, Nothing)

theSession.SetUndoMarkName(markId1, "Expressions")

Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

theSession.SetUndoMarkName(markId7, "Expressions Dialog")

' ----------------------------------------------
'   Dialog Begin Expressions
' ----------------------------------------------
Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Expressions")

Dim markId9 As NXOpen.Session.UndoMarkId = Nothing
markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Make Up to Date")

Dim markId10 As NXOpen.Session.UndoMarkId = Nothing
markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Expression")

Dim expression2 As NXOpen.Expression = Nothing
expression2 = workPart.Expressions.CreateWithUnits("CL=.47", unit1)

Dim markId11 As NXOpen.Session.UndoMarkId = Nothing
markId11 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Check Circular")

Dim objects3(0) As NXOpen.NXObject
objects3(0) = expression2
theSession.UpdateManager.MakeUpToDate(objects3, markId11)

expression2.EditComment("")

Dim objects4(0) As NXOpen.NXObject
objects4(0) = expression2
theSession.UpdateManager.MakeUpToDate(objects4, markId9)

Dim markId12 As NXOpen.Session.UndoMarkId = Nothing
markId12 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "NX update")

Dim nErrs2 As Integer = Nothing
nErrs2 = theSession.UpdateManager.DoUpdate(markId12)

theSession.DeleteUndoMark(markId12, "NX update")

theSession.DeleteUndoMark(markId9, Nothing)

theSession.DeleteUndoMark(markId8, Nothing)

theSession.SetUndoMarkName(markId7, "Expressions")

Dim markId13 As NXOpen.Session.UndoMarkId = Nothing
markId13 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

theSession.SetUndoMarkName(markId13, "Expressions Dialog")

' ----------------------------------------------
'   Dialog Begin Expressions
' ----------------------------------------------
Dim markId14 As NXOpen.Session.UndoMarkId = Nothing
markId14 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Expressions")

Dim markId15 As NXOpen.Session.UndoMarkId = Nothing
markId15 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Make Up to Date")

Dim markId16 As NXOpen.Session.UndoMarkId = Nothing
markId16 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Expression")

Dim expression3 As NXOpen.Expression = Nothing
expression3 = workPart.Expressions.CreateWithUnits("WD=.023", unit1)

Dim markId17 As NXOpen.Session.UndoMarkId = Nothing
markId17 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Check Circular")

Dim objects5(0) As NXOpen.NXObject
objects5(0) = expression3
theSession.UpdateManager.MakeUpToDate(objects5, markId17)

expression3.EditComment("")

Dim objects6(0) As NXOpen.NXObject
objects6(0) = expression3
theSession.UpdateManager.MakeUpToDate(objects6, markId15)

Dim markId18 As NXOpen.Session.UndoMarkId = Nothing
markId18 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "NX update")

Dim nErrs3 As Integer = Nothing
nErrs3 = theSession.UpdateManager.DoUpdate(markId18)

theSession.DeleteUndoMark(markId18, "NX update")

theSession.DeleteUndoMark(markId15, Nothing)

theSession.DeleteUndoMark(markId14, Nothing)

theSession.SetUndoMarkName(markId13, "Expressions")

Dim markId19 As NXOpen.Session.UndoMarkId = Nothing
markId19 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

theSession.SetUndoMarkName(markId19, "Expressions Dialog")

' ----------------------------------------------
'   Dialog Begin Expressions
' ----------------------------------------------
theSession.UndoToMark(markId19, Nothing)

theSession.DeleteUndoMark(markId19, Nothing)

' ----------------------------------------------
'   Menu: Insert->Curve->Helix...
' ----------------------------------------------
Dim markId20 As NXOpen.Session.UndoMarkId = Nothing
markId20 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim nullNXOpen_Features_Helix As NXOpen.Features.Helix = Nothing

Dim helixBuilder1 As NXOpen.Features.HelixBuilder = Nothing
helixBuilder1 = workPart.Features.CreateHelixBuilder(nullNXOpen_Features_Helix)

Dim expression4 As NXOpen.Expression = Nothing
expression4 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

Dim expression5 As NXOpen.Expression = Nothing
expression5 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)

helixBuilder1.OrientationOption = NXOpen.Features.HelixBuilder.OrientationOptions.Specified

theSession.SetUndoMarkName(markId20, "Helix Dialog")

helixBuilder1.Spine.DistanceTolerance = 0.00040000000000000002

helixBuilder1.Spine.ChainingTolerance = 0.00038000000000000002

helixBuilder1.SizeLaw.AlongSpineData.Spine.DistanceTolerance = 0.00040000000000000002

helixBuilder1.SizeLaw.AlongSpineData.Spine.ChainingTolerance = 0.00038000000000000002

helixBuilder1.SizeLaw.LawCurve.DistanceTolerance = 0.00040000000000000002

helixBuilder1.SizeLaw.LawCurve.ChainingTolerance = 0.00038000000000000002

helixBuilder1.PitchLaw.AlongSpineData.Spine.DistanceTolerance = 0.00040000000000000002

helixBuilder1.PitchLaw.AlongSpineData.Spine.ChainingTolerance = 0.00038000000000000002

helixBuilder1.PitchLaw.LawCurve.DistanceTolerance = 0.00040000000000000002

helixBuilder1.PitchLaw.LawCurve.ChainingTolerance = 0.00038000000000000002

helixBuilder1.Spine.AngleTolerance = 0.5

helixBuilder1.SizeLaw.AlongSpineData.Spine.AngleTolerance = 0.5

helixBuilder1.SizeLaw.LawCurve.AngleTolerance = 0.5

helixBuilder1.PitchLaw.AlongSpineData.Spine.AngleTolerance = 0.5

helixBuilder1.PitchLaw.LawCurve.AngleTolerance = 0.5

helixBuilder1.StartLimit.IsPercentUsed = False

helixBuilder1.StartLimit.Expression.RightHandSide = "0"

helixBuilder1.EndLimit.IsPercentUsed = False

helixBuilder1.EndLimit.Expression.RightHandSide = "5"

Dim origin1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
Dim xDirection1 As NXOpen.Vector3d = New NXOpen.Vector3d(1.0, 0.0, 0.0)
Dim yDirection1 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 1.0, 0.0)
Dim xform1 As NXOpen.Xform = Nothing
xform1 = workPart.Xforms.CreateXform(origin1, xDirection1, yDirection1, NXOpen.SmartObject.UpdateOption.WithinModeling, 1.0)

Dim cartesianCoordinateSystem1 As NXOpen.CartesianCoordinateSystem = Nothing
cartesianCoordinateSystem1 = workPart.CoordinateSystems.CreateCoordinateSystem(xform1, NXOpen.SmartObject.UpdateOption.WithinModeling)

helixBuilder1.CoordinateSystem = cartesianCoordinateSystem1

Dim scaleAboutPoint1 As NXOpen.Point3d = New NXOpen.Point3d(-0.15095609687158709, 0.20605970278483521, 0.0)
Dim viewCenter1 As NXOpen.Point3d = New NXOpen.Point3d(0.15095609687158709, -0.20605970278483496, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint1, viewCenter1)

Dim scaleAboutPoint2 As NXOpen.Point3d = New NXOpen.Point3d(-0.16554234549568222, 0.25757462848104401, 0.0)
Dim viewCenter2 As NXOpen.Point3d = New NXOpen.Point3d(0.16554234549568222, -0.25757462848104379, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint2, viewCenter2)

Dim scaleAboutPoint3 As NXOpen.Point3d = New NXOpen.Point3d(-0.1808810593265758, 0.32196828560130486, 0.0)
Dim viewCenter3 As NXOpen.Point3d = New NXOpen.Point3d(0.1808810593265758, -0.32196828560130486, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint3, viewCenter3)

Dim scaleAboutPoint4 As NXOpen.Point3d = New NXOpen.Point3d(-0.23514537712454861, 0.38256344047570778, 0.0)
Dim viewCenter4 As NXOpen.Point3d = New NXOpen.Point3d(0.23514537712454861, -0.38256344047570778, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint4, viewCenter4)

Dim scaleAboutPoint5 As NXOpen.Point3d = New NXOpen.Point3d(-0.26679956250669934, 0.40585187686400465, 0.0)
Dim viewCenter5 As NXOpen.Point3d = New NXOpen.Point3d(0.26679956250669934, -0.40585187686400465, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint5, viewCenter5)

Dim scaleAboutPoint6 As NXOpen.Point3d = New NXOpen.Point3d(-0.3334994531333742, 0.50731484608000577, 0.0)
Dim viewCenter6 As NXOpen.Point3d = New NXOpen.Point3d(0.3334994531333742, -0.50731484608000554, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint6, viewCenter6)

Dim scaleAboutPoint7 As NXOpen.Point3d = New NXOpen.Point3d(-0.41687431641671774, 0.63414355760000696, 0.0)
Dim viewCenter7 As NXOpen.Point3d = New NXOpen.Point3d(0.41687431641671774, -0.63414355760000696, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint7, viewCenter7)

Dim scaleAboutPoint8 As NXOpen.Point3d = New NXOpen.Point3d(-0.52109289552089721, 0.79267944700000836, 0.0)
Dim viewCenter8 As NXOpen.Point3d = New NXOpen.Point3d(0.52109289552089721, -0.79267944700000836, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint8, viewCenter8)

Dim scaleAboutPoint9 As NXOpen.Point3d = New NXOpen.Point3d(-0.65136611940112144, 0.99084930875001087, 0.0)
Dim viewCenter9 As NXOpen.Point3d = New NXOpen.Point3d(0.65136611940112144, -0.99084930875001087, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint9, viewCenter9)

Dim scaleAboutPoint10 As NXOpen.Point3d = New NXOpen.Point3d(-2.2977215864467517, 1.2730619600583353, 0.0)
Dim viewCenter10 As NXOpen.Point3d = New NXOpen.Point3d(2.2977215864467517, -1.2730619600583353, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint10, viewCenter10)

Dim scaleAboutPoint11 As NXOpen.Point3d = New NXOpen.Point3d(-1.810577009860743, 1.0184495680466683, 0.0)
Dim viewCenter11 As NXOpen.Point3d = New NXOpen.Point3d(1.8105770098607448, -1.0184495680466683, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint11, viewCenter11)

Dim scaleAboutPoint12 As NXOpen.Point3d = New NXOpen.Point3d(-1.3115643217771729, 0.814759654437335, 0.0)
Dim viewCenter12 As NXOpen.Point3d = New NXOpen.Point3d(1.3115643217771735, -0.814759654437335, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint12, viewCenter12)

Dim scaleAboutPoint13 As NXOpen.Point3d = New NXOpen.Point3d(-1.0139231255220167, 0.65180772354986782, 0.0)
Dim viewCenter13 As NXOpen.Point3d = New NXOpen.Point3d(1.0139231255220171, -0.65180772354986782, 0.0)
workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint13, viewCenter13)

Dim rotMatrix1 As NXOpen.Matrix3x3 = Nothing
rotMatrix1.Xx = -0.13856635211821514
rotMatrix1.Xy = 0.073353779093022028
rotMatrix1.Xz = -0.98763282101873406
rotMatrix1.Yx = -0.67582786231772207
rotMatrix1.Yy = 0.72195702624008462
rotMatrix1.Yz = 0.14844107510265442
rotMatrix1.Zx = 0.7239171683112019
rotMatrix1.Zy = 0.68803871646539339
rotMatrix1.Zz = -0.050464423794839557
Dim translation1 As NXOpen.Point3d = New NXOpen.Point3d(1.1979738124665305, -0.86155967367609043, 1.5752247462176718)
workPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix1, translation1, 3.6856632150910755)

helixBuilder1.EndLimit.Expression.RightHandSide = "OL"

helixBuilder1.EndLimit.Expression.RightHandSide = "OL"

helixBuilder1.EndLimit.Expression.RightHandSide = "WD"

helixBuilder1.EndLimit.Expression.RightHandSide = "WD"

helixBuilder1.Destroy()

Dim markId21 As NXOpen.Session.UndoMarkId = Nothing
markId21 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "")

Dim nErrs4 As Integer = Nothing
nErrs4 = theSession.UpdateManager.DoUpdate(markId21)

theSession.DeleteUndoMark(markId21, "")

workPart.Expressions.Delete(expression4)

workPart.Expressions.Delete(expression5)

theSession.UndoToMark(markId20, Nothing)

theSession.DeleteUndoMark(markId20, Nothing)

' ----------------------------------------------
'   Menu: Window->Switch Window...
' ----------------------------------------------
Dim markId22 As NXOpen.Session.UndoMarkId = Nothing
markId22 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Change Display Part")

Dim part1 As NXOpen.Part = CType(theSession.Parts.FindObject("TF3406-040-01"), NXOpen.Part)

Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
Dim status1 As NXOpen.PartCollection.SdpsStatus = Nothing
status1 = theSession.Parts.SetDisplay(part1, False, True, partLoadStatus1)

workPart = theSession.Parts.Work ' TF3406-040-01
displayPart = theSession.Parts.Display ' TF3406-040-01
partLoadStatus1.Dispose()
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module