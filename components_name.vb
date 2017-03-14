

Option Strict Off
 
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies
 
Module Module1
 
    Dim theSession As Session = Session.GetSession()
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim lw As ListingWindow = theSession.ListingWindow
 
    Sub Main()
        Dim workPart As Part = theSession.Parts.Work
        Dim dispPart As Part = theSession.Parts.Display
 
        lw.Open()
        Try
            Dim c As ComponentAssembly = dispPart.ComponentAssembly
            If Not IsNothing(c.RootComponent) Then
                reportComponentChildren(c.RootComponent, 0)
            End If
        Catch e As Exception
            theSession.ListingWindow.WriteLine("Failed: " & e.ToString)
        End Try
        lw.Close()
 
    End Sub
 
    Sub reportComponentChildren(ByVal comp As Component, _
        ByVal indent As Integer)
 
        For Each child As Component In comp.GetChildren()
            '*** insert code to process component or subassembly
            If LoadComponent(child) Then
                lw.WriteLine("component name: " & child.Name)
                lw.WriteLine("file name: " & child.Prototype.OwningPart.Leaf)
                lw.WriteLine("")
            Else
                'component could not be loaded
            End If
            '*** end of code to process component or subassembly
            reportComponentChildren(child, indent + 1)
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
 
End Module

