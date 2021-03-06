'Module NXJournal
'    Sub Main(ByVal args() As String)

'        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
'        Dim workPart As NXOpen.Part = theSession.Parts.Work

'        Dim displayPart As NXOpen.Part = theSession.Parts.Display

'        ' ----------------------------------------------
'        '   Menu: File->Open...
'        ' ----------------------------------------------
'        Dim basePart1 As NXOpen.BasePart = Nothing
'        Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
'        basePart1 = theSession.Parts.OpenBaseDisplay("C:\Users\ievgenz\Downloads\9657K920_STEEL COMPRESSION SPRING.SLDPRT", partLoadStatus1)

'        workPart = theSession.Parts.Work ' 9657K920_STEEL COMPRESSION SPRING_sldprt
'        displayPart = theSession.Parts.Display ' 9657K920_STEEL COMPRESSION SPRING_sldprt
'        partLoadStatus1.Dispose()
'        ' ----------------------------------------------
'        '   Menu: File->Save As...
'        ' ----------------------------------------------
'        Dim partSaveStatus1 As NXOpen.PartSaveStatus = Nothing
'        partSaveStatus1 = workPart.SaveAs("L:\Tooling Fixtures\MODULAR COMPONENTS\VERIFIED\HARDWARE\9657K920")

'        partSaveStatus1.Dispose()
'        ' ----------------------------------------------
'        '   Menu: Tools->Journal->Stop Recording
'        ' ----------------------------------------------

'    End Sub
'End Module
'=====================================================================================
'Journal created by Ievgen Zorii to help import stp files

' NX 11.0.1.11
' Journal created by ievgenz on Wed May 03 10:26:22 2017 Eastern Daylight Time
'

Imports System
Imports System.IO
Imports System.Windows.Forms
'Imports Scripting
Imports System.Runtime.InteropServices

Imports System.Diagnostics

Imports NXOpen
Module NXJournal
	Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
	Dim lw As ListingWindow = theSession.ListingWindow
	
	'Dim defaultImportFolder as String = "L:\Tooling Fixtures\HARDWARE\"
	Dim defaultImportFolder as String = "L:\Tooling Fixtures\MODULAR COMPONENTS\VERIFIED\HARDWARE\"	
	
	Dim inputFileName as String	= ""
	
	
Sub Main (ByVal args() As String) 
		Dim myStream As Stream = Nothing
		Dim openFileDialog1 As New OpenFileDialog()
		Dim initialFolder as String
		Dim inputFile as String
		'Dim inputFileName as String		
		Dim outputFileName as String
		Dim outputFile as String
		lw.Open()
		initialFolder = RegKeyRead("{374DE290-123F-4565-9164-39C4925E467B}")
		If initialFolder = "" then 
			openFileDialog1.InitialDirectory = "c:\"
		Else 
			openFileDialog1.InitialDirectory = initialFolder
		End If
		'openFileDialog1.InitialDirectory = "c:\"
		openFileDialog1.Filter = "SolidWorks files (*.SLDPRT;*.SLDASM)|*.SLDPRT;*.SLDASM|All files (*.*)|*.*"
		openFileDialog1.FilterIndex = 1
		openFileDialog1.RestoreDirectory = True
		If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then	
			inputFile = openFileDialog1.FileName
            inputFileName = getFileName(inputFile)
            'inputFileName = inputFileName.Replace("_sldprt", "").Replace("_sldasm", "")
            outputFileName = inputFileName.Split("_")(0)
            outputFileName = InputBox("Please enter new filename:", "New .prt file name", outputFileName)
			outputFile = defaultImportFolder + outputFileName + ".prt"
			'msgBox (inputFile & vbNewLine & outputFile)
			lw.writeline ("Input file: " & inputFile & vbNewLine & "Output file: " & outputFile & vbNewLine)
			if	NOT IO.File.Exists(outputFile) Then
				Try
					ImportSolidWorks(inputFile, outputFile)

				Catch Ex As Exception
					lw.WriteLine("Error: " & Ex.Message)
					lw.WriteLine(Ex.StackTrace)					
					'MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
				Finally

				End Try
			Else 
				lw.WriteLine("File " & outputFile & " already exist.")
				'msgBox("File " & outputFile & " already exist.")
			End If
		End If
		lw.Close()
End Sub

