Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpenUI

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
        'For inx As Integer = 0 To numsel - 1
        '    selobj = theUI.SelectionManager.GetSelectedObject(inx)
        '    ufs.Obj.AskTypeAndSubtype(selobj.Tag, type, subtype)
        '    lw.WriteLine(" Object: " & selobj.ToString())
        '    lw.WriteLine(" Tag: " & selobj.Tag.ToString())
        '    lw.WriteLine(" Type(text): " & selobj.GetType().ToString())
        '    lw.WriteLine(" Type: " & type.ToString())
        '    lw.WriteLine(" Subtype: " & subtype.ToString())
        '    If type = 63 And subtype = 1 Then
        '        Dim selectedPart As Part
        '        selectedPart = selobj
        '        lw.WriteLine(" Location: " & selectedPart.fullpath)
        '    End If

        '    lw.WriteLine("")
        'Next
        'lw.CloseWindow()
        lw.Close()
        lw.Open()
        Dim sOut As String
        sOut = "<!DOCTYPE html><html><body><table>"
        For inx As Integer = 0 To numsel - 1
            selobj = theUI.SelectionManager.GetSelectedObject(inx)
            ufs.Obj.AskTypeAndSubtype(selobj.Tag, type, subtype)
            sOut = sOut + "<tr>"
            sOut = sOut + String.Format("<th>Object: {0} </th>", selobj.ToString())
            sOut = sOut + String.Format("<th>Tag: {0} </th>", selobj.Tag.ToString())
            sOut = sOut + String.Format("<th>Type(text): {0} </th>", selobj.GetType().ToString())
            sOut = sOut + String.Format("<th>Type: {0} </th>", type.ToString())
            sOut = sOut + String.Format("<th>Subtype: {0} </th>", subtype.ToString())
            If type = 63 And subtype = 1 Then
                Dim selectedPart As Part
                selectedPart = selobj
                sOut = sOut + String.Format("<th>Location: {0} </th>", selectedPart.fullpath)
            End If
        Next
        lw.WriteFullline(sOut)
        lw.Close()
        'lw.open()

    End Sub
End Module



