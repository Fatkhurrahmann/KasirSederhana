﻿Public Class FormLapDataMaster
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormLapDataMasterAdmin.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FormLapDataMasterPelanggan.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormLapDataMasterBarang.ShowDialog()
    End Sub
End Class