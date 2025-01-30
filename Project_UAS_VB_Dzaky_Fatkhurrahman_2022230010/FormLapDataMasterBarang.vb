Imports MySql.Data.MySqlClient

Public Class FormLapDataMasterBarang
    Private Sub FormLapDataMasterBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Panggil koneksi ke database
        Call koneksi()

        ' Query untuk mengambil data admin
        Dim query As String = "SELECT kode_barang, nama_barang, harga_barang, jumlah_barang, satuan_barang FROM barang"
        Dim da As New MySqlDataAdapter(query, con)
        Dim ds As New DataSet()

        ' Isi DataSet dengan hasil query
        da.Fill(ds, "barang")

        ' Bind DataGridView dengan DataSet
        DataGridView1.DataSource = ds.Tables("barang")

        ' Atur properti DataGridView
        DataGridView1.ReadOnly = True
        DataGridView1.AutoResizeColumns()

        ' Tampilkan tanggal pada Label
        Label3.Text = DateTime.Now.ToString("dd MMMM yyyy")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class