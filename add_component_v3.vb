' NX 11.0.2.7
' Journal created by Ievgen Zorii on Thu Apr 12 08:45:23 2018 Eastern Daylight Time
'
' ----------------------------------------------
'   Create new assembly component  with correct file name according to Leesta standard
' ----------------------------------------------
'Journal modified To look parts names In entire session, Not In assembly
'
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies
Module NXJournal

	'Constants
	CONST modelTemplateName = "LEESTA_MODEL_INCH.prt"

	'Global vars
    Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Dim workPart As NXOpen.Part = theSession.Parts.Work
    Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim ufs As UFSession = UFSession.GetUFSession()

    Dim lw As ListingWindow = theSession.ListingWindow
    Dim sessionParts = New List(Of String)
	
    Sub Main(ByVal args() As String)
        Dim newComponentName As String
        Dim assemblyLocation As String
        Try
            GetSessionParts(theSession, sessionParts)
            GetNextComponent(displayPart.fullpath, assemblyLocation, newComponentName)
            Do While AddComponent(assemblyLocation, newComponentName) = newComponentName
                sessionParts.Add(newComponentName)
                GetNextComponent(displayPart.fullpath, assemblyLocation, newComponentName)
            Loop

        Catch e As Exception
        theSession.ListingWindow.Open()
        theSession.ListingWindow.WriteLine("Failed: " & e.ToString)
        End Try
    End Sub

    Function AddComponent(assemblyLocation As String, newComponentName As String) As String
        AddComponent = ""
        Try
            Dim fileNew1 As NXOpen.FileNew = Nothing
            fileNew1 = theSession.Parts.FileNew()
            fileNew1.TemplateFileName = modelTemplateName
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
                AddComponent = newComponentName
            Else
                theSession.ListingWindow.Open
                theSession.ListingWindow.WriteLine("Failed: " & ex.ToString)
            End If
        End Try
    End Function

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

        While File.Exists(GetComponentPath(directoryName, assemblyName, counter, assemblyRevision)) Or sessionParts.Contains(GetComponentName(assemblyName, counter, assemblyRevision))
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

    Sub GetSessionParts(ByRef theSession As NXOpen.Session, ByRef sessionParts As List(Of String))
        lw.Open
        For Each prt As Part In theSession.Parts
            sessionParts.Add(prt.Name)
        Next
    End Sub
		
End Module