' NX 12.0.2.9
' Journal created by Ievgenz on Wed Feb 26 13:04:49 2020 Eastern Standard Time
'
Imports System
Imports System.IO
Imports NXOpen
Module NXJournal
Const InitFolder = "L:\Tooling Fixtures\"

Sub Main (ByVal args() As String) 
	Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
	' ----------------------------------------------
	'   Menu: File->Plot...
	' ----------------------------------------------
	Dim plotBuilder1 As NXOpen.PlotBuilder = Nothing

	'Dim FullCurrentPath As String = displayPart.fullpath
	'Dim CurrentFileName As String = Path.GetFileName(displayPart.FullPath)
	Dim CurrentFileName As String = Path.GetFileNameWithoutExtension(displayPart.FullPath)
Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Plot")

	plotBuilder1 = workPart.PlotManager.CreatePlotBuilder()
	plotBuilder1.Copies = 1
	plotBuilder1.Tolerance = 0.001
	plotBuilder1.RasterImages = True
	plotBuilder1.XDisplay = NXOpen.PlotBuilder.XdisplayOption.Right
	plotBuilder1.XOffset = 0.14999999999999999
	plotBuilder1.CharacterSize = 0.059999999999999998
	plotBuilder1.Rotation = NXOpen.PlotBuilder.RotationOption.Degree90
	plotBuilder1.JobName = CurrentFileName
	plotBuilder1.ImageResolution = NXOpen.PlotBuilder.ImageResolutionOption.High
	Dim sheets1(0) As NXOpen.NXObject
	Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing
	sheets1(0) = nullNXOpen_NXObject
	plotBuilder1.SourceBuilder.SetSheets(sheets1)
	plotBuilder1.PlotterText = "JPEG"
	plotBuilder1.ProfileText = "<System Profile>"
	plotBuilder1.ColorsWidthsBuilder.Colors = NXOpen.PlotColorsWidthsBuilder.Color.AsDisplayed
	plotBuilder1.ColorsWidthsBuilder.Widths = NXOpen.PlotColorsWidthsBuilder.Width.CustomThreeWidths
	Dim filenames1(0) As String
	filenames1(0) = InitFolder & left(CurrentFileName,6) & "\" & CurrentFileName & ".jpg"
	'msgbox(filenames1(0))
	plotBuilder1.SetGraphicFilenames(filenames1)
	Dim filenames2(0) As String
	filenames2(0) = "%temp%\temp_Display.cgm"
	plotBuilder1.SetFilenames(filenames2)
	Dim nXObject1 As NXOpen.NXObject = Nothing
	nXObject1 = plotBuilder1.Commit()
	theSession.DeleteUndoMark(markId3, Nothing)
	plotBuilder1.Destroy()
	
	msgbox(filenames1(0) + " saved!")
' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------
End Sub
End Module

