'NXJournaling.com
'March 21, 2012
'Journal purpose: export shaded model views to a pdf file
'  the journal creates an "A" or "A4" size sheet for each model view, places
'  the view on the sheet and scales it to fit. The journal then exports all
'  the sheets to the specified pdf file
' ref: http://www.eng-tips.com/viewthread.cfm?qid=327914

Option Strict Off  
Imports System  
Imports System.IO  
Imports System.Windows.Forms  
Imports System.Collections  
Imports NXOpen  

Module Export_Shaded_pdf  

    Dim theSession As Session = Session.GetSession()  
    Dim workPart As Part = theSession.Parts.Work  
    Dim ufs As UF.UFSession = UF.UFSession.GetUFSession()  

    Dim pdfSheets As New ArrayList  

    Sub Main()  

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

        outputFileName(0) = OutputPath()  
        outputFileName(1) = Path.GetFileNameWithoutExtension(workPart.FullPath) & ".pdf"  
        pdfFile = Path.Combine(outputFileName(0), outputFileName(1))  

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