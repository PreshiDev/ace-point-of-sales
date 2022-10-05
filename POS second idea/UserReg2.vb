Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class UserReg2

    Dim connection As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connection.Open()
        Dim inst As String = "INSERT INTO UserReg Values ('" & TextBox1.Text & "','" &
            TextBox5.Text & "','" & ComboBox1.Text & "')"
        Dim command As New SqlCommand(inst, connection)
        command.ExecuteNonQuery()
        connection.Close()
        MessageBox.Show("Successfully Saved", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox5.Clear()
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Dash_Board.Show()
    End Sub
End Class