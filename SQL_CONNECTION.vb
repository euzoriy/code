' NX 11.0.2.7
' Journal created by ievgenz on Thu Dec 13 13:47:51 2018 Eastern Standard Time
'
Imports System
Imports System.Data
Imports System.Data.SqlClient

Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI

Module NXJournal

        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        'Dim workPart As NXOpen.Part = theSession.Parts.Work
        'Dim displayPart As NXOpen.Part = theSession.Parts.Display
		Dim lw As ListingWindow = theSession.ListingWindow

    Sub Main(ByVal args() As String)			
        GetConnection()
    End Sub


    Sub GetConnection()
        Dim connetionString As String
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As String

        Dim cnn As SqlConnection

        connetionString = "Data Source=LSTENGDB\SQLEXPRESS;Initial Catalog=Inventory;User ID=sa;Password=76Te"
        connetionString = "Server=LSTENGDB\SQLEXPRESS;Database=Inventory;User ID=sa;Password=76Te"

        
        'connetionString = "Provider=SQLNCLI10;Data Source=LSTENGDB\SQLEXPRESS;Initial Catalog=Inventory;UID=sa;Pwd=76Te"

        cnn = New SqlConnection(connetionString)
        cnn.Open()
        'Try
        myCmd = cnn.CreateCommand
        myCmd.CommandText = "SELECT * FROM ItemListView"
        myReader = myCmd.ExecuteReader()
            results = ""
        'cnn.Open()
        Do While myReader.Read()
'			results = results & "<TR>"
            results = results & "" & myReader.GetString(1) &  vbTab & "" &
                myReader.GetString(1) & vbLf
        Loop
		
        'Display results.
        'MsgBox(results)
        lw.Open()
        lw.WriteLine(results)
		
            'MsgBox("Connection Open ! ")

            myReader.Close()
            cnn.Close()
        'Catch ex As Exception
        'MsgBox("Can not open connection ! " & ex.ToString)
        'End Try
    End Sub

End Module


