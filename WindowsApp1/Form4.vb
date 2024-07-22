Imports MySql.Data.MySqlClient
Public Class Form4
    Dim connectionString As String = "data source = localhost; user id = root; database = db_xelvrim"
    Dim connection As MySqlConnection

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New MySqlConnection(connectionString)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim selectedCarPartIDs As New List(Of Integer)

        If CheckBox1.Checked Then selectedCarPartIDs.Add(1)
        If CheckBox2.Checked Then selectedCarPartIDs.Add(2)
        If CheckBox3.Checked Then selectedCarPartIDs.Add(3)
        If CheckBox4.Checked Then selectedCarPartIDs.Add(4)
        If CheckBox5.Checked Then selectedCarPartIDs.Add(5)
        If CheckBox6.Checked Then selectedCarPartIDs.Add(6)
        If CheckBox7.Checked Then selectedCarPartIDs.Add(7)
        If CheckBox8.Checked Then selectedCarPartIDs.Add(8)
        If CheckBox9.Checked Then selectedCarPartIDs.Add(9)
        If CheckBox10.Checked Then selectedCarPartIDs.Add(10)

        Dim form5 As New Form5()
        form5.SelectedCarPartIDs = selectedCarPartIDs
        form5.Show()
        Me.Hide()
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim searchTerm As String = Textbox1.Text
        If String.IsNullOrEmpty(searchTerm) Then
            MsgBox("Please enter a search term.")
            Return
        End If

        Try

            connection.Open()


            Dim query As String = "SELECT * FROM CarPart WHERE Name LIKE @searchTerm"
            Dim cmd As MySqlCommand = New MySqlCommand(query, connection)


            cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim table As DataTable = New DataTable()

            adapter.Fill(table)


            DataGridView1.DataSource = Nothing

            DataGridView1.DataSource = table


            If table.Rows.Count = 0 Then
                MsgBox("Item not available.")
            End If
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message)
        Finally

            connection.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Form6.Show()
        Me.Hide()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Form5.Show()
        Me.Hide()
    End Sub

End Class