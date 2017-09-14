Option Strict Off
Imports System
Imports System.Web
Imports System.Diagnostics 
Imports System.IO
Imports System.Net
Imports System.Text


Imports System.Collections.Generic
Imports NXOpen
Imports NXOpenUI
Imports NXOpen.UF
Imports NXOpen.Assemblies
 
Module Module1

    Dim theSession As Session = Session.GetSession()
    Dim theUI As UI = UI.GetUI
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim lw As ListingWindow = theSession.ListingWindow 
	
    Sub Main()
	
        If IsNothing(theSession.Parts.BaseWork) Then
            'active part required
            Return
        End If
 
        Dim numsel As Integer = theUI.SelectionManager.GetNumSelectedObjects()
        ''Dim theComps As New List(Of Assemblies.Component)
 
		Dim comp As Component
		Dim arr As New List(Of String) ' let brackets empty, not Dim arr(1) As Variant !
		Dim compName as String
		''Dim compCount as Integer = 0
 
        For i As Integer = 0 To numsel - 1
            Dim selObj As TaggedObject = theUI.SelectionManager.GetSelectedTaggedObject(i)
            If TypeOf (selObj) Is Assemblies.Component Then
			
				comp = selObj
				If LoadComponent(comp) Then	
					compName = comp.Name
					if Not arr.contains(compName) then 
						OpenMcMaster(compName)
						arr.add(compName)
						''compCount = compCount + 1						
						lw.WriteLine("component name: " & compName)						
					end if
				Else
                'component could not be loaded
				End If
				
                ''theComps.Add(selObj)
            End If
        Next
 
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Open Components on web")
 
    End Sub 
 
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
 
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
 
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
	
	Sub OpenMcMaster(URL as String)
	'*********************************************

   
	'**************************************************
	  ' Creates an HttpWebRequest for the specified URL. 
	  Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://www.mcmaster.com/#" & URL), HttpWebRequest)
	  ' Sends the request and waits for a response.			
	  Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
	  ' Calls the method GetResponseStream to return the stream associated with the response.
	  Dim receiveStream As Stream = myHttpWebResponse.GetResponseStream()
	  Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
	  ' Pipes the response stream to a higher level stream reader with the required encoding format. 
	  Dim readStream As New StreamReader(receiveStream, encode)
	  lw.WriteLine(ControlChars.Lf + ControlChars.Cr + "Response stream received")
	  Dim read(256) As [Char]
	  ' Reads 256 characters at a time.    
	  Dim count As Integer = readStream.Read(read, 0, 256)
	  lw.WriteLine("HTML..." + ControlChars.Lf + ControlChars.Cr)
	  While count > 0
		  ' Dumps the 256 characters to a string and displays the string to the console.
		  Dim str As New [String](read, 0, count)
		  lw.WriteLine(str)
		  count = readStream.Read(read, 0, 256)
	  End While
	  lw.WriteLine("")
	  ' Releases the resources of the Stream.
	  readStream.Close()
	' Releases the resources of the response.
	  myHttpWebResponse.Close()
	 
	'****************************************************** 
	 
        Try
            '// try set browser if there was an error (browser not installed)
            Process.Start("default", "https://www.mcmaster.com/#" & URL)
        Catch ex As Exception
            '// use default browser
            Process.Start("https://www.mcmaster.com/#" & URL)
        End Try
	End Sub
 
End Module




