Imports System.IO

Public Class Form1
    Const InitPath = "L:\TOOLING FIXTURES\MODULAR COMPONENTS\VERIFIED"

    Private Sub ButtonSelectPath_Click(sender As Object, e As EventArgs) Handles ButtonSelectPath.Click
        SelectPath()
    End Sub

    Private Sub SelectPath()
        Throw New NotImplementedException()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        ImportSTEPFile()
    End Sub

    Private Sub ImportSTEPFile()
        Throw New NotImplementedException()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateCategories()
    End Sub

    Private Sub PopulateCategories()
        Dim directoryName As String
        Dim foldersList = System.IO.Directory.EnumerateDirectories(InitPath, "*", System.IO.SearchOption.TopDirectoryOnly)
        For Each f In foldersList
            'directoryName = Path.GetDirectoryName(f)
            Console.WriteLine("{0}", (Split(f, "\").Reverse()(0)))
            ComboBoxCategory.Items.Add(Split(f, "\").Reverse()(0))
        Next
    End Sub
End Class
