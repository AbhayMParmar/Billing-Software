Imports System.Data.OleDb
Public Class Product_Details1
    Dim cn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim adp As New OleDbDataAdapter
    Dim ds As New DataSet
    Private Sub Product_Details1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DELL\source\repos\Login\Login\Database1.accdb"

        cn.Open()
        display()
        cn.Close()
    End Sub
    Public Sub display()
        ds.Clear()
        adp = New OleDbDataAdapter("Select * from product", cn)
        adp.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninsert.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("insert into product values(" & Textid.Text & ",
              '" & TextBox2.Text & "'," & TextBox3.Text & ")", cn)
            cmd.ExecuteNonQuery()


            MessageBox.Show("Record Insert Successfull", "Product Details", MessageBoxButtons.OK)
            display()

        Catch ex As Exception
            MessageBox.Show("Insert Error ", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        End Try
        cn.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("select * from product where product_id=" & Textid.Text & "", cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                TextBox2.Text = dr.Item(1)
                TextBox3.Text = dr.Item(2)
            End While
        Catch ex As Exception
            MsgBox(ex.ToString, "Record Not Found")
        End Try
        cn.Close()

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("update product set product_name='" & TextBox2.Text & "'
           ,product_price=" & TextBox3.Text & " where product_id=" & Textid.Text & "", cn)
            cmd.ExecuteNonQuery()
            MsgBox("Recored update")
            display()

        Catch ex As Exception
            MsgBox("update details")
        End Try
        cn.Close()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            cn.Open()
            cmd = New OleDbCommand("delete from product where product_id=" & Textid.Text & "", cn)
            cmd.ExecuteNonQuery()
            MsgBox("Recored Delete")
            display()

        Catch ex As Exception
            MsgBox("Delete details")
        End Try
        cn.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Textid.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        Textid.Text = DataGridView1.Item(0, i).Value
        TextBox2.Text = DataGridView1.Item(1, i).Value
        TextBox3.Text = DataGridView1.Item(2, i).Value
    End Sub



    Private Sub btnsales_Click(sender As Object, e As EventArgs) Handles btnsales.Click
        Bill.Show()
    End Sub

    Private Sub btnreport_Click(sender As Object, e As EventArgs) Handles btnreport.Click
        report1.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class