Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class Login

    Dim connect As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = Nothing And TextBox2.Text = Nothing Then
            MessageBox.Show("Invaild Input")
        End If
        connect.Open()
        Dim command As New SqlCommand("Select * From UserReg Where Username = '" + TextBox1.Text + "' and Password = '" + TextBox2.Text + "'", connect)
        Dim dataread As SqlDataReader = command.ExecuteReader()
        Dim ba As Boolean = False
        Dim userrole As String = " "
        While dataread.Read()
            If (TextBox1.Text = dataread(0).ToString() And TextBox2.Text = dataread(1).ToString()) Then
                ba = True
                userrole = dataread(2).ToString()
            End If
        End While
        connect.Close()
        Dim ns As New Form
        If (ba) Then
            If (userrole = "Cashier") Then
                Form1.Show()
                Me.Hide()
                TextBox1.Text = " "
                TextBox2.Text = " "
            ElseIf (userrole = "Manager") Then
                Dash_Board.Show()
                Me.Hide()
                TextBox1.Text = " "
                TextBox2.Text = " "
            Else
                MessageBox.Show("Welcome Admiin", "Hurry")
                Dashboard2.Show()
                Me.Hide()
            End If
        Else
            MessageBox.Show("Invaild User")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class