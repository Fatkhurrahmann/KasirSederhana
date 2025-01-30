Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class FormLogin
    Sub buka()
        FormMenuUtama.LoginToolStripMenuItem.Visible = False
        FormMenuUtama.LogoutToolStripMenuItem.Visible = True
        FormMenuUtama.MasterToolStripMenuItem.Visible = True
        FormMenuUtama.TransaksiToolStripMenuItem.Visible = True
        FormMenuUtama.LaporanToolStripMenuItem.Visible = True
    End Sub

    Sub awal()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
        TextBox2.PasswordChar = "*"

        ' Set Form background image
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Customize TextBox
        TextBox1.BorderStyle = BorderStyle.FixedSingle
        TextBox2.BorderStyle = BorderStyle.FixedSingle

        ' Customize Buttons
        Button1.BackColor = Color.FromArgb(220, 20, 60) ' Dark Red color
        Button1.ForeColor = Color.White
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button2.BackColor = Color.FromArgb(220, 20, 60) ' Dark Red color
        Button2.ForeColor = Color.White
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Kode Admin atau Password Tidak Boleh Kosong!", MsgBoxStyle.Critical)
        Else
            Call koneksi()
            cmd = New MySqlCommand("SELECT * FROM user WHERE username=@username AND password=@password", con)
            cmd.Parameters.AddWithValue("@username", TextBox1.Text)
            cmd.Parameters.AddWithValue("@password", TextBox2.Text)
            rd = cmd.ExecuteReader()

            If rd.Read() Then
                MsgBox("Berhasil Login!", MsgBoxStyle.Information)
                Me.Close()
                Call buka()
                FormMenuUtama.STLabel2.Text = rd("id_user").ToString()
                FormMenuUtama.STLabel4.Text = rd("username").ToString()
                FormMenuUtama.STLabel6.Text = rd("status").ToString()

                If FormMenuUtama.STLabel6.Text = "user" Then
                    FormMenuUtama.AdminToolStripMenuItem.Enabled = False
                End If
            Else
                MsgBox("Kode Admin atau Password Salah!", MsgBoxStyle.Critical)
                TextBox1.Focus()
            End If
            rd.Close()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub FormLogin_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If MouseButtons.Right Then
            ContextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub UsernamePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsernamePasswordToolStripMenuItem.Click
        FormUsername.ShowDialog()
    End Sub
End Class
