Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI
 
Module journal
 
  Sub Main
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
 
    For inx As Integer = 0 To numsel-1
      selobj = theUI.SelectionManager.GetSelectedObject(inx)
 
      ufs.Obj.AskTypeAndSubtype(selobj.Tag, type, subtype)
      lw.WriteLine("Object: " & selobj.ToString())
      lw.WriteLine(" Tag: " & selobj.Tag.ToString())
      lw.WriteLine(" Type: " & type.ToString())
	  lw.WriteLine(" Type(text): " & selobj.GetType().ToString())
      lw.WriteLine(" Subtype: " & subtype.ToString())
      lw.WriteLine("")
    Next
 
    End Sub
 
End Module