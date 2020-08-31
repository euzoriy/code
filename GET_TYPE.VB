Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI
'Imports NXOpen.BlockStyler

Module journal
    Sub Main()
        Dim s As Session = Session.GetSession()
        Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
        Dim selobj As NXObject
        Dim type As Integer
        Dim subtype As Integer
        Dim lw As ListingWindow = s.ListingWindow
        Dim theUI As UI = ui.GetUI
        Dim numsel As Integer = theUI.SelectionManager.GetNumSelectedObjects()
        lw.Open()
        lw.WriteLine("Selected Objects: " & numsel.ToString())
		
'		Dim Table(numsel,5) As Object		
		
        For inx As Integer = 0 To numsel - 1
            selobj = theUI.SelectionManager.GetSelectedObject(inx)
            ufs.Obj.AskTypeAndSubtype(selobj.Tag, type, subtype)
            lw.WriteLine(" Object: " & selobj.ToString() )
            lw.WriteLine(" Tag: " & selobj.Tag.ToString())
            lw.WriteLine(" Type(text): " & selobj.GetType().ToString())
            lw.WriteLine(" Type: " & type.ToString())
            lw.WriteLine(" Subtype: " & subtype.ToString())
            lw.WriteLine("")

'			Table(inx,1)= selobj.ToString() 
'			Table(inx,2)= selobj.Tag.ToString()
'			Table(inx,3)= selobj.GetType().ToString()
'			Table(inx,4)= type.ToString()
'			Table(inx,0)= subtype.ToString()
			
        Next
		
'		lw.WriteLine(Table.ToString())
		
    End Sub
End Module