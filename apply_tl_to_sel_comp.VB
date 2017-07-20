Option Strict Off
Imports System
Imports System.Collections.Generic
Imports NXOpen
Imports NXOpenUI
 
 
Module Module1
 
    Sub Main()
 
        Dim theSession As Session = Session.GetSession()
        Dim theUI As UI = UI.GetUI
 
        If IsNothing(theSession.Parts.BaseWork) Then
            'active part required
            Return
        End If
 
        Dim numsel As Integer = theUI.SelectionManager.GetNumSelectedObjects()
        Dim theComps As New List(Of Assemblies.Component)
 
        For i As Integer = 0 To numsel - 1
            Dim selObj As TaggedObject = theUI.SelectionManager.GetSelectedTaggedObject(i)
            If TypeOf (selObj) Is Assemblies.Component Then
                theComps.Add(selObj)
            End If
        Next
 
        If theComps.Count = 0 Then
            'no components found among the preselected objects
            Return
        End If
 
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Edit Object Display")
 
        Dim displayModification1 As DisplayModification
        displayModification1 = theSession.DisplayManager.NewDisplayModification()
        With displayModification1
            .ApplyToAllFaces = False
            .ApplyToOwningParts = False
            .NewTranslucency = 50
            .Apply(theComps.ToArray)
        End With
        displayModification1.Dispose()
 
    End Sub 
 
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
 
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
 
    End Function
 
End Module