Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Public Class FormMasterBarang
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        ComboBox1.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Call koneksi()
        PopulateListView()
    End Sub

    Sub isi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        ComboBox1.Enabled = True
        Call MunculSatuan()
    End Sub

    Sub MunculSatuan()
        Call koneksi()
        cmd = New MySqlCommand("select distinct satuan_barang from barang", con)
        rd = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item("satuan_barang"))
        Loop
    End Sub



    Private Sub PopulateListView()
        ListView1.Clear()
        ListView1.View = View.Details
        ListView1.Columns.Add("Kode Barang", 80)
        ListView1.Columns.Add("Nama Barang", 150)
        ListView1.Columns.Add("Harga Barang", 100)
        ListView1.Columns.Add("Jumlah Barang", 100)
        ListView1.Columns.Add("Satuan Barang", 85)

        Call koneksi()
        Dim cmd As MySqlCommand = New MySqlCommand("SELECT kode_barang, nama_barang, harga_barang, jumlah_barang, satuan_barang FROM barang", con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim item As New ListViewItem(reader("kode_barang").ToString())
            item.SubItems.Add(reader("nama_barang").ToString())
            item.SubItems.Add(reader("harga_barang").ToString())
            item.SubItems.Add(reader("jumlah_barang").ToString())
            item.SubItems.Add(reader("satuan_barang").ToString())
            ListView1.Items.Add(item)
        End While
        reader.Close()
    End Sub
    Private Sub FormMasterBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call isi()
            Call NomorOtomatis()
            TextBox3.Enabled = False
            TextBox1.Focus()
        Else

            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim InputData As String = "INSERT INTO barang (kode_barang, nama_barang, harga_barang, jumlah_barang, satuan_barang) VALUES (@kode_barang, @nama_barang, @harga_barang, @jumlah_barang, @satuan_barang)"
                cmd = New MySqlCommand(InputData, con)
                cmd.Parameters.AddWithValue("@kode_barang", TextBox3.Text)
                cmd.Parameters.AddWithValue("@nama_barang", TextBox1.Text)
                cmd.Parameters.AddWithValue("@harga_barang", TextBox2.Text)
                cmd.Parameters.AddWithValue("@jumlah_barang", TextBox4.Text)
                cmd.Parameters.AddWithValue("@satuan_barang", ComboBox1.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call KondisiAwal()
                PopulateListView() 
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim updateData As String = "UPDATE barang SET nama_barang = @nama_barang, harga_barang = @harga_barang, jumlah_barang = @jumlah_barang, satuan_barang = @satuan_barang WHERE kode_barang = @kode_barang"
                Using cmd As New MySqlCommand(updateData, con)
                    cmd.Parameters.AddWithValue("@kode_barang", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@nama_barang", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@harga_barang", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@jumlah_barang", TextBox4.Text)
                    cmd.Parameters.AddWithValue("@satuan_barang", ComboBox1.Text)
                    cmd.ExecuteNonQuery()
                End Using
                MsgBox("Update Data Berhasil")
                Call KondisiAwal()
            End If

        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From barang where kode_barang='" & TextBox3.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Kode Pelanggan tidak Ada")
            Else
                TextBox3.Text = rd.Item("kode_barang")
                TextBox1.Text = rd.Item("nama_barang")
                TextBox2.Text = rd.Item("harga_barang")
                TextBox4.Text = rd.Item("jumlah_barang")
                ComboBox1.Text = rd.Item("satuan_barang")
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim hapusData As String = "DELETE FROM barang WHERE kode_barang = @kode_barang"
                cmd = New MySqlCommand(hapusData, con)
                cmd.Parameters.AddWithValue("@kode_barang", TextBox3.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Sub NomorOtomatis()
        Call koneksi()
        cmd = New MySqlCommand("Select * from barang where kode_barang in (select max(kode_barang) from barang)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "BRG" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "BRG" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox3.Text = UrutanKode
    End Sub
End Class