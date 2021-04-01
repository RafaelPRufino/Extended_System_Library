Namespace Db
    Namespace Connections
        <Serializable()>
        Public Class DataConnection
            Inherits Dispose
            '/**
            '* author Rafael Rufino
            '* date 06/09/2018
            '* property StringConnection

            '* variable stringStringConnection
            '* type String
            '**/

            Private stringStringConnection As String

            ''' <summary>
            ''' Get and Set variable stringStringConnection
            ''' @return and @param type String
            ''' </summary>
            Protected Property StringConnection() As String
                Get
                    Return stringStringConnection
                End Get
                Set(ByVal newStringStringConnection As String)
                    stringStringConnection = newStringStringConnection
                End Set
            End Property

            Private _conn As New SqlClient.SqlConnection

            Public ReadOnly Property connection() As SqlClient.SqlConnection
                Get
                    Try
                        If open() = False Then
                            wait()
                        End If
                    Catch ex As Exception

                    End Try
                    Return _conn
                End Get
            End Property

            Private Sub New()
            End Sub

            ''' <summary>
            ''' Instance new Object Class DataConnection
            ''' </summary>
            ''' <param name="UserBd"> Usuário de Conexão </param>
            ''' <param name="SenhaBd"> Senha de Conexão </param>
            ''' <param name="servidorBd"> Servidor de Conexão </param>
            ''' <param name="bancoBaseBd"> Banco de Dados </param>
            ''' <returns> Type DataConnection </returns>
            Public Shared Function buildStringConnection(ByVal userBd As String, ByVal senhaBd As String, ByVal servidorBd As String, ByVal bancoBaseBd As String) As String
                Try
                    Dim stringConnection As New System.Text.StringBuilder

                    stringConnection = New System.Text.StringBuilder
                    stringConnection.Append("Password=")
                    stringConnection.Append(senhaBd)
                    stringConnection.Append(";Persist Security Info=True;User ID=")
                    stringConnection.Append(userBd)
                    stringConnection.Append(";Initial Catalog=")
                    stringConnection.Append(bancoBaseBd)
                    stringConnection.Append(";Data Source=")
                    stringConnection.Append(servidorBd)
                    stringConnection.Append(";MultipleActiveResultSets=True")

                    Return stringConnection.ToString
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Instance new Object Class DataConnection
            ''' </summary>
            ''' <param name="UserBd"> Usuário de Conexão </param>
            ''' <param name="SenhaBd"> Senha de Conexão </param>
            ''' <param name="servidorBd"> Servidor de Conexão </param>
            ''' <param name="bancoBaseBd"> Banco de Dados </param>
            ''' <returns> Type DataConnection </returns>
            Public Shared Function Instance(ByVal userBd As String, ByVal senhaBd As String, ByVal servidorBd As String, ByVal bancoBaseBd As String) As DataConnection
                Dim localDataConnection As DataConnection
                localDataConnection = New DataConnection()
                localDataConnection.StringConnection = DataConnection.buildStringConnection(userBd, senhaBd, servidorBd, bancoBaseBd)
                Return localDataConnection
            End Function

            ''' <summary>
            ''' Instance new Object Class DataConnection
            ''' </summary>
            ''' <param name="localStringStringConnection"> String de Conexão </param>
            ''' <returns> Type DataConnection </returns>
            Public Shared Function Instance(ByVal localStringStringConnection As String) As DataConnection
                Try
                    Dim localDataConnection As DataConnection
                    localDataConnection = New DataConnection()
                    localDataConnection.stringStringConnection = localStringStringConnection
                    Return localDataConnection
                Catch ex As Exception

                End Try
            End Function

            Public Function open() As Boolean
                Try
                    Try
                        _conn.Close()
                    Catch ex As Exception

                    End Try

                    _conn.ConnectionString = StringConnection.ToString
                    _conn.Open()
                    SqlClient.SqlConnection.ClearPool(_conn)
                    Return True
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            Public Function close() As Boolean
                Try
                    _conn.Close()
                    Return True
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            Public Function wait() As Boolean
                Try
                    While _conn.State = ConnectionState.Closed
                        Me.connection.ToString()
                    End While

                    While _conn.State = ConnectionState.Connecting
                    End While

                    Return True
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Alterar conexão com banco de dados
            ''' </summary>
            ''' <param name="stringConnection"> String de Conexão </param>
            Public Sub ChangeConnection(ByVal stringConnection As String)
                Try
                    Me.StringConnection = stringConnection
                Catch ex As Exception

                End Try
            End Sub

            Protected Overrides Sub DisposeObjects()
                Try
                    If _conn IsNot Nothing Then _conn.Dispose()

                    StringConnection = Nothing
                    _conn = Nothing
                Catch ex As Exception

                End Try
            End Sub

            Public Function Clone() As DataConnection
                Clone = New DataConnection
                Clone.ChangeConnection(Me.StringConnection)
            End Function

            Public Function ChangeDatabase(ByVal database As String) As Boolean
                ChangeDatabase = True
                Try
                    Dim password As String
                    Dim server As String
                    Dim username As String

                    password = Lang.String.handleContentTAGNoEdit(Me.StringConnection, "Password=", ";")
                    server = Lang.String.handleContentTAGNoEdit(Me.StringConnection, "Data Source=", ";")
                    username = Lang.String.handleContentTAGNoEdit(Me.StringConnection, "User ID=", ";")

                    Me.StringConnection = DataConnection.buildStringConnection(username, password, server, database)
                Catch ex As Exception
                    ChangeDatabase = False
                End Try
            End Function
        End Class
    End Namespace
End Namespace