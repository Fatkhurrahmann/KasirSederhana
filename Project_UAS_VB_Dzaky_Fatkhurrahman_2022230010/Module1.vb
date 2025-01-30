Imports MySql.Data.MySqlClient

Module Module1
    Public con As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public db As String
    Public Sub koneksi()
        db = "Server=localhost;user id=root;password=;database=visual_basic"
        con = New MySqlConnection(db)
        If con.State = ConnectionState.Closed Then con.Open()
    End Sub
End Module
