' NX 12.0.2.9
' Journal created by ievgenz on Fri Jan  3 08:43:45 2020 Eastern Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Preferences->Visualization...
' ----------------------------------------------
Dim currentMode1 As NXOpen.Preferences.SessionVisualizationHighEndRendering.MaterialEditorEditingMode = Nothing
currentMode1 = theSession.Preferences.HighEndRenderingVisualization.StudioMaterialEditorEditingMode

' ----------------------------------------------
'   Dialog Begin Color
' ----------------------------------------------
' ----------------------------------------------
'   Dialog Begin Color
' ----------------------------------------------
workPart.Preferences.ColorSettingVisualization.MonochromeDisplay = True

workPart.Preferences.ColorSettingVisualization.MonochromeForegroundColor = 216

workPart.Preferences.ColorSettingVisualization.MonochromeBackgroundColor = 80

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module