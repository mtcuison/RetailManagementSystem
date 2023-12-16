Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(Environment.GetEnvironmentVariable(TextBox1.Text))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Path As String = Environment.GetEnvironmentVariable(TextBox1.Text) & "sampol"

        If Not File.Exists(Path) Then File.Create(Path).Dispose()

        My.Computer.FileSystem.WriteAllText(Path, "test", True)
    End Sub
End Class