Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class UserReg

    Dim connection As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connection.Open()
        Dim inst As String = "INSERT INTO UserReg Values ('" & TextBox1.Text & "','" &
            TextBox5.Text & "','" & ComboBox1.Text & "')"
        Dim command As New SqlCommand(inst, connection)
        command.ExecuteNonQuery()
        connection.Close()
        MessageBox.Show("Successfully Saved", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox5.Clear()
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub UserReg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Admin")
        ComboBox1.Items.Add("Manager")
        ComboBox1.Items.Add("Cashier")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dashboard2.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class