// NX 11.0.0.33
// Journal created by ievgenz on Fri Dec 09 14:44:25 2016 Eastern Standard Time
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    // ----------------------------------------------
    //   Menu: Edit->Settings...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.DisplayableObject[] viewlabels1 = new NXOpen.DisplayableObject[1];
    
	// add object sselection
	NXOpen.Annotations.Note note1 = (NXOpen.Annotations.Note)workPart.FindObject("ENTITY 25 4 1");
	//===========================
	
    viewlabels1[0] = note1;
    NXOpen.Drawings.EditViewLabelSettingsBuilder editViewLabelSettingsBuilder1;
    editViewLabelSettingsBuilder1 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels1);
    
    theSession.SetUndoMarkName(markId1, "Settings Dialog");
    
    NXOpen.Drafting.BaseEditSettingsBuilder[] editsettingsbuilders1 = new NXOpen.Drafting.BaseEditSettingsBuilder[1];
    editsettingsbuilders1[0] = editViewLabelSettingsBuilder1;
    workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders1);
    
    editViewLabelSettingsBuilder1.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Note;
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings");
    
    NXOpen.NXObject nXObject1;
    nXObject1 = editViewLabelSettingsBuilder1.Commit();
    
    theSession.DeleteUndoMark(markId3, null);
    
    theSession.SetUndoMarkName(markId1, "Settings");
    
    editViewLabelSettingsBuilder1.Destroy();
    
    // ----------------------------------------------
    //   Menu: Edit->Settings...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.DisplayableObject[] viewlabels2 = new NXOpen.DisplayableObject[1];
    viewlabels2[0] = note1;
    NXOpen.Drawings.EditViewLabelSettingsBuilder editViewLabelSettingsBuilder2;
    editViewLabelSettingsBuilder2 = workPart.SettingsManager.CreateDrawingEditViewLabelSettingsBuilder(viewlabels2);
    
    theSession.SetUndoMarkName(markId4, "Settings Dialog");
    
    NXOpen.Drafting.BaseEditSettingsBuilder[] editsettingsbuilders2 = new NXOpen.Drafting.BaseEditSettingsBuilder[1];
    editsettingsbuilders2[0] = editViewLabelSettingsBuilder2;
    workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders2);
    
    editViewLabelSettingsBuilder2.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Label;
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings");
    
    theSession.DeleteUndoMark(markId5, null);
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings");
    
    NXOpen.NXObject nXObject2;
    nXObject2 = editViewLabelSettingsBuilder2.Commit();
    
    theSession.DeleteUndoMark(markId6, null);
    
    theSession.SetUndoMarkName(markId4, "Settings");
    
    editViewLabelSettingsBuilder2.Destroy();
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
