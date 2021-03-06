'NXJournaling.com
'tested with NX 7.5
'May 31, 2012
'When run, the journal will highlight all dimensions in the part that have manually entered text.
'The listing window will report the dimension text, view, and sheet for each dimension
'with manually entered text.
'
'January 21, 2015
'Instead of using the .Highlight method, change the color of the dimension with manual text.
 
Option Strict Off
Imports System
Imports NXOpen
Imports NXOpen.UF
 
Module Report_Dimensions_manual_text_2
 
    Dim theSession As Session = Session.GetSession()
    Dim ufs As UFSession = UFSession.GetUFSession()
    Dim workPart As Part = theSession.Parts.Work
    Dim displayPart As Part = theSession.Parts.Display
 
    Sub Main()
 
        Dim lw As ListingWindow = theSession.ListingWindow
        Dim mpi(99) As Integer
        Dim mpr(69) As Double
        Dim radius_value As String
        Dim diameter_value As String
        Dim manualTextCount As Integer = 0
        Dim dictionary As New Collections.Generic.Dictionary(Of String, String)
        Dim viewList() As View
        Dim text1() As String
        Dim text2() As String
 
        lw.Open()
 
        Const undoMarkName As String = "highlight manual dimensions"
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, undoMarkName)
 
        For Each sheet As Drawings.DrawingSheet In workPart.DrawingSheets
            viewList = sheet.GetDraftingViews
            For Each myView As View In viewList
                dictionary.Add(myView.Name, sheet.Name)
				    lw.WriteLine("Adding Sheet: " & sheet.Name)
                    lw.WriteLine("Adding View: " & myView.Name)
				
            Next
        Next
 
        '******************************************************
        'change this value to the color number of your choosing
        Const manualDimColor As Integer = 84
        '******************************************************
 
        'find the color name of the chosen color above
        Dim colorName As String = ""
        Dim colorModel As Integer = UFConstants.UF_DISP_rgb_model
        Dim colorValues(2) As Double
        ufs.Disp.AskColor(manualDimColor, colorModel, colorName, colorValues)
 
        Dim partDimensions() As Annotations.Dimension
        partDimensions = workPart.Dimensions.ToArray
        If partDimensions.Length > 0 Then
            For Each partDimension As Annotations.Dimension In partDimensions
                ufs.Drf.AskObjectPreferences(partDimension.Tag, mpi, mpr, radius_value, diameter_value)
 
                If mpi(7) = 3 OrElse mpi(7) = 4 Then
                    'dimension has manual text
                    'partDimension.Highlight()
                    'color 84 = strong gold (in default CDF)
                    ColorDim(partDimension, manualDimColor)
                    'lw.WriteLine("Belongs to view: " & partDimension.GetAssociativity(1).ObjectView.Name)
					if dictionary.ContainsKey(partDimension.GetAssociativity(1).ObjectView.Name) Then					
						lw.WriteLine("Sheet: " & dictionary(partDimension.GetAssociativity(1).ObjectView.Name))
					end if
                    lw.WriteLine("View: " & partDimension.GetAssociativity(1).ObjectView.Name)
                    partDimension.GetDimensionText(text1, text2)
                    Dim j As Integer
                    lw.WriteLine("Dimension Text: ")
                    For j = text1.GetLowerBound(0) To text1.GetUpperBound(0)
                        lw.WriteLine("  " & text1(j))
                    Next
                    manualTextCount += 1
                    lw.WriteLine("")
                End If
            Next
            If manualTextCount > 0 Then
                MsgBox(manualTextCount & " dimensions have manual text and have been changed to color: " & manualDimColor.ToString & " (" & colorName & ")")
            Else
                MsgBox("There are no dimensions with manual text in the part")
            End If
        Else
            MsgBox("There are no dimensions in the work part")
        End If
 
        lw.Close()
 
    End Sub
 
    Sub ColorDim(ByRef theDim As Annotations.Dimension, ByVal theColor As Integer)
 
        Dim objects1(0) As DisplayableObject
        objects1(0) = theDim
 
        Dim displayModification1 As DisplayModification
        displayModification1 = theSession.DisplayManager.NewDisplayModification()
 
        With displayModification1
            .ApplyToAllFaces = True
            .ApplyToOwningParts = False
            .NewColor = theColor
            .Apply(objects1)
            .Dispose()
        End With
 
    End Sub
 
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
 
        'Unloads the image when the NX session terminates
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.AtTermination
 
    End Function
 
End Module