Sub ImportSolidWorks (inputFile as String, outputFile as String)


        Try
            'do Until IO.File.Exists(outputFile)
            '	Threading.Thread.Sleep(1000) ' 1000 milliseconds = 1 second
            '			If waitTime > maxWaitTime Then
            '							Exit Do
            '					End If
            '	waitTime = waitTime + 1
            'Loop

            'If IO.File.Exists(outputFile) Then
            '	Dim basePart1 As NXOpen.BasePart = Nothing
            '	Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
            '	basePart1 = theSession.Parts.OpenBaseDisplay(outputFile, partLoadStatus1)
            '	workPart = theSession.Parts.Work ' 97155A525
            '	displayPart = theSession.Parts.Display ' 97155A525
            '	partLoadStatus1.Dispose()		

            '	setAttribute("PART NAME", inputFileName)
            'Else 
            '	'TODO open folder
            'End If


            Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
            Dim workPart As NXOpen.Part = theSession.Parts.Work
            Dim displayPart As NXOpen.Part = theSession.Parts.Display

            ' ----------------------------------------------
            '   Menu: File->Open...
            ' ----------------------------------------------
            Dim basePart1 As NXOpen.BasePart = Nothing
            Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
            basePart1 = theSession.Parts.OpenBaseDisplay(inputFile, partLoadStatus1)

            workPart = theSession.Parts.Work ' 9657K920_STEEL COMPRESSION SPRING_sldprt
            displayPart = theSession.Parts.Display ' 9657K920_STEEL COMPRESSION SPRING_sldprt
            partLoadStatus1.Dispose()
            ' ----------------------------------------------
            '   Menu: File->Save As...
            ' ----------------------------------------------
            setAttribute("PART NAME", inputFileName)

            Dim partSaveStatus1 As NXOpen.PartSaveStatus = Nothing
            partSaveStatus1 = workPart.SaveAs(outputFile)
            partSaveStatus1.Dispose()


        Catch Ex As Exception
		lw.WriteLine("Error: " & Ex.Message)
		lw.WriteLine(Ex.StackTrace)					
		'MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
	Finally
		
		
	End Try

End Sub

Sub ImportStp (inputFile as String, outputFile as String)	
	Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
	markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
	Dim step214Importer1 As NXOpen.Step214Importer = Nothing
	step214Importer1 = theSession.DexManager.CreateStep214Importer()
	step214Importer1.SimplifyGeometry = True
	step214Importer1.LayerDefault = 1
	step214Importer1.ImportTo = NXOpen.Step214Importer.ImportToOption.NewPart
	step214Importer1.SettingsFile = "C:\Program Files\Siemens\NX 11.0\step214ug\step214ug.def"
	step214Importer1.ObjectTypes.Curves = True
	step214Importer1.ObjectTypes.Surfaces = True
	step214Importer1.ObjectTypes.Solids = True
	step214Importer1.ObjectTypes.Csys = True
	step214Importer1.ObjectTypes.ProductData = True
	step214Importer1.ObjectTypes.PmiData = True
	step214Importer1.SewSurfaces = True
	step214Importer1.Optimize = True
	step214Importer1.FlattenAssembly = True
	step214Importer1.InputFile = ""
	step214Importer1.OutputFile = ""
	theSession.SetUndoMarkName(markId1, "Import STEP214 Dialog")
	''**************************************************************************************************
	step214Importer1.InputFile = inputFile '"C:\Users\ievgenz\Downloads\97155A525_PLASTIC DOWEL PIN.STEP"
	step214Importer1.OutputFile = outputFile'"L:\Tooling Fixtures\HARDWARE\97155A525.prt"
	step214Importer1.FileOpenFlag = False
	Dim nXObject1 As NXOpen.NXObject = Nothing
	nXObject1 = step214Importer1.Commit()
	theSession.SetUndoMarkName(markId1, "Import STEP214")
	step214Importer1.Destroy()
	
	'TODO put wait and check if file exist
	Dim maxWaitTime as Integer = 60 'seconds
	Dim waitTime as Integer = 0
	
	try
		do Until IO.File.Exists(outputFile)
			Threading.Thread.Sleep(1000) ' 1000 milliseconds = 1 second
					If waitTime > maxWaitTime Then
									Exit Do
							End If
			waitTime = waitTime + 1
		Loop
		
		If IO.File.Exists(outputFile) Then
			Dim basePart1 As NXOpen.BasePart = Nothing
			Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
			basePart1 = theSession.Parts.OpenBaseDisplay(outputFile, partLoadStatus1)
			workPart = theSession.Parts.Work ' 97155A525
			displayPart = theSession.Parts.Display ' 97155A525
			partLoadStatus1.Dispose()		
		
			setAttribute("PART NAME", inputFileName)
		Else 
			'TODO open folder
		End If
		
	Catch Ex As Exception
		lw.WriteLine("Error: " & Ex.Message)
		lw.WriteLine(Ex.StackTrace)					
		'MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
	Finally
		
		
	End Try

