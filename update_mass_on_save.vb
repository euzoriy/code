' NX 11.0.2.7
' Journal created by ievgenz on Fri Dec 01 10:26:02 2017 Eastern Standard Time
'
Imports System
Imports NXOpen
Module NXJournal
Sub Main (ByVal args() As String) 
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
' ----------------------------------------------
'   Menu: File->Properties
' ----------------------------------------------
Dim objects1(0) As NXOpen.NXObject
objects1(0) = workPart
Dim attributePropertiesBuilder1 As NXOpen.AttributePropertiesBuilder = Nothing
attributePropertiesBuilder1 = theSession.AttributeManager.CreateAttributePropertiesBuilder(workPart, objects1, NXOpen.AttributePropertiesBuilder.OperationType.None)
attributePropertiesBuilder1.IsArray = False

Dim objects2(0) As NXOpen.NXObject
objects2(0) = workPart
Dim massPropertiesBuilder1 As NXOpen.MassPropertiesBuilder = Nothing
massPropertiesBuilder1 = workPart.PropertiesManager.CreateMassPropertiesBuilder(objects2)

Dim objects6(0) As NXOpen.NXObject
objects6(0) = workPart
attributePropertiesBuilder1.SetAttributeObjects(objects6)
massPropertiesBuilder1.UpdateOnSave = NXOpen.MassPropertiesBuilder.UpdateOptions.Yes

Dim updateoption1 As NXOpen.MassPropertiesBuilder.UpdateOptions = Nothing
updateoption1 = massPropertiesBuilder1.UpdateOnSave
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = massPropertiesBuilder1.Commit()
workPart.PartPreviewMode = NXOpen.BasePart.PartPreview.OnSave

attributePropertiesBuilder1.Destroy()
massPropertiesBuilder1.Destroy()

' ----------------------------------------------
'   Menu: File->Save Work Part Only
' ----------------------------------------------
Dim partSaveStatus1 As NXOpen.PartSaveStatus = Nothing
partSaveStatus1 = workPart.Save(NXOpen.BasePart.SaveComponents.False, NXOpen.BasePart.CloseAfterSave.False)
partSaveStatus1.Dispose()
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module