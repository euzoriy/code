Imports System
Imports System.IO
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.MessageBox
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI

Module xtras

	Dim theSession As Session = Session.GetSession()
	Dim workPart As Part = theSession.Parts.Work
	Dim displayPart As Part = theSession.Parts.Display
	Dim lw As ListingWindow = theSession.ListingWindow 	
		
		
'Forming timestamp string
	Function Timestamp () as String
		Timestamp = DateTime.Now & " :" & vbTab
	End Function

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
End Module