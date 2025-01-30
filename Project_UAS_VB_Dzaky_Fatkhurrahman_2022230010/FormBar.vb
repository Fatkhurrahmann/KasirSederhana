Public Class FormBar

    Private Sub FormBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Logging Out"
        Label2.Text = "%"
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Untuk menghitung waktu berjalan nya
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 2
        ElseIf ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Dim result As DialogResult = MessageBox.Show("Anda telah berhasil logout", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If result = DialogResult.OK Then
                Application.Exit()
            End If
        End If

        ' Perintah untuk membuat loading info
        If ProgressBar1.Value = 20 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Pertama"
        ElseIf ProgressBar1.Value = 40 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Kedua"
        ElseIf ProgressBar1.Value = 50 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Ketiga"
        ElseIf ProgressBar1.Value = 60 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Keempat"
        ElseIf ProgressBar1.Value = 70 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Kelima"
        ElseIf ProgressBar1.Value = 80 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri Sesi Keenam"
        ElseIf ProgressBar1.Value = 90 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Tunggu beberapa saat lagi"
        ElseIf ProgressBar1.Value = 100 Then
            Label1.ForeColor = Color.Black
            Label1.Text = "Mengakhiri semua sesi"
        End If

        ' Perintah untuk percentage
        Label2.Text = Math.Round((ProgressBar1.Value / 100) * 100, 2) & "%"
    End Sub
End Class
