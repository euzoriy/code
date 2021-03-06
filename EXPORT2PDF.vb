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

'**********************************************************

	 Sub Main
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
		Try
			UpdateFrame
		Catch ex As exception
			msgbox("Error occurred while updating frame" & vbcrlf & ex.message, vbokonly, "Error")
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
		  Else 
'running in native mode:
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
				Exit Sub
		  End If
		  strOutputFolder = strOutputFolder & "\"
		  rspAdvancePrint = MessageBox.Show("Do you want to add watermark?", "Add watermark?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		  'rspAdvancePrint = vbYes
		  Dim shts As New ArrayList()
		  For Each sheet in dwgs
				shts.Add(sheet.Name)
		  Next
		  shts.Sort()
		  i = 0
		Dim watermatkText as String
		watermatkText = ""
		If rspAdvancePrint = vbyes Then
				'watermatkText = NXInputBox.GetInputString("Enter watermark text", "Watermark text", "DRAFT")
				watermatkText = InputBox("Enter watermark text", "Watermark text", "DRAFT")
'plain .NET input box
				'Dim answer As String = ""
				'answer = InputBox("prompt", "title bar caption", "initial text")
				'MsgBox("answer: """ & answer & """")
				'Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
				'Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
				'answer = InputBox("This input box has been positioned at: (50,10)", "Input box with defined position", " ", 50, 10)
		END IF
		  Dim sht As String
		  For Each sht in shts
			For Each sheet in dwgs
					 If sheet.name = sht Then
						  i = i + 1
						  If rspAdvancePrint = vbyes Then
						'watermatkText = NXInputBox.GetInputString("Enter watermark text", "Watermark text", "DRAFT")
								'pdfFile = strOutputFolder & exportFile & "_advance" & ".pdf"
						pdfFile = strOutputFolder & exportFile & ".pdf"
						  Else
								If strRevision <> "" Then
									 pdfFile = strOutputFolder & exportFile & "_" & strRevision & ".pdf"
								Else
									 pdfFile = strOutputFolder & exportFile & ".pdf"
								End If
						  End If
'the pdf export uses 'append file', if we are on sheet 1 make sure the user wants to overwrite
'if the drawing is multisheet, don't ask on subsequent sheets
						  If i = 1 Then
								If File.Exists(pdfFile) Then
									 rspFileExists = msgbox("The file: '" & pdfFile & "' already exists; overwrite?", vbyesno + vbquestion)
									 If rspFileExists = vbYes Then
										  Try
												File.Delete(pdfFile)
										  Catch ex As Exception
												msgbox(ex.message & vbcrlf & "Journal exiting", vbcritical + vbokonly, "Error")
												Exit Sub
										  End Try
									 Else
										  'msgbox("journal exiting", vbokonly)
										  Exit Sub
									 End If
								End If
						  End If
'update any views that are out of date
						  theSession.Parts.Work.DraftingViews.UpdateViews(Drawings.DraftingViewCollection.ViewUpdateOption.OutOfDate, sheet)
						  Try
								ExportPDF(sheet, pdfFile, partUnits, rspAdvancePrint, watermatkText)
						  Catch ex As exception
								msgbox("Error occurred in PDF export" & vbcrlf & ex.message & vbcrlf & "journal exiting", vbcritical + vbokonly, "Error")
								Exit Sub
						  End Try
						  Exit For
					 End If
				Next
		  Next
		  If i = 0 Then
				MessageBox.Show("This part has no drawing sheets to export", "PDF export failure", MessageBoxButtons.ok, MessageBoxIcon.Warning)
		  Else
				'MessageBox.Show("Exported: " & i & " sheet(s) to pdf file" & vbcrlf & pdfFile, "PDF export success", MessageBoxButtons.ok, MessageBoxIcon.Information)
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
		'Requires:
		'	 Imports System.IO
		'	 Imports System.Windows.Forms
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
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
			Dim drawingFile as String
			Dim sPath as String
			sPath = ""
'Search folders:
			Dim lst() As String = {"L:\Tooling Fixtures\", "L:\Process Models\"}
			
'Iterate through the list.
			For Each pth As String In lst
				'drawingFile = "L:\Tooling Fixtures\" & Left(GetFileName, 6)
				drawingFile = pth & Left(GetFileName, 6)
				If Directory.Exists(drawingFile & "\DRAWINGS") Then
					sPath = drawingFile + "\DRAWINGS\"
				elseIf Directory.Exists(drawingFile & "\CAD") Then
					sPath = drawingFile + "\CAD\"
				end if
			Next

			If sPath<>"" Then
				 .SelectedPath = sPath
'Use Parent file path location:
			ElseIf Directory.Exists(GetFilePath)
				.SelectedPath = GetFilePath
'Use last used path:
			ElseIf Directory.Exists(strLastPath) Then
				 '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
				 'If Directory.Exists(strLastPath) Then
				 .SelectedPath = strLastPath
			Else
'Use default path (first in search folders list):
				 .SelectedPath = lst(0)
			End If
'Prompt the user with a custom message:
			.Description = "Select the directory to export .pdf file." & vbNewLine & "Current path:" & vbNewLine & .SelectedPath
			If .ShowDialog = DialogResult.OK Then
' Display the selected folder if the user clicked on the OK button:
				 OutputPath = .SelectedPath
' save the output folder path in the registry for use on next run:
				 SaveSetting("NX journal", "Export pdf", "ExportPath", .SelectedPath)
			Else
'user pressed 'cancel', exit the subroutine
				 OutputPath = 0
				'exit sub
			End If
		End With
	End Function
'**********************************************************
	Sub ExportPDF(dwg As Drawings.DrawingSheet, outputFile As String, units As Integer, advancePrint As Integer, watermatkText as String)
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
				'printPDFBuilder1.Watermark = "DRAFT " & Today
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
	 Sub UpdateFrame
		'REF AUTHOR-DATE.vb CODE
		  Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
		  If IsNothing(theSession.Parts.BaseWork) Then
				'active part required
				Return
		  End If
		  Dim workPart As NXOpen.Part = theSession.Parts.Work
		  Dim lw As ListingWindow = theSession.ListingWindow
		  Const attributeName As String = "AUTHOR"
		Const attributeName1 As String = "AUTHOR_DATE"
		  Dim attributeInfo As NXObject.AttributeInformation
		  Dim caps As String
		  Dim opt As Update.Option = 0
		Dim attributeToSet As String
		Dim attributeToSet1 As String
		'Dim updateFrame As Boolean = FALSE
'		'Added check for entered attributes to avoid overriding
'		Dim attr_info() As NXObject.AttributeInformation = workPart.GetUserAttributes
'		  For Each ainfo As NXObject.AttributeInformation In attr_info
'				'if type is string, do something with the value
'			MsgBox (ainfo.title + " " + TypeName(ainfo))
'				If ainfo.Type <> NXObject.AttributeType.String Then
'				MsgBox (ainfo.title)
'				if ainfo.title = attributeName or ainfo.title = attributeName1 then
'				'MsgBox (ainfo.StringValue)
'					if ainfo.StringValue = FALSE then
'						updateFrame = TRUE
'					end if
'				end if
'				End If
'		  Next
			Try
                'is the attribute set ?
				If workPart.GetStringAttribute(attributeName) & workPart.GetStringAttribute(attributeName1) = "" then
					''do nothing othervise generate an error
					''updateFrame = TRUE
					''msgbox(workPart.GetStringAttribute(attributeName) & " " & workPart.GetStringAttribute(attributeName1))
				END IF
            Catch ex As ApplicationException
			''msgbox(InStr(ex.ToString, "attribute not found"))
                If InStr(ex.ToString, "attribute not found") > 0 Then
					workPart.setAttribute(attributeName1, DateTime.Now.ToString("MM/dd/yyyy"))
					caps = Environment.UserName
					workPart.SETAttribute(attributeName, caps.ToUpper)
					Dim markId1 As NXOpen.Session.UndoMarkId
					markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Expression")
					theSession.UpdateManager.DoUpdate(markId1)
                Else
                    'some other error occurred
                    lw.WriteLine("## Error: " & ex.ToString)
                End If
            End Try
	 End Sub
	 '**********************************************************
End Module