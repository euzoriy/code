' NX 12.0.2.9
' Journal created by Ievgenz on Wed Feb 26 13:27:14 2020 Eastern Standard Time
'
Imports System
Imports NXOpen
Module NXJournal
Sub Main (ByVal args() As String) 
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim displayPart As NXOpen.Part = theSession.Parts.Display
' ----------------------------------------------
'   Menu: File->Plot...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start")
Dim plotBuilder1 As NXOpen.PlotBuilder = Nothing
plotBuilder1 = workPart.PlotManager.CreatePlotBuilder()
plotBuilder1.Copies = 1
plotBuilder1.Tolerance = 0.001
plotBuilder1.RasterImages = True
plotBuilder1.ImageResolution = NXOpen.PlotBuilder.ImageResolutionOption.High
plotBuilder1.XDisplay = NXOpen.PlotBuilder.XdisplayOption.Right
plotBuilder1.XOffset = 0.14999999999999999
plotBuilder1.CharacterSize = 0.059999999999999998
plotBuilder1.Rotation = NXOpen.PlotBuilder.RotationOption.Degree90
plotBuilder1.JobName = "Ievgenz_TF3776-01"
theSession.SetUndoMarkName(markId1, "Plot Dialog")
Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Plot")
theSession.DeleteUndoMark(markId2, Nothing)
Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Plot")
Dim sheets1(0) As NXOpen.NXObject
Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing
sheets1(0) = nullNXOpen_NXObject
plotBuilder1.SourceBuilder.SetSheets(sheets1)
plotBuilder1.PlotterText = "JPEG"
plotBuilder1.ProfileText = "<System Profile>"
plotBuilder1.ColorsWidthsBuilder.Colors = NXOpen.PlotColorsWidthsBuilder.Color.AsDisplayed
plotBuilder1.ColorsWidthsBuilder.Widths = NXOpen.PlotColorsWidthsBuilder.Width.CustomThreeWidths
Dim filenames1(0) As String
filenames1(0) = "u:\Pictures\Fixture\TF3776-01.jpg"
plotBuilder1.SetGraphicFilenames(filenames1)
Dim filenames2(0) As String
filenames2(0) = "C:\Users\ievgenz\AppData\Local\Temp\Ievgenz_TF3776-01_Display.cgm"
plotBuilder1.SetFilenames(filenames2)
Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = plotBuilder1.Commit()
theSession.DeleteUndoMark(markId3, Nothing)
theSession.SetUndoMarkName(markId1, "Plot")
plotBuilder1.Destroy()
theSession.DeleteUndoMark(markId1, Nothing)
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module