Imports System.Reflection
Imports System.Text

Namespace Lang
    Public Class [Property]
        Public Shared Function [default](Of T)(ByRef item As T) As Boolean
            Try
                Dim Propriedades As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)

                For i As Integer = 0 To Propriedades.Length - 1
                    Dim value = Propriedades(i).GetValue(item, Nothing)

                    If value Is Nothing Then
                        If Propriedades(i).PropertyType Is "".GetType Then
                            value = ""
                        ElseIf Propriedades(i).PropertyType Is (Now).GetType Then
                            value = Date.MinValue
                        End If
                    End If

                    Propriedades(i).SetValue(item, value, Nothing)
                Next

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function asDictionary(ByVal source As Object, ByVal Optional bindingAttr As BindingFlags = BindingFlags.DeclaredOnly Or BindingFlags.[Public] Or BindingFlags.Instance) As IDictionary(Of String, Object)
            Return source.[GetType]().GetProperties(bindingAttr).ToDictionary(Function(propInfo) propInfo.Name, Function(propInfo) propInfo.GetValue(source, Nothing))
        End Function

        Public Shared Function toObject(Of T As {Class, New})(ByVal source As IDictionary(Of String, Object)) As T
            Try
                Dim someObject = New T()
                Dim someObjectType = someObject.[GetType]()

                For Each item In source
                    Dim info = someObjectType.GetProperty(item.Key)
                    Try
                        If info Is Nothing = False Then
                            info.SetValue(someObject, GetValueAt(source, item.Key, info.PropertyType), Nothing)
                        End If
                    Catch ex As Exception

                    End Try
                Next

                Return someObject
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function ToAnonymousType(Of T)(ByVal source As IDictionary(Of String, Object), ByVal AnonymousType As T) As T
            Try
                Dim someObject = AnonymousType
                Dim someObjectType = AnonymousType.GetType

                For Each item In source
                    Dim info = someObjectType.GetProperty(item.Key)
                    If info Is Nothing = False Then
                        info.SetValue(someObject, getValueAt(source, item.Key, info.PropertyType), Nothing)
                    End If
                Next

                Return someObject
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function AsType(Of T As {Class, New})(ByVal source As IDictionary(Of String, Object)) As T
            Try
                Dim someObject = New T()
                Dim someObjectType = someObject.[GetType]()
                Dim someObjectProperties() = someObjectType.GetProperties()

                For Each info As PropertyInfo In someObjectProperties
                    Dim custom As CustomAttributeData = Nothing
                    Dim Named As String

                    If info.CustomAttributes Is Nothing = False AndAlso info.CustomAttributes.Count > 0 Then
                        custom = info.CustomAttributes.First
                    End If

                    If custom Is Nothing = False Then
                        Named = custom.ConstructorArguments.First.Value
                    Else
                        Named = info.Name
                    End If
                    Try
                        info.SetValue(someObject, getValueAt(source, Named, info.PropertyType), Nothing)
                    Catch ex As Exception

                    End Try

                Next

                Return someObject
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function getValueAt(ByVal Onwer As Object, ByVal Column As String, ByVal expectType As Type)
            Try
                Dim value As Object = Onwer(Column)
                Dim T = expectType
                If value Is Nothing OrElse IsDBNull(value) Then
                    Return IIf(expectType.Equals(GetType(String)), "", Nothing)
                End If

                If value.GetType Is expectType Then
                    Return Convert.ChangeType(value, expectType)
                ElseIf (T.Equals(GetType(Char)) OrElse T.Equals(GetType(System.Nullable(Of Char)))) AndAlso TypeOf value Is String Then
                    Return Convert.ChangeType(TryCast(TryCast(value, String)(0), Object), expectType)
                ElseIf (T.Equals(GetType(Decimal)) OrElse T.Equals(GetType(System.Nullable(Of Decimal)))) AndAlso TypeOf value Is Decimal Then
                    value = Decimal.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(Double)) OrElse T.Equals(GetType(System.Nullable(Of Double)))) AndAlso TypeOf value Is Double Then
                    value = Double.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(DateTime)) OrElse T.Equals(GetType(System.Nullable(Of DateTime)))) Then
                    value = DateTime.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(Long)) Or (T.Equals(GetType(Int64)))) Then
                    value = Long.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(Integer)) Or (T.Equals(GetType(Int32)))) Then
                    value = Integer.Parse(value.ToString())
                ElseIf (T.Equals(GetType(Int16)) Or (T.Equals(GetType(Short)))) Then
                    value = Short.Parse(value.ToString())
                ElseIf (T.Equals(GetType(System.Nullable(Of Long))) OrElse T.Equals(GetType(System.Nullable(Of Int64)))) Then
                    value = Long.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(System.Nullable(Of Integer))) OrElse T.Equals(GetType(System.Nullable(Of Int32)))) Then
                    value = Integer.Parse(value.ToString())
                    Return value
                ElseIf (T.Equals(GetType(System.Nullable(Of Int16))) OrElse T.Equals(GetType(System.Nullable(Of Short)))) Then
                    value = Short.Parse(value.ToString())
                    Return value
                End If

                Return value
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function getValueAt(Of T)(ByVal Onwer As Object, ByVal Column As String)
            Try
                Return GetValueAt(Onwer, Column, GetType(T))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace