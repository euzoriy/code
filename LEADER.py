# NX 11.0.0.33
# Journal created by ievgenz on Tue Dec 13 12:26:49 2016 Eastern Standard Time
#
import math
import NXOpen
import NXOpen.Annotations
import NXOpen.Drafting
import NXOpen.Drawings
def main() : 

    theSession  = NXOpen.Session.GetSession()
    workPart = theSession.Parts.Work
    displayPart = theSession.Parts.Display
    # ----------------------------------------------
    #   Menu: Edit->Settings...
    # ----------------------------------------------
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
    
    viewlabels1 = [NXOpen.DisplayableObject.Null] * 1 
    note1 = workPart.FindObject("HANDLE R-1901271")
    viewlabels1[0] = note1
    editViewLabelSettingsBuilder1 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels1)
    
    theSession.SetUndoMarkName(markId1, "Settings Dialog")
    
    editsettingsbuilders1 = [NXOpen.Drafting.BaseEditSettingsBuilder.Null] * 1 
    editsettingsbuilders1[0] = editViewLabelSettingsBuilder1
    workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders1)
    
    # ----------------------------------------------
    #   Dialog Begin Settings
    # ----------------------------------------------
    editViewLabelSettingsBuilder1.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Note
    
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
    
    nXObject1 = editViewLabelSettingsBuilder1.Commit()
    
    theSession.DeleteUndoMark(markId2, None)
    
    theSession.SetUndoMarkName(markId1, "Settings")
    
    editViewLabelSettingsBuilder1.Destroy()
    
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
    
    viewlabels2 = [NXOpen.DisplayableObject.Null] * 1 
    viewlabels2[0] = note1
    editViewLabelSettingsBuilder2 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels2)
    
    theSession.SetUndoMarkName(markId3, "Settings Dialog")
    
    editsettingsbuilders2 = [NXOpen.Drafting.BaseEditSettingsBuilder.Null] * 1 
    editsettingsbuilders2[0] = editViewLabelSettingsBuilder2
    workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders2)
    
    # ----------------------------------------------
    #   Dialog Begin Settings
    # ----------------------------------------------
    editViewLabelSettingsBuilder2.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Label
    
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
    
    nXObject2 = editViewLabelSettingsBuilder2.Commit()
    
    theSession.DeleteUndoMark(markId4, None)
    
    theSession.SetUndoMarkName(markId3, "Settings")
    
    editViewLabelSettingsBuilder2.Destroy()
    
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
    
    viewlabels3 = [NXOpen.DisplayableObject.Null] * 1 
    viewlabels3[0] = note1
    editViewLabelSettingsBuilder3 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels3)
    
    theSession.SetUndoMarkName(markId5, "Settings Dialog")
    
    editsettingsbuilders3 = [NXOpen.Drafting.BaseEditSettingsBuilder.Null] * 1 
    editsettingsbuilders3[0] = editViewLabelSettingsBuilder3
    workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders3)
    
    # ----------------------------------------------
    #   Dialog Begin Settings
    # ----------------------------------------------
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
    
    theSession.DeleteUndoMark(markId6, None)
    
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
    
    nXObject3 = editViewLabelSettingsBuilder3.Commit()
    
    theSession.DeleteUndoMark(markId7, None)
    
    theSession.SetUndoMarkName(markId5, "Settings")
    
    editViewLabelSettingsBuilder3.Destroy()
    
    # ----------------------------------------------
    #   Menu: Tools->Journal->Stop Recording
    # ----------------------------------------------
    
if __name__ == '__main__':
    main()