Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class Supplier

    Dim connect As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")
    Private Sub SalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Me.Hide()
        Dashboard2.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect.Open()
        Dim collect As String = "INSERT Into Products Values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & DateTimePicker1.Text & "')"

        Dim command As SqlCommand = New SqlCommand(collect, connect)
        command.ExecuteNonQuery()
        connect.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Dashboard2.Show()
    End Sub
End Class