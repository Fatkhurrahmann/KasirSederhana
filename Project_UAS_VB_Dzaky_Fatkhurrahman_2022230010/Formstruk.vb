Imports System.Windows.Forms

Public Class FormStruk
    Private NoJual As String
    Private Tanggal As String
    Private NamaPelanggan As String
    Private AlamatPelanggan As String
    Private NoTeleponPelanggan As String
    Private NamaAdmin As String
    Private Items As List(Of String())

    ' Tambahkan konstruktor untuk menerima data transaksi
    Public Sub New(noJual As String, tanggal As String, namaPelanggan As String, alamatPelanggan As String, noTeleponPelanggan As String, namaAdmin As String, items As List(Of String()), totalBayar As String, kembalian As String)
        ' Panggil konstruktor dasar
        InitializeComponent()

        ' Set parameter ke variabel
        Me.NoJual = noJual
        Me.Tanggal = tanggal
        Me.NamaPelanggan = namaPelanggan
        Me.AlamatPelanggan = alamatPelanggan
        Me.NoTeleponPelanggan = noTeleponPelanggan
        Me.NamaAdmin = namaAdmin
        Me.Items = items
    End Sub

    Private Sub FormStruk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan data transaksi ke kontrol pada form
        lblNoJual.Text = NoJual
        lblTanggal.Text = Tanggal
        lblNamaAdmin.Text = NamaAdmin

        ' Tampilkan item ke label
        For i As Integer = 0 To Items.Count - 1
            Dim item As String() = Items(i)
            Controls("lblKodeBarang" & (i + 1)).Text = item(0)
            Controls("lblNamaBarang" & (i + 1)).Text = item(1)
            Controls("lblHargaBarang" & (i + 1)).Text = item(2)
            Controls("lblJumlahBarang" & (i + 1)).Text = item(3)
            Controls("lblSubtotal" & (i + 1)).Text = item(4)
        Next
    End Sub

    Private Sub lblNamaPelanggan_Click(sender As Object, e As EventArgs)

    End Sub
End Class
