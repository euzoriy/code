

Option Compare Database



Private Sub addItems_Click()
''TODO
''Call Inventory_Transaction Form
DoCmd.OpenForm "Inventory_Transaction", _
        WhereCondition:="tblTransaction.item_ID='" & Me.PartNumber & "'"
End Sub


Private Sub Calibration_Report_But_Click()
On Error GoTo Error_Handle

DoCmd.OpenReport "Inventory List", acViewPreview, "", "[ID] = " & Nz(ID, 0), , acNormal

If (Not IsNull(ID)) Then
   TempVars.Add "CurrentID", Me.ID.Value
   End If

If IsNull(ID) Then
    TempVars.Add "CurrentID", Nz(DMax("[ID]", [Form].[RecordSource]), 0)
End If

''DoCmd.Requery ""

''DoCmd.SearchForRecord , "", acFirst, "[ID]=" & TempVars!CurrentID

TempVars.Remove "CurrentID"

go_here:
Exit Sub

Error_Handle:
MsgBox Err.number & " - " & Err.Description, vbInformation, "Error"

Resume go_here

End Sub

Private Sub Command0_Click()
    If Form_Inventory_Details.Dirty Then
        If MsgBox("Would You Like To Save The Changes To This Record?", vbQuestion + vbYesNo + vbDefaultButton1, _
        "Save Changes to Record ???") = vbNo Then
            Form_Inventory_Details.Undo
            DoCmd.Close
        Else
            DoCmd.Close
        End If
    Else
        DoCmd.Close
    End If
End Sub

Private Sub Command1_Click()

If Form_Inventory_Details.Dirty Then
    If MsgBox("Would You Like To Save The Changes To This Record?", vbQuestion + vbYesNo + vbDefaultButton1, _
    "Save Changes to Record ???") = vbNo Then
        Form_Inventory_Details.Undo
        DoCmd.Close
    Else
        DoCmd.Close
    End If
Else
    DoCmd.Close
End If

End Sub



Private Sub Edit_rec_Click()
Dim msg, Style, Title, Help, Ctxt, Response, MyString

msg = "Do you want to edit record?"
Style = vbYesNo + vbQuestion + vbDefaultButton2
Title = "MsgBox Demonstration"
Ctxt = 1000

Response = MsgBox(msg, Style, Title, Help, Ctxt)

If Response = vbYes Then    ' User chose Yes.
    MyString = "Yes"    ' Perform some action.
    Call EnableEditing
    Form_Inventory_Details.Edit_rec.Enabled = False
Else    ' User chose No.
    MyString = "No"    ' Perform some action.
End If


End Sub


Private Sub seeOnWeb_Click()
    Dim strfile As String
    Dim strBase As Variant
        strBase = DLookup("Web", "Contacts", "Company = '" & [Forms]![Inventory_Details]![Vendor] & "'")
        'MsgBox (strBase)
    'If strBase <> Null Then
        strfile = strBase + Me.VendorPartNumber
        Application.FollowHyperlink strfile
    'Else
        'MsgBox ("Please check if vendor and vendor part number are specified.")
    'End If
End Sub

Private Sub LeestaPartID_LostFocus()
On Error GoTo Error_Handle

If Not IsNull(Me.LeestaPartID.Value) Then

Dim intnewrec As Integer

  intnewrec = Form_Inventory_Details.NewRecord

  If intnewrec = True Then

  RecordVerification "LeestaPartID", Me.LeestaPartID.Value
      
  End If
  
End If


go_here:
Exit Sub

Error_Handle:
MsgBox Err.number & " - " & Err.Description, vbInformation, "Error"

Resume go_here
  
End Sub

Private Sub New_Record_Click()
    RunCommand acCmdRecordsGoToNew
    Form_Inventory_Details.Edit_rec.Visible = False
    Form_Inventory_Details.New_Record.Visible = False
    Call EnableEditing
End Sub

Private Sub partNumber_LostFocus()
  
  Me.PartNumber.Value = UCase(Me.PartNumber.Value) ''Part Number Always in Upper Case
  
  Dim intnewrec As Integer

  intnewrec = Form_Inventory_Details.NewRecord

  If intnewrec = True Then
   
    RecordVerification "PartNumber", Form_Inventory_Details.PartNumber.Value
 
  End If
 
