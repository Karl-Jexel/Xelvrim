Imports MySql.Data.MySqlClient

Public Class Form8
    Dim connectionString As String = "data source=localhost;user id=root;database=db_talaba"

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadData()
        End Sub

        Private Sub LoadData()
            Dim dt As New DataTable()

            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()
                    Dim query As String = "SELECT * FROM customer"
                    Dim cmd As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using

                DataGridView1.DataSource = dt
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading data: " & ex.Message)
            End Try
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

                Dim itemIdColumnName As String = "CustomerID"
                If DataGridView1.Columns.Contains(itemIdColumnName) Then
                    Dim itemIdToDelete As Integer
                    If Integer.TryParse(selectedRow.Cells(itemIdColumnName).Value.ToString(), itemIdToDelete) Then
                        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo)
                        If confirmResult = DialogResult.Yes Then
                            DeleteItem(itemIdToDelete)
                            LoadData()
                        End If
                    Else
                        MessageBox.Show("Invalid Customer ID.")
                    End If
                Else
                    MessageBox.Show("Column '" & itemIdColumnName & "' does not exist in the DataGridView.")
                End If
            Else
                MessageBox.Show("Please select a row to delete.")
            End If
        End Sub

        Private Sub DeleteItem(itemId As Integer)
            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim checkSQL As String = "SELECT COUNT(*) FROM orders WHERE CustomerID = @CustomerID"
                    Dim checkCmd As New MySqlCommand(checkSQL, connection)
                    checkCmd.Parameters.AddWithValue("@CustomerID", itemId)
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count > 0 Then
                        MessageBox.Show("Cannot delete customer because dependent records exist.")
                        Return
                    End If

                    Dim deleteSQL As String = "DELETE FROM customer WHERE CustomerID = @CustomerID"
                    Dim deleteCmd As New MySqlCommand(deleteSQL, connection)
                    deleteCmd.Parameters.AddWithValue("@CustomerID", itemId)
                    Dim rowsAffected As Integer = deleteCmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Customer deleted successfully.")
                    Else
                        MessageBox.Show("No customer found with the given ID.")
                    End If
                End Using
            Catch ex As MySqlException
                MessageBox.Show("MySQL Error: " & ex.Message & vbCrLf & ex.StackTrace)
            Catch ex As Exception
                MessageBox.Show("General Error: " & ex.Message & vbCrLf & ex.StackTrace)
            End Try
        End Sub

        Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

                ' Ensure this matches the column names in your DataGridView
                TextBox1.Text = selectedRow.Cells("Name").Value.ToString()
                TextBox2.Text = selectedRow.Cells("Email").Value.ToString()
                TextBox3.Text = selectedRow.Cells("Phone").Value.ToString()
                TextBox4.Text = selectedRow.Cells("Address").Value.ToString()
            End If
        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
                Dim customerId As Integer

                Dim itemIdColumnName As String = "CustomerID"
                If Integer.TryParse(selectedRow.Cells(itemIdColumnName).Value.ToString(), customerId) Then
                    UpdateCustomer(customerId, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text)
                    LoadData()
                Else
                    MessageBox.Show("Invalid Customer ID.")
                End If
            Else
                MessageBox.Show("Please select a row to update.")
            End If
        End Sub

        Private Sub UpdateCustomer(customerId As Integer, name As String, email As String, phone As String, address As String)
            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim updateSQL As String = "UPDATE customer SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE CustomerID = @CustomerID"
                    Dim updateCmd As New MySqlCommand(updateSQL, connection)
                    updateCmd.Parameters.AddWithValue("@Name", name)
                    updateCmd.Parameters.AddWithValue("@Email", email)
                    updateCmd.Parameters.AddWithValue("@Phone", phone)
                    updateCmd.Parameters.AddWithValue("@Address", address)
                    updateCmd.Parameters.AddWithValue("@CustomerID", customerId)
                    Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Customer updated successfully.")
                    Else
                        MessageBox.Show("No customer found with the given ID.")
                    End If
                End Using
            Catch ex As MySqlException
                MessageBox.Show("MySQL Error: " & ex.Message & vbCrLf & ex.StackTrace)
            Catch ex As Exception
                MessageBox.Show("General Error: " & ex.Message & vbCrLf & ex.StackTrace)
            End Try
        End Sub

End Class