REM modified by Ievgen Zorii for Leesta. 2017

REM NX Journal that will allow you to add numbers to your existing dimensions. 
REM Journal Capabilities: After your journal has started, you will be required to specify a starting number for your labels. 
REM After entering your starting number, choose the dimensions or notes. 
REM The ID symbol text will start with the number you specified and increment by 1 each pick. 
REM The journal will then add an associative ID symbol to the dimension or note chosen.
REM Journal Requirements: This journal assumes you have the drafting application active.
REM http://www.nxjournaling.com/content/creating-qc-inspection-numbers-using-nx-journal



Option Strict Off
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpen.Annotations


Module NXJournal
Sub Main

'*************************************************************************
'change the following offset distances to get something that works for you
'offsets from dimension text
Const XOffsetDim as Double = 0
Const YOffsetDim as Double = 3
'offsets from notes
Const XOffsetNote as Double = -3.5
Const YOffsetNote as Double = 2
'offsets from surface finish
Const XOffsetSF as Double = 4
Const YOffsetSF as Double = 1
'offsets from multiple baloons
'Const XOffsetMB as Double array [-3,-7]
'Const YOffsetMB as Double = -2.5
Const dY as Double = -4.5


'*************************************************************************

Dim theSession As Session = Session.GetSession()
Dim workPart As Part = theSession.Parts.Work
Dim displayPart As Part = theSession.Parts.Display
Dim lw As ListingWindow = theSession.ListingWindow
Dim noteDim As Annotation
Dim noteDimOrigin as Point3D = Nothing
'Dim letterPref as LetteringPreferences = Nothing
Dim symbolPref as SymbolPreferences = Nothing
Dim noteNumber as Integer
Dim input as String
'Dim theAnnotationManager as NXOpen.Annotations.AnnotationManager = workPart.Annotations
Dim theAnnotationManager as AnnotationManager = workPart.Annotations
Dim prevNoteDim As Annotation = Nothing
'Multiple bubbles
REM Dim multipleB as boolean = false
REM Dim mPrompt as String = "Enter starting QC number " & vbNewLine & _ 
	REM "or 'M' - for multiple bubbles: "
Dim mBubbleCount as integer = 0
Dim prevNoteDimTag as String = ""

Dim markId1 As Session.UndoMarkId
markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Start")
 
Do
	
    input = InputBox("Enter starting QC number:" , _
	"Label dimensions with QC inspection numbers", "1")
		If String.IsNullOrEmpty(input) Then
   ' Cancelled, or empty
			exit sub
		REM else if UCase(CStr(input))="M"
			REM multipleB = true
			REM mPrompt = "Multiple bubbles selected." & vbNewLine & vbNewLine & "Enter starting QC number:"
		end if 
Loop Until (isNumeric(input))
noteNumber = input
 
'lw.Open()
 
While selectNoteDimension("Select Dimension or Note", noteDim) = Selection.Response.Ok
    noteDimOrigin = noteDim.AnnotationOrigin
	
    'lw.WriteLine("origin: " & noteDimOrigin.X & ", " & noteDimOrigin.Y)
    'letterPref = noteDim.GetLetteringPreferences()
    symbolPref = theAnnotationManager.Preferences.GetSymbolPreferences()
    'lw.WriteLine("Alignment Position: " & letterPref.AlignmentPosition.ToString())
    'lw.WriteLine("ID Symbol size: " & symbolPref.IDSymbolSize)
    'lw.WriteLine("Annotation type: " & noteDim.GetType().ToString())
 
    Dim nullAnnotations_IdSymbol As Annotations.IdSymbol = Nothing
    Dim idSymbolBuilder1 As Annotations.IdSymbolBuilder
 
    idSymbolBuilder1 = workPart.Annotations.IdSymbols.CreateIdSymbolBuilder(nullAnnotations_IdSymbol)
    idSymbolBuilder1.Origin.Plane.PlaneMethod = Annotations.PlaneBuilder.PlaneMethodType.XyPlane
	
    idSymbolBuilder1.Type = Annotations.IdSymbolBuilder.SymbolTypes.Circle
    'idSymbolBuilder1.Type = Annotations.IdSymbolBuilder.SymbolTypes.TriangleUp
    idSymbolBuilder1.UpperText = noteNumber
    'use the symbol size set in the part
    idSymbolBuilder1.Size = symbolPref.IDSymbolSize
 
    Dim assocOrigin1 As Annotations.Annotation.AssociativeOriginData
    With assocOrigin1
        .OriginType = Annotations.AssociativeOriginType.OffsetFromText
        .OffsetAnnotation = noteDim
        .OffsetAlignmentPosition = Annotations.AlignmentPosition.TopLeft
		'msgBox(noteDim.GetType().ToString())
        if noteDim.GetType().ToString() = "NXOpen.Annotations.Note" Then
            .XOffsetFactor = XOffsetNote
            .YOffsetFactor = YOffsetNote
		else if noteDim.GetType().ToString() = "NXOpen.Annotations.DraftingSurfaceFinish" Then
			.XOffsetFactor = XOffsetSF
            .YOffsetFactor = YOffsetSF
		else if noteDim.Tag.toString() = prevNoteDimTag
            .XOffsetFactor = XOffsetNote
			 mBubbleCount = mBubbleCount + 1			
            .YOffsetFactor = YOffsetNote + dY * mBubbleCount
        Else
            .XOffsetFactor = XOffsetDim
            .YOffsetFactor = YOffsetDim
        End if
    End With
    idSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)
 
    Dim QC_IDSymbol As IDSymbol
    QC_IDSymbol = idSymbolBuilder1.Commit()
    'change IDSymbol layer to match that of the dimension or note it is attached to
    QC_IDSymbol.Layer = noteDim.Layer
    idSymbolBuilder1.Destroy()
 
    noteNumber += 1

	
	if noteDim.Tag.toString() <> prevNoteDimTag
		mBubbleCount = 0
	end if
	
	prevNoteDimTag = noteDim.Tag.toString()
	
End While
'lw.Close
theSession.SetUndoMarkName(markId1, "Label Dimensions")
theSession.SetUndoMarkVisibility(markId1, Nothing, Session.MarkVisibility.Visible)        

End Sub 'Main
 
'**************************************************
    Function selectNoteDimension(ByVal prompt As String, ByRef obj As Annotation)
    'Annotation class covers dimensions and notes
    'Annotation -> Dimension
    'Annotation -> DraftingAid -> SimpleDraftingAid -> NoteBase -> BaseNote -> Note
        Dim ui As UI = GetUI()
        Dim mask(5) As Selection.MaskTriple
        With mask(0)
            .Type = UFConstants.UF_dimension_type			
            .Subtype = 0
            .SolidBodySubtype = 0
        End With
        With mask(1)
            .Type = UFConstants.UF_drafting_entity_type
            .Subtype = UFConstants.UF_draft_note_subtype
            .SolidBodySubtype = 0
        End With
        With mask(2)
            .Type = UFConstants.UF_drafting_entity_type
            .Subtype = UFConstants.UF_draft_label_subtype
            .SolidBodySubtype = 0
        End With
        With mask(3)
            .Type = UFConstants.UF_drafting_entity_type
            .Subtype = UFConstants.UF_draft_user_defined_subtype
            .SolidBodySubtype = 0
        End With
		'surface finish
        With mask(4)
            .Type = 158
            .Subtype = 2
            .SolidBodySubtype = 0
        End With
		'gd&t
        With mask(5)
            .Type = 25
            .Subtype = 4
            .SolidBodySubtype = 0
        End With

        Dim cursor As Point3d = Nothing
 
        Dim resp As Selection.Response = _
        ui.SelectionManager.SelectObject(prompt, prompt, _
            Selection.SelectionScope.AnyInAssembly, _
            Selection.SelectionAction.ClearAndEnableSpecific, _
            False, False, mask, obj, cursor)
 
        If resp = Selection.Response.ObjectSelected Or _
        resp = Selection.Response.ObjectSelectedByName Then
            Return Selection.Response.Ok
        Else
            Return Selection.Response.Cancel
        End If
    End Function    'selectNoteDimension
'**************************************************
 
End Module