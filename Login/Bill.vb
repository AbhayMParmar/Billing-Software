
Imports System.Data.OleDb
Public Class Bill
    Dim cn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim adp As New OleDbDataAdapter
    Dim ds As New DataSet
    Private Sub Bill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DELL\source\repos\Login\Login\Database1.accdb"
        cn.Open()
        display()
        cn.Close()

    End Sub
    Public Sub display()
        ds.Clear()
        adp = New OleDbDataAdapter("Select * from billitem1", cn)
        adp.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            cn.Open()

            Dim Payment As String = ""
            If RadioButton1.Checked Then
                Payment = RadioButton1.Text
            ElseIf RadioButton2.Checked Then
                Payment = RadioButton2.Text
            Else
                MessageBox.Show("Error")
            End If

            Dim cmd1 As New OleDbCommand("INSERT INTO billitem1(p_id,p_name,p_price,quntity,total) VALUES ('" & textproductid.Text & "',
                      '" & textprodname.Text & "','" & textprice.Text & "','" & textqunt.Text & "','" & texttotal.Text & "')", cn)
            cmd1.ExecuteNonQuery()

            Dim cmd2 As New OleDbCommand("INSERT INTO CustomerBill1(Customer,Phone_no,Total,Payment) VALUES ('" & textcust.Text & "',
                     '" & textphone.Text & "','" & texttotal.Text & "','" & Payment & "')", cn)
            cmd2.ExecuteNonQuery()

            MessageBox.Show("Record Insert Successfull", "Product Details", MessageBoxButtons.OK)
            display()
        Catch ex As Exception
            MessageBox.Show("Error ", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("select * from product where product_id=" & textproductid.Text & "", cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                textprodname.Text = dr.Item(1)
                textprice.Text = dr.Item(2)
            End While
        Catch ex As Exception
            MsgBox(ex.ToString, "Record Not Found")
        End Try
        cn.Close()

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        textcust.Clear()
        textphone.Clear()
        textprice.Clear()
        textprodname.Clear()
        textproductid.Clear()
        textqunt.Clear()
        texttotal.Clear()
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("delete from billitem1 where p_id=" & textproductid.Text & "", cn)
            cmd.ExecuteNonQuery()
            MsgBox("Recored Delete")
            display()

        Catch ex As Exception
            MsgBox("Error")
        End Try
        cn.Close()

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        textproductid.Text = DataGridView1.Item(0, i).Value
        textprodname.Text = DataGridView1.Item(1, i).Value
        textprice.Text = DataGridView1.Item(2, i).Value
        textqunt.Text = DataGridView1.Item(3, i).Value
        texttotal.Text = DataGridView1.Item(4, i).Value
    End Sub

    Private Sub textprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textprice.TextChanged
        If textprice.Text = "" Or textqunt.Text = "" Then
        Else
            Dim num21 As Double = textprice.Text
            Dim num22 As Double = textqunt.Text
            Dim ans As Double = num21 * num22
            texttotal.Text = (FormatNumber(ans))
        End If

    End Sub

    Private Sub textqunt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textqunt.TextChanged
        If textprice.Text = "" Or textqunt.Text = "" Then
        Else
            Dim num13 As Double = textqunt.Text
            Dim num14 As Double = textprice.Text
            Dim ans As Double = num13 * num14
            texttotal.Text = (FormatNumber(ans))
        End If
        If textprice.Text = "" Then
            textqunt.Text = ""
            texttotal.Text = ""
        Else
        End If

    End Sub
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Home.Show()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles printpre.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub


    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("AARAV SALES", New Font("Arial", 22, FontStyle.Bold), Brushes.Red, New Point(300, 30))
        e.Graphics.DrawString("Anand Mahel road,adajan,Surat,Gujrat", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(280, 60))
        e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------------------------", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(0, 70))
        e.Graphics.DrawString("Customer:-" + textcust.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(20, 80))
        e.Graphics.DrawString("Phone no:-" + textphone.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(20, 100))
        e.Graphics.DrawString("Date" + DateTime.Now(), New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(600, 80))
        e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------------------------", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(0, 120))
        e.Graphics.DrawString("Item Description", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(20, 130))
        e.Graphics.DrawString("Quantity", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(400, 130))
        e.Graphics.DrawString("Price", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(700, 130))
        e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------------------------", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(0, 150))
        e.Graphics.DrawString(textprodname.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(20, 170))
        e.Graphics.DrawString(textqunt.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(400, 170))
        e.Graphics.DrawString(textprice.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(700, 170))
        e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------------------------", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(0, 600))
        e.Graphics.DrawString("Amount-", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(600, 615))
        e.Graphics.DrawString(texttotal.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(700, 615))
        e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------------------------------------", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, New Point(0, 630))
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        PrintDocument1.Print()
    End Sub

    Private Sub texttotal_TextChanged(sender As Object, e As EventArgs) Handles texttotal.TextChanged

    End Sub
End Class
