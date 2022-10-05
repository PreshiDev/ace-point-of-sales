Public Class LoadingPage
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 1
        If ProgressBar1.Value <= 10 Then
            Label1.Text = "Initializing System"
        ElseIf ProgressBar1.Value <= 30
            Label1.Text = "Loading Components"
        ElseIf ProgressBar1.Value <= 50
            Label1.Text = "Loading Data components"
        ElseIf ProgressBar1.Value <= 80
            Label1.Text = "Please Wait"
        ElseIf ProgressBar1.Value <= 100
            Label1.Text = "Welcome To Ace"

            If ProgressBar1.Value = 100 Then
                Timer1.Dispose()
                Me.Hide()
                Login.ShowDialog()
            End If
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub LoadingPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub
End Class