Imports ParameterDirection = System.Data.ParameterDirection
Imports System.Data.SqlClient

Namespace Db
    Namespace Parameters
        '/**
        '* author Rafael Rufino
        '* date 06/09/2018
        '**/
        <Serializable()>
        Public Class Parameter
            Inherits Dispose

            '/**
            '* author Rafael Rufino
            '* date 06/09/2018
            '* property Parameter

            '* variable sqlparameterParameter
            '* type SqlParameter
            '**/

            Private sqlparameterParameter As SqlParameter

            ''' <summary>
            ''' Get and Set variable sqlparameterParameter
            ''' @return and @param type SqlParameter
            ''' </summary>
            Friend Property Parameter() As SqlParameter
                Get
                    Return sqlparameterParameter
                End Get
                Set(ByVal newSqlparameterParameter As SqlParameter)
                    sqlparameterParameter = newSqlparameterParameter
                End Set
            End Property

            ''' <summary>
            ''' Get and Set variable Parameter.Value
            ''' @return and @param type Object
            ''' </summary>
            Public Property Value() As Object
                Get
                    Return Parameter.Value
                End Get
                Set(ByVal newObjectValue As Object)
                    Parameter.Value = newObjectValue
                End Set
            End Property

            ''' <summary>
            ''' Get and Set variable Parameter.Name
            ''' @return and @param type String
            ''' </summary>
            Public Property Name() As String
                Get
                    If Parameter.ParameterName.Length = 0 Then
                        Return ""
                    End If
                    Return Parameter.ParameterName.Replace("@", "")
                End Get
                Set(ByVal newStringName As String)
                    Parameter.ParameterName = "@" & newStringName.Replace("@", "")
                End Set
            End Property

            ''' <summary>
            ''' Get and Set variable Parameter.Direction
            ''' @return and @param type ParameterDirection
            ''' </summary>
            Public Property Direction() As ParameterDirection
                Get
                    Return Parameter.Direction
                End Get
                Set(ByVal newParameterdirectionDirection As ParameterDirection)
                    Parameter.Direction = newParameterdirectionDirection
                End Set
            End Property

            ''' <summary>
            ''' Get and Set variable Parameter.DbType
            ''' @return and @param type DbType
            ''' </summary>
            Public Property PType() As DbType
                Get
                    Return Parameter.DbType
                End Get
                Set(ByVal newDbtypePType As DbType)
                    Parameter.DbType = newDbtypePType
                End Set
            End Property

            ''' <summary>
            ''' Get and Set variable Parameter.Size
            ''' @return and @param type Long
            ''' </summary>
            Public Property Size() As Long
                Get
                    Return Parameter.Size
                End Get
                Set(ByVal newLongSize As Long)
                    Parameter.Size = newLongSize
                End Set
            End Property

            Private Sub New()
                Parameter = New SqlParameter
            End Sub

            ''' <summary>
            ''' Instance new Object Class Parameter
            ''' </summary>
            ''' <param name="localObjectValue"> Valor do Parâmetro :: DBNull.Value </param>
            ''' <param name="localStringName"> Nome do Parâmetro </param>
            ''' <param name="localParameterdirectionDirection"> Direção do Parâmetro :: InputOutput </param>
            ''' <param name="localDbtypePType"> Tipo de Parâmetro </param>
            ''' <param name="localLongSize"> Tamanho de Parâmetro </param>
            ''' <returns> Type Parameter </returns>
            Public Shared Function Instance(Optional ByVal localObjectValue As Object = Nothing, Optional ByVal localStringName As String = Nothing, Optional ByVal localParameterdirectionDirection As ParameterDirection = ParameterDirection.InputOutput, Optional ByVal localDbtypePType As DbType = Nothing, Optional ByVal localLongSize As Long = Nothing) As Parameter
                Dim localParameter As Parameter
                localParameter = New Parameter()

                If localStringName Is Nothing Then
                    Throw New Exception("Nome do Parâmetro não definido!")
                End If

                If localObjectValue Is Nothing Then
                    localObjectValue = DBNull.Value
                End If

                localParameter.Value = localObjectValue
                localParameter.Name = localStringName
                localParameter.Direction = localParameterdirectionDirection
                localParameter.PType = localDbtypePType
                localParameter.Size = localLongSize
                Return localParameter
            End Function

            Public Shared Function Instance(ByVal localStringName As String, ByVal localObjectValue As Object) As Parameter
                Dim localParameter As Parameter
                localParameter = New Parameter()

                If localStringName Is Nothing Then
                    Throw New Exception("Nome do Parâmetro não definido!")
                End If

                If localObjectValue Is Nothing Then
                    localObjectValue = DBNull.Value
                End If

                localParameter.Value = localObjectValue
                localParameter.Name = localStringName
                If localObjectValue Is Nothing = False AndAlso localObjectValue.GetType() Is GetType(String) Then
                    localParameter.Size = localObjectValue.ToString.Length
                Else

                End If

                Return localParameter
            End Function

            Protected Overrides Sub DisposeObjects()
                Parameter = Nothing
            End Sub

            Public Overrides Function ToString() As String
                Dim str As New System.Text.StringBuilder
                Try
                    str.Append(Name)
                Catch ex As Exception

                End Try
                Return str.ToString
            End Function
        End Class

        <Serializable()>
        Public Class ParametersCollection
            Inherits Dispose

            '/**
            '* variable listParameter
            '* type List(Of Hashtable)
            '**/
            Private listParameter As Hashtable

            Public Sub New()
                listParameter = New Hashtable
            End Sub

            ''' <summary>
            ''' @return Parameter by name
            ''' </summary>
            ''' <param name="name"> Nome do Parâmetro </param>
            ''' <returns> Parameter </returns>
            Default Public Property Item(ByVal name As String) As Parameter
                Get
                    name = name.Replace("@", "").Trim
                    Return GetItem(name)
                End Get
                Set(ByVal Value As Parameter)
                    name = name.Replace("@", "").Trim
                    listParameter(name) = Value
                End Set
            End Property

            Private Function GetItem(ByVal name As String) As Parameter
                name = name.Replace("@", "").Trim
                Try
                    If listParameter.ContainsKey(name) = False Then
                        Add(name, Nothing)
                    End If
                    Return CType(listParameter(name), Parameter)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            ''' <summary>
            ''' Adicionar um novo Parâmetro
            ''' </summary>
            ''' <param name="item"> Parâmetro </param>
            Public Sub Add(ByVal item As Parameter)
                If Not Me.Contains(item) Then
                    listParameter.Add(item.Name, item)
                End If
            End Sub

            ''' <summary>
            ''' Adicionar um novo Parâmetro
            ''' </summary>
            ''' <param name="localObjectValue"> Valor do Parâmetro :: DBNull.Value </param>
            ''' <param name="localStringName"> Nome do Parâmetro </param>
            ''' <param name="localParameterdirectionDirection"> Direção do Parâmetro :: InputOutput </param>
            ''' <param name="localDbtypePType"> Tipo de Parâmetro </param>
            ''' <param name="localLongSize"> Tamanho de Parâmetro </param>
            ''' <returns> Add::ParameterCollection </returns>
            Public Function Add(Optional ByVal localStringName As String = Nothing, Optional ByVal localObjectValue As Object = Nothing, Optional ByVal localParameterdirectionDirection As ParameterDirection = ParameterDirection.InputOutput, Optional ByVal localDbtypePType As DbType = Nothing, Optional ByVal localLongSize As Long = Nothing) As ParametersCollection
                Try
                    Add(Parameter.Instance(localObjectValue, localStringName, localParameterdirectionDirection, localDbtypePType, localLongSize))
                    Return Me
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Adicionar um novo Parâmetro
            ''' </summary>
            ''' <param name="localObjectValue"> Valor do Parâmetro :: DBNull.Value </param>
            ''' <param name="localStringName"> Nome do Parâmetro </param>
            Public Function Add(ByVal localStringName As String, ByVal localObjectValue As Object) As ParametersCollection
                Try
                    Add(Parameter.Instance(localStringName, localObjectValue))
                    Return Me
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

            ''' <summary>
            ''' Verificar se parâmentro já existe
            ''' </summary>
            ''' <param name="item"> Parâmetro </param>
            ''' <returns> Contains::Boolean </returns>
            Public Function Contains(ByVal item As Parameter) As Boolean
                Try
                    Return listParameter.ContainsKey(item.Name)
                Catch ex As Exception
                    Return False
                End Try
            End Function

            ''' <summary>
            ''' Converte em lista
            ''' </summary>
            ''' <returns> ToList::List(Of Parameter) </returns>
            ReadOnly Property ToList() As List(Of Parameter)
                Get
                    Return privateToList()
                End Get
            End Property

            Private Function privateToList() As List(Of Parameter)
                Try
                    privateToList = New List(Of Parameter)
                    For Each param In listParameter.Values
                        privateToList.Add(param)
                    Next
                Catch ex As Exception
                    Return New List(Of Parameter)
                End Try
            End Function

            ''' <summary>
            ''' Retorna total de parâmentros incluídos
            ''' </summary>
            ''' <returns> Count::Integer </returns>
            Public ReadOnly Property Count() As Integer
                Get
                    Return listParameter.Count
                End Get
            End Property

            ''' <summary>
            ''' Remove um parâmetro da lista
            ''' </summary>
            ''' <param name="item"> Parâmetro </param>
            ''' <returns> Remove::Boolean </returns>
            Public Function Remove(ByVal item As Parameter) As Boolean
                Try
                    listParameter.Remove(item.Name)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            ''' <summary>
            ''' Instance new Object Class ParameterCollection
            ''' </summary>
            ''' <returns> Type ParameterCollection </returns>
            Public Shared Function Instance() As ParametersCollection
                Try
                    Instance = New ParametersCollection()
                Catch ex As Exception
                    Instance = New ParametersCollection()
                End Try
            End Function

            ''' <summary>
            ''' Remove todos os parâmentros
            ''' </summary>
            ''' <returns> Realese::Boolean </returns>
            Public Function Realese() As Boolean
                Try
                    Me.Clear()
                Catch ex As Exception

                End Try
            End Function

            ''' <summary>
            ''' Remove todos os parâmentros
            ''' </summary>
            Public Sub Clear()
                listParameter.Clear()
            End Sub

            Protected Overrides Sub DisposeObjects()
                Try
                    Realese()
                    listParameter = Nothing
                Catch ex As Exception

                End Try
            End Sub

            Public Function load(Of T)(ByVal obj As T) As Boolean
                Try
                    Dim propriedades = Lang.Property.asDictionary(obj)

                    For Each prop In propriedades
                        Me.Add(prop.Key, prop.Value, ParameterDirection.Input, SqlType(prop.Value.GetType()))
                    Next
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Private Function SqlType(ByVal oType As Type) As DbType
                Try
                    Dim typeMap As Dictionary(Of Type, DbType) = New Dictionary(Of Type, DbType)()

                    typeMap(GetType(Byte)) = DbType.Byte
                    typeMap(GetType(SByte)) = DbType.SByte
                    typeMap(GetType(Short)) = DbType.Int16
                    typeMap(GetType(UShort)) = DbType.UInt16
                    typeMap(GetType(Integer)) = DbType.Int32
                    typeMap(GetType(UInteger)) = DbType.UInt32
                    typeMap(GetType(Long)) = DbType.Int64
                    typeMap(GetType(ULong)) = DbType.UInt64
                    typeMap(GetType(Single)) = DbType.Single
                    typeMap(GetType(Double)) = DbType.Double
                    typeMap(GetType(Decimal)) = DbType.Decimal
                    typeMap(GetType(Boolean)) = DbType.Boolean
                    typeMap(GetType(String)) = DbType.String
                    typeMap(GetType(Char)) = DbType.StringFixedLength
                    typeMap(GetType(Guid)) = DbType.Guid
                    typeMap(GetType(DateTime)) = DbType.DateTime
                    typeMap(GetType(DateTimeOffset)) = DbType.DateTimeOffset
                    typeMap(GetType(Byte())) = DbType.Binary
                    typeMap(GetType(Byte?)) = DbType.Byte
                    typeMap(GetType(SByte?)) = DbType.SByte
                    typeMap(GetType(Short?)) = DbType.Int16
                    typeMap(GetType(UShort?)) = DbType.UInt16
                    typeMap(GetType(Integer?)) = DbType.Int32
                    typeMap(GetType(UInteger?)) = DbType.UInt32
                    typeMap(GetType(Long?)) = DbType.Int64
                    typeMap(GetType(ULong?)) = DbType.UInt64
                    typeMap(GetType(Single?)) = DbType.Single
                    typeMap(GetType(Double?)) = DbType.Double
                    typeMap(GetType(Decimal?)) = DbType.Decimal
                    typeMap(GetType(Boolean?)) = DbType.Boolean
                    typeMap(GetType(Char?)) = DbType.StringFixedLength
                    typeMap(GetType(Guid?)) = DbType.Guid
                    typeMap(GetType(DateTime?)) = DbType.DateTime
                    typeMap(GetType(DateTimeOffset?)) = DbType.DateTimeOffset

                    If typeMap.ContainsKey(oType) Then
                        Return typeMap(oType)
                    End If

                    Return DbType.AnsiString
                Catch ex As Exception
                    Return DbType.AnsiString
                End Try
            End Function
        End Class
    End Namespace
End Namespace