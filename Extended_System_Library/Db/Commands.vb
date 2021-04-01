Imports Extended_System_Library.Db.Connectors
Imports Extended_System_Library.Db.Parameters
Imports Extended_System_Library.Db.Connections
Namespace Db
    <Serializable()>
    Public MustInherit Class Commands
        Inherits Dispose
        Protected Overridable Function innerExecuteCommand(ByRef connection As DataConnection, ByVal commandText As String,
                                            Optional ByRef parameters As ParametersCollection = Nothing,
                                            Optional ByVal commandType As Data.CommandType = CommandType.Text) As Boolean
            Try
                Dim command As SqlClient.SqlCommand

                command = New SqlClient.SqlCommand
                With command
                    .CommandText = commandText.Trim
                    .Connection = connection.connection
                    .CommandTimeout = 0
                    .CommandType = commandType
                    If parameters Is Nothing = False Then
                        For Each parameter As Parameter In parameters.ToList
                            .Parameters.Add(parameter.Parameter)
                        Next
                    End If

                    .ExecuteNonQuery()
                End With

                If parameters Is Nothing = False Then
                    Try
                        Dim index As Integer = command.Parameters.Count
                        Dim array(index - 1) As SqlClient.SqlParameter
                        command.Parameters.CopyTo(array, 0)

                        For Each parameter As SqlClient.SqlParameter In array
                            parameters(parameter.ParameterName).Value = parameter.Value
                        Next
                    Catch ex As Exception

                    End Try
                End If


                connection.close()
                connection.Dispose()
                command.Dispose()
                command = Nothing
                connection = Nothing
                Return True
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End Function

        Protected Overridable Function innerExecuteDataTable(ByRef connection As DataConnection, ByVal commandText As String,
                                            Optional ByRef parameters As ParametersCollection = Nothing,
                                            Optional ByVal commandType As Data.CommandType = CommandType.Text) As DataTable
            Try
                Dim command As SqlClient.SqlCommand
                Dim dataAdapter As SqlClient.SqlDataAdapter

                command = New SqlClient.SqlCommand
                innerExecuteDataTable = New DataTable

                With command
                    .CommandText = commandText.Trim
                    .CommandType = commandType
                    .Connection = connection.connection
                    .CommandTimeout = 0

                    If parameters Is Nothing = False Then
                        For Each parameter As Parameter In parameters.ToList
                            .Parameters.Add(parameter.Parameter)
                        Next
                    End If
                End With


                dataAdapter = New SqlClient.SqlDataAdapter(command)
                dataAdapter.SelectCommand.CommandTimeout = 0
                dataAdapter.Fill(innerExecuteDataTable)

                If parameters Is Nothing = False Then
                    Try
                        Dim index As Integer = command.Parameters.Count
                        Dim array(index - 1) As SqlClient.SqlParameter
                        command.Parameters.CopyTo(array, 0)

                        For Each parameter As SqlClient.SqlParameter In array
                            parameters(parameter.ParameterName).Value = parameter.Value
                        Next
                    Catch ex As Exception

                    End Try
                End If

                connection.close()
                connection.Dispose()
                dataAdapter.Dispose()
                command.Dispose()

                command = Nothing
                connection = Nothing
                dataAdapter = Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End Function

        Protected Overridable Function innerExecuteDataTableReader(ByRef connection As DataConnection, ByVal commandText As String, ByRef commandParameters As ParametersCollection, ByVal commandType As Data.CommandType) As DataTableReader
            Try
                Return New DataTableReader(innerExecuteDataTable(connection, commandText, commandParameters, commandType))
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End Function
    End Class
End Namespace