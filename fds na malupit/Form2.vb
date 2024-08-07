Imports MySql.Data.MySqlClient

Public Class Form2
    Dim connectionString As String = "data source=localhost;user id=root;database=db_talaba"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            Using connection As New MySqlConnection(connectionString)
                connection.Open()


                Dim sql As String = "INSERT INTO Customer (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)"
                Dim cmd As New MySqlCommand(sql, connection)


                cmd.Parameters.AddWithValue("@Name", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Email", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Phone", TextBox3.Text)
                cmd.Parameters.AddWithValue("@Address", TextBox4.Text)

                cmd.ExecuteNonQuery()

                MsgBox("Customer Inserted Successfully")
            End Using

            Form4.Show()
            Me.Hide()

        Catch ex As Exception

            MsgBox("An error occurred: " & ex.Message)
        End Try
    End Sub
End Class
