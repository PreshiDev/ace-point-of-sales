Imports System.Data.Sql
Imports System.Data.SqlClient
Imports syatem.drawing.imaging

Public Class Registration

    Dim connecting As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Public Sub employeesID()
        Dim em As New Random
        Label12.Text = em.Next(0000001, 999999)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connecting.Open()
        Dim send As String = "INSERT INTO employeesReg Values('" & Label12.Text & "','" & TextBox1.Text & "','" &
            TextBox2.Text & "','" & TextBox3.Text & "','" &
             TextBox4.Text & "','" & TextBox5.Text & "','" &
             TextBox6.Text & "','" & TextBox7.Text & "','" &
             TextBox8.Text & "','" & ComboBox1.Text & "')"

        Dim command As New SqlCommand(send, connecting)
        command.ExecuteNonQuery()
        connecting.Close()
        MessageBox.Show("Employess Info Successfully Saved", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        ComboBox1.SelectedIndex = -1

        employeesID()
    End Sub

    Private Sub Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        employeesID()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Dashboard2.Show()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub
End Class