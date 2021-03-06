' NX 11.0.1.11
' Journal created by ievgenz on Thu May 18 15:49:23 2017 Eastern Daylight Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Analysis->Measure Distance...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing

Dim measureDistanceBuilder1 As NXOpen.MeasureDistanceBuilder = Nothing
measureDistanceBuilder1 = workPart.MeasureManager.CreateMeasureDistanceBuilder(nullNXOpen_NXObject)

measureDistanceBuilder1.Mtype = NXOpen.MeasureDistanceBuilder.MeasureType.Minimum

theSession.SetUndoMarkName(markId1, "Measure Distance Dialog")

Dim component1 As NXOpen.Assemblies.Component = CType(workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT 5319888-01 1"), NXOpen.Assemblies.Component)

Dim component2 As NXOpen.Assemblies.Component = CType(component1.FindObject("COMPONENT 5319888-02 1"), NXOpen.Assemblies.Component)

Dim face1 As NXOpen.Face = CType(component2.FindObject("PROTO#.Features|INSTANCE[3](53)|FACE 180 {(-1.0135757415498,-3.1194653734481,0.81925) REVOLVED(7)}"), NXOpen.Face)

measureDistanceBuilder1.DiameterObjects.Value = face1

measureDistanceBuilder1.RequirementMode = NXOpen.MeasureBuilder.RequirementType.New

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start")

Dim nullNXOpen_Validate_Requirement As NXOpen.Validate.Requirement = Nothing

Dim requirementBuilder1 As NXOpen.Validate.RequirementBuilder = Nothing
requirementBuilder1 = workPart.Requirements.CreateRequirementBuilder(nullNXOpen_Validate_Requirement)

requirementBuilder1.RelationalOperatorOptionOnMinimumValue = NXOpen.Validate.RequirementBuilder.RelationalOperatorOptions.LessThan

requirementBuilder1.RelationalOperatorOptionOnMaximumValue = NXOpen.Validate.RequirementBuilder.RelationalOperatorOptions.LessThan

requirementBuilder1.Name = "D_6-560"

requirementBuilder1.SeverityOption = NXOpen.Validate.RequirementBuilder.SeverityOptions.Warning

requirementBuilder1.SingleSidedValue = "6.560"

Dim validvalues1(-1) As String
requirementBuilder1.SetValidValues(validvalues1)

Dim requirementdescription1(-1) As String
requirementBuilder1.SetRequirementDescription(requirementdescription1)

theSession.SetUndoMarkName(markId2, "Ad Hoc Requirement Dialog")

' ----------------------------------------------
'   Dialog Begin Ad Hoc Requirement
' ----------------------------------------------
Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Ad Hoc Requirement")

theSession.DeleteUndoMark(markId3, Nothing)

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Ad Hoc Requirement")

theSession.UndoToMarkWithStatus(markId4, Nothing)

theSession.DeleteUndoMark(markId4, Nothing)

requirementBuilder1.Name = "D_6-561"

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Ad Hoc Requirement")

theSession.DeleteUndoMark(markId5, Nothing)

Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Ad Hoc Requirement")

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = requirementBuilder1.Commit()

theSession.DeleteUndoMark(markId6, Nothing)

theSession.SetUndoMarkName(markId2, "Ad Hoc Requirement")

requirementBuilder1.Destroy()

theSession.DeleteUndoMark(markId2, Nothing)

Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Distance")

theSession.DeleteUndoMark(markId7, Nothing)

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Distance")

Dim unit1 As NXOpen.Unit = CType(workPart.UnitCollection.FindObject("Inch"), NXOpen.Unit)

Dim measureDistance1 As NXOpen.MeasureDistance = Nothing
measureDistance1 = workPart.MeasureManager.NewDistance(unit1, face1, NXOpen.MeasureManager.RadialMeasureType.Diameter)

Dim measure1 As NXOpen.Measure = Nothing
measure1 = measureDistance1.CreateFeature()

measureDistance1.Dispose()
Dim requirement1 As NXOpen.Validate.Requirement = CType(nXObject1, NXOpen.Validate.Requirement)

Dim requirementCheck1 As NXOpen.Validate.RequirementCheck = Nothing
requirementCheck1 = requirement1.NewCheck("p66.A", "p66")

measureDistanceBuilder1.RequirementMode = NXOpen.MeasureBuilder.RequirementType.None

Dim datadeleted1 As Boolean = Nothing
datadeleted1 = theSession.DeleteTransientDynamicSectionCutData()

theSession.DeleteUndoMark(markId8, Nothing)

theSession.SetUndoMarkName(markId1, "Measure Distance")

measureDistanceBuilder1.Destroy()

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module