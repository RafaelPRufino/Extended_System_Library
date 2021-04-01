Imports System.Data.SqlClient
Imports Extended_System_Library.Db.Connections
Imports Extended_System_Library.Db.Parameters

Namespace Db
    Namespace Connectors
        Public Class LinkConnection
            Inherits Commands
            Implements ICloneable


            Public Sub New()
                Try
                    dataconnectionConnection = DataConnection.Instance("")
                    parametercollectionParameters = ParametersCollection.Instance
                Catch ex As Exception

                End Try
            End Sub
            '/**
            '* author Rafael Rufino
            '* date 06/09/2018
            '* property Connection

            '* variable dataconnectionConnection
            '* type DataConnection
            '**/

            Private dataconnectionConnection As DataConnection

            ''' <summary>
            ''' Get variable dataconnectionConnection
            ''' @return type DataConnection
            ''' </summary>
            Protected ReadOnly Property Connection() As DataConnection
                Get
                    Return dataconnectionConnection
                End Get
            End Property

            '/**
            '* author Rafael Rufino
            '* date 06/09/2018
            '* property Parameters

            '* variable parametercollectionParameters
            '* type ParameterCollection
            '**/

            Private parametercollectionParameters As ParametersCollection

            ''' <summary>
            ''' Get and Set variable parametercollectionParameters
            ''' @return and @param type ParameterCollection
            ''' </summary>
            Public Property Parameters() As ParametersCollection
                Get
                    Return parametercollectionParameters
                End Get
                Set(ByVal newParametercollectionParameters As ParametersCollection)
                    parametercollectionParameters = newParametercollectionParameters
                End Set
            End Property

            ''' <summary>
            ''' Executa query em banco e retorna uma DataTableReader
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <returns> ExecuteDataTableReader </returns>
            Public Function ExecuteDataTableReader(ByVal commandText As String) As DataTableReader
                Try
                    Return New DataTableReader(MyBase.innerExecuteDataTable(Connection, commandText, Parameters))
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Executa query em banco e retorna uma DataTableReader
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> ExecuteDataTableReader </returns>
            Public Function ExecuteDataTableReader(ByVal commandText As String, ByVal commandType As Data.CommandType) As DataTableReader
                Try
                    Return New DataTableReader(MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType))
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Executa query em banco e retorna uma ExecuteDataTable
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> ExecuteDataTable </returns>
            Public Function ExecuteDataTable(ByVal commandText As String, ByVal commandType As Data.CommandType) As DataTable
                Try
                    Return MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Executa query em banco e retorna uma List(Of Object)
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> Tolist </returns>
            Public Function Tolist(ByVal commandText As String, ByVal commandType As Data.CommandType) As List(Of Object)
                Try
                    Dim dataTable = MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                    Dim list = New List(Of Object)()
                    Dim columns = dataTable.Columns.Cast(Of DataColumn)()

                    For Each row In dataTable.Rows.Cast(Of DataRow)()

                        Dim item = New Dictionary(Of String, Object)
                        For Each column In columns
                            Dim value = row.Item(column)
                            If (value Is DBNull.Value) = False Then
                                item.Add(column.ColumnName, value)
                            Else
                                item.Add(column.ColumnName, Nothing)
                            End If
                        Next

                        list.Add(item)
                    Next

                    Return list
                Catch ex As Exception
                    Throw ex
                End Try
            End Function


            ''' <summary>
            ''' Executa query em banco e retorna uma List(Of T)
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> Tolist </returns>
            Public Function Tolist(Of T As {Class, New})(ByVal commandText As String, ByVal commandType As Data.CommandType) As List(Of T)
                Try
                    Dim dataTable = MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                    Dim list = New List(Of T)()
                    Dim columns = dataTable.Columns.Cast(Of DataColumn)()

                    For Each row In dataTable.Rows.Cast(Of DataRow)()
                        Dim item = New Dictionary(Of String, Object)

                        For Each column In columns
                            Dim value = row.Item(column)
                            If (value Is DBNull.Value) = False Then
                                item.Add(column.ColumnName, value)
                            Else
                                item.Add(column.ColumnName, Nothing)
                            End If
                        Next

                        list.Add(Extended_System_Library.Lang.Property.toObject(Of T)(item))
                    Next

                    Return list
                Catch ex As Exception
                    Throw ex
                End Try
            End Function


            ''' <summary>
            ''' Executa query em banco e retorna uma List(Of T)
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> Tolist </returns>
            Public Function AsTypeList(Of T As {Class, New})(ByVal commandText As String, ByVal commandType As Data.CommandType) As List(Of T)
                Try
                    Dim dataTable = MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                    Return AsTypeList(Of T)(dataTable)
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Executa query em banco e retorna uma List(Of T)
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> Tolist </returns>
            Public Function AsType(Of T As {Class, New})(ByVal commandText As String, ByVal commandType As Data.CommandType) As T
                Try
                    Dim dataTable = MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                    Return AsType(Of T)(dataTable)
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            Public Function AsTypeList(Of T As {Class, New})(ByVal dataTable As DataTable) As List(Of T)
                Try
                    Dim list = New List(Of T)()
                    Dim columns = dataTable.Columns.Cast(Of DataColumn)()

                    For Each row In dataTable.Rows.Cast(Of DataRow)()
                        Dim item = New Dictionary(Of String, Object)

                        For Each column In columns
                            Dim value = row.Item(column)
                            If (value Is DBNull.Value) = False Then
                                item.Add(column.ColumnName, value)
                            Else
                                item.Add(column.ColumnName, Nothing)
                            End If
                        Next

                        list.Add(Extended_System_Library.Lang.Property.asType(Of T)(item))
                    Next

                    Return list
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            Public Function AsType(Of T As {Class, New})(ByVal dataTable As DataTable) As T
                Try
                    Dim list = New T
                    Dim columns = dataTable.Columns.Cast(Of DataColumn)()

                    For Each row In dataTable.Rows.Cast(Of DataRow)()
                        Dim item = New Dictionary(Of String, Object)

                        For Each column In columns
                            Dim value = row.Item(column)
                            If (value Is DBNull.Value) = False Then
                                item.Add(column.ColumnName, value)
                            Else
                                item.Add(column.ColumnName, Nothing)
                            End If
                        Next

                        list = (Extended_System_Library.Lang.Property.asType(Of T)(item))
                    Next

                    Return list
                Catch ex As Exception
                    Throw ex
                End Try
            End Function
            ''' <summary>
            ''' Executa query em banco e retorna uma List(Of T)
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <param name="AnonymousType"> T </param>
            ''' <returns> Tolist </returns>
            Public Function Tolist(Of T)(ByVal commandText As String, ByVal commandType As Data.CommandType, ByVal AnonymousType As T) As List(Of T)
                Try
                    Dim dataTable = MyBase.innerExecuteDataTable(Connection, commandText, Parameters, commandType)
                    Dim list = New List(Of T)()
                    Dim columns = dataTable.Columns.Cast(Of DataColumn)()

                    For Each row In dataTable.Rows.Cast(Of DataRow)()
                        Dim item = New Dictionary(Of String, Object)

                        For Each column In columns
                            Dim value = row.Item(column)
                            If (value Is DBNull.Value) = False Then
                                item.Add(column.ColumnName, value)
                            Else
                                item.Add(column.ColumnName, Nothing)
                            End If
                        Next

                        list.Add(Extended_System_Library.Lang.Property.toAnonymousType(Of T)(item, AnonymousType))
                    Next

                    Return list
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Executa um comando no Banco
            ''' </summary>
            ''' <param name="commandText"> Query String SQL </param>
            ''' <param name="CommandType"> Tipo de Query: Procedure, Text </param>
            ''' <returns> ExecuteCommand </returns>
            Public Function ExecuteCommand(ByVal commandText As String, ByVal commandType As Data.CommandType) As Boolean
                Try
                    Return MyBase.innerExecuteCommand(Connection, commandText, Parameters, commandType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            Protected Overrides Sub DisposeObjects()
                Try
                    Parameters.Dispose()
                Catch ex As Exception

                End Try
            End Sub

            ''' <summary>
            ''' Alterar conexão com banco de dados
            ''' <param name="stringConnection"> String de Conexão </param>
            ''' </summary>

            Public Sub ChangeConnection(ByVal stringConnection As String)
                Try
                    Connection.ChangeConnection(stringConnection)
                Catch ex As Exception
                    Throw New Exception("Erro ao alterar a conexão", ex)
                End Try
            End Sub

            ''' <summary>
            ''' Alterar conexão com banco de dados
            ''' <param name="targetConnection"> SqlConnection </param>
            ''' </summary>

            Public Sub ChangeConnection(ByVal targetConnection As SqlConnection)
                Try
                    Connection.ChangeConnection(targetConnection.ConnectionString)
                Catch ex As Exception
                    Throw New Exception("Erro ao alterar a conexão", ex)
                End Try
            End Sub

            ''' <summary>
            ''' Alterar conexão com banco de dados
            ''' <param name="Username"> Usuário de Conexão </param>
            ''' <param name="Password"> Senha de Conexão </param>
            ''' <param name="Server"> Servidor de Conexão </param>
            ''' <param name="DataBase"> Banco de Dados </param>
            ''' </summary>
            Public Sub ChangeConnection(ByVal username As String, ByVal password As String, ByVal Server As String, ByVal DataBase As String)
                Try

                    Connection.ChangeConnection(DataConnection.buildStringConnection(username, password, Server, DataBase))
                Catch ex As Exception
                    Throw New Exception("Erro ao alterar a conexão", ex)
                End Try
            End Sub

            Public Function ChangeDatabase(ByVal database As String) As Boolean
                ChangeDatabase = Connection.ChangeDatabase(database)
            End Function

            Public Function Clone() As Object Implements ICloneable.Clone
                Try
                    Clone = New LinkConnection
                    DirectCast(Clone, LinkConnection).dataconnectionConnection = Me.Connection.Clone
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public ReadOnly Property ToSqlConnection As SqlClient.SqlConnection
                Get
                    Return Me.Connection.connection
                End Get
            End Property

            Public Function GetValue(Of T)(ByRef sqlReader As Object, index As String) As T
                Try
                    If sqlReader IsNot Nothing AndAlso Not sqlReader.IsClosed Then
                        Dim value As Object = sqlReader(index)
                        If value Is DBNull.Value Then
                            Return Nothing
                        End If
                        If TypeOf value Is T Then
                            Return DirectCast(value, T)
                        Else
                            Throw New Exception("Verifique o tipo de dado " + GetType(T).FullName)
                        End If
                    End If
                    Return Nothing
                Catch ex As Exception
                    Throw ex
                End Try
            End Function
        End Class
    End Namespace
End Namespace