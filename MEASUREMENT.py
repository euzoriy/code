# NX 11.0.0.33
# Journal created by ievgenz on Tue Dec 06 12:12:58 2016 Eastern Standard Time
#
import math
import NXOpen
import NXOpen.Assemblies
import NXOpen.Validate
def main() : 

    theSession  = NXOpen.Session.GetSession()
    workPart = theSession.Parts.Work
    displayPart = theSession.Parts.Display
    # ----------------------------------------------
    #   Menu: Analysis->Measure Angle...
    # ----------------------------------------------
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
    
    measureAngleBuilder1 = workPart.MeasureManager.CreateMeasureAngleBuilder(NXOpen.NXObject.Null)
    
    theSession.SetUndoMarkName(markId1, "Measure Angle Dialog")
  
   
    component1 = workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT 5321478-01 1")
    component2 = component1.FindObject("COMPONENT 5321478-02 1")
    face1 = component2.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(1)|FACE 565 {(3.8980508156116,-0.9627352096243,2.3693754987049) UNPARAMETERIZED_FEATURE(1)}")
    line1 = workPart.Lines.CreateFaceAxis(face1, NXOpen.SmartObject.UpdateOption.AfterModeling)
    
    face2 = component2.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(1)|FACE 248 {(-4.0060872982709,0,1.9483415020066) UNPARAMETERIZED_FEATURE(1)}")
    line2 = workPart.Lines.CreateFaceAxis(face2, NXOpen.SmartObject.UpdateOption.AfterModeling)
    
    face3 = component2.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(1)|FACE 564 {(3.9590987177381,-0.8532510434827,2.3380711089057) UNPARAMETERIZED_FEATURE(1)}")
    measureAngleBuilder1.Object1.Value = face3
    
    objects1 = [NXOpen.NXObject.Null] * 1 
    objects1[0] = line1
    nErrs1 = theSession.UpdateManager.AddToDeleteList(objects1)
    
    objects2 = [NXOpen.NXObject.Null] * 1 
    objects2[0] = line2
    nErrs2 = theSession.UpdateManager.AddToDeleteList(objects2)
    
    line3 = workPart.Lines.CreateFaceAxis(face2, NXOpen.SmartObject.UpdateOption.AfterModeling)
    
    face4 = component2.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(1)|FACE 559 {(3.7579505605509,-1.5100687350102,2.3380711089057) UNPARAMETERIZED_FEATURE(1)}")
    measureAngleBuilder1.Object2.Value = face4
    
    measureAngleBuilder1.RequirementMode = NXOpen.MeasureBuilder.RequirementType.Existing
    
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start")
    
    theSession.SetUndoMarkName(markId2, "Select Requirement Dialog")
    
    # ----------------------------------------------
    #   Dialog Begin Select Requirement
    # ----------------------------------------------
	
	objects3 = [NXOpen.NXObject.Null] * 1 
    objects3[0] = line3
    nErrs3 = theSession.UpdateManager.AddToDeleteList(objects3)
    
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Select Requirement")
    
    theSession.DeleteUndoMark(markId3, None)
    
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Select Requirement")
    
    theSession.DeleteUndoMark(markId4, None)
    
    theSession.SetUndoMarkName(markId2, "Select Requirement")
    
    theSession.DeleteUndoMark(markId2, None)
    
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Angle")
    
    theSession.DeleteUndoMark(markId5, None)
    
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Angle")
    
    unit1 = workPart.UnitCollection.FindObject("Degrees")
    measureAngle1 = workPart.MeasureManager.NewAngle(unit1, face3, NXOpen.MeasureManager.EndpointType.StartPoint, face4, NXOpen.MeasureManager.EndpointType.StartPoint, True, False)
    
    measure1 = measureAngle1.CreateFeature()
    
    measureAngle1.Dispose()
    requirement1 = workPart.Requirements.FindObject("SOURCE='USER' NAME='360/37'")
    requirementCheck1 = requirement1.NewCheck("p328.U", "p328")
    
    measureAngleBuilder1.RequirementMode = NXOpen.MeasureBuilder.RequirementType.NotSet
    
    datadeleted1 = theSession.DeleteTransientDynamicSectionCutData()
    
    theSession.DeleteUndoMark(markId6, None)
    
    theSession.SetUndoMarkName(markId1, "Measure Angle")
    
    measureAngleBuilder1.Destroy()
    
    # ----------------------------------------------
    #   Menu: Tools->Journal->Stop Recording
    # ----------------------------------------------
    
if __name__ == '__main__':
    main()