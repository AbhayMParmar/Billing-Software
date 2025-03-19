Imports System.Data.OleDb

Public Class Home
    Dim NewForm As New Product_Details1
    Dim cn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim adp As New OleDbDataAdapter
    Dim ds As New DataSet

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnproddetails.Click
        NewForm.Show()
    End Sub

    Private Sub btnsales_Click(sender As Object, e As EventArgs) Handles btnsales.Click
        Bill.Show()
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DELL\source\repos\Login\Login\Database1.accdb"

        cn.Open()
        display()
        cn.Close()
    End Sub
    Public Sub display()
        ds.Clear()
        adp = New OleDbDataAdapter("Select * from CustomerBill1", cn)
        adp.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub btnreport_Click(sender As Object, e As EventArgs) Handles btnreport.Click
        report1.Show()
    End Sub
End Class