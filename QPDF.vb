'' Journal to fast saving PDF file with default settings and P1-watermark
'' Created by I.Zorii 03-09-2017
' NX 11.0.0.33
' Journal created by ievgenz on Fri Nov 11 07:21:23 2016 Eastern Standard Time
' NX Journal: Converting a Drawing into a .pdf File
Option Strict Off
Imports System
Imports System.IO
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.MessageBox
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI

Module NXJournal
    Dim theSession As Session = Session.GetSession()
    Dim workPart As Part = theSession.Parts.Work
    Dim displayPart As Part = theSession.Parts.Display
    Dim lw As ListingWindow = theSession.ListingWindow
	
	Dim ufs As UF.UFSession = UF.UFSession.GetUFSession()  

    Dim pdfSheets As New ArrayList  
		
    '**********************************************************
    Sub Main()
        Dim dwgs As Drawings.DrawingSheetCollection
        dwgs = workPart.DrawingSheets
        Dim sheet As Drawings.DrawingSheet
        Dim i As Integer
        Dim pdfFile As String
        Dim currentPath As String
        Dim currentFile As String
        Dim exportFile As String
        Dim partUnits As Integer
        Dim strOutputFolder As String
        Dim strRevision As String
        Dim rspFileExists
        Dim rspAdvancePrint
        lw.Open()
        Try
            UpdateFrame()
        Catch ex As Exception
            lw.WriteLine(DateTime.Now & " :" & vbTab & "Error occurred while updating frame")
        End Try
        'Updating drawing frame
        'determine if we are running under TC or native
        Dim IsTcEng As Boolean = False
        Dim UFSes As UFSession = UFSession.GetUFSession()
        UFSes.UF.IsUgmanagerActive(IsTcEng)
        partUnits = displayPart.PartUnits
        '0 = inch
        '1 = metric
        If IsTcEng Then
            currentFile = workPart.GetStringAttribute("DB_PART_NO")
            strRevision = workPart.GetStringAttribute("DB_PART_REV")
        Else 'running in native mode
            'currentFile = GetFilePath() & GetFileName() & ".prt"
            currentPath = GetFilePath()
            currentFile = GetFileName()
            Try
                strRevision = workPart.GetStringAttribute("REVISION")
                strRevision = Trim(strRevision)
            Catch ex As Exception
                strRevision = ""
            End Try
        End If
        exportFile = currentFile
        strOutputFolder = OutputPath()
        'if we don't have a valid directory (ie the user pressed 'cancel') exit the journal
        If Not Directory.Exists(strOutputFolder) Then
            lw.WriteLine(strOutputFolder & " does not exist.")
            Exit Sub
        End If
        strOutputFolder = strOutputFolder & "\"
        rspAdvancePrint = vbYes
        Dim shts As New ArrayList()
        For Each sheet In dwgs
            shts.Add(sheet.Name)
        Next
        shts.Sort()
        i = 0

        Dim watermatkText As String = "P1"
        pdfFile = strOutputFolder & exportFile & ".pdf"

        Dim sht As String
        For Each sht In shts

            For Each sheet In dwgs
                If sheet.name = sht Then
                    i = i + 1
