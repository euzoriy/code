' NX 11.0.2.7
' Journal created by ievgenz on Tue Aug 22 09:30:21 2017 Eastern Daylight Time
' to simplify dimensions validation
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.UI
Imports NXOpen.Annotations
Module NXJournal
    Dim theSession As Session = Session.GetSession()
    Dim workPart As Part = theSession.Parts.Work
    Dim displayPart As Part = theSession.Parts.Display
    Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
    Dim lw As ListingWindow = theSession.ListingWindow
    Dim selAnnotation As Annotation
    Dim noteDim As Dimension
    Sub Main(ByVal args() As String)
        lw.Open()
        ProcessDimensions()
        ' ----------------------------------------------
        '   Menu: Tools->Journal->Stop Recording
        ' ----------------------------------------------
        lw.Close()
    End Sub
    Sub ProcessDimensions()
        While selectDimension("Select Dimension", selAnnotation) = Selection.Response.Ok
            Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
            markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
            Dim noteDim As NXOpen.Annotations.Dimension = CType(selAnnotation, NXOpen.Annotations.Dimension)
            'noteDim.SetDimensionPreferences(IsInspectionDimension = true)
            Dim dimPrefs As Annotations.DimensionPreferences
            dimPrefs = noteDim.GetDimensionPreferences()
            Dim calculationPrecision As Integer
            Dim resultPrecision As Integer
            Dim upperValue As Double
            Dim tolerance As Double
            Dim nomValue As Double

            Dim aText As AppendedText


            'set precision > 0 for dimensions to avoid values like x.x999999999			
            calculationPrecision = 7
            resultPrecision = 3
            If (calculationPrecision >= 0) Then
                nomValue = Math.Round(noteDim.ComputedSize, calculationPrecision)
            Else
                nomValue = noteDim.ComputedSize
            End If

            aText = noteDim.GetAppendedText()
            lw.writeline("")
            lw.writeline(noteDim.ToString.PadRight(60, "="))
            lw.writeline(vbTab & String.Join(vbNewLine, aText.GetAboveText))
            lw.writeline(String.Join(vbNewLine, aText.GetBeforeText) & " XXXX " & String.Join(vbNewLine, aText.GetAfterText))
            lw.writeline(String.Join(vbNewLine, aText.GetBelowText))
            lw.writeline("Upper tolerance value (XXXX): " & noteDim.UpperToleranceValue)
            lw.writeline("Nominal value (XXXX): " & nomValue)
            lw.writeline("Lower tolerance value (XXXX): " & noteDim.LowerToleranceValue)
            If Not noteDim.UpperToleranceValue = Math.Abs(noteDim.LowerToleranceValue) Then
                lw.writeline("!!! Attention tolerances are not symmetric !!!")
                noteDim.InspectionDimensionFlag = True
            End If
            lw.writeline("Upper/Lower dimension limit: " & (nomValue + noteDim.UpperToleranceValue) & "/" & (nomValue + noteDim.LowerToleranceValue))
            'User input target value
            upperValue = nomValue
            getTargetValue("Enter upper or lower limit", upperValue)

            'Getting absolute value oftolerance
            tolerance = Math.Abs(upperValue - nomValue)

            resultPrecision = maxPrecision(nomValue + tolerance, nomValue - tolerance)
            'MsgBox(resultPrecision)
            lw.writeline("Result value is:")
            lw.writeline(vbTab & (nomValue + tolerance))
            lw.writeline(vbTab & (nomValue - tolerance))
            lw.writeline("Result precision: " & resultPrecision)
            lw.writeline("")

            If Not upperValue = nomValue Then
                noteDim.ToleranceType = 2 '2-lines limits
                noteDim.UpperToleranceValue = tolerance
                noteDim.LowerToleranceValue = -tolerance
                noteDim.NominalDecimalPlaces = resultPrecision
                noteDim.ToleranceDecimalPlaces = resultPrecision
            End If

            noteDim.RedisplayObject

            theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
            theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
        End While
    End Sub

    Function maxPrecision(upper As Double, louer As Double) As Integer
        maxPrecision = MaxOf(CDbl(InStr(sReverse(upper), ".") - 1), CDbl(InStr(sReverse(louer), ".") - 1))
    End Function

    Function MaxOf(a As Double, b As Double)
        MaxOf = b
        If a > b Then
            MaxOf = a
        End If
    End Function

    Function getDouble(prompt As String, initialVal As Double) As Double
        Dim current As Double
        Try
            ' if number then format it.
            current = CDbl(InputBox(prompt, "Enter a value", initialVal))
            'current = Math.Round(current, d)
            'MsgBox(getDouble)
            Return current
        Catch ex As System.InvalidCastException
            ' item is not a number, do not format... leave as a string
            'msgbox(ex.StackTrace)
            Return initialVal
        End Try
    End Function

    Function getTargetValue(prompt As String, ByRef initialVal As Double) As Double
        Dim current As Double
        Dim strInput As String
        'precVal = 3 'Default
        Try
            ' if number then format it.
            strInput = InputBox(prompt, "Enter a value", initialVal)
            current = CDbl(strInput)
            'precVal = InStr(sReverse(strInput),".")-1
            'current = Math.Round(current, d)
            'MsgBox(precVal)
            initialVal = current
            Return current
        Catch ex As System.InvalidCastException
            ' item is not a number, do not format... leave as a string
            'msgbox(ex.StackTrace)
            Return initialVal
        End Try
    End Function

    Function selectDimension(ByVal prompt As String, ByRef obj As Annotation)
        Dim ui As UI = GetUI()
        Dim mask(5) As Selection.MaskTriple
        With mask(0)
            .Type = UFConstants.UF_dimension_type
            .Subtype = 0
            .SolidBodySubtype = 0
        End With
        Dim cursor As Point3d = Nothing
        Dim resp As Selection.Response =
        ui.SelectionManager.SelectObject(prompt, prompt,
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
    Function sReverse(ByVal value As String) As String
        ' Convert to char array.
        Dim arr() As Char = value.ToCharArray()
        ' Use Array.Reverse function.
        Array.Reverse(arr)
        ' Construct new string.
        Return New String(arr)
    End Function
End Module