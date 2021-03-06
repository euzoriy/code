REM ' NX 12.0.2.9
REM ' Journal created by Ievgenz on Wed Jun 10 11:05:42 2020 Eastern Daylight Time
REM '
REM Imports System
REM Imports NXOpen
REM Module NXJournal
REM Sub Main (ByVal args() As String) 
REM Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
REM Dim workPart As NXOpen.Part = theSession.Parts.Work
REM Dim displayPart As NXOpen.Part = theSession.Parts.Display

REM Dim note1 As NXOpen.Annotations.Note = CType(workPart.FindObject("HANDLE R-1726921"), NXOpen.Annotations.Note)
REM Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
REM draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(note1)
REM ' ----------------------------------------------
REM '   Dialog Begin Note
REM ' ----------------------------------------------
REM Dim text1(9) As String
REM text1(0) = "NOTES:"
REM text1(1) = ""
REM text1(2) = " 1. ASSOCIATED COMPUTER DATA FILES ARE: "
REM text1(3) = "           NX DWG        <W@$SH_PART_NAME>.PRT"
REM text1(4) = "           NX MODEL      <W@$SH_MASTER_PART_NAME>.PRT"
REM text1(5) = " 2. ALL DIMENSIONS ARE IN INCHES UOS"
REM text1(6) = " 3. CORNERS MUST HAVE FILLETS R .005 - .020 UOS"
REM text1(7) = " 4. BREAK SHARP EDGES .003 - .015 UOS"
REM text1(8) = " 5. DWG INTERPRETATIONS PER ASME Y14.5-2009"
REM text1(9) = " 6. ALL MACHINED SURFACES ARE 125 Ra UOS"
REM draftingNoteBuilder1.Text.TextBlock.SetText(text1)
REM Dim nXObject1 As NXOpen.NXObject = Nothing
REM nXObject1 = draftingNoteBuilder1.Commit()
REM draftingNoteBuilder1.Destroy()
REM ' ----------------------------------------------
REM '   Menu: Tools->Journal->Stop Recording
REM ' ----------------------------------------------
REM End Sub
REM End Module

'---------------------------------------------------------
'NX journal created by Ievgen Zoriy 10/16/2018
'to create numbered list
'---------------------------------------------------------
Option Strict Off
Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpenUI

Module Module1
    Dim isDebugging As Boolean = False


    Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Dim workPart As NXOpen.Part = theSession.Parts.Work
    Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
    Dim aType As Integer
    Dim subtype As Integer
    Dim theUI As UI = ui.GetUI
    Dim selobj As NXObject
    'Dim ui As UI

    Dim lw As ListingWindow = theSession.ListingWindow

    Dim selAnnotation As NXOpen.Annotations.Annotation 'NXObject
    Dim origAnnotation As NXOpen.Annotations.Annotation 'NXObject
    Dim NewAlignmentPosition As Integer
    Dim updateCanceled As Boolean

    Sub Main()

        lw.Open()

        If IsNothing(theSession.Parts.Work) Then
            'active part required
            lw.writeline("active part required")
            Return
        End If

        Const undoMarkName As String = "Creating numbered list from annotation"
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, undoMarkName)

        'pUpdateCanceled = False

        'Select annotation to work with
        ufs.Ui.SetPrompt("Select Annotation to edit")
        ufs.Ui.SetStatus("Selecting Annotation to edit")
        'selectAnnotation()

        Dim counter As Integer
        counter = 1

        While selectNoteDimension("Select annotation to edit.", selAnnotation, True) = Selection.Response.Ok
            If selAnnotation IsNot Nothing Then

                '++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim note1 As NXOpen.Annotations.Note = CType(selAnnotation, NXOpen.Annotations.Note)
                Dim draftingNoteBuilder1 As NXOpen.Annotations.DraftingNoteBuilder = Nothing
                draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(note1)



                Dim aText() As String
                aText = note1.GetText()
                For i As Integer = 0 To aText.Length - 1
                    'Check if not empty line
                    lw.writeline(aText(i))
                    If aText(i).Length > 3 Then
                        'check if not a tab (4spaces)
                        If aText(i).Length > 5 And Left(aText(i), 5) <> "     " Then
                            'Determine the length to trim
                            Dim trimLength As Integer
                            trimLength = 0

                            For j As Integer = 0 To aText(i).Length - 1
                                If Not Regex.IsMatch(aText(i)(j), "^[0-9. ]+$") Then
                                    trimLength = j
                                    Exit For
                                End If
                            Next

                            If trimLength > 0 Then
                                'Formating and numbering the lines
                                aText(i) = String.Format(("{0,2}. "), counter) & aText(i).Remove(0, trimLength)
                                counter = counter + 1
                            End If

                        End If

                        'If Regex.IsMatch(left(aText(i), 3), "^[0-9. ]+$") And aText(i)(4) <> " " Then

                        'End If
                    End If
                Next

                'For Each lineText As String In aText 'note1.GetText()
                '    'lw.writeline(lineText)
                '    If Len(lineText) > 5 Then

                '        If Regex.IsMatch(left(lineText, 4), "^[0-9. ]+$") And lineText(4) <> " " Then
                '            'lw.writeline(String.Format(counter, ("{3,0}. ")) & lineText)
                '            lw.writeline(String.Format(counter, ("{2,3}")) & ". " & lineText.Remove(0, 4))
                '            lineText = String.Format(counter, ("{2,3}")) & ". " & lineText.Remove(0, 4)
                '            counter = counter + 1
                '        End If
                '    End If
                'Next

                draftingNoteBuilder1.Text.TextBlock.SetText(aText)

                Dim nXObject1 As NXOpen.NXObject = Nothing
                nXObject1 = draftingNoteBuilder1.Commit()
                draftingNoteBuilder1.Destroy()
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++

            End If

            DeselectAnnotations()

        End While

        lw.Close()

        '       If pUpdateCanceled Then
        '           'Undo()
        '           theSession.UndoToMark(markId1, undoMarkName)
        '           theSession.DeleteUndoMark(markId1, Nothing)
        '       Else
        '           theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
        '           Dim nErrs1 As Integer = Nothing
        '           nErrs1 = theSession.UpdateManager.DoUpdate(markId1)
        '       End If

        'DeselectAnnotations()

    End Sub

    Public Function GetUnloadOption(ByVal dummy As String) As Integer

        'Unloads the image when the NX session terminates
        'GetUnloadOption = NXOpen.Session.LibraryUnloadOption.AtTermination

        '----Other unload options-------
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately

        'Unloads the image explicitly, via an unload dialog
        'GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Explicitly
        '-------------------------------

    End Function

    Private Function haveCircularReference(annotationToCheck As NXOpen.Annotations.Annotation, origin As NXOpen.Annotations.Annotation) As Boolean
        haveCircularReference = False

        Dim nullNXOpen_Point3d As NXOpen.Point3d = Nothing
        Dim originData As NXOpen.Annotations.Annotation.AssociativeOriginData
        Dim originOrigin As NXOpen.Annotations.Annotation
        originData = origin.GetAssociativeOrigin(nullNXOpen_Point3d)

        If originData.OffsetAnnotation IsNot Nothing Then
            originOrigin = originData.OffsetAnnotation
            If annotationToCheck.Equals(originOrigin) Then
                haveCircularReference = True
            Else
                haveCircularReference = haveCircularReference(origin, originOrigin)
            End If
        End If

    End Function

    Public Sub selectAnnotation()
        selectNoteDimension("Select annotation to locate", selAnnotation)
    End Sub

    Sub RedrawAnnotation(annotation As NXOpen.DisplayableObject)

        annotation.RedisplayObject 'not working rightaway
        'workPart.Views.WorkView.Regenerate() 'blinking all drawing (annoyng)

        'a bit crazy way, but it's working
        Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Refresh")
        Dim objects1(0) As NXOpen.DisplayableObject
        objects1(0) = annotation
        Dim nErrs1 As Integer = Nothing
        theSession.DisplayManager.BlankObjects(objects1)
        'nErrs1 = theSession.UpdateManager.DoUpdate(markId1)
        theSession.DisplayManager.UnBlankObjects(objects1)

        nErrs1 = theSession.UpdateManager.DoUpdate(markId1)

        theSession.DeleteUndoMark(markId1, Nothing)


    End Sub

    Public Sub Undo()
        Dim marksRecycled1 As Boolean = Nothing
        Dim undoUnavailable1 As Boolean = Nothing
        theSession.UndoLastNVisibleMarks(1, marksRecycled1, undoUnavailable1)
    End Sub

    Public Sub DeselectAnnotations()
        selAnnotation.Unhighlight()
    End Sub

    '**************************************************
    Function selectNoteDimension(ByVal prompt As String, ByRef obj As NXOpen.Annotations.Annotation, Optional ByVal KeepSel As Boolean = False)
        'Annotation class covers dimensions and notes
        'Annotation -> Dimension
        'Annotation -> DraftingAid -> SimpleDraftingAid -> NoteBase -> BaseNote -> Note
        'Dim ui As UI = GetUI()
        Dim mask(1) As Selection.MaskTriple

        With mask(0)
            .Type = UFConstants.UF_drafting_entity_type
            .Subtype = UFConstants.UF_draft_note_subtype
            .SolidBodySubtype = 0
        End With

        Dim cursor As NXOpen.Point3d = Nothing

        Dim resp As Selection.Response =
        theUI.SelectionManager.SelectObject(prompt, prompt,
            Selection.SelectionScope.WorkPart,
            Selection.SelectionAction.ClearAndEnableSpecific,
            False, KeepSel, mask, obj, cursor)

        If resp = Selection.Response.ObjectSelected Or
        resp = Selection.Response.ObjectSelectedByName Then
            Return Selection.Response.Ok
        Else
            Return Selection.Response.Cancel
        End If
    End Function    'selectNoteDimension
    '**************************************************

End Module

' -----------------------------------------------------------------