TryAgain:
                    'the pdf export uses 'append file', if we are on sheet 1 make sure the user wants to overwrite
                    'if the drawing is multisheet, don't ask on subsequent sheets
                    If i = 1 Then
                        If File.Exists(pdfFile) Then
                            Try
                                File.Delete(pdfFile)
                            Catch ex As Exception
                                If InStr(ex.Message, "by another process") > 0 Then
                                    pdfFile = InputBox("Enter modified filename", "File used by another process", pdfFile)
                                    GoTo TryAgain
                                End If
                                lw.WriteLine(DateTime.Now & " :" & vbTab & ex.Message & vbcrlf & "Journal exiting")
                                Exit Sub
                            End Try

                        End If
                    End If
                    'update any views that are out of date
                    theSession.Parts.Work.DraftingViews.UpdateViews(Drawings.DraftingViewCollection.ViewUpdateOption.OutOfDate, sheet)
                    Try
                        ExportPDF(sheet, pdfFile, partUnits, rspAdvancePrint, watermatkText)
                    Catch ex As Exception
                        lw.WriteLine(DateTime.Now & " :" & vbTab & "Error occurred in PDF export" & vbcrlf & ex.Message & vbcrlf & "journal exiting")
                        Exit Sub
                    End Try
                    Exit For
                End If
            Next
        Next
        If i = 0 Then
            lw.WriteLine("This part has no drawing sheets to export")
			'print screenshots
			exportShaded(pdfFile)  
        Else
            'MessageBox.Show("Exported: " & i & " sheet(s) to pdf file" & vbcrlf & pdfFile, "PDF export success", MessageBoxButtons.ok, MessageBoxIcon.Information)
            lw.WriteLine(DateTime.Now & " :" & vbTab & pdfFile & " have been saved.")
        End If
    End Sub
    '**********************************************************
    Function GetFileName()
        Dim strPath As String
        Dim strPart As String
        Dim pos As Integer
        'get the full file path
        strPath = displayPart.fullpath
        'get the part file name
        pos = InStrRev(strPath, "\")
        strPart = Mid(strPath, pos + 1)
        strPath = Left(strPath, pos)
        'strip off the ".prt" extension
        strPart = Left(strPart, Len(strPart) - 4)
        GetFileName = strPart
    End Function
    '**********************************************************
    'TODO copy file path ../DRAWINGS to initial path in FolderBrowserDialog1
    Function GetFilePath()
        Dim strPath As String
        Dim strPart As String
        Dim pos As Integer
        'get the full file path
        strPath = displayPart.fullpath
        'get the part file name
        pos = InStrRev(strPath, "\")
        strPart = Mid(strPath, pos + 1)
        strPath = Left(strPath, pos)
        'strip off the ".prt" extension
        strPart = Left(strPart, Len(strPart) - 4)
        GetFilePath = strPath
    End Function
    '**********************************************************
    Function OutputPath()
        Dim strLastPath As String
        Dim strOutputPath As String
        'Key will show up in HKEY_CURRENT_USER\Software\VB and VBA Program Settings
        Try
            'Get the last path used from the registry
            strLastPath = GetSetting("NX journal", "Export pdf", "ExportPath")
        Catch e As ArgumentException
        Catch e As Exception
            lw.WriteLine(e.GetType.ToString)
        Finally
        End Try
        Dim FolderBrowserDialog1 As New FolderBrowserDialog
        ' Then use the following code to create the Dialog window
        ' Change the .SelectedPath property to the default location
        With FolderBrowserDialog1
            ' Desktop is the root folder in the dialog.
            .RootFolder = Environment.SpecialFolder.Desktop
            ' Select the D:\home directory on entry.
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Dim drawingFile As String
            Dim sPath As String
            sPath = ""
            Dim lst() As String = {"L:\Tooling Fixtures", "L:\Process Models"}
            ' Iterate through the list.
            For Each pth As String In lst
                'drawingFile = "L:\Tooling Fixtures\" & Left(GetFileName, 6)
                drawingFile = pth & "\" & Left(GetFileName, 6)
                ''lw.WriteLine(drawingFile + "\DRAWINGS")
                If Directory.Exists(drawingFile & "\DRAWINGS") Then

                    sPath = drawingFile + "\DRAWINGS"
                ElseIf Directory.Exists(drawingFile & "\CAD") Then
                    sPath = drawingFile + "\CAD"
                End If
            Next

            If sPath <> "" Then
                .SelectedPath = sPath
            ElseIf Directory.Exists(GetFilePath) Then
                .SelectedPath = GetFilePath()
            ElseIf Directory.Exists(strLastPath) Then
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                'If Directory.Exists(strLastPath) Then			
                .SelectedPath = strLastPath
            Else
                .SelectedPath = lst(0) '"L:\Tooling Fixtures"
            End If
            ' Display the selected folder if the user clicked on the OK button.
            OutputPath = .SelectedPath
            ' save the output folder path in the registry for use on next run
            SaveSetting("NX journal", "Export pdf", "ExportPath", .SelectedPath)
        End With
    End Function
    '**********************************************************
    Sub ExportPDF(dwg As Drawings.DrawingSheet, outputFile As String, units As Integer, advancePrint As Integer, watermatkText As String)
        Dim printPDFBuilder1 As PrintPDFBuilder

        printPDFBuilder1 = workPart.PlotManager.CreatePrintPdfbuilder()
        printPDFBuilder1.Scale = 1.0
        printPDFBuilder1.Action = PrintPDFBuilder.ActionOption.Native
        printPDFBuilder1.Colors = PrintPDFBuilder.Color.BlackOnWhite
        printPDFBuilder1.Size = PrintPDFBuilder.SizeOption.ScaleFactor
        If units = 0 Then
            printPDFBuilder1.Units = PrintPDFBuilder.UnitsOption.English
        Else
            printPDFBuilder1.Units = PrintPDFBuilder.UnitsOption.Metric
        End If
        printPDFBuilder1.XDimension = dwg.height
        printPDFBuilder1.YDimension = dwg.length
        printPDFBuilder1.OutputText = PrintPDFBuilder.OutputTextOption.Polylines
        printPDFBuilder1.RasterImages = True
        'TO APPLY CUSTOM WIDTH
        printPDFBuilder1.Widths = NXOpen.PrintPDFBuilder.Width.CustomThreeWidths
        printPDFBuilder1.ImageResolution = PrintPDFBuilder.ImageResolutionOption.High
        printPDFBuilder1.Append = True
        If advancePrint = vbyes Then
            printPDFBuilder1.AddWatermark = True
            '===========================watermark to uppercase===========================
            printPDFBuilder1.Watermark = UCASE(watermatkText)
        Else
            printPDFBuilder1.AddWatermark = False
            printPDFBuilder1.Watermark = ""
        End If
        Dim sheets1(0) As NXObject
        Dim drawingSheet1 As Drawings.DrawingSheet = CType(dwg, Drawings.DrawingSheet)
        sheets1(0) = drawingSheet1
        printPDFBuilder1.SourceBuilder.SetSheets(sheets1)
        printPDFBuilder1.Filename = outputFile
        Dim nXObject1 As NXObject
        nXObject1 = printPDFBuilder1.Commit()
        printPDFBuilder1.Destroy()
    End Sub
    '**********************************************************
    Sub UpdateFrame()
        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        If IsNothing(theSession.Parts.BaseWork) Then
            Return
        End If
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        ''Dim lw As ListingWindow = theSession.ListingWindow
        Const attributeName As String = "AUTHOR"
        Const attributeName1 As String = "AUTHOR_DATE"
        Dim attributeInfo As NXObject.AttributeInformation
        Dim caps As String
        Dim opt As Update.Option = 0
        Dim attributeToSet As String
        Dim attributeToSet1 As String
        Try
            If workPart.GetStringAttribute(attributeName) & workPart.GetStringAttribute(attributeName1) = "" Then
            End If
        Catch ex As ApplicationException
            If InStr(ex.ToString, "attribute not found") > 0 Then
                workPart.setAttribute(attributeName1, DateTime.Now.ToString("MM/dd/yyyy"))
                caps = Environment.UserName
                workPart.SETAttribute(attributeName, caps.ToUpper)
                Dim markId1 As NXOpen.Session.UndoMarkId
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Expression")
                theSession.UpdateManager.DoUpdate(markId1)
            Else
                'some other error occurred
                lw.WriteLine(DateTime.Now & " :" & vbTab & "## Error: " & ex.ToString)
            End If
        End Try
    End Sub
    '**********************************************************	
	
	
'**********************************************************Export Shaded'**********************************************************

'    Dim theSession As Session = Session.GetSession()  
'    Dim workPart As Part = theSession.Parts.Work  
'   Dim ufs As UF.UFSession = UF.UFSession.GetUFSession()  

'    Dim pdfSheets As New ArrayList  

    Sub exportShaded(outputFile)  

        Dim markId1 As Session.UndoMarkId  
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Start")  
        theSession.SetUndoMarkName(markId1, "pdf_sheets")  

        Dim pdfSheet As Drawings.DrawingSheet  
        Dim outputFileName(1) As String  
        Dim pdfFile As String  

        Dim wpModelingView As ModelingView  
        For Each wpModelingView In workPart.ModelingViews  
 'create new sheet for view
            pdfSheet = CreateSheet()  
            pdfSheet.SetName(wpModelingView.Name)  
 'add view to new sheet
            AddView(wpModelingView, pdfSheet)  
            pdfSheets.Add(pdfSheet)  
        Next  

'        outputFileName(0) = OutputPath()  
 '       outputFileName(1) = Path.GetFileNameWithoutExtension(workPart.FullPath) & ".pdf"  
 '       pdfFile = Path.Combine(outputFileName(0), outputFileName(1))  
 pdfFile = outputFile

        If My.Computer.FileSystem.FileExists(pdfFile) Then  
            My.Computer.FileSystem.DeleteFile(pdfFile)  
        End If  

 'output new sheets to pdf
        Try  
            Call CreatePDF(pdfFile)  
        Catch ex As UnauthorizedAccessException  
            MessageBox.Show("You do not have permission to write to this directory", "PDF file creation error", MessageBoxButtons.OK, MessageBoxIcon.Error)  
        Catch ex As ApplicationException  
            MessageBox.Show(ex.Message, "PDF file creation error", MessageBoxButtons.OK, MessageBoxIcon.Error)  
        End Try  

 'optional: delete pdf sheets
        Dim markId2 As Session.UndoMarkId  
        markId2 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Delete")  

        Dim objects1(pdfSheets.Count - 1) As NXObject  
        For i As Integer = 0 To pdfSheets.Count - 1  
            objects1(i) = pdfSheets.Item(i)  
        Next  
        pdfSheets.Clear()  

        Dim nErrs1 As Integer  
        nErrs1 = theSession.UpdateManager.AddToDeleteList(objects1)  

        Dim notifyOnDelete2 As Boolean  
        notifyOnDelete2 = theSession.Preferences.Modeling.NotifyOnDelete  

        Dim nErrs2 As Integer  
        nErrs2 = theSession.UpdateManager.DoUpdate(markId2)  


    End Sub  

    Public Function CreateSheet() As Drawings.DrawingSheet  
 ' ----------------------------------------------
 '   Menu: Insert->Sheet...
 ' ----------------------------------------------

 'Dim markId1 As Session.UndoMarkId
 'markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Start")

        Dim nullDrawings_DrawingSheet As Drawings.DrawingSheet = Nothing  

        Dim drawingSheetBuilder1 As Drawings.DrawingSheetBuilder  
        drawingSheetBuilder1 = workPart.DrawingSheets.DrawingSheetBuilder(nullDrawings_DrawingSheet)  

        drawingSheetBuilder1.Option = Drawings.DrawingSheetBuilder.SheetOption.StandardSize  
 'start with 1:1 scale sheet, view will be scaled to fit later
        drawingSheetBuilder1.ScaleNumerator = 1.0  
        drawingSheetBuilder1.ScaleDenominator = 1.0  

        If workPart.PartUnits = Part.Units.Inches Then  
            drawingSheetBuilder1.Units = Drawings.DrawingSheetBuilder.SheetUnits.English  
 'insert standard letter size sheet
            drawingSheetBuilder1.Height = 8.5  
            drawingSheetBuilder1.Length = 11.0  
Else  'metric unit file
            drawingSheetBuilder1.Units = Drawings.DrawingSheetBuilder.SheetUnits.Metric  
 'insert standard A4 size sheet
            drawingSheetBuilder1.Height = 210  
            drawingSheetBuilder1.Length = 297  
        End If  

 'sheetLength = drawingSheetBuilder1.Length
 'sheetHeight = drawingSheetBuilder1.Height

        drawingSheetBuilder1.ProjectionAngle = Drawings.DrawingSheetBuilder.SheetProjectionAngle.Third  

        Dim pdfSheet As Drawings.DrawingSheet  
        pdfSheet = drawingSheetBuilder1.Commit()  

 'theSession.SetUndoMarkName(markId1, "##01Sheet")

        drawingSheetBuilder1.Destroy()  

        Return pdfSheet  

    End Function  

    Public Sub AddView(ByVal view As ModelingView, ByVal sheet As Drawings.DrawingSheet)  
 ' ----------------------------------------------
 '   Menu: Insert->View->Base...
 ' ----------------------------------------------
 'Dim markId3 As Session.UndoMarkId
 'markId3 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Start")

        sheet.Open()  

        Dim nullDrawings_BaseView As Drawings.BaseView = Nothing  

        Dim baseViewBuilder1 As Drawings.BaseViewBuilder  
        baseViewBuilder1 = workPart.DraftingViews.CreateBaseViewBuilder(nullDrawings_BaseView)  

        baseViewBuilder1.SelectModelView.SelectedView = view  

 'theSession.SetUndoMarkName(markId3, "Base View Dialog")

        baseViewBuilder1.Style.ViewStyleBase.Part = workPart  

        baseViewBuilder1.Style.ViewStyleBase.PartName = workPart.FullPath  

        Dim arrangement1 As Assemblies.Arrangement = workPart.ComponentAssembly.ActiveArrangement  

        baseViewBuilder1.Style.ViewStyleBase.Arrangement.SelectedArrangement = arrangement1  

        baseViewBuilder1.Style.ViewStyleShading.RenderingStyle = Preferences.ShadingRenderingStyleOption.FullyShaded  

 'place the view in the center of the sheet
        Dim point1 As Point3d = New Point3d(sheet.Length / 2, sheet.Height / 2, 0.0)  
        baseViewBuilder1.Placement.Placement.SetValue(Nothing, workPart.Views.WorkView, point1)  

        Dim baseView1 As Drawings.BaseView  
        baseView1 = baseViewBuilder1.Commit()  

 'theSession.SetUndoMarkName(markId3, "Base View")

        Dim viewSize(3) As Double  
 'retrieve the size of the view
        ufs.Draw.AskViewBorders(baseView1.Tag, viewSize)  
        Dim viewLength As Double = viewSize(2) - viewSize(0)  
        Dim viewHeight As Double = viewSize(3) - viewSize(1)  

 'change scale of view so that it fits on 90% of the selected sheet size
        baseView1.Style.General.Scale = Math.Min((sheet.Length * 0.9) / viewLength, (sheet.Height * 0.9) / viewHeight)  

        baseView1.Commit()  

        baseViewBuilder1.Destroy()  

    End Sub  

    Public Sub CreatePDF(ByVal outputFile As String)  
        Dim printPDFBuilder1 As PrintPDFBuilder  

        printPDFBuilder1 = workPart.PlotManager.CreatePrintPdfbuilder()  
        printPDFBuilder1.Scale = 1.0  
        printPDFBuilder1.Action = PrintPDFBuilder.ActionOption.Native  
        printPDFBuilder1.Colors = PrintPDFBuilder.Color.BlackOnWhite  
        printPDFBuilder1.Size = PrintPDFBuilder.SizeOption.ScaleFactor  
        printPDFBuilder1.Units = workPart.PartUnits  
 'If units = 0 Then
 '    printPDFBuilder1.Units = PrintPDFBuilder.UnitsOption.English
 'Else
 '    printPDFBuilder1.Units = PrintPDFBuilder.UnitsOption.Metric
 'End If
        printPDFBuilder1.XDimension = pdfSheets.Item(1).Height  
        printPDFBuilder1.YDimension = pdfSheets.Item(1).Length  
        printPDFBuilder1.OutputText = PrintPDFBuilder.OutputTextOption.Polylines  
        printPDFBuilder1.RasterImages = True  
        printPDFBuilder1.ImageResolution = PrintPDFBuilder.ImageResolutionOption.Medium  
        printPDFBuilder1.Append = True  
        printPDFBuilder1.AddWatermark = False  

        Dim sheets1(pdfSheets.Count - 1) As NXObject  
 'Dim drawingSheet1 As Drawings.DrawingSheet = CType(dwg, Drawings.DrawingSheet)
        For i As Integer = 0 To pdfSheets.Count - 1  
            sheets1(i) = pdfSheets.Item(i)  
        Next  

 'sheets1(0) = drawingSheet1
        printPDFBuilder1.SourceBuilder.SetSheets(sheets1)  

        printPDFBuilder1.Filename = outputFile  
 'printPDFBuilder1.Filename = "C:\Temp\test.pdf"

        Dim nXObject1 As NXObject  
        nXObject1 = printPDFBuilder1.Commit()  

        printPDFBuilder1.Destroy()  

    End Sub  

    Function OutputPath()  
 'Requires:
 '    Imports System.IO
 '    Imports System.Windows.Forms
 'if the user presses OK on the dialog box, the chosen path is returned
 'if the user presses cancel on the dialog box, 0 is returned

        Dim strLastPath As String  
        Dim strOutputPath As String  

 'Key will show up in HKEY_CURRENT_USER\Software\VB and VBA Program Settings
        Try  
 'Get the last path used from the registry
            strLastPath = GetSetting("NX journal", "Export pdf", "ExportPath")  
 'msgbox("Last Path: " & strLastPath)
        Catch e As ArgumentException  
        Catch e As Exception  
            msgbox(e.GetType.ToString)  
        Finally  
        End Try  

        Dim FolderBrowserDialog1 As New FolderBrowserDialog  

 ' Then use the following code to create the Dialog window
 ' Change the .SelectedPath property to the default location
        With FolderBrowserDialog1  
 ' Desktop is the root folder in the dialog.
            .RootFolder = Environment.SpecialFolder.Desktop  
 ' Select the D:\home directory on entry.
            If Directory.Exists(strLastPath) Then  
                .SelectedPath = strLastPath  
            Else  
                .SelectedPath = Environment.SpecialFolder.MyDocuments  
            End If  
 ' Prompt the user with a custom message.
            .Description = "Select the directory to export .pdf file"  
            If .ShowDialog = DialogResult.OK Then  
 ' Display the selected folder if the user clicked on the OK button.
                OutputPath = .SelectedPath  
 ' save the output folder path in the registry for use on next run
                SaveSetting("NX journal", "Export pdf", "ExportPath", .SelectedPath)  
            Else  
 'user pressed 'cancel', exit the subroutine
                OutputPath = 0  
            End If  
        End With  

    End Function  


    Public Function GetUnloadOption(ByVal dummy As String) As Integer  

 'Unloads the image when the NX session terminates
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.AtTermination  

    End Function  

End Module