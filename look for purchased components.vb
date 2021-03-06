' NX 11.0.2.7
' Journal created by ievgenz on Fri Aug 18 14:39:19 2017 Eastern Daylight Time
'
Imports System
Imports System.IO
Imports System.Diagnostics
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies

Module NXJournal
Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim ufs As UFSession = UFSession.GetUFSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work
Dim dispPart As NXOpen.Part = theSession.Parts.Display
Dim lw As ListingWindow = theSession.ListingWindow
    Sub Main()
        lw.Open()
        Try
            Dim c As ComponentAssembly = dispPart.ComponentAssembly
            If Not IsNothing(c.RootComponent) Then
                searchComponentChildren(c.RootComponent)
            End If
        Catch e As Exception
            theSession.ListingWindow.WriteLine("Failed: " & e.ToString)
        End Try
        lw.Close()
    End Sub
    Sub searchComponentChildren(ByVal comp As Component)
        For Each child As Component In comp.GetChildren()
            '*** insert code to process component or subassembly
            If LoadComponent(child) Then                
                'lw.WriteLine("file name: " & child.Prototype.OwningPart.Leaf)                
				lookFor("child.Name")
            Else
                'component could not be loaded
            End If
            '*** end of code to process component or subassembly
        Next
    End Sub
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
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        Return Session.LibraryUnloadOption.Immediately
    End Function
	Sub lookFor(componentName As String)
		Dim process As New Process()
			Dim headers as string = 0		
			Dim dbLocation As String = "L:\Tooling Fixtures\StandardPartsInventory_Dev.accdb"
			Dim sql As String		
			sql = "SELECT InventoryList.ID, InventoryList.PartNumber, InventoryList.Location, InventoryList.LeestaPartNumber, InventoryList.Qty, InventoryList.VendorPartNumber FROM InventoryList where InventoryList.PartNumber like '*" & componentName & "*' or InventoryList.VendorPartNumber like '*" & componentName & "*' or InventoryList.LeestaPartNumber like '*" & componentName & "*' "
		process.StartInfo.FileName = "C:\Users\ievgenz\Documents\SharpDevelop Projects\db_interface\db_interface\bin\Debug\db_interface.exe" 
		process.StartInfo.Arguments = "0 """ & dblocation & """ """ & sql & """"

		process.StartInfo.UseShellExecute = False
		process.StartInfo.RedirectStandardOutput = True
		process.Start()
			lw.Open()
		' Synchronously read the standard output of the spawned process. 
		Dim reader As StreamReader = process.StandardOutput
		Dim output As String = reader.ReadToEnd()
			'Dim output As String = replace(output,Chr(5),vbTab & vbTab)
		lw.WriteLine(replace(output,Chr(5),vbTab))
		process.WaitForExit()
		process.Close()	
	End Sub
End Module