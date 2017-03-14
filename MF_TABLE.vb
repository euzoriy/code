Option Strict Off
Imports System
Imports NXOpen
Imports NXOpen.Annotations
Imports NXOpen.UI
Imports NXOpen.UF
Imports NXOpen.Utilities

Imports NXOpen.Assemblies

Module create_tabnote_with_assembly_components
    Dim s As Session = Session.GetSession()
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim ui As UI = UI.GetUI()
    Dim lw As ListingWindow = s.ListingWindow
    Dim workPart As Part = s.Parts.Work
    Dim dispPart As Part = s.Parts.Display
	
    Dim n_new_columns As Integer = 1
    Dim pt1 As Point = Nothing	
    Dim columns(n_new_columns - 1) As NXOpen.Tag
    Dim cells(n_new_columns) As NXOpen.Tag
	
    Dim row As NXOpen.Tag
    Dim cell As NXOpen.Tag
	

    Sub Main()
	
		'=============================
		'Dim cell As NXOpen.Tag
		Dim startcell0 As String = "MAKE FROM"

        Dim xvalue As Double = Nothing
        Dim response1 As Selection.Response = Selection.Response.Cancel
        ' Get a location for the tabular note
        Dim cursor As Point3d
        ' Dim response As Selection.DialogResponse = SelectScreenPos(cursor, dwgview)
        ' If response <> Selection.DialogResponse.Pick Then
        '     Return
        ' End If
        ' Create the tabular note

		
        Dim tabnote As NXOpen.Tag = CreateTabnoteWithSize(0, n_new_columns, cursor)
        ' Get the column tags
        'Dim columns(n_new_columns - 1) As NXOpen.Tag
        For ii As Integer = 0 To n_new_columns - 1
            ufs.Tabnot.AskNthColumn(tabnote, ii, columns(ii))
        Next
        'Dim pt1 As Point = Nothing
        'Dim row As NXOpen.Tag
        'Dim cell As NXOpen.Tag
        'Dim cells(n_new_columns) As NXOpen.Tag
        ' add start row1
        ' MATERIAL
        ufs.Tabnot.CreateRow(1, row)
        ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
        For i As Integer = 0 To n_new_columns-1
            ufs.Tabnot.AskCellAtRowCol(row, columns(i), cells(i))
        Next
        REM ufs.Tabnot.MergeCells(cells(0), cells(1))
        REM ufs.Tabnot.MergeCells(cells(2), cells(3))
        ufs.Tabnot.SetCellText(cells(0), startcell0)    
	
	'=============================
	
	
	
 
        lw.Open()
        Try
            Dim c As ComponentAssembly = dispPart.ComponentAssembly
            If Not IsNothing(c.RootComponent) Then
                reportComponentChildren(c.RootComponent, 1 , tabnote)
            End If
        Catch e As Exception
            s.ListingWindow.WriteLine("Failed: " & e.ToString)
        End Try
        lw.Close()
		
 
    End Sub	
	
	
	
	
     REM Public Function SelectScreenPos(ByRef pos As Point3d, ByVal view As View) As Selection.DialogResponse
         REM Dim letteringPrefs As LetteringPreferences = Nothing
         REM Dim userSymPrefs As UserSymbolPreferences = Nothing
         REM Return ui.SelectionManager.SelectScreenPosition("Select location for tabnote", view, pos)
     REM End Function

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
         '    ufs.Tabnot.oveRow(row)
             ufs.Obj.DeleteObject(row)
         Next
         Dim nmColumns As Integer = 0
         ufs.Tabnot.AskNmColumns(tabnote, nmColumns)
         For ii As Integer = 0 To nmColumns - 1
             Dim column As NXOpen.Tag
             ufs.Tabnot.AskNthColumn(tabnote, 0, column)
        '     ufs.Tabnot.oveColumn(column)
             ufs.Obj.DeleteObject(column)
         Next
         ' Now add our columns as needed
         Dim columns(nColumns - 1) As NXOpen.Tag
         For ii As Integer = 0 To nColumns - 1
             ufs.Tabnot.CreateColumn(1, columns(ii))
             ufs.Tabnot.AddColumn(tabnote, columns(ii), UFConstants.UF_TABNOT_APPEND)
         Next
         ' Now add our rows as needed
         Dim rows(nRows - 1) As NXOpen.Tag
         For ii As Integer = 0 To nRows - 1
             ufs.Tabnot.CreateRow(1, rows(ii))
             ufs.Tabnot.AddRow(tabnote, rows(ii), UFConstants.UF_TABNOT_APPEND)
         Next
         Return tabnote
     End Function

    REM Public Function select_a_point(ByRef pt1 As Point) As Selection.Response
        REM Dim mask(0) As Selection.MaskTriple
        REM With mask(0)
            REM .Type = UFConstants.UF_point_type
            REM .Subtype = 0
            REM .SolidBodySubtype = 0
        REM End With
        REM Dim cursor As Point3d = Nothing
        REM ufs.Ui.SetCursorView(0)
        REM ufs.Ui.LockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        REM Dim resp As Selection.Response = _
	REM ui.SelectionManager.SelectObject("Select a point", "Select a point", _
       REM Selection.SelectionScope.AnyInAssembly, _
       REM Selection.SelectionAction.ClearAndEnableSpecific, _
       REM False, False, mask, pt1, cursor)
        REM ufs.Ui.UnlockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        REM If resp = Selection.Response.ObjectSelected Or _
           REM resp = Selection.Response.ObjectSelectedByName Then
            REM Return Selection.Response.Ok
        REM ElseIf resp = Selection.Response.Back Then
            REM Return Selection.Response.Back
        REM Else
            REM Return Selection.Response.Cancel
        REM End If
    REM End Function

    REM Function SelectScreenPoint(ByRef screenpos As Point3d)
        REM Dim displayPart As Part = s.Parts.Display
        REM Dim baseView1 As View = s.Parts.Work.Views.WorkView
        REM Dim point As Double() = {0.0, 0.0, 0.0}
        REM Dim response As Integer = 0
        REM Dim viewTag As Tag = Nothing
        REM Dim viewType As UFView.Type = Nothing
        REM Dim aView As View = Nothing
        REM Dim viewSubtype As UFView.Subtype = Nothing
        REM ufs.Ui.LockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        REM ufs.Ui.SetCursorView(1)
        REM Try
            REM ufs.Ui.SpecifyScreenPosition("Select Label Pos", Nothing, IntPtr.Zero, point, _
                                                   REM viewTag, response)
        REM Finally
            REM ufs.Ui.UnlockUgAccess(NXOpen.UF.UFConstants.UF_UI_FROM_CUSTOM)
        REM End Try
        REM If (response <> NXOpen.UF.UFConstants.UF_UI_PICK_RESPONSE) Then Return Selection.Response.Cancel
        REM screenpos.X = point(0)
        REM screenpos.Y = point(1)
        REM screenpos.Z = point(2)
        REM Return Selection.Response.Ok
    REM End Function
	
	'==========================================================================================================================================
 


 
 
 
 
    Sub reportComponentChildren(ByVal comp As Component, _
        ByVal indent As Integer, ByVal tabnote As String) 
		
        'Dim row As NXOpen.Tag
        'Dim cells(n_new_columns) As NXOpen.Tag
 
        For Each child As Component In comp.GetChildren()
            '*** insert code to process component or subassembly
            If LoadComponent(child) Then
                lw.WriteLine("component name: " & child.Name)
                REM lw.WriteLine("file name: " & child.Prototype.OwningPart.Leaf)
                REM lw.WriteLine("")
				
				' Add a row for each point
				ufs.Tabnot.CreateRow(1, row)
				ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
				ufs.Tabnot.AskCellAtRowCol(row, columns(0), cell)
				ufs.Tabnot.SetCellText(cell, child.Name.ToString())

            Else
                'component could not be loaded
            End If
            '*** end of code to process component or subassembly
            reportComponentChildren(child, indent + 1, tabnote)
        Next
    End Sub
 
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
                s.Parts.Open(partName, theLoadStatus)
 
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
 
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        Return Session.LibraryUnloadOption.Immediately
    End Function
	
	
End Module 





