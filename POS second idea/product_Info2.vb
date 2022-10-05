Imports System.Data.Sql
Imports System.Data.SqlClient



Public Class product_Info2

    Dim connect As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        connect.Open()
        Dim collect As String = "SELECT * From sales_report"

        Using command As SqlCommand = New SqlCommand(collect, connect)
            Using da As New SqlDataAdapter
                da.SelectCommand = command
                Using dat As New DataTable
                    da.Fill(dat)
                    DataGridView1.DataSource = dat

                    DataGridView1.AllowUserToAddRows = False
                End Using
            End Using

        End Using
        connect.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Dash_Board.Show()
    End Sub
End Class