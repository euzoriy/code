'' Journal to create TF assembly and folder structure corresponding to "TEMPLATE"
'' Created by I.Zorii 03-09-2017
' NX 11.0.0.33
' Journal created by ievgenz on Mon Feb 27 09:14:24 2017 Eastern Standard Time
'
Imports System
Imports NXOpen
Imports System.Windows.Forms
Imports NXOpen.UF

Module NXJournal
    Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Dim workPart As NXOpen.Part = theSession.Parts.Work
    Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim lw As ListingWindow = theSession.ListingWindow
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim templatesDir As String = ""


    Dim templateLocationList() As String = {"assembly-inch-template.prt", "leesta-assembly-inch.prt"}

    Sub Main(ByVal args() As String)
        Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
        Dim baseDir As String = ""
        Dim nxNumber As String = ""
        'ufs.UF.TranslateVariable("NX_CUSTOM_DIR", baseDir)
        ufs.UF.TranslateVariable("LEESTA_CUSTOM", baseDir)
        ufs.UF.TranslateVariable("NX_Number", nxNumber)
        'templatesDir = baseDir & "\" & nxNumber & "library\Templates\"
        templatesDir = baseDir & "\Templates\"

        'lw.Open()		
        ''******************tf number entry***************
        Dim tfNum As String
        Dim tfDir As String
        Dim tfPartFullName As String
        Dim currentPN As String
        Dim assyRev As Integer = 0
        tfNum = InputBox("Enter TF-name.", "TF-name", "TF")
        If UCase(tfNum) = "TF" Or tfNum = "" Then
            lw.WriteLine("TF number is not valid. Journal exiting")
            Exit Sub
        End If
        tfDir = "L:\Tooling Fixtures\"
        createTFFolder(tfDir, tfNum)
        ''tfPartFullName = tfDir & tfNum & "\MODELING\" & tfNum & "-01.prt"

        Do
            assyRev = assyRev + 1
            tfPartFullName = tfDir & tfNum & "\MODELING\" & tfNum & "-" & Format(assyRev, "00") & ".prt"
            ''[ Continue Do ]  
            ''[ statements ]  
            ''[ Exit Do ]  
            ''[ statements ]  
        Loop While IO.File.Exists(tfPartFullName)

        If assyRev > 1 Then
            Dim createNewRev As DialogResult
            createNewRev = MessageBox.Show("File '" & tfDir & tfNum & "\MODELING\" & tfNum & "-" & Format(assyRev - 1, "00") & ".prt" _
                                        & "' already exist." & " Do you want to create new revision? ('No' to exit journal)",
                                        "Create new revision?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If createNewRev = vbNo Then
                Exit Sub
            End If
        End If

        ''******************tf number entry end***************	
        Try
            If Not workPart Is Nothing Then
                If IO.File.Exists(workPart.FullPath) Then
                    currentPN = IO.Path.GetFileName(workPart.FullPath)
                    createParentAssy(currentPN, tfPartFullName)
                    'savePart()
                    'exit try							
                Else
                    createEmptyTFAssy(tfPartFullName)
                End If
            Else
                createEmptyTFAssy(tfPartFullName)
                'savePart()									
            End If
            savePart()
        Catch ex As Exception
            Dim exMessage As String = ex.Message
            If Not exMessage.Contains("No object found with this name") Then
                lw.WriteLine(Timestamp() & ex.Message & vbNewLine & ex.StackTrace)
            End If
            ''lw.WriteLine(ex.Message & vbNewLine & ex.StackTrace)
            Exit Try
        End Try
    End Sub
    Sub createTFFolder(tfRoot As String, tfNumber As String)
        Try
            'System.Diagnostics.Process.Start(myfile)	
            ''Create directory for assembly and copy structure from templates
            If Len(Dir(tfRoot & tfNumber, vbDirectory)) = 0 Then

                MkDir(tfRoot & tfNumber)
            End If
            My.Computer.FileSystem.CopyDirectory(tfRoot & "TEMPLATE", tfRoot & tfNumber, False)
            lw.Writeline(Timestamp() & "Folder '" & tfRoot & tfNumber & "' created.")
        Catch ex As Exception
            lw.WriteLine(Timestamp() & "Specified folder: " & tfRoot & tfNumber & " Cannot be created.")
        End Try
    End Sub
    ''**********************************************************************************************
    Sub createParentAssy(currentPN As String, tfPartFullName As String)
        ' ----------------------------------------------
        '   Menu: Assemblies->Components->Create New Parent...
        ' ----------------------------------------------
        Dim fileNew1 As NXOpen.FileNew = Nothing
        fileNew1 = theSession.Parts.FileNew()
        Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
        markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create New Parent")
        theSession.DeleteUndoMark(markId2, Nothing)
        Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
        markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create New Parent")

        fileNew1.TemplateFileName = assyTemplateName()

        fileNew1.UseBlankTemplate = False
        fileNew1.ApplicationName = "AssemblyTemplate"
        fileNew1.Units = NXOpen.Part.Units.Inches
        fileNew1.RelationType = ""
        fileNew1.UsesMasterModel = "No"
        fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item
        fileNew1.TemplatePresentationName = "Assembly"
        fileNew1.ItemType = ""
        fileNew1.Specialization = ""
        fileNew1.SetCanCreateAltrep(False)
        fileNew1.NewFileName = tfPartFullName
        fileNew1.MasterFileName = ""
        fileNew1.MakeDisplayedPart = True
        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = fileNew1.Commit()
        workPart = theSession.Parts.Work ' assembly1
        displayPart = theSession.Parts.Display ' assembly1
        theSession.DeleteUndoMark(markId3, Nothing)
        fileNew1.Destroy()
        Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
        markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create New Parent")
        Dim part1 As NXOpen.Part = CType(theSession.Parts.FindObject(currentPN), NXOpen.Part)
        Dim basePoint1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
        Dim orientation1 As NXOpen.Matrix3x3 = Nothing
        orientation1.Xx = 1.0
        orientation1.Xy = 0.0
        orientation1.Xz = 0.0
        orientation1.Yx = 0.0
        orientation1.Yy = 1.0
        orientation1.Yz = 0.0
        orientation1.Zx = 0.0
        orientation1.Zy = 0.0
        orientation1.Zz = 1.0
        Dim partLoadStatus1 As NXOpen.PartLoadStatus = Nothing
        Dim component1 As NXOpen.Assemblies.Component = Nothing
        component1 = workPart.ComponentAssembly.AddMasterPartComponent(part1, "None", currentPN, basePoint1, orientation1, -1, partLoadStatus1)
        partLoadStatus1.Dispose()
        Dim arrangement1 As NXOpen.Assemblies.Arrangement = CType(part1.ComponentAssembly.Arrangements.FindObject("Arrangement 1"), NXOpen.Assemblies.Arrangement)
        component1.SetUsedArrangement(arrangement1)
    End Sub
    ''**********************************************************************************************
    Sub createEmptyTFAssy(tfPartFullName As String)
        Dim fileNew2 As NXOpen.FileNew = Nothing
        fileNew2 = theSession.Parts.FileNew()

        fileNew2.TemplateFileName = assyTemplateName()

        fileNew2.UseBlankTemplate = False
        fileNew2.ApplicationName = "AssemblyTemplate"
        fileNew2.Units = NXOpen.Part.Units.Inches
        fileNew2.RelationType = ""
        fileNew2.UsesMasterModel = "No"
        fileNew2.TemplateType = NXOpen.FileNewTemplateType.Item
        fileNew2.TemplatePresentationName = "Assembly"
        fileNew2.ItemType = ""
        fileNew2.Specialization = ""
        fileNew2.SetCanCreateAltrep(False)
        ''******************************************************************************************
        fileNew2.NewFileName = tfPartFullName
        ''******************************************************************************************
        fileNew2.MasterFileName = ""
        fileNew2.MakeDisplayedPart = True
        Dim nXObject2 As NXOpen.NXObject = Nothing
        nXObject2 = fileNew2.Commit()
        workPart = theSession.Parts.Work
        displayPart = theSession.Parts.Display
        fileNew2.Destroy()
        theSession.ApplicationSwitchImmediate("UG_APP_GATEWAY")
        theSession.ApplicationSwitchImmediate("UG_APP_MODELING")
        Dim exists1 As Boolean = Nothing


    End Sub
    ''**********************************************************************************************
    Sub savePart()
        If Not workPart Is Nothing Then
            Try
                Dim partSaveStatus1 As NXOpen.PartSaveStatus = Nothing
                ''Object reference not set to an instance of an object
                partSaveStatus1 = workPart.Save(NXOpen.BasePart.SaveComponents.False, NXOpen.BasePart.CloseAfterSave.False)
                partSaveStatus1.Dispose()
                lw.WriteLine(Timestamp() & "Part '" & workPart.FullPath & "' saved.")

            Catch ex As Exception
                lw.WriteLine(Timestamp() & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        Else
            lw.Writeline(Timestamp() & "Part file not created.")
        End If
    End Sub

    ''************************************************************************************************
    Function Timestamp() As String
        Timestamp = DateTime.Now & " :" & vbTab
    End Function

    ''************************************************************************************************	
    Function assyTemplateName() As String
        assyTemplateName = ""
        For Each fileName As String In templateLocationList
            'msgbox (templatesDir & fileName)
            If IO.File.Exists(templatesDir & fileName) Then	
                assyTemplateName = fileName
                Exit For
            End If
        Next
        If assyTemplateName = "" Then
            msgbox("Template file is incorrect")
 
        End If
    End Function

End Module