' NX 11.0.2.7
' Journal created by Ievgen Zorii on Thu Apr 12 08:45:23 2018 Eastern Daylight Time
'
' ----------------------------------------------
'   Create new assembly component  with correct file name according to Leesta standard
' ----------------------------------------------
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies
Module NXJournal
    Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Dim workPart As NXOpen.Part = theSession.Parts.Work
    Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim ufs As UFSession = UFSession.GetUFSession()

    Dim lw As ListingWindow = theSession.ListingWindow
    Dim assemblyComponentsList = New List(Of String)

    Sub Main(ByVal args() As String)
        Dim newComponentName As String
        Dim assemblyLocation As String

        Try
            Dim c As ComponentAssembly = displayPart.ComponentAssembly
            If Not IsNothing(c.RootComponent) Then
                reportComponentChildren(c.RootComponent, assemblyComponentsList)
            End If

            'Dim c As ComponentAssembly
            'For Each pt As Part In theSession.Parts
            '    lw.WriteLine("component name: " & pt.Name)
            '    c = pt.ComponentAssembly
            '    If Not IsNothing(c.RootComponent) Then
            '        reportComponentChildren(c.RootComponent, assemblyComponentsList)
            '    End If
            'Next

        Catch e As Exception
            theSession.ListingWindow.Open()
            If e.ToString.Contains("File already exists") Then
                theSession.ListingWindow.WriteLine("File already exists. Please check if loaded files are saved.")
            Else
                theSession.ListingWindow.WriteLine("Failed: " & e.ToString)
            End If
        End Try

        If False Then
            'Dim assemblyName As String = "TF3465"
            'Dim componentNumber As String = "030"
            'Dim assemblyRevision As String = "02"
            'Dim assemblyLocation = "L:\Tooling Fixtures\" & assemblyName & "\MODELING\"
            'Dim NewComponentName = Join({assemblyName, componentNumber, assemblyRevision}, "-")
        Else
            GetNextComponent(displayPart.fullpath, assemblyLocation, newComponentName)
        End If

        'msgBox(assemblyLocation + "/" + newComponentName)
        'MsgBox(String.Join("---", assemblyComponentsList))

        AddComponent(assemblyLocation, newComponentName)

    End Sub

    Sub AddComponent(assemblyLocation As String, newComponentName As String)
        Try
            Dim fileNew1 As NXOpen.FileNew = Nothing
            fileNew1 = theSession.Parts.FileNew()
            fileNew1.TemplateFileName = "LEESTA_MODEL_INCH.prt"
            fileNew1.UseBlankTemplate = False
            fileNew1.ApplicationName = "ModelTemplate"
            fileNew1.Units = NXOpen.Part.Units.Inches
            fileNew1.RelationType = ""
            fileNew1.UsesMasterModel = "No"
            fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item
            fileNew1.TemplatePresentationName = "Model"
            fileNew1.ItemType = ""
            fileNew1.Specialization = ""
            fileNew1.SetCanCreateAltrep(False)

            fileNew1.NewFileName = assemblyLocation & newComponentName & ".prt" 'New Component File Path
            fileNew1.MasterFileName = ""
            fileNew1.MakeDisplayedPart = False
            Dim createNewComponentBuilder1 As NXOpen.Assemblies.CreateNewComponentBuilder = Nothing
            createNewComponentBuilder1 = workPart.AssemblyManager.CreateNewComponentBuilder()
            createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.EntirePartOnly
            createNewComponentBuilder1.ReferenceSetName = "Entire Part"
            createNewComponentBuilder1.NewComponentName = newComponentName '"TF3465-020-02"
            ' ----------------------------------------------
            '   Dialog Begin Create New Component
            ' ----------------------------------------------
            createNewComponentBuilder1.NewFile = fileNew1
            Dim nXObject1 As NXOpen.NXObject = Nothing
            nXObject1 = createNewComponentBuilder1.Commit()
            createNewComponentBuilder1.Destroy()
            ' ----------------------------------------------
            '   Menu: Tools->Journal->Stop Recording
            ' ----------------------------------------------
        Catch ex As Exception
            If ex.ToString.Contains("File already exists") Then
                theSession.ListingWindow.WriteLine("File already exists. Please check if loaded files are saved.")
            Else
                theSession.ListingWindow.WriteLine("Failed: " & ex.ToString)
            End If
        End Try

    End Sub

    Sub GetNextComponent(ByVal filepath As String, ByRef directoryName As String, ByRef componentName As String)
        Dim Result = False
        Dim fileName As String
        Dim assemblyName As String

        Dim assemblyRevision As String

        Dim counter As Integer = 10
        Dim iterator As Integer = 10

        Dim fileNameSplitted() As String
        directoryName = Path.GetDirectoryName(filepath) + "\"
        fileName = Path.GetFileNameWithoutExtension(filepath)
        fileNameSplitted = fileName.Split("-")
        If fileNameSplitted.Length > 0 Then
            assemblyName = fileNameSplitted(0)
            assemblyRevision = fileNameSplitted(fileNameSplitted.Length - 1)
        End If

        'componentNumber = FormatException(counter, "000")
        'GetComponentName(directoryName, assemblyName, counter, assemblyRevision)

        While File.Exists(GetComponentPath(directoryName, assemblyName, counter, assemblyRevision)) Or assemblyComponentsList.Contains(GetComponentName(assemblyName, counter, assemblyRevision))
            counter = counter + iterator
        End While
        componentName = GetComponentName(assemblyName, counter, assemblyRevision)
    End Sub

    Function GetComponentPath(directoryName, assemblyName, counter, assemblyRevision) As String
        Return Path.Combine(directoryName, GetComponentName(assemblyName, counter, assemblyRevision) + ".prt")
    End Function

    Function GetComponentName(assemblyName, counter, assemblyRevision) As String
        Return Join({assemblyName, Format(counter, "000"), assemblyRevision}, "-")
    End Function

    Function reportComponentChildren(ByVal comp As Component, ByRef assemblyComponentsList As List(Of String))
        For Each child As Component In comp.GetChildren()
            '*** insert code to process component or subassembly
            If LoadComponent(child) Then
                'lw.WriteLine("component name: " & child.Name)
                'lw.WriteLine("file name: " & child.Prototype.OwningPart.Leaf)
                'lw.WriteLine("")
                If Not assemblyComponentsList.Contains(child.Name) Then
                    assemblyComponentsList.Add(child.Name)
                End If
            Else
                'component could not be loaded
            End If
            '*** end of code to process component or subassembly
            reportComponentChildren(child, assemblyComponentsList)
        Next

    End Function

    Private Function LoadComponent(ByVal theComponent As Component) As Boolean
        Dim thePart As Part = theComponent.Prototype.OwningPart
        Dim partName As String = ""
        Dim refsetName As String = ""
        Dim instanceName As String = ""
        Dim origin(2) As Double
        Dim csysMatrix(8) As Double
        Dim transform(3, 3) As Double
        Try
            If thePart.IsFullyLoaded Then
                'component is fully loaded
            Else
                'component is partially loaded
            End If
            Return True
        Catch ex As NullReferenceException
            'component is not loaded
            Try
                ufs.Assem.AskComponentData(theComponent.Tag, partName, refsetName, instanceName, origin, csysMatrix, transform)
                Dim theLoadStatus As PartLoadStatus
                theSession.Parts.Open(partName, theLoadStatus)
                If theLoadStatus.NumberUnloadedParts > 0 Then
                    Dim allReadOnly As Boolean = True
                    For i As Integer = 0 To theLoadStatus.NumberUnloadedParts - 1
                        If theLoadStatus.GetStatus(i) = 641058 Then
                            'read-only warning, file loaded ok
                        Else
                            '641044: file not found
                            lw.WriteLine("file not found")
                            allReadOnly = False
                        End If
                    Next
                    If allReadOnly Then
                        Return True
                    Else
                        'warnings other than read-only...
                        Return False
                    End If
                Else
                    Return True
                End If
            Catch ex2 As NXException
                lw.WriteLine("error: " & ex2.Message)
                Return False
            End Try
        Catch ex As NXException
            'unexpected error
            lw.WriteLine("error: " & ex.Message)
            Return False
        End Try
    End Function
End Module