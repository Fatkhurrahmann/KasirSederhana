Imports MySql.Data.MySqlClient

Public Class FormMasterPelanggan
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
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
    End Sub

    Private Sub PopulateListView()
        ListView1.Clear()
        ListView1.View = View.Details
        ListView1.Columns.Add("ID User", 70).ToString()
        ListView1.Columns.Add("Username", 100).ToString()
        ListView1.Columns.Add("Alamat", 150).ToString()
        ListView1.Columns.Add("No Hp", 100).ToString()

        Call koneksi()
        Dim cmd As MySqlCommand = New MySqlCommand("SELECT id_pelanggan, username, alamat, no_hp FROM pelanggan", con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim item As New ListViewItem(reader("id_pelanggan").ToString())
            item.SubItems.Add(reader("username").ToString())
            item.SubItems.Add(reader("alamat").ToString())
            item.SubItems.Add(reader("no_hp").ToString())
            ListView1.Items.Add(item)
        End While
        reader.Close()
    End Sub

    Private Sub FormMasterPelanggan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                Dim InputData As String = "INSERT INTO pelanggan (id_pelanggan, username, alamat, no_hp) VALUES (@id_pelanggan, @username,@alamat ,@no_hp)"
                cmd = New MySqlCommand(InputData, con)
                cmd.Parameters.AddWithValue("@id_pelanggan", TextBox3.Text)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBox2.Text)
                cmd.Parameters.AddWithValue("@no_hp", TextBox4.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call KondisiAwal()
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
                Dim updateData As String = "UPDATE pelanggan SET username = @username, status = @status WHERE id_pelanggan = @id_pelanggan"
                cmd = New MySqlCommand(updateData, con)
                cmd.Parameters.AddWithValue("@id_pelanggan", TextBox3.Text)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBox2.Text)
                cmd.Parameters.AddWithValue("@no_hp", TextBox4.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Update Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub



    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From pelanggan where id_pelanggan='" & TextBox3.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Kode Pelanggan tidak Ada")
            Else
                TextBox3.Text = rd.Item("id_pelanggan")
                TextBox1.Text = rd.Item("username")
                TextBox2.Text = rd.Item("alamat")
                TextBox4.Text = rd.Item("no_hp")
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
                Dim hapusData As String = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan"
                cmd = New MySqlCommand(hapusData, con)
                cmd.Parameters.AddWithValue("@id_pelanggan", TextBox3.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub
    Sub NomorOtomatis()
        Call koneksi()
        cmd = New MySqlCommand("Select * from pelanggan where id_pelanggan in (select max(id_pelanggan) from pelanggan)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "PLG" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "PLG" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox3.Text = UrutanKode
    End Sub

End Class
