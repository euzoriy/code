Option Strict Off
Imports System
Imports System.Windows.Forms
Imports NXOpen




Module Module1

    Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Dim workPart As NXOpen.Part = theSession.Parts.Work
    Dim displayPart As NXOpen.Part = theSession.Parts.Display
    Dim lw As ListingWindow = theSession.ListingWindow


    Private Const _attributeTitle As String = "PROJECT"
    Public ReadOnly Property AttributeTitle() As String
        Get
            Return _attributeTitle
        End Get
    End Property


    Private _attributeValue As String = ""
    Public Property AttributeValue() As String
        Get
            Return _attributeValue
        End Get
        Set(ByVal value As String)
            _attributeValue = value
        End Set
    End Property

    Sub Main()


        If IsNothing(theSession.Parts.Work) Then
            'active part required
            Return
        End If

        lw.Open()
        lw.writeline("journal opened")

        Const undoMarkName As String = "NXJ form demo journal"
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, undoMarkName)

        Dim myAttributeInfo As NXObject.AttributeInformation

        Try
            myAttributeInfo = workPart.GetUserAttribute(_attributeTitle, NXObject.AttributeType.String, -1)
            _attributeValue = myAttributeInfo.StringValue

        Catch ex As NXException
            If ex.ErrorCode = 512008 Then
                'attribute not found
            Else
                'unexpected error: show error message, undo, and exit journal
                MsgBox(ex.ErrorCode & ": " & ex.Message)
                theSession.UndoToMark(markId1, undoMarkName)
                Return
            End If

        Finally

        End Try

        'create new form object
        Dim myForm As New fPosition
        'set form object properties (current part attribute title and value)
        'myForm.AttributeTitle = _attributeTitle
        'myForm.AttributeValue = _attributeValue
        'display our form
        myForm.ShowDialog()

        'If myForm.Canceled Then
        '    'user pressed cancel, exit journal
        '    Return
        'Else
        '    'user pressed OK, assign value from form to part attribute
        '    _attributeValue = myForm.AttributeValue
        '    workPart.SetUserAttribute(_attributeTitle, -1, _attributeValue, Update.Option.Later)

        'End If





        lw.Close()

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



    Sub moveNote(delta_x As Double, delta_y As Double)
        ' ----------------------------------------------
        '   Menu: Edit->Annotation->Annotation Object
        ' ----------------------------------------------
        Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
        markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")


        'select annotation
        Dim draftingDatum1 As NXOpen.Annotations.DraftingDatum = CType(workPart.FindObject("ENTITY 25 1 1"), NXOpen.Annotations.DraftingDatum)

        'create annotation builder
        Dim draftingDatumFeatureSymbolBuilder1 As NXOpen.Annotations.DraftingDatumFeatureSymbolBuilder = Nothing
        draftingDatumFeatureSymbolBuilder1 = workPart.Annotations.Datums.CreateDraftingDatumFeatureSymbolBuilder(draftingDatum1)


        Dim originData As NXOpen.Annotations.Annotation.AssociativeOriginData
        originData = draftingDatumFeatureSymbolBuilder1.Origin.GetAssociativeOrigin()

        'Namespaces > NXOpen.Annotations > Annotation > Annotation.AssociativeOriginData
        lw.writeline("old position " & originData.XOffsetFactor & ";" & originData.YOffsetFactor)
        'lw.writeline("Horizontal Annotation " & originData.HorizAnnotation.ToString)
        'lw.writeline("Vertical Annotation " & originData.VertAnnotation.ToString)
        lw.writeline("Type of associativity  " & originData.OriginType.ToString)

        theSession.SetUndoMarkName(markId1, "Datum Feature Symbol Dialog")
        draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
        'Dim leaderData1 As NXOpen.Annotations.LeaderData = Nothing
        'leaderData1 = workPart.Annotations.CreateLeaderData()
        'leaderData1.StubSize = 0.125
        'leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow
        'leaderData1.VerticalAttachment = NXOpen.Annotations.LeaderVerticalAttachment.Center
        'draftingDatumFeatureSymbolBuilder1.Leader.Leaders.Append(leaderData1)
        'leaderData1.TerminatorType = NXOpen.Annotations.LeaderData.LeaderType.Datum
        'leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred
        'leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledDatum
        draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
        draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
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
        Dim majorAngularDimension1 As NXOpen.Annotations.MajorAngularDimension = CType(workPart.FindObject("HANDLE R-1805062"), NXOpen.Annotations.MajorAngularDimension)


        assocOrigin1.OffsetAnnotation = majorAngularDimension1
        'Alignment type specification
        assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft

        lw.writeline("old position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)

        assocOrigin1.XOffsetFactor = assocOrigin1.XOffsetFactor + delta_x ' 5.0
        assocOrigin1.YOffsetFactor = assocOrigin1.YOffsetFactor + delta_y '10.0
        'lw.writeline("offset " & delta_x & ";" & delta_y)
        lw.writeline("new position position " & assocOrigin1.XOffsetFactor & ";" & assocOrigin1.YOffsetFactor)


        assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above
        draftingDatumFeatureSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1)
        'Dim point1 As NXOpen.Point3d = New NXOpen.Point3d(10.296795134302666, 17.104233695665958, 0.0)
        'draftingDatumFeatureSymbolBuilder1.Origin.Origin.SetValue(Nothing, nullNXOpen_View, point1)
        draftingDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(True)
        theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = draftingDatumFeatureSymbolBuilder1.Commit()
        theSession.SetUndoMarkName(markId1, "Datum Feature Symbol")
        draftingDatumFeatureSymbolBuilder1.Destroy()
        theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
        ' ----------------------------------------------
        '   Menu: Tools->Journal->Stop Recording
        ' ----------------------------------------------
    End Sub



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

    Private Sub btAnnotation_Click(sender As Object, e As EventArgs) Handles btAnnotation.Click

    End Sub

    Private Sub btOrigin_Click(sender As Object, e As EventArgs) Handles btOrigin.Click

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
        Me.posX.Location = New System.Drawing.Point(32, 71)
        Me.posX.Name = "posX"
        Me.posX.Size = New System.Drawing.Size(134, 20)
        Me.posX.TabIndex = 0
        Me.posX.Text = "0"
        Me.posX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btUp
        '
        Me.btUp.Location = New System.Drawing.Point(12, 123)
        Me.btUp.Name = "btUp"
        Me.btUp.Size = New System.Drawing.Size(154, 23)
        Me.btUp.TabIndex = 1
        Me.btUp.Text = "UP"
        Me.btUp.UseVisualStyleBackColor = True
        '
        'btDown
        '
        Me.btDown.Location = New System.Drawing.Point(12, 198)
        Me.btDown.Name = "btDown"
        Me.btDown.Size = New System.Drawing.Size(154, 23)
        Me.btDown.TabIndex = 2
        Me.btDown.Text = "DOWN"
        Me.btDown.UseVisualStyleBackColor = True
        '
        'btLeft
        '
        Me.btLeft.Location = New System.Drawing.Point(12, 152)
        Me.btLeft.Name = "btLeft"
        Me.btLeft.Size = New System.Drawing.Size(75, 40)
        Me.btLeft.TabIndex = 3
        Me.btLeft.Text = "LEFT"
        Me.btLeft.UseVisualStyleBackColor = True
        '
        'btRight
        '
        Me.btRight.Location = New System.Drawing.Point(93, 152)
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
        Me.numPositionIncrement.Location = New System.Drawing.Point(12, 227)
        Me.numPositionIncrement.Name = "numPositionIncrement"
        Me.numPositionIncrement.Size = New System.Drawing.Size(60, 20)
        Me.numPositionIncrement.TabIndex = 5
        Me.numPositionIncrement.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'posY
        '
        Me.posY.Location = New System.Drawing.Point(32, 97)
        Me.posY.Name = "posY"
        Me.posY.Size = New System.Drawing.Size(134, 20)
        Me.posY.TabIndex = 0
        Me.posY.Text = "0"
        Me.posY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "X:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Y:"
        '
        'btAnnotation
        '
        Me.btAnnotation.Location = New System.Drawing.Point(12, 12)
        Me.btAnnotation.Name = "btAnnotation"
        Me.btAnnotation.Size = New System.Drawing.Size(154, 23)
        Me.btAnnotation.TabIndex = 7
        Me.btAnnotation.Text = "Select Annotaion"
        Me.btAnnotation.UseVisualStyleBackColor = True
        '
        'btOrigin
        '
        Me.btOrigin.Location = New System.Drawing.Point(12, 41)
        Me.btOrigin.Name = "btOrigin"
        Me.btOrigin.Size = New System.Drawing.Size(154, 23)
        Me.btOrigin.TabIndex = 8
        Me.btOrigin.Text = "Select Origin"
        Me.btOrigin.UseVisualStyleBackColor = True
        '
        'fPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(178, 259)
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
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
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
