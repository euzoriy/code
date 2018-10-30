'---------------------------------------------------------
'NX journal created by Ievgen Zoriy 10/16/2018
'to simplify positioning associated notes and dimensions 
'---------------------------------------------------------
Option Strict Off
Imports System
Imports System.Threading
Imports System.Windows.Forms
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

    'create new form object
    Dim myForm As New fPosition

    Public Property pNewAlignmentPosition As Integer
        Get
            Return NewAlignmentPosition
        End Get
        Set(value As Integer)
            NewAlignmentPosition = value
        End Set
    End Property

    Public Property pUpdateCanceled As Boolean
        Get
            Return updateCanceled
        End Get
        Set(value As Boolean)
            updateCanceled = value
        End Set
    End Property

    Sub Main()

        Dim strFrmat As String = "0.0000"

        'lw.Open()

        If IsNothing(theSession.Parts.Work) Then
            'active part required
            lw.writeline("active part required")
            Return
        End If

        Const undoMarkName As String = "Modifiying annotation position"
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, undoMarkName)

        pUpdateCanceled = False

        'Select annotation to work with
        ufs.Ui.SetPrompt("Select Annotation to edit")
        ufs.Ui.SetStatus("Selecting Annotation to edit")
        'selectAnnotation()
        If selectNoteDimension("Select annotation to locate.", selAnnotation, True) = Selection.Response.Ok Then
            If selAnnotation IsNot Nothing Then

                Dim nullNXOpen_Point3d As NXOpen.Point3d = Nothing
                Dim originData As NXOpen.Annotations.Annotation.AssociativeOriginData
                originData = selAnnotation.GetAssociativeOrigin(nullNXOpen_Point3d)
                'Namespaces > NXOpen.Annotations > Annotation > Annotation.AssociativeOriginData
                If isDebugging Then
                    lw.writeline("old position " & originData.XOffsetFactor & ";" & originData.YOffsetFactor)
                End If


                myForm.posX.Text = Format(originData.XOffsetFactor, strFrmat)
                myForm.posY.Text = Format(originData.YOffsetFactor, strFrmat)
                'work with alignment
                NewAlignmentPosition = originData.OffsetAlignmentPosition

                Select Case NewAlignmentPosition
                    Case 9
                        myForm.rb9.Checked = True
                    Case 8
                        myForm.rb8.Checked = True
                    Case 7
                        myForm.rb7.Checked = True
                    Case 6
                        myForm.rb6.Checked = True
                    Case 5
                        myForm.rb5.Checked = True
                    Case 4
                        myForm.rb4.Checked = True
                    Case 3
                        myForm.rb3.Checked = True
                    Case 2
                        myForm.rb2.Checked = True
                    Case Else
                        myForm.rb1.Checked = True
                End Select

                If isDebugging Then
                    lw.writeline("Horizontal Alignment Position " & originData.HorizAlignmentPosition)
                    lw.writeline("Vertical Alignment Position " & originData.VertAlignmentPosition)
                    lw.writeline("OffsetAlignmentPosition " & originData.OffsetAlignmentPosition)
                    lw.writeline("Type of associativity  " & originData.OriginType.ToString) 'to be 'OffsetFromText'
                End If

                ufs.Ui.SetPrompt("Select Origin Annotation")
                ufs.Ui.SetStatus("Selecting Origin Annotation")
                'selectOrigin()
                selectNoteDimension("Select origin", origAnnotation)

                If originData.OffsetAnnotation Is Nothing And origAnnotation Is Nothing Then
                    lw.writeline("Origin annotation is required")
                    DeselectAnnotations()
                    Return
                Else
                    'lw.writeline("Annotation to align to  " & originData.OffsetAnnotation.Tag)
                    If originData.OffsetAnnotation IsNot Nothing Then
                        origAnnotation = originData.OffsetAnnotation
                    End If

                    'reset position if linking to other annotation
                    If Not origAnnotation.Equals(originData.OffsetAnnotation) Then
                        myForm.posX.Text = Format(0, strFrmat)
                        myForm.posY.Text = Format(0, strFrmat)
                    End If

                    'avoid circular reference
                    If haveCircularReference(selAnnotation, origAnnotation) Then
                        MessageBox.Show("Selection will create circular reference.", "Circular reference")
                        DeselectAnnotations()
                        Return
                    End If
                    'avoid selection of same object (double-check)
                    If origAnnotation.Equals(selAnnotation) Then
                        lw.writeline("Select two different objects")
                        DeselectAnnotations()
                        Return
                    End If

                    'display form
                    myForm.ShowDialog()
                End If

                'draftingSymbolBuilder0.Destroy()

            End If

        End If

        lw.Close()

        If pUpdateCanceled Then
            'Undo()
            theSession.UndoToMark(markId1, undoMarkName)
            theSession.DeleteUndoMark(markId1, Nothing)
        Else
            theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
            Dim nErrs1 As Integer = Nothing
            nErrs1 = theSession.UpdateManager.DoUpdate(markId1)
        End If

        DeselectAnnotations()

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

    Public Sub selectOrigin()
        selectNoteDimension("Select origin", origAnnotation)
    End Sub

    Sub moveNote(delta_x As Double, delta_y As Double, Optional ByVal Allignment As Integer = 1)

        'Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        'markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
        ' ----------------------------------------------
        '   Dialog Begin Origin Tool
        ' ----------------------------------------------
        Dim assocOrigin1 As NXOpen.Annotations.Annotation.AssociativeOriginData = Nothing
        assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.OffsetFromText
        Dim nullNXOpen_View As NXOpen.View = Nothing
        assocOrigin1.View = nullNXOpen_View
        assocOrigin1.ViewOfGeometry = nullNXOpen_View
        Dim nullNXOpen_Point As NXOpen.Point = Nothing
        assocOrigin1.PointOnGeometry = nullNXOpen_Point
        Dim nullNXOpen_Annotations_Annotation As NXOpen.Annotations.Annotation = Nothing
        assocOrigin1.VertAnnotation = nullNXOpen_Annotations_Annotation
        assocOrigin1.VertAlignmentPosition = Allignment 'NXOpen.Annotations.AlignmentPosition.TopLeft
        assocOrigin1.HorizAnnotation = nullNXOpen_Annotations_Annotation
        assocOrigin1.HorizAlignmentPosition = Allignment 'NXOpen.Annotations.AlignmentPosition.TopLeft
        assocOrigin1.AlignedAnnotation = nullNXOpen_Annotations_Annotation
        assocOrigin1.DimensionLine = 0
        assocOrigin1.AssociatedView = nullNXOpen_View
        assocOrigin1.AssociatedPoint = nullNXOpen_Point
        'specifiyng origin annotetion
        assocOrigin1.OffsetAnnotation = origAnnotation
        'Alignment type specification
        assocOrigin1.OffsetAlignmentPosition = Allignment ' NXOpen.Annotations.AlignmentPosition.TopLeft
        'lw.writeline("old position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)
        assocOrigin1.XOffsetFactor = assocOrigin1.XOffsetFactor + delta_x ' 5.0
        assocOrigin1.YOffsetFactor = assocOrigin1.YOffsetFactor + delta_y '10.0
        'debuging
        If isDebugging Then
            lw.writeline("new position position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)
        End If

        assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
        ' ----------------------------------------------
        Dim nullNXOpen_Point3d As NXOpen.Point3d = Nothing
        selAnnotation.SetAssociativeOrigin(assocOrigin1, nullNXOpen_Point3d)
        RedrawAnnotation(selAnnotation)
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

        'workPart.Views.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly)
        ' ----------------------------------------------
        '   Menu: Edit->Undo List->1 Hide
        ' ----------------------------------------------
        'Dim marksRecycled1 As Boolean = Nothing
        'Dim undoUnavailable1 As Boolean = Nothing
        'theSession.UndoLastNVisibleMarks(1, marksRecycled1, undoUnavailable1)
        'Undo()

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
            .Subtype = 0
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
Public Class fPosition
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim myRadioButton As RadioButton = Nothing
        'For Each myRadioButton In Me.ChildControls(Of RadioButton).Where(Function(rb As RadioButton) rb.Name.Contains("rb"))
        '    AddHandler RadioButton.CheckedChanged, AddressOf radioButtonCheckedChanged
        'Next
    End Sub

    Private Sub btRight_Click(sender As Object, e As EventArgs) Handles btRight.Click
        IncVal(posX.Text, numPositionIncrement.Value)
        UpdatePosition()
    End Sub

    Private Sub btLeft_Click(sender As Object, e As EventArgs) Handles btLeft.Click
        IncVal(posX.Text, -numPositionIncrement.Value)
        UpdatePosition()
    End Sub

    Private Sub btUp_Click(sender As Object, e As EventArgs) Handles btUp.Click
        IncVal(posY.Text, numPositionIncrement.Value)
        UpdatePosition()
    End Sub

    Private Sub btDown_Click(sender As Object, e As EventArgs) Handles btDown.Click
        IncVal(posY.Text, -numPositionIncrement.Value)
        UpdatePosition()
    End Sub

    Private Sub UpdatePosition()
        moveNote(CDbl(posX.Text), CDbl(posY.Text), pNewAlignmentPosition)
    End Sub

    Private Sub IncVal(ByRef strVal As String, ByRef strI As Double)
        Dim strFrmat As String = "0.0000"
        If IsNumeric(strVal) Then
            strVal = Format(CDbl(strVal) + strI, strFrmat)
        Else
            strVal = Format(0, strFrmat)
        End If
    End Sub

    Private _canceled As Boolean = False
    Public ReadOnly Property Canceled() As Boolean
        Get
            Return _canceled
        End Get
    End Property

    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        UpdatePosition()
        Me.Close()
    End Sub

    Private Sub ptCancel_Click(sender As Object, e As EventArgs) Handles ptCancel.Click
        pUpdateCanceled = True
        _canceled = True
        DeselectAnnotations()
        Me.Close()
    End Sub




    Private Sub radioButtonCheckedChanged(sender As Object, e As EventArgs)
        Dim myRadioButton As RadioButton = DirectCast(sender, RadioButton)
        Dim c As Boolean = myRadioButton.Checked
        pNewAlignmentPosition = myRadioButton.Tag
    End Sub

    Private Sub rb1_CheckedChanged(sender As Object, e As EventArgs) Handles rb1.CheckedChanged
        pNewAlignmentPosition = 1
        'UpdatePosition()
    End Sub

    Private Sub rb2_CheckedChanged(sender As Object, e As EventArgs) Handles rb2.CheckedChanged
        pNewAlignmentPosition = 2
        'UpdatePosition()
    End Sub

    Private Sub rb3_CheckedChanged(sender As Object, e As EventArgs) Handles rb3.CheckedChanged
        pNewAlignmentPosition = 3
        'UpdatePosition()
    End Sub

    Private Sub rb4_CheckedChanged(sender As Object, e As EventArgs) Handles rb4.CheckedChanged
        pNewAlignmentPosition = 4
        'UpdatePosition()
    End Sub

    Private Sub rb5_CheckedChanged(sender As Object, e As EventArgs) Handles rb5.CheckedChanged
        pNewAlignmentPosition = 5
        'UpdatePosition()
    End Sub

    Private Sub rb6_CheckedChanged(sender As Object, e As EventArgs) Handles rb6.CheckedChanged
        pNewAlignmentPosition = 6
        'UpdatePosition()
    End Sub

    Private Sub rb7_CheckedChanged(sender As Object, e As EventArgs) Handles rb7.CheckedChanged
        pNewAlignmentPosition = 7
        'UpdatePosition()
    End Sub

    Private Sub rb8_CheckedChanged(sender As Object, e As EventArgs) Handles rb8.CheckedChanged
        pNewAlignmentPosition = 8
        'UpdatePosition()
    End Sub

    Private Sub rb9_CheckedChanged(sender As Object, e As EventArgs) Handles rb9.CheckedChanged
        pNewAlignmentPosition = 9
        'UpdatePosition()
    End Sub

    Private Sub gbAlignmentPosition_Leave(sender As Object, e As EventArgs) Handles gbAlignmentPosition.Leave
        UpdatePosition()
    End Sub
    '''not working - crashes NX
    ''Private Sub btAnnotation_Click(sender As Object, e As EventArgs) Handles btAnnotation.Click

    ''    'Dim myThread As New Thread(AddressOf selectAnnotation)
    ''    Dim myThread As New Thread(Sub()
    ''                                   selectAnnotation()
    ''                               End Sub)
    ''    myThread.Start()
    ''    myThread.Join()

    ''End Sub


    'Private Sub btOrigin_Click(sender As Object, e As EventArgs) Handles btOrigin.Click
    '    'selectNoteDimension("Select origin annotation.", origAnnotation)
    '    selectOrigin()
    'End Sub

    Private Sub fPosition_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        If (e.Button = MouseButtons.Middle) Then
            UpdatePosition()
            Me.Close()
        End If
    End Sub

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fPosition
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.posX = New System.Windows.Forms.TextBox()
        Me.btUp = New System.Windows.Forms.Button()
        Me.btDown = New System.Windows.Forms.Button()
        Me.btLeft = New System.Windows.Forms.Button()
        Me.btRight = New System.Windows.Forms.Button()
        Me.numPositionIncrement = New System.Windows.Forms.NumericUpDown()
        Me.posY = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.gbAlignmentPosition = New System.Windows.Forms.GroupBox()
        Me.rb1 = New System.Windows.Forms.RadioButton()
        Me.rb2 = New System.Windows.Forms.RadioButton()
        Me.rb3 = New System.Windows.Forms.RadioButton()
        Me.rb4 = New System.Windows.Forms.RadioButton()
        Me.rb5 = New System.Windows.Forms.RadioButton()
        Me.rb6 = New System.Windows.Forms.RadioButton()
        Me.rb7 = New System.Windows.Forms.RadioButton()
        Me.rb8 = New System.Windows.Forms.RadioButton()
        Me.rb9 = New System.Windows.Forms.RadioButton()
        Me.createdBy = New System.Windows.Forms.Label()
        Me.btOk = New System.Windows.Forms.Button()
        Me.ptCancel = New System.Windows.Forms.Button()
        CType(Me.numPositionIncrement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAlignmentPosition.SuspendLayout()
        Me.SuspendLayout()
        '
        'posX
        '
        Me.posX.Location = New System.Drawing.Point(32, 132)
        Me.posX.Name = "posX"
        Me.posX.Size = New System.Drawing.Size(134, 20)
        Me.posX.TabIndex = 3
        Me.posX.Text = "0"
        Me.posX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btUp
        '
        Me.btUp.Location = New System.Drawing.Point(12, 184)
        Me.btUp.Name = "btUp"
        Me.btUp.Size = New System.Drawing.Size(154, 23)
        Me.btUp.TabIndex = 5
        Me.btUp.Text = "UP"
        Me.btUp.UseVisualStyleBackColor = True
        '
        'btDown
        '
        Me.btDown.Location = New System.Drawing.Point(12, 259)
        Me.btDown.Name = "btDown"
        Me.btDown.Size = New System.Drawing.Size(154, 23)
        Me.btDown.TabIndex = 8
        Me.btDown.Text = "DOWN"
        Me.btDown.UseVisualStyleBackColor = True
        '
        'btLeft
        '
        Me.btLeft.Location = New System.Drawing.Point(12, 213)
        Me.btLeft.Name = "btLeft"
        Me.btLeft.Size = New System.Drawing.Size(75, 40)
        Me.btLeft.TabIndex = 6
        Me.btLeft.Text = "LEFT"
        Me.btLeft.UseVisualStyleBackColor = True
        '
        'btRight
        '
        Me.btRight.Location = New System.Drawing.Point(93, 213)
        Me.btRight.Name = "btRight"
        Me.btRight.Size = New System.Drawing.Size(73, 40)
        Me.btRight.TabIndex = 7
        Me.btRight.Text = "RIGHT"
        Me.btRight.UseVisualStyleBackColor = True
        '
        'numPositionIncrement
        '
        Me.numPositionIncrement.DecimalPlaces = 2
        Me.numPositionIncrement.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.numPositionIncrement.Location = New System.Drawing.Point(93, 103)
        Me.numPositionIncrement.Name = "numPositionIncrement"
        Me.numPositionIncrement.Size = New System.Drawing.Size(73, 20)
        Me.numPositionIncrement.TabIndex = 2
        Me.numPositionIncrement.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'posY
        '
        Me.posY.Location = New System.Drawing.Point(32, 158)
        Me.posY.Name = "posY"
        Me.posY.Size = New System.Drawing.Size(134, 20)
        Me.posY.TabIndex = 4
        Me.posY.Text = "0"
        Me.posY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "X:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Y:"
        '
        'gbAlignmentPosition
        '
        Me.gbAlignmentPosition.Controls.Add(Me.rb9)
        Me.gbAlignmentPosition.Controls.Add(Me.rb6)
        Me.gbAlignmentPosition.Controls.Add(Me.rb3)
        Me.gbAlignmentPosition.Controls.Add(Me.rb8)
        Me.gbAlignmentPosition.Controls.Add(Me.rb5)
        Me.gbAlignmentPosition.Controls.Add(Me.rb2)
        Me.gbAlignmentPosition.Controls.Add(Me.rb7)
        Me.gbAlignmentPosition.Controls.Add(Me.rb4)
        Me.gbAlignmentPosition.Controls.Add(Me.rb1)
        Me.gbAlignmentPosition.Location = New System.Drawing.Point(12, 12)
        Me.gbAlignmentPosition.Name = "gbAlignmentPosition"
        Me.gbAlignmentPosition.Size = New System.Drawing.Size(154, 85)
        Me.gbAlignmentPosition.TabIndex = 1
        Me.gbAlignmentPosition.TabStop = False
        Me.gbAlignmentPosition.Text = "Alignment Position"
        '
        'rb1
        '
        Me.rb1.AutoSize = True
        'Me.rb1.Checked = True
        Me.rb1.Location = New System.Drawing.Point(6, 19)
        Me.rb1.Name = "rb1"
        Me.rb1.Size = New System.Drawing.Size(14, 13)
        Me.rb1.TabIndex = 0
        Me.rb1.TabStop = True
        Me.rb1.Tag = "1"
        Me.rb1.UseVisualStyleBackColor = True
        '
        'rb2
        '
        Me.rb2.AutoSize = True
        Me.rb2.Location = New System.Drawing.Point(71, 19)
        Me.rb2.Name = "rb2"
        Me.rb2.Size = New System.Drawing.Size(14, 13)
        Me.rb2.TabIndex = 0
        Me.rb2.Tag = "2"
        Me.rb2.UseVisualStyleBackColor = True
        '
        'rb3
        '
        Me.rb3.AutoSize = True
        Me.rb3.Location = New System.Drawing.Point(134, 19)
        Me.rb3.Name = "rb3"
        Me.rb3.Size = New System.Drawing.Size(14, 13)
        Me.rb3.TabIndex = 0
        Me.rb3.Tag = "3"
        Me.rb3.UseVisualStyleBackColor = True
        '
        'rb4
        '
        Me.rb4.AutoSize = True
        Me.rb4.Location = New System.Drawing.Point(6, 38)
        Me.rb4.Name = "rb4"
        Me.rb4.Size = New System.Drawing.Size(14, 13)
        Me.rb4.TabIndex = 0
        Me.rb4.Tag = "4"
        Me.rb4.UseVisualStyleBackColor = True
        '
        'rb5
        '
        Me.rb5.AutoSize = True
        Me.rb5.Location = New System.Drawing.Point(71, 38)
        Me.rb5.Name = "rb5"
        Me.rb5.Size = New System.Drawing.Size(14, 13)
        Me.rb5.TabIndex = 0
        Me.rb5.Tag = "5"
        Me.rb5.UseVisualStyleBackColor = True
        '
        'rb6
        '
        Me.rb6.AutoSize = True
        Me.rb6.Location = New System.Drawing.Point(134, 38)
        Me.rb6.Name = "rb6"
        Me.rb6.Size = New System.Drawing.Size(14, 13)
        Me.rb6.TabIndex = 0
        Me.rb6.Tag = "6"
        Me.rb6.UseVisualStyleBackColor = True
        '
        'rb7
        '
        Me.rb7.AutoSize = True
        Me.rb7.Location = New System.Drawing.Point(6, 57)
        Me.rb7.Name = "rb7"
        Me.rb7.Size = New System.Drawing.Size(14, 13)
        Me.rb7.TabIndex = 0
        Me.rb7.Tag = "7"
        Me.rb7.UseVisualStyleBackColor = True
        '
        'rb8
        '
        Me.rb8.AutoSize = True
        Me.rb8.Location = New System.Drawing.Point(71, 57)
        Me.rb8.Name = "rb8"
        Me.rb8.Size = New System.Drawing.Size(14, 13)
        Me.rb8.TabIndex = 0
        Me.rb8.Tag = "8"
        Me.rb8.UseVisualStyleBackColor = True
        '
        'rb9
        '
        Me.rb9.AutoSize = True
        Me.rb9.Location = New System.Drawing.Point(134, 57)
        Me.rb9.Name = "rb9"
        Me.rb9.Size = New System.Drawing.Size(14, 13)
        Me.rb9.TabIndex = 0
        Me.rb9.Tag = "9"
        Me.rb9.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.createdBy.AutoSize = True
        Me.createdBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 3.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.createdBy.Location = New System.Drawing.Point(125, 5)
        Me.createdBy.Name = "createdBy"
        Me.createdBy.Size = New System.Drawing.Size(135, 5)
        Me.createdBy.TabIndex = 11
        Me.createdBy.Text = "created by I Zorii"
        '
        'btOk
        '
        Me.btOk.Location = New System.Drawing.Point(12, 309)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(93, 37)
        Me.btOk.TabIndex = 9
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'ptCancel
        '
        Me.ptCancel.Location = New System.Drawing.Point(111, 309)
        Me.ptCancel.Name = "ptCancel"
        Me.ptCancel.Size = New System.Drawing.Size(55, 37)
        Me.ptCancel.TabIndex = 10
        Me.ptCancel.Text = "Cancel"
        Me.ptCancel.UseVisualStyleBackColor = True
        '
        'fPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(178, 358)
        Me.Controls.Add(Me.ptCancel)
        Me.Controls.Add(Me.btOk)
        Me.Controls.Add(Me.createdBy)
        Me.Controls.Add(Me.gbAlignmentPosition)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numPositionIncrement)
        Me.Controls.Add(Me.btRight)
        Me.Controls.Add(Me.btLeft)
        Me.Controls.Add(Me.btDown)
        Me.Controls.Add(Me.btUp)
        Me.Controls.Add(Me.posY)
        Me.Controls.Add(Me.posX)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        'Me.Location = New System.Drawing.Point(500, 0)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fPosition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Note positioning"
        CType(Me.numPositionIncrement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAlignmentPosition.ResumeLayout(False)
        Me.gbAlignmentPosition.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents posX As TextBox
    Friend WithEvents btUp As Button
    Friend WithEvents btDown As Button
    Friend WithEvents btLeft As Button
    Friend WithEvents btRight As Button
    Friend WithEvents numPositionIncrement As NumericUpDown
    Friend WithEvents posY As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents gbAlignmentPosition As GroupBox
    Friend WithEvents rb9 As RadioButton
    Friend WithEvents rb6 As RadioButton
    Friend WithEvents rb3 As RadioButton
    Friend WithEvents rb8 As RadioButton
    Friend WithEvents rb5 As RadioButton
    Friend WithEvents rb2 As RadioButton
    Friend WithEvents rb7 As RadioButton
    Friend WithEvents rb4 As RadioButton
    Friend WithEvents rb1 As RadioButton
    Friend WithEvents createdBy As Label
    Friend WithEvents btOk As Button
    Friend WithEvents ptCancel As Button
End Class





