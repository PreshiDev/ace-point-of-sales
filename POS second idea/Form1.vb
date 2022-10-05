Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form1

    Dim WithEvents pd As New PrintDocument
    Dim ppd As New PrintPreviewDialog
    Dim longpaper As Integer

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Application.Exit()
    End Sub

    Private bitmap As Bitmap

    Private Function cost_of_items() As Double
        Dim sum As Double = 0
        Dim i As Integer = 0

        For i = 0 To DataGridView1.Rows.Count - 1
            sum = sum + Convert.ToDouble(DataGridView1.Rows(i).Cells(2).Value)
        Next i
        Return sum
    End Function

    Sub add_cost()
        Dim tax, g As Double
        tax = 3.9

        If DataGridView1.Rows.Count > 0 Then
            Label9.Text = FormatCurrency(((cost_of_items() * tax / 100).ToString("0.00")))
            Label11.Text = FormatCurrency(cost_of_items().ToString("0.00"))

            g = ((cost_of_items() * tax / 100))
            Label10.Text = FormatCurrency(g + cost_of_items().ToString("0.00"))
        End If
    End Sub

    Sub change()
        Dim tax, g, c As Double
        tax = 3.9

        If DataGridView1.Rows.Count > 0 Then
            g = ((cost_of_items() * tax) / 100) + cost_of_items()
            c = Val(Label4.Text)
            Label5.Text = FormatCurrency((c - g).ToString("0.00"))
        End If
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Label4.Text = "0"
        Label5.Text = Nothing
        Label11.Text = Nothing
        Label9.Text = Nothing
        Label10.Text = Nothing
        ComboBox1.SelectedIndex = -1
        DataGridView1.Rows.Clear()
        DataGridView1.Refresh()
    End Sub

    Private Sub NumbersOnly(sender As Object, e As EventArgs) Handles Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button11.Click, Button1.Click, Button10.Click
        Dim b As Button = sender

        If (Label4.Text = "0") Then
            Label4.Text = ""
            Label4.Text = b.Text
        ElseIf (b.Text = ".") Then
            If (Not Label4.Text.Contains(".")) Then
                Label4.Text = Label4.Text + b.Text
            End If
        Else
            Label4.Text = Label4.Text + b.Text
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ComboBox1.SelectedIndex = -1
        DataGridView1.Rows.Clear()
        DataGridView1.Refresh()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'PointofsalesDataSet2.sales_report' table. You can move, or remove it, as needed.

        'TODO: This line of code loads data into the 'PointofsalesDataSet1.sales_report' table. You can move, or remove it, as needed.

        'TODO: This line of code loads data into the 'PointofsalesDataSet.sales_report' table. You can move, or remove it, as needed.

        ComboBox1.Items.Add("Cash")
        ComboBox1.Items.Add("Direct Card")
        ComboBox1.Items.Add("Visa Card")
        ComboBox1.Items.Add("Master Card")
    End Sub

    Dim connect As New SqlConnection("Data Source=DESKTOP-RCROGBA;Initial Catalog=pointofsalesProject;Integrated Security=True")

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If (ComboBox1.Text = "Cash") Then
            change()
        Else
            Label5.Text = ""
            Label4.Text = "0"
        End If
        MessageBox.Show("Payment Successful", "information", MessageBoxButtons.OK)

        connect.Open()
        Dim collect As String = "INSERT INTO sales_report Values('" & Label4.Text & "','" & Label11.Text & "','" & Label9.Text & "','" & Label10.Text & "','" & Label5.Text & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Value & "')"

        Dim command As SqlCommand = New SqlCommand(collect, connect)
        command.ExecuteNonQuery()
        connect.Close()
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        add_cost()

        If (ComboBox1.Text = "Cash") Then
            change()
        Else
            Label5.Text = ""
            Label4.Text = "0"
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click

        Dim cost_of_item As Double = 150
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Coca-Cola" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Coca-Cola", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        ppd.Document = pd
        ppd.ShowDialog()

        Dim height As Integer = DataGridView1.Height
        DataGridView1.Height = (DataGridView1.RowCount + 1) * DataGridView1.RowTemplate.Height
        bitmap = New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(bitmap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()
        DataGridView1.Height = height

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("custom", 180, 300)
        pd.DefaultPageSettings = pagesetup
        e.Graphics.DrawImage(bitmap, 10, 10)
    End Sub

    Private Sub pd_PrintPage(sender As Object, e As PrintPageEventArgs) Handles pd.PrintPage
        Dim f8 As New Font("Calibri", 14, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 16, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 16, FontStyle.Regular)
        Dim f14 As New Font("Calibri", 18, FontStyle.Bold)

        Dim leftmargin As Integer = pd.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = pd.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = pd.DefaultPageSettings.PaperSize.Width

        ' font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat
        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "--------------------------------------------------------------------------------------------------------------------------------"
        e.Graphics.DrawString("Ace mall ", f14, Brushes.Black, centermargin, 10, center)
        e.Graphics.DrawString("Awolowo road, Old bodija, Ibadan, Oyo State ", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString(" Tel +2348179665932 ", f8, Brushes.Black, centermargin, 60, center)

        e.Graphics.DrawString(" Invoice ID ", f8, Brushes.Black, 0, 55)
        e.Graphics.DrawString(" : ", f8, Brushes.Black, 50, 60)
        e.Graphics.DrawString(" KBG234561 ", f8, Brushes.Black, 80, 65)

        e.Graphics.DrawString(" Cashier ", f8, Brushes.Black, 0, 80)
        e.Graphics.DrawString(" : ", f8, Brushes.Black, 50, 82)
        e.Graphics.DrawString(TextBox1.Text, f8, Brushes.Black, 80, 85)

        e.Graphics.DrawString(DateTimePicker1.Text, f8, Brushes.Black, 70, 90)

        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 100)

        Dim height As Integer
        Dim i As Long
        DataGridView1.AllowUserToAddRows = False
        For row As Integer = 0 To DataGridView1.RowCount - 1
            height += 15
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(1).Value.ToString, f10, Brushes.Black, 0, 100 + height)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(0).Value.ToString, f10, Brushes.Black, 25, 100 + height)
            i = DataGridView1.Rows(row).Cells(2).Value = Format(i, cost_of_items)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(2).Value.ToString, f10, Brushes.Black, rightmargin, 100 + height, right)
        Next
        Dim height2 As Integer
        height2 = 110 + height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total : ", f8, Brushes.Black, centermargin, 20 + height2, right)
        e.Graphics.DrawString(Label10.Text, f8, Brushes.Black, 330, 205, center)

        e.Graphics.DrawString("Thanks For Shopping with us ", f14, Brushes.Black, centermargin, 40 + height2, center)
        e.Graphics.DrawString("Ace mall ", f14, Brushes.Black, centermargin, 65 + height2, center)
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim cost_of_item As Double = 125
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Bottle-Water" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Bottle-Water", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim cost_of_item As Double = 65
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Indomie" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Indomie", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim cost_of_item As Double = 170
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Can-Malt" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Can-Malt", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim cost_of_item As Double = 1250
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Power-Oil" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Power-Oil", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Dim cost_of_item As Double = 950
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Juice" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Juice", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Dim cost_of_item As Double = 250
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Chocolates" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Chocolates", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub FillByToolStripButton_Click(sender As Object, e As EventArgs)
        Try
            Me.Sales_reportTableAdapter.FillBy(Me.PointofsalesDataSet.sales_report)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

        If DataGridView1.SelectedCells.Count >= 0 Then
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                ListView1.Items.Add(row.Cells(1).Value.ToString())
                ListView1.Items.Add(row.Cells(2).Value.ToString())
            Next
        End If
    End Sub

    Private Sub TextOnly(sender As Object, e As ControlEventArgs) Handles Button39.ControlAdded, Button38.ControlAdded, Button36.ControlAdded, Button35.ControlAdded, Button34.ControlAdded, Button33.ControlAdded, Button32.ControlAdded, Button31.ControlAdded, Button30.ControlAdded, Button29.ControlAdded, Button28.ControlAdded, Button27.ControlAdded, Button26.ControlAdded, Button25.ControlAdded, Button22.ControlAdded, Button19.ControlAdded, Button17.ControlAdded, Button15.ControlAdded, Button14.ControlAdded, Button13.ControlAdded
        cost_of_items()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Me.Hide()
        Login.Show()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim cost_of_item As Double = 300
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Malt" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Malt", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim cost_of_item As Double = 280
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Soda" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Soda", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Dim cost_of_item As Double = 400
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Candies" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Candies", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim cost_of_item As Double = 1600
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Wine" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Wine", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Dim cost_of_item As Double = 350
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Bath-Soap" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Bath-Soap", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim cost_of_item As Double = 500
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Sensodine" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Sensodine", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim cost_of_item As Double = 500
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Chocolates-Bread" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Chocolates-Bread", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim cost_of_item As Double = 850
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Coffee" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Coffee", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Dim cost_of_item As Double = 2500
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Lip-Stick" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Lip-Stick", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim cost_of_item As Double = 3960
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Makeup-set" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Makeup-set", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim cost_of_item As Double = 2350
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Eyeshadow-Stick" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Eyeshadow-Stick", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Dim cost_of_item As Double = 2000
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Face-Brush" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Face-Bush", "1", cost_of_item)
        add_cost()
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        Dim cost_of_item As Double = 16000
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "Shampoo" Then
                row.Cells(1).Value = Double.Parse(row.Cells(1).Value + 1)
                row.Cells(2).Value = Double.Parse(row.Cells(1).Value) * cost_of_item
                Exit Sub
            End If
        Next

        DataGridView1.Rows.Add("Shampoo", "1", cost_of_item)
        add_cost()
    End Sub
End Class