End Sub


Function getFileName(filePath As String) As String
	Dim sFullFilename as String
	Dim sFilename as String
	sFullFilename = Right(filePath, Len(filePath) - InStrRev(filePath, "\"))
	sFilename = Left(sFullFilename, (InStr(sFullFilename, ".") - 1))
	getFileName = sFilename
End Function

<DllImport("shell32.dll")> _
Function SHGetKnownFolderPath(<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid, ByVal dwFlags As UInt32, ByVal hToken As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByRef pszPath As System.Text.StringBuilder) As Int32
End Function

'reads the value for the registry key i_RegKey
'if the key cannot be found, the return value is ""
Function RegKeyRead(i_RegKey As String) As String
    'Dim FolderDownloads As New Guid("374DE290-123F-4565-9164-39C4925E467B")
	Dim FolderDownloads As New Guid(i_RegKey)
    Dim sb As New System.Text.StringBuilder(128)
        SHGetKnownFolderPath(FolderDownloads, 0, IntPtr.Zero, sb)
        'MsgBox(sb.ToString)		
		RegKeyRead = sb.ToString
End Function

Sub setAttribute (attrName As String, attrValue As String) 
	Dim objects1(0) As NXOpen.NXObject
	objects1(0) = workPart
	Dim attributePropertiesBuilder1 As NXOpen.AttributePropertiesBuilder = Nothing
	attributePropertiesBuilder1 = theSession.AttributeManager.CreateAttributePropertiesBuilder(workPart, objects1, NXOpen.AttributePropertiesBuilder.OperationType.None)
	attributePropertiesBuilder1.IsArray = False
	attributePropertiesBuilder1.DataType = NXOpen.AttributePropertiesBaseBuilder.DataTypeOptions.String
	'attributePropertiesBuilder1.Units = "Inch"
	Dim objects2(0) As NXOpen.NXObject
	objects2(0) = workPart
	Dim massPropertiesBuilder1 As NXOpen.MassPropertiesBuilder = Nothing
	massPropertiesBuilder1 = workPart.PropertiesManager.CreateMassPropertiesBuilder(objects2)
	Dim selectNXObjectList1 As NXOpen.SelectNXObjectList = Nothing
	selectNXObjectList1 = massPropertiesBuilder1.SelectedObjects
	massPropertiesBuilder1.LoadPartialComponents = True
	massPropertiesBuilder1.Accuracy = 0.98999999999999999
	Dim objects4(0) As NXOpen.NXObject
	objects4(0) = workPart
	Dim objects5(0) As NXOpen.NXObject
	objects5(0) = workPart
	attributePropertiesBuilder1.SetAttributeObjects(objects5)
	'attributePropertiesBuilder1.Units = "Inch"
	massPropertiesBuilder1.UpdateOnSave = NXOpen.MassPropertiesBuilder.UpdateOptions.Yes	
	attributePropertiesBuilder1.Category = "LEESTA ATTRIBUTE CATALOGUE" 'optional
	attributePropertiesBuilder1.Title = attrName
	attributePropertiesBuilder1.StringValue = attrValue
	Dim nXObject1 As NXOpen.NXObject = Nothing
	nXObject1 = attributePropertiesBuilder1.Commit()
	Dim updateoption1 As NXOpen.MassPropertiesBuilder.UpdateOptions = Nothing
	updateoption1 = massPropertiesBuilder1.UpdateOnSave
	Dim nXObject2 As NXOpen.NXObject = Nothing
	nXObject2 = massPropertiesBuilder1.Commit()
	Dim id1 As NXOpen.Session.UndoMarkId = Nothing
	Dim nErrs1 As Integer = Nothing
	nErrs1 = theSession.UpdateManager.DoUpdate(id1)
	attributePropertiesBuilder1.Destroy()
	massPropertiesBuilder1.Destroy()

End Sub

End Module