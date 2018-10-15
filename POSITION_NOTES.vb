Option Strict Off
Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpenUI
'Imports NXOpen.Annotations

Module Module1

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

    'create new form object
    Dim myForm As New fPosition

    Sub Main()



        'lw.Open()
        'lw.writeline("journal opened")

        If IsNothing(theSession.Parts.Work) Then
            'active part required
            lw.writeline("active part required")
            Return
        End If

        Const undoMarkName As String = "Modifiying annotation position"
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, undoMarkName)

        'Select annotation to work with
        ufs.Ui.SetPrompt("Select Annotation to edit")
        ufs.Ui.SetStatus("Selecting Annotation to edit")
        'selectAnnotation()
        If selectNoteDimension("Select annotation to locate.", selAnnotation) = Selection.Response.Ok Then
            If selAnnotation IsNot Nothing Then

                'Dim draftingSymbolBuilder0 As NXOpen.Annotations.Builder = Nothing
                'draftingSymbolBuilder0 = workPart.Annotations.Annotation.CreateDraftingDatumFeatureSymbolBuilder(selAnnotation)
                Dim nullNXOpen_Point3d As NXOpen.Point3d = Nothing
                Dim originData As NXOpen.Annotations.Annotation.AssociativeOriginData
                'originData = draftingSymbolBuilder0.Origin.GetAssociativeOrigin()
                originData = selAnnotation.GetAssociativeOrigin(nullNXOpen_Point3d)

                'Namespaces > NXOpen.Annotations > Annotation > Annotation.AssociativeOriginData
                lw.writeline("old position " & originData.XOffsetFactor & ";" & originData.YOffsetFactor)

                myForm.posX.Text = originData.XOffsetFactor
                myForm.posY.Text = originData.YOffsetFactor
                'lw.writeline("Horizontal Alignment Position " & originData.HorizAlignmentPosition)
                'lw.writeline("Vertical Alignment Position " & originData.VertAlignmentPosition)
                lw.writeline("Type of associativity  " & originData.OriginType.ToString) 'to be 'OffsetFromText'
                ufs.Ui.SetPrompt("Select Origin Annotation")
                ufs.Ui.SetStatus("Selecting Origin Annotation")
                'selectOrigin()
                selectNoteDimension("Select origin", origAnnotation)
                If originData.OffsetAnnotation Is Nothing And origAnnotation Is Nothing Then
                    lw.writeline("Origin annotation is required")
                    Return
                Else
                    'lw.writeline("Annotation to align to  " & originData.OffsetAnnotation.Tag)

                    If originData.OffsetAnnotation IsNot Nothing Then
                        origAnnotation = originData.OffsetAnnotation
                    End If
                    'display form
                    myForm.ShowDialog()

                End If

                'draftingSymbolBuilder0.Destroy()

            End If

        End If

        lw.Close()
        theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
        theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)

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

    Public Sub selectAnnotation()
        selectNoteDimension("Select annotation to locate", selAnnotation)

    End Sub

    Public Sub selectOrigin()
        selectNoteDimension("Select origin", origAnnotation)
    End Sub

    Sub moveNote(delta_x As Double, delta_y As Double)
        ' ----------------------------------------------
        '   Menu: Edit->Annotation->Annotation Object
        ' ----------------------------------------------
        Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

        'Dim draftingDatumFeatureSymbolBuilder1 As NXOpen.Annotations.DraftingDatumFeatureSymbolBuilder = Nothing
        'draftingDatumFeatureSymbolBuilder1 = workPart.Annotations.Datums.CreateDraftingDatumFeatureSymbolBuilder(selAnnotation)
        'draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)


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
        assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
        assocOrigin1.HorizAnnotation = nullNXOpen_Annotations_Annotation
        assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
        assocOrigin1.AlignedAnnotation = nullNXOpen_Annotations_Annotation
        assocOrigin1.DimensionLine = 0
        assocOrigin1.AssociatedView = nullNXOpen_View
        assocOrigin1.AssociatedPoint = nullNXOpen_Point
        'specifiyng origin annotetion
        assocOrigin1.OffsetAnnotation = origAnnotation
        'Alignment type specification
        assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft
        lw.writeline("old position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)
        assocOrigin1.XOffsetFactor = assocOrigin1.XOffsetFactor + delta_x ' 5.0
        assocOrigin1.YOffsetFactor = assocOrigin1.YOffsetFactor + delta_y '10.0
        'debuging
        lw.writeline("new position position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)
        assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
        ' ----------------------------------------------
        Dim nullNXOpen_Point3d As NXOpen.Point3d = Nothing
        selAnnotation.SetAssociativeOrigin(assocOrigin1, nullNXOpen_Point3d)
        RedrawAnnotation(selAnnotation)

        'selAnnotation.RedisplayObject
        'workPart.Views.WorkView.Regenerate()

        'draftingDatumFeatureSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)

        'draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
        'theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
        'Dim nXObject1 As NXOpen.NXObject = Nothing
        'nXObject1 = draftingDatumFeatureSymbolBuilder1.Commit()
        'draftingDatumFeatureSymbolBuilder1.Destroy()


    End Sub

    Sub RedrawAnnotation(annotation As NXOpen.DisplayableObject)
        Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Refresh")
        Dim objects1(0) As NXOpen.DisplayableObject
        objects1(0) = annotation
        theSession.DisplayManager.BlankObjects(objects1)
        Dim nErrs1 As Integer = Nothing
        nErrs1 = theSession.UpdateManager.DoUpdate(markId1)
        workPart.Views.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly)
        ' ----------------------------------------------
        '   Menu: Edit->Undo List->1 Hide
        ' ----------------------------------------------
        Dim marksRecycled1 As Boolean = Nothing
        Dim undoUnavailable1 As Boolean = Nothing
        theSession.UndoLastNVisibleMarks(1, marksRecycled1, undoUnavailable1)
    End Sub

    '**************************************************
    Function selectNoteDimension(ByVal prompt As String, ByRef obj As NXOpen.Annotations.Annotation)
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
            .Subtype = 4
            .SolidBodySubtype = 0
        End With

        Dim cursor As NXOpen.Point3d = Nothing

        Dim resp As Selection.Response =
        theUI.SelectionManager.SelectObject(prompt, prompt,
            Selection.SelectionScope.AnyInAssembly,
            Selection.SelectionAction.ClearAndEnableSpecific,
            False, False, mask, obj, cursor)

        If resp = Selection.Response.ObjectSelected Or
        resp = Selection.Response.ObjectSelectedByName Then
            Return Selection.Response.Ok
        Else
            Return Selection.Response.Cancel
        End If
    End Function    'selectNoteDimension
    '**************************************************

End Module






Public Class fPosition
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btRight_Click(sender As Object, e As EventArgs) Handles btRight.Click
        IncVal(posX.Text, numPositionIncrement.Value)
        moveNote(CDbl(posX.Text), CDbl(posY.Text))
    End Sub

    Private Sub btLeft_Click(sender As Object, e As EventArgs) Handles btLeft.Click
        IncVal(posX.Text, -numPositionIncrement.Value)
        moveNote(CDbl(posX.Text), CDbl(posY.Text))
    End Sub

    Private Sub btUp_Click(sender As Object, e As EventArgs) Handles btUp.Click
        IncVal(posY.Text, numPositionIncrement.Value)
        moveNote(CDbl(posX.Text), CDbl(posY.Text))
    End Sub

    Private Sub btDown_Click(sender As Object, e As EventArgs) Handles btDown.Click
        IncVal(posY.Text, -numPositionIncrement.Value)
        moveNote(CDbl(posX.Text), CDbl(posY.Text))
    End Sub


    Private Sub IncVal(ByRef strVal As String, ByRef strI As Double)
        Dim strFrmat As String = "0.0000"
        If IsNumeric(strVal) Then
            strVal = Format(CDbl(strVal) + strI, strFrmat)
        Else
            strVal = Format(0, strFrmat)
        End If
    End Sub

    ''not working - crashes NX
    'Private Sub btAnnotation_Click(sender As Object, e As EventArgs) Handles btAnnotation.Click

    '    'Dim myThread As New Thread(AddressOf selectAnnotation)
    '    Dim myThread As New Thread(Sub()
    '                                   selectAnnotation()
    '                               End Sub)
    '    myThread.Start()
    '    myThread.Join()

    'End Sub


    Private Sub btOrigin_Click(sender As Object, e As EventArgs) Handles btOrigin.Click
        'selectNoteDimension("Select origin annotation.", origAnnotation)
        selectOrigin()
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
        Me.btAnnotation = New System.Windows.Forms.Button()
        Me.btOrigin = New System.Windows.Forms.Button()
        CType(Me.numPositionIncrement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'posX
        '
        Me.posX.Location = New System.Drawing.Point(32, 44)
        Me.posX.Name = "posX"
        Me.posX.Size = New System.Drawing.Size(134, 20)
        Me.posX.TabIndex = 0
        Me.posX.Text = "0"
        Me.posX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btUp
        '
        Me.btUp.Location = New System.Drawing.Point(12, 96)
        Me.btUp.Name = "btUp"
        Me.btUp.Size = New System.Drawing.Size(154, 23)
        Me.btUp.TabIndex = 1
        Me.btUp.Text = "UP"
        Me.btUp.UseVisualStyleBackColor = True
        '
        'btDown
        '
        Me.btDown.Location = New System.Drawing.Point(12, 171)
        Me.btDown.Name = "btDown"
        Me.btDown.Size = New System.Drawing.Size(154, 23)
        Me.btDown.TabIndex = 2
        Me.btDown.Text = "DOWN"
        Me.btDown.UseVisualStyleBackColor = True
        '
        'btLeft
        '
        Me.btLeft.Location = New System.Drawing.Point(12, 125)
        Me.btLeft.Name = "btLeft"
        Me.btLeft.Size = New System.Drawing.Size(75, 40)
        Me.btLeft.TabIndex = 3
        Me.btLeft.Text = "LEFT"
        Me.btLeft.UseVisualStyleBackColor = True
        '
        'btRight
        '
        Me.btRight.Location = New System.Drawing.Point(93, 125)
        Me.btRight.Name = "btRight"
        Me.btRight.Size = New System.Drawing.Size(73, 40)
        Me.btRight.TabIndex = 4
        Me.btRight.Text = "RIGHT"
        Me.btRight.UseVisualStyleBackColor = True
        '
        'numPositionIncrement
        '
        Me.numPositionIncrement.DecimalPlaces = 2
        Me.numPositionIncrement.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.numPositionIncrement.Location = New System.Drawing.Point(12, 200)
        Me.numPositionIncrement.Name = "numPositionIncrement"
        Me.numPositionIncrement.Size = New System.Drawing.Size(60, 20)
        Me.numPositionIncrement.TabIndex = 5
        Me.numPositionIncrement.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'posY
        '
        Me.posY.Location = New System.Drawing.Point(32, 70)
        Me.posY.Name = "posY"
        Me.posY.Size = New System.Drawing.Size(134, 20)
        Me.posY.TabIndex = 0
        Me.posY.Text = "0"
        Me.posY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "X:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Y:"
        '
        'btAnnotation
        '
        Me.btAnnotation.Enabled = False
        Me.btAnnotation.Location = New System.Drawing.Point(12, 12)
        Me.btAnnotation.Name = "btAnnotation"
        Me.btAnnotation.Size = New System.Drawing.Size(154, 10)
        Me.btAnnotation.TabIndex = 7
        Me.btAnnotation.Text = "Select Annotaion"
        Me.btAnnotation.UseVisualStyleBackColor = True
        Me.btAnnotation.Visible = False
        '
        'btOrigin
        '
        Me.btOrigin.Enabled = False
        Me.btOrigin.Location = New System.Drawing.Point(12, 28)
        Me.btOrigin.Name = "btOrigin"
        Me.btOrigin.Size = New System.Drawing.Size(154, 10)
        Me.btOrigin.TabIndex = 8
        Me.btOrigin.Text = "Select Origin"
        Me.btOrigin.UseVisualStyleBackColor = True
        Me.btOrigin.Visible = False
        '
        'fPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(178, 232)
        Me.Controls.Add(Me.btOrigin)
        Me.Controls.Add(Me.btAnnotation)
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
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fPosition"
        Me.Text = "Note positioning"
        CType(Me.numPositionIncrement, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btAnnotation As Button
    Friend WithEvents btOrigin As Button
End Class





