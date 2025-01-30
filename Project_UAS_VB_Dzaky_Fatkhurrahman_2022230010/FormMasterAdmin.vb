Imports MySql.Data.MySqlClient

Public Class FormMasterAdmin
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Call koneksi()
        da = New MySqlDataAdapter("SELECT id_user, username, status FROM user", con)
        ds = New DataSet()
        da.Fill(ds, "user")
        DataGridView1.DataSource = ds.Tables("user")
        DataGridView1.ReadOnly = True
    End Sub

    Sub isi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.Items.Add("ADMIN")
        ComboBox1.Items.Add("USER")
    End Sub

    Private Sub FormMasterAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim InputData As String = "INSERT INTO user (id_user, username, status) VALUES (@id_user, @username, @status)"
                cmd = New MySqlCommand(InputData, con)
                cmd.Parameters.AddWithValue("@id_user", TextBox3.Text)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@status", ComboBox1.Text)
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim updateData As String = "UPDATE user SET username = @username, status = @status WHERE id_user = @id_user"
                cmd = New MySqlCommand(updateData, con)
                cmd.Parameters.AddWithValue("@id_user", TextBox3.Text)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@status", ComboBox1.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Update Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From user where id_user='" & TextBox3.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Kode Admin tidak Ada")
            Else
                TextBox3.Text = rd.Item("id_user")
                TextBox1.Text = rd.Item("username")
                TextBox2.Text = rd.Item("Password")
                ComboBox1.Text = rd.Item("status")
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua Field")
            Else
                Call koneksi()
                Dim hapusData As String = "DELETE FROM user WHERE id_user = @id_user"
                cmd = New MySqlCommand(hapusData, con)
                cmd.Parameters.AddWithValue("@id_user", TextBox3.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If

    End Sub

    Sub NomorOtomatis()
        Call koneksi()
        cmd = New MySqlCommand("Select * from user where id_user in (select max(id_user) from user)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "USR" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "USR" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox3.Text = UrutanKode
    End Sub
End Class
