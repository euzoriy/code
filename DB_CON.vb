
' NX 11.0.2.7
' Journal created by ievgenz on Fri Aug 18 14:39:19 2017 Eastern Daylight Time
'
Imports System
Imports System.IO
Imports System.Diagnostics
Imports NXOpen

Module NXJournal


Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display
Dim lw As ListingWindow = theSession.ListingWindow

Sub Main (ByVal args() As String) 
	TEST
End Sub

Sub TEST()
		

    Dim process As New Process()
		
		Dim headers as string = 0		
		
		Dim dbLocation As String = "L:\Tooling Fixtures\StandardPartsInventory_Dev.accdb"
		
		Dim sql As String = "SELECT id,PartNumber FROM InventoryList WHERE id < 10"		

		'sql = "SELECT InventoryList.ID, InventoryList.Location, InventoryList.LeestaPartNumber, InventoryList.Qty, InventoryList.VendorPartNumber, InventoryList.PartNumber FROM InventoryList"

		
		
    process.StartInfo.FileName = "C:\Users\ievgenz\Documents\SharpDevelop Projects\db_interface\db_interface\bin\Debug\db_interface.exe" '"ipconfig.exe"
		
		process.StartInfo.Arguments = "0 """ & dblocation & """ """ & sql & """"
		
    'process.StartInfo.FileName = "ipconfig.exe"
		
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