End Sub

Private Sub VendorPartNumber_LostFocus()

On Error GoTo Error_Handle

If Not IsNull(Me.LeestaPartID.Value) Then

Dim intnewrec As Integer

  intnewrec = Form_Inventory_Details.NewRecord

  If intnewrec = True Then

   RecordVerification "VendorPartNumber", Me.VendorPartNumber.Value
   
  End If
  
End If
go_here:
Exit Sub

Error_Handle:
MsgBox Err.number & " - " & Err.Description, vbInformation, "Error"

Resume go_here
    
    
End Sub


Private Sub btnGenerateName_Click()
    params = toName(Nz(Me.SizeA.Value), "") & toName(Nz(Me.SizeB.Value, "")) & toName(Nz(Me.SizeC.Value, "")) & toName(Nz(Me.SizeD.Value, ""))
    'MsgBox (toName("1-2/4-20unc"))
    trams = Abbr(Nz(Me.Description.Value, "")) & "_" & Nz(Me.Type.Value, "")
    Me.PartNumber.Value = trams & params
End Sub


'****************************************************************



Private Function toName(sInput As String) As String
    If sInput = Null Then
        toName = ""
    Else
        Dim sRevInput As String
        sInput = Trim(sInput)
        sRevInput = StrReverse(sInput)
        'checking if fractional number
        If InStr(sRevInput, "/") > 0 Then
        'checking if thread
            If InStr(sRevInput, "/") > InStr(sRevInput, "-") Then
                'getting numbers of threads out
                nonFrac = StrReverse(Mid(sRevInput, 1, InStr(sRevInput, "-")))
                'getting thread diameter
                frac = StrReverse(Mid(sRevInput, InStr(sRevInput, "-") + 1))
                toName = FractionToDecimal(CStr(frac)) & nonFrac
            Else
                toName = FractionToDecimal(sInput)
            End If
        Else
            toName = sInput
        End If
        'cleaning
        toName = Clean(toName)
        'Placing separator
        toName = "_" & UCase(toName)
    End If
End Function

Private Function Clean(toName As String) As String
    'Removing lead and trail spaces
    toName = Trim(toName)
    'cleaning
    notAllowedChars = Array("/", "\", "|", "<", ">", ",", ".", "!", "@", "$", "%", "?", " ")
    For Each cr In notAllowedChars
        toName = Replace(toName, cr, "-")
    Next
    'Remove doule underscores
    Do While InStr(toName, "--") > 0
        toName = Replace(toName, "--", "-")
    Loop
    Clean = toName
End Function

Private Function Abbr(text As String) As String
    initials = ""
    words = Split(Clean(text), "-")
    For Each word In words
        initials = CStr(initials) + CStr(Mid(word, 1, 1))
    Next
    Abbr = initials
End Function

Private Function FractionToDecimal(frac As String) As String
        'decimalVal = "0"
        On Error GoTo Error_Handle

            upper = 0
            lower = 0
            remain = 0
            
            If InStr(frac, "-") > 0 Then
                frac = Replace(frac, "-", " ")
            End If
            
            If InStr(frac, "/") > 0 Then
            'split whole + fractional
                If InStr(frac, " ") > 0 Then
                    remain = Mid(frac, 1, (InStr(frac, " ")) - 1)
                    frac = Mid(frac, (InStr(frac, " ")) + 1)
                End If
            End If
            
            upper = Mid(frac, 1, InStr(frac, "/") - 1)
            lower = Mid(frac, InStr(frac, "/") + 1)
            'MsgBox (upper & lower)
            If upper > lower Then
                FractionToDecimal = "_ERR"
            Else
                If Not IsNumeric(upper) Or Not IsNumeric(lower) Then
                    FractionToDecimal = frac
                Else
                    'FractionToDecimal = CStr(remain + (upper / lower))
                    FractionToDecimal = CStr(FormatNumber(CDbl(remain + (upper / lower)), 3))   'ToString("F3")
                End If
            End If
        
go_here:
Exit Function

Error_Handle:
If frac = Null Then
    FractionToDecimal = ""
Else
    FractionToDecimal = frac
End If
    
Resume go_here
        
End Function
