Imports MySql.Data.MySqlClient

Public Class FormTransjual
    Dim TglMySQL As String

    Sub KondisiAwal()
        LBLNamaPlg.Text = ""
        LBLAlamat.Text = ""
        LBLTelephone.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = FormMenuUtama.STLabel4.Text
        LBLKembali.Text = ""
        TextBox2.Text = ""
        LBLNamaBarang.Text = ""
        LBLHargaBarang.Text = ""
        TextBox3.Text = ""
        TextBox3.Enabled = False
        LBLItem.Text = ""
        Call MunculPelanggan()
        Call NomorOtomatis()
        Call BuatKolom()
        Label9.Text = "0"
        TextBox1.Text = ""
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub

    Private Sub FormTransjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Sub MunculPelanggan()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New MySqlCommand("Select * from Pelanggan", con)
        rd = cmd.ExecuteReader()
        Do While rd.Read()
            ComboBox1.Items.Add(rd.Item(0))
        Loop
        rd.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New MySqlCommand("Select * from Pelanggan where id_pelanggan ='" & ComboBox1.Text & "'", con)
        rd = cmd.ExecuteReader()
        rd.Read()
        If rd.HasRows Then
            LBLNamaPlg.Text = rd!username
            LBLAlamat.Text = rd!alamat
            LBLTelephone.Text = rd!no_hp
        End If
        rd.Close()
    End Sub

    Sub NomorOtomatis()
        Call koneksi()
        cmd = New MySqlCommand("Select * from jual where no_jual in (select max(no_jual) from jual)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader()
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "J" + Format(Now, "yyMMdd") + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 9) + 1
            UrutanKode = "J" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLNojual.Text = UrutanKode
        rd.Close()
    End Sub

    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode")
        DataGridView1.Columns.Add("Nama", "Nama Barang")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Jumlah", "Jumlah")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From barang where kode_barang ='" & TextBox2.Text & "'", con)
            rd = cmd.ExecuteReader()
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Kode barang tidak Ada")
            Else
                TextBox2.Text = rd.Item("kode_barang")
                LBLNamaBarang.Text = rd.Item("nama_barang")
                LBLHargaBarang.Text = rd.Item("harga_barang")
                TextBox3.Enabled = True
            End If
            rd.Close()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If LBLNamaBarang.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Silahkan masukan Kode Barang dan Tekan ENTER!")
        Else
            DataGridView1.Rows.Add(New String() {TextBox2.Text, LBLNamaBarang.Text, LBLHargaBarang.Text, TextBox3.Text, Val(LBLHargaBarang.Text) * Val(TextBox3.Text)})
            Call RumusSubtotal()
            TextBox2.Text = ""
            LBLNamaBarang.Text = ""
            LBLHargaBarang.Text = ""
            TextBox3.Text = ""
            TextBox3.Enabled = False
            Call RumusCariItem()
        End If
    End Sub

    Sub RumusSubtotal()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitung += DataGridView1.Rows(i).Cells(4).Value
        Next
        Label9.Text = hitung.ToString()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(Label9.Text) Then
                MsgBox("Pembayaran Kurang!")
            ElseIf Val(TextBox1.Text) = Val(Label9.Text) Then
                LBLKembali.Text = "0"
            ElseIf Val(TextBox1.Text) > Val(Label9.Text) Then
                LBLKembali.Text = (Val(TextBox1.Text) - Val(Label9.Text)).ToString()
                Button1.Focus()
            End If
        End If
    End Sub

    Sub RumusCariItem()
        Dim HitungItem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            HitungItem += DataGridView1.Rows(i).Cells(3).Value
        Next
        LBLItem.Text = HitungItem.ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If LBLKembali.Text = "" Or LBLNamaPlg.Text = "" Or Label9.Text = "" Then
            MsgBox("Transaksi Tidak Ada, silahkan lakukan transaksi terlebih dahulu")
        Else
            TglMySQL = Format(Today, "yyyy-MM-dd")
            Dim SimpanJual As String = "INSERT INTO jual VALUES ('" & LBLNojual.Text & "', '" & TglMySQL & "', '" & LBLJam.Text & "', '" & LBLItem.Text & "', '" & Label9.Text & "', '" & TextBox1.Text & "', '" & LBLKembali.Text & "','" & ComboBox1.Text & "', '" & FormMenuUtama.STLabel2.Text & "')"

            ' Using statement to ensure cmd is disposed
            Using cmd As New MySqlCommand(SimpanJual, con)
                cmd.ExecuteNonQuery()
            End Using

            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "INSERT INTO detail_jual VALUES('" & LBLNojual.Text & "','" & DataGridView1.Rows(Baris).Cells(0).Value & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "', '" & DataGridView1.Rows(Baris).Cells(2).Value & "', '" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "')"

                ' Using statement to ensure cmd is disposed
                Using cmd As New MySqlCommand(SimpanDetail, con)
                    cmd.ExecuteNonQuery()
                End Using

                ' Using statement to ensure cmd and reader are disposed
                Using cmd As New MySqlCommand("SELECT * FROM barang WHERE kode_barang = @kode_barang", con)
                    cmd.Parameters.AddWithValue("@kode_barang", DataGridView1.Rows(Baris).Cells(0).Value)

                    Using rd As MySqlDataReader = cmd.ExecuteReader()
                        If rd.Read() Then
                            Dim KurangiStok As String = "UPDATE barang SET jumlah_barang = @jumlah_barang WHERE kode_barang = @kode_barang"
                            Dim jumlahBarang As Integer = rd.Item("jumlah_barang") - DataGridView1.Rows(Baris).Cells(3).Value
                            rd.Close() ' Close the reader before executing the next command

                            ' Using statement to ensure cmd is disposed
                            Using cmdUpdate As New MySqlCommand(KurangiStok, con)
                                cmdUpdate.Parameters.AddWithValue("@jumlah_barang", jumlahBarang)
                                cmdUpdate.Parameters.AddWithValue("@kode_barang", DataGridView1.Rows(Baris).Cells(0).Value)
                                cmdUpdate.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End Using
            Next

            ' Buat list untuk menyimpan item
            Dim itemList As New List(Of String())
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    itemList.Add(New String() {row.Cells(0).Value.ToString(), row.Cells(1).Value.ToString(), row.Cells(2).Value.ToString(), row.Cells(3).Value.ToString(), row.Cells(4).Value.ToString()})
                End If
            Next

            Call KondisiAwal()
            MsgBox("Transaksi Telah Berhasil Disimpan")

            ' Tampilkan FormStruk setelah transaksi berhasil disimpan
            Dim formStruk As New FormStruk(LBLNojual.Text, TglMySQL, LBLNamaPlg.Text, LBLAlamat.Text, LBLTelephone.Text, LBLAdmin.Text, itemList, Label9.Text, LBLKembali.Text)
            formStruk.Show()
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub LBLNojual_Click(sender As Object, e As EventArgs) Handles LBLNojual.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
