Option Strict Off
Imports System
Imports NXOpen
Imports NXOpen.Annotations
Imports NXOpen.UI
Imports NXOpen.UF
Imports NXOpen.Utilities

Module create_tabnote_with_all_3d_points_coordinates
    Dim s As Session = Session.GetSession()
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim ui As UI = UI.GetUI()
    Dim lw As ListingWindow = s.ListingWindow
    Dim workPart As Part = s.Parts.Work

    Sub Main()
        Dim startcell0 As String = "MATERIAL"
        Dim startcell1 As String = "C/L BEND RAD"
        Dim startcell2 As String = "END: A"
        Dim startcell3 As String = "END: B"
        Dim startcell4 As String = "PIPE TO CONFORM TO JCB STD 1000/002"
        Dim endcell0 As String = "NUMBER OF BENDS"
        Dim endcell1 As String = "DEVELOPED LENGTH"
        Dim endcell2 As String = "THIS COMPONENT MUST CONFORM TO: JCB STANDARD 9993/003 HYDRAULIC CLEANLINESS"
        Dim endcell3 As String = "ZINC PLATE TO: JCB STANDARD 9994/0800 - CORROSION PROTECTION TO: JCB STANDARD 1007/007 CATEGORY 'E'"
        Dim xvalue As Double = Nothing
        Dim response1 As Selection.Response = Selection.Response.Cancel
        ' Get a view for the IDs
        Dim dwgview As View = Nothing
        If select_a_drawing_member_view(dwgview) <> Selection.Response.Ok Then
            Return
        End If
        ' Get a location for the tabular note
        Dim cursor As Point3d
        Dim response As Selection.DialogResponse = SelectScreenPos(cursor, dwgview)
        If response <> Selection.DialogResponse.Pick Then
            Return
        End If
        ' Create the tabular note
        Dim n_new_columns As Integer = 4
        Dim tabnote As NXOpen.Tag = CreateTabnoteWithSize(0, n_new_columns, cursor)
        ' Get the column tags
        Dim columns(n_new_columns - 1) As NXOpen.Tag
        For ii As Integer = 0 To n_new_columns - 1
            ufs.Tabnot.AskNthColumn(tabnote, ii, columns(ii))
        Next
        Dim pt1 As Point = Nothing
        Dim row As NXOpen.Tag
        Dim cell As NXOpen.Tag
        Dim cells(3) As NXOpen.Tag
        ' add start row1
        ' MATERIAL
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell0)
        ' C/L BEND RAD
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell1)
        ' END: A
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell2)
        ' END: B
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell3)
        ' PIPE TO CONFORM TO JCB STD 1000/002
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell4)
        ' Add points Header Row
        Dim headerrow As NXOpen.Tag
        ufs.Tabnot.CreateRow(40, headerrow)
        ufs.Tabnot.AddRow(tabnote, headerrow, UFConstants.UF_TABNOT_APPEND)
        
        ufs.Tabnot.AskCellAtRowCol(headerrow, columns(0), cell)
        '   ufs.Tabnot.SetCellText(cell, "ID")
        ufs.Tabnot.AskCellAtRowCol(headerrow, columns(1), cell)
        ufs.Tabnot.SetCellText(cell, "X")
        ufs.Tabnot.AskCellAtRowCol(headerrow, columns(2), cell)
        ufs.Tabnot.SetCellText(cell, "Y")
        ufs.Tabnot.AskCellAtRowCol(headerrow, columns(3), cell)
        ufs.Tabnot.SetCellText(cell, "Z")
        Dim letteringPrefs As LetteringPreferences = Nothing
        Dim userSymPrefs As UserSymbolPreferences = Nothing
        Dim markId1 As Session.UndoMarkId
        markId1 = s.SetUndoMark(Session.MarkVisibility.Visible, "Start")
        Dim jj As Integer = 0
        Dim id As Integer = 1
start1:
        ' Add one row for each point
        response1 = select_a_point(pt1)
        If response1 <> Selection.Response.Ok Then GoTo end1
        '  For Each pt As Point In pcol1
        id = jj + 1
        ' Get the Coordinates
        Dim pt3d As Point3d = pt1.Coordinates
        ' Add a row for each point

        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        ufs.Tabnot.AskCellAtRowCol(row, columns(0), cell)
        ufs.Tabnot.SetCellText(cell, id.ToString())
        ' Set the cell text
        ufs.Tabnot.AskCellAtRowCol(row, columns(1), cell)
        ufs.Tabnot.SetCellText(cell, FormatNumber(pt3d.X, 1).ToString())
        ufs.Tabnot.AskCellAtRowCol(row, columns(2), cell)
        ufs.Tabnot.SetCellText(cell, FormatNumber(pt3d.Y, 1).ToString())
        ufs.Tabnot.AskCellAtRowCol(row, columns(3), cell)
        ufs.Tabnot.SetCellText(cell, FormatNumber(pt3d.Z, 1).ToString())
        ' Add ID notes to the points
        AddNoteToPoint(id, pt1, pt3d, dwgview)
        jj = jj + 1
        Dim markId2 As Session.UndoMarkId
        markId2 = s.SetUndoMark(Session.MarkVisibility.Invisible, "Note")
        GoTo start1
end1:
        ' add the ending rows
        ' NUMBER OF BENDS
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), endcell0)
        ' DEVELOPED LENGTH
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(1))
        ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), endcell1)
        ' THIS COMPONENT MUST CONFORM TO: JCB STANDARD 9993/003 HYDRAULIC CLEANLINESS
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(3))
        ufs.Tabnot.SetCellText(cells(0), endcell2)
        ' ZINC PLATE TO: JCB STANDARD 9994/0800 - CORROSION PROTECTION TO: JCB STANDARD 1007/007 CATEGORY 'E'
        ufs.Tabnot.CreateRow(40, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To 3
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        ufs.Tabnot.MergeCells(cells(0), cells(3))
        ufs.Tabnot.SetCellText(cells(0), endcell3)

    End Sub

    Public Function SelectScreenPos(ByRef pos As Point3d, ByVal view As View) As Selection.DialogResponse
        Dim letteringPrefs As LetteringPreferences = Nothing
        Dim userSymPrefs As UserSymbolPreferences = Nothing
        Return ui.SelectionManager.SelectScreenPosition("Select location for tabnote", view, pos)
    End Function

    Function select_a_drawing_member_view(ByRef dwgview As View) As Selection.Response
        Dim mask(1) As Selection.MaskTriple
        mask(0).Type = UFConstants.UF_view_type
        mask(0).Subtype = UFConstants.UF_view_imported_subtype
        mask(0).SolidBodySubtype = 0
        mask(1).Type = UFConstants.UF_view_type
        mask(1).Subtype = UFConstants.UF_view_orthographic_subtype
        mask(1).SolidBodySubtype = 0
        Dim cursor As Point3d = Nothing
        Dim vw As View = Nothing
        Dim resp As Selection.Response = _
        ui.SelectionManager.SelectObject("Select Points Drawing View", _
            "Select Points Drawing View", _
            Selection.SelectionScope.AnyInAssembly, _
            Selection.SelectionAction.ClearAndEnableSpecific, _
            False, False, mask, vw, cursor)
        If resp = Selection.Response.ObjectSelected Or _
           resp = Selection.Response.ObjectSelectedByName Then
            dwgview = CType(vw, View)
            Return Selection.Response.Ok
        Else
            Return Selection.Response.Cancel
        End If
    End Function

    Public Function CreateTabnoteWithSize( _
        ByVal nRows As Integer, ByVal nColumns As Integer, ByVal loc As Point3d) As NXOpen.Tag
        ' Create the tabular note
        Dim secPrefs As UFTabnot.SectionPrefs
        ufs.Tabnot.AskDefaultSectionPrefs(secPrefs)
        Dim cellPrefs As UFTabnot.CellPrefs
        ufs.Tabnot.AskDefaultCellPrefs(cellPrefs)
        cellPrefs.zero_display = UFTabnot.ZeroDisplay.ZeroDisplayZero
        ufs.Tabnot.SetDefaultCellPrefs(cellPrefs)
        Dim origin(2) As Double
        origin(0) = loc.X
        origin(1) = loc.Y
        origin(2) = loc.Z
        Dim tabnote As NXOpen.Tag
        ufs.Tabnot.Create(secPrefs, origin, tabnote)
        ' Delete all existing columns and rows (we create them as needed)
        Dim nmRows As Integer = 0
        ufs.Tabnot.AskNmRows(tabnote, nmRows)
        For ii As Integer = 0 To nmRows - 1
            Dim row As NXOpen.Tag
            ufs.Tabnot.AskNthRow(tabnote, 0, row)
            ufs.Tabnot.RemoveRow(row)
            ufs.Obj.DeleteObject(row)
        Next
        Dim nmColumns As Integer = 0
        ufs.Tabnot.AskNmColumns(tabnote, nmColumns)
        For ii As Integer = 0 To nmColumns - 1
            Dim column As NXOpen.Tag
            ufs.Tabnot.AskNthColumn(tabnote, 0, column)
            ufs.Tabnot.RemoveColumn(column)
            ufs.Obj.DeleteObject(column)
        Next
        ' Now add our columns as needed
        Dim columns(nColumns - 1) As NXOpen.Tag
        For ii As Integer = 0 To nColumns - 1
            ufs.Tabnot.CreateColumn(40, columns(ii))
            ufs.Tabnot.AddColumn(tabnote, columns(ii), UFConstants.UF_TABNOT_APPEND)
        Next
        ' Now add our rows as needed
        Dim rows(nRows - 1) As NXOpen.Tag
        For ii As Integer = 0 To nRows - 1
            ufs.Tabnot.CreateRow(10, rows(ii))
            ufs.Tabnot.AddRow(tabnote, rows(ii), UFConstants.UF_TABNOT_APPEND)
        Next
        Return tabnote
    End Function

    Public Sub AddNoteToPoint(ByVal id As Integer, ByVal pnt1 As Point, _
                              ByVal pt As Point3d, ByVal dwgview As View)
        Dim screenpos As Point3d
        SelectScreenPoint(screenpos)
        Dim nullAnnotations_SimpleDraftingAid As Annotations.SimpleDraftingAid = Nothing
        Dim draftingNoteBuilder1 As Annotations.DraftingNoteBuilder
        draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullAnnotations_SimpleDraftingAid)
        draftingNoteBuilder1.Origin.Plane.PlaneMethod = Annotations.PlaneBuilder.PlaneMethodType.XyPlane
        draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(True)
        draftingNoteBuilder1.Origin.Anchor = Annotations.OriginBuilder.AlignmentPosition.MidCenter
        Dim text1(0) As String
        text1(0) = id.ToString
        draftingNoteBuilder1.Text.TextBlock.SetText(text1)
        Dim leaderData1 As Annotations.LeaderData
        leaderData1 = workPart.Annotations.CreateLeaderData()
        leaderData1.StubSize = 5.0
        leaderData1.Arrowhead = Annotations.LeaderData.ArrowheadType.OpenArrow
        draftingNoteBuilder1.Leader.Leaders.Append(leaderData1)
        leaderData1.StubSide = Annotations.LeaderSide.Inferred
        leaderData1.Leader.SetValue(pnt1, dwgview, pt)
        Dim assocOrigin1 As Annotations.Annotation.AssociativeOriginData
        assocOrigin1.OriginType = Annotations.AssociativeOriginType.RelativeToGeometry
        Dim nullView As View = Nothing
        draftingNoteBuilder1.Origin.Origin.SetValue(Nothing, nullView, screenpos)
        Dim nXObject1 As NXObject
        nXObject1 = draftingNoteBuilder1.Commit()
        draftingNoteBuilder1.Destroy()
    End Sub

    Public Function select_a_point(ByRef pt1 As Point) As Selection.Response
        Dim mask(0) As Selection.MaskTriple
        With mask(0)
            .Type = UFConstants.UF_point_type
            .Subtype = 0
            .SolidBodySubtype = 0
        End With
        Dim cursor As Point3d = Nothing
        ufs.Ui.SetCursorView(0)
        ufs.Ui.LockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        Dim resp As Selection.Response = _
   ui.SelectionManager.SelectObject("Select a point", "Select a point", _
       Selection.SelectionScope.AnyInAssembly, _
       Selection.SelectionAction.ClearAndEnableSpecific, _
       False, False, mask, pt1, cursor)
        ufs.Ui.UnlockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        If resp = Selection.Response.ObjectSelected Or _
           resp = Selection.Response.ObjectSelectedByName Then
            Return Selection.Response.Ok
        ElseIf resp = Selection.Response.Back Then
            Return Selection.Response.Back
        Else
            Return Selection.Response.Cancel
        End If
    End Function

    Function SelectScreenPoint(ByRef screenpos As Point3d)
        Dim displayPart As Part = s.Parts.Display
        Dim baseView1 As View = s.Parts.Work.Views.WorkView
        Dim point As Double() = {0.0, 0.0, 0.0}
        Dim response As Integer = 0
        Dim viewTag As Tag = Nothing
        Dim viewType As UFView.Type = Nothing
        Dim aView As View = Nothing
        Dim viewSubtype As UFView.Subtype = Nothing
        ufs.Ui.LockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        ufs.Ui.SetCursorView(1)
        Try
            ufs.Ui.SpecifyScreenPosition("Select Label Pos", Nothing, IntPtr.Zero, point, _
                                                   viewTag, response)
        Finally
            ufs.Ui.UnlockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        End Try
        If (response <> NXOpen.UF.UFConstants.UF_UI_PICK_RESPONSE) Then Return Selection.Response.Cancel
        screenpos.X = point(0)
        screenpos.Y = point(1)
        screenpos.Z = point(2)
        Return Selection.Response.Ok
    End Function

    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        Return Session.LibraryUnloadOption.Immediately
    End Function
End Module 

'http://www.eng-tips.com/viewthread.cfm?qid=336107