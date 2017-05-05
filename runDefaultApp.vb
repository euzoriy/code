Option Strict Off
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpen.Annotations
Imports System
Imports System.Web
Imports System.Diagnostics 
Imports System.Windows.Forms

Module NXJournal


Sub Main
	Dim theSession As Session = Session.GetSession()
	Dim lw As ListingWindow = theSession.ListingWindow	
	Dim myURL as String
	lw.Open()
	myURL = InputBox("Enter location to open or command to run...", "Run", "L:\Tooling Fixtures")
	If TypeOf (myURL) Is String And myURL <> "" Then
			OpenLocation(myURL)					
			lw.WriteLine(DateTime.Now & " :" &vbTab & "Opening " & myURL)				
    End If
End Sub
	
	Sub OpenLocation(myURL as String)
        Try
            Process.Start("default", myURL)
        Catch ex As Exception
            Process.Start(myURL)		
        End Try
	End Sub
End Module

	