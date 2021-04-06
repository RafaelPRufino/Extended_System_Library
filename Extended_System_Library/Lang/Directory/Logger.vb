Imports Directory = Extended_System_Library.Lang.Directory.Directory

Namespace Lang
    Namespace Directory
        Public Class Logger
            Shared Property Extension As String = "dbo"
            Shared Property LenSeparator As Integer = 15

            Public Shared ReadOnly Property [Path]() As String
                Get
                    Return Directory.CurrentPath.FullName
                End Get
            End Property

            Public Shared Function Write(ByVal folder As String, ByVal ObjectLog As Object) As Boolean
                Try
                    Dim logFile = MapFile(folder)


                    WriteLine(logFile.FullName, Logger.ObjectToString(ObjectLog))
                Catch ex As Exception

                End Try
            End Function

            Private Shared Function [MapFile](ByVal folder As String) As File
                Try
                    Dim sCurrentPath As String = Logger.Path & "\"
                    Dim dDirectory As IO.DirectoryInfo
                    Dim sFolder() As String = folder.Split("/")

                    sCurrentPath = sCurrentPath

                    dDirectory = New IO.DirectoryInfo(sCurrentPath)

                    For x As Integer = 0 To sFolder.Length - 2
                        dDirectory = New IO.DirectoryInfo(sCurrentPath & sFolder(x))

                        If dDirectory.Exists = False Then
                            dDirectory.Create()
                        End If

                        sCurrentPath = dDirectory.FullName & "\"
                    Next

                    [MapFile] = File.Instance(sCurrentPath & sFolder(sFolder.Length - 1) & "." & Logger.Extension)
                    If [MapFile].Exists = False Then [MapFile].Create()
                Catch ex As Exception
                    Return File.Instance(Logger.Path & "RecordFile." & Logger.Extension)
                End Try
            End Function

            Private Shared Function WriteLine(ByVal sPLArqLog As String, ByVal sLine As String) As Boolean
                Try
                    Dim escritorLOG As New IO.StreamWriter(New IO.FileStream(sPLArqLog, IO.FileMode.Append))
                    escritorLOG.WriteLine(sLine)
                    escritorLOG.Close()
                    escritorLOG.Dispose()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                End Try
            End Function

            Private Shared Function ObjectToString(ByVal ObjectToRead As Object) As String
                ObjectToString = ObjectToRead.ToString
                Try
                    Dim sToString As New System.Text.StringBuilder

                    If ObjectToRead Is Nothing = False Then
                        Dim object_properties() As Object
                        Dim object_getValue() As Object
                        Dim object_type_name As String

                        object_properties = ObjectToRead.GetType().GetProperties
                        object_type_name = ObjectToRead.GetType().Name

                        sToString.Append(ToString_Object_Children(object_properties, ObjectToRead, object_getValue))
                    End If

                    ObjectToString = sToString.ToString()
                Catch ex As Exception
                    Return ""
                End Try
            End Function

            Private Shared Function ToString_Object_Children(ByVal _object_properties(), ByVal _object, ByVal _object_getValue)
                Dim ToString As New System.Text.StringBuilder
                Try
                    Dim StringSeparetor As String = "".PadRight(Logger.LenSeparator)
                    For Each properties As System.Reflection.PropertyInfo In _object_properties

                        Dim _obj_tag_name As String
                        Dim _obj_tag_type As System.Type
                        Dim _obj_tag_value As Object
                        Dim _obj_tag_properties() As System.Reflection.PropertyInfo
                        Dim _obj_children_values As Object
                        Dim _obj_children_val As Object

                        _obj_tag_name = properties.Name
                        _obj_tag_type = properties.PropertyType
                        _obj_tag_value = properties.GetValue(_object, _object_getValue)
                        _obj_tag_properties = _obj_tag_value.GetType.GetProperties
                        _obj_children_values = ""
                        _obj_children_val = ""

                        If ToString_Object_AllowedType(_obj_tag_type) Then

                            _obj_children_values = ToString_Object_Children(_obj_tag_properties, _obj_tag_value, _object_getValue)

                            If Not _obj_children_values.ToString() = "" Then
                                _obj_tag_value = (vbLf & _obj_children_values.ToString()).ToString.Trim
                            End If

                        ElseIf ToString_Object_IsTypeCollections(_obj_tag_type) Then

                            Try
                                For Each _obj_children In _obj_tag_value

                                    Dim _obj_children_name = _obj_children.GetType.Name
                                    Dim _obj_children_type As System.Type = _obj_children.GetType
                                    Dim xml_children_properties() As System.Reflection.PropertyInfo = _obj_children.GetType.GetProperties

                                    _obj_children_val = ToString_Object_Children(_obj_children.GetType.GetProperties, _obj_children, _object_getValue)

                                    If Not _obj_children_val.ToString() = "" Then
                                        ToString.Append("{" & _obj_children_name & " = " & _obj_children_val & "}" & StringSeparetor)
                                    End If
                                Next
                            Catch ex As Exception

                            End Try


                            If Not _obj_children_values.ToString() = "" Then
                                _obj_tag_value = (vbLf & _obj_children_values.ToString()).ToString.Trim
                            End If

                        End If

                        If ToString_Object_AllowedValueType(_obj_tag_value.GetType) Then
                            ToString.Append("{" & _obj_tag_name & " = " & _obj_tag_value & "}" & StringSeparetor)
                        End If
                    Next

                    Return ToString.ToString.Trim
                Catch ex As Exception
                    Return ToString.ToString.Trim
                End Try
            End Function

            Private Shared Function ToString_Object_AllowedType(ByVal _xml_tag_type As System.Type)
                Try
                    Dim string_type As System.Type = "".GetType
                    Dim boolean_type As System.Type = True.GetType
                    Dim integer_type As System.Type = CInt(0).GetType
                    Dim double_type As System.Type = CDbl(0).GetType
                    Dim long_type As System.Type = CLng(0).GetType
                    Dim date_type As System.Type = CDate(Now).GetType

                    If string_type Is _xml_tag_type Then
                        Return False
                    ElseIf boolean_type Is _xml_tag_type Then
                        Return False
                    ElseIf integer_type Is _xml_tag_type Then
                        Return False
                    ElseIf double_type Is _xml_tag_type Then
                        Return False
                    ElseIf long_type Is _xml_tag_type Then
                        Return False
                    ElseIf date_type Is _xml_tag_type Then
                        Return False
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("System.Collections") > -1 Then
                        Return False
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("[]") > -1 Then
                        Return False
                    End If

                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Private Shared Function ToString_Object_AllowedValueType(ByVal _xml_tag_type As System.Type)
                Try
                    Dim string_type As System.Type = "".GetType
                    Dim boolean_type As System.Type = True.GetType
                    Dim integer_type As System.Type = CInt(0).GetType
                    Dim double_type As System.Type = CDbl(0).GetType
                    Dim long_type As System.Type = CLng(0).GetType
                    Dim date_type As System.Type = CDate(Now).GetType

                    If string_type Is _xml_tag_type Then
                        Return True
                    ElseIf boolean_type Is _xml_tag_type Then
                        Return True
                    ElseIf integer_type Is _xml_tag_type Then
                        Return True
                    ElseIf double_type Is _xml_tag_type Then
                        Return True
                    ElseIf long_type Is _xml_tag_type Then
                        Return True
                    ElseIf date_type Is _xml_tag_type Then
                        Return True
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("System.Collections") > -1 Then
                        Return False
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("[]") > -1 Then
                        Return False
                    End If

                    Return False
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Private Shared Function ToString_Object_IsTypeCollections(ByVal _xml_tag_type As System.Type)
                Try
                    Dim string_type As System.Type = "".GetType
                    Dim boolean_type As System.Type = True.GetType
                    Dim integer_type As System.Type = CInt(0).GetType
                    Dim double_type As System.Type = CDbl(0).GetType
                    Dim long_type As System.Type = CLng(0).GetType
                    Dim date_type As System.Type = CDate(Now).GetType

                    If string_type Is _xml_tag_type Then
                        Return False
                    ElseIf boolean_type Is _xml_tag_type Then
                        Return False
                    ElseIf integer_type Is _xml_tag_type Then
                        Return False
                    ElseIf double_type Is _xml_tag_type Then
                        Return False
                    ElseIf long_type Is _xml_tag_type Then
                        Return False
                    ElseIf date_type Is _xml_tag_type Then
                        Return False
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("System.Collections") > -1 Then
                        Return True
                    ElseIf _xml_tag_type.FullName.ToString.IndexOf("[]") > -1 Then
                        Return True
                    End If

                    Return False
                Catch ex As Exception
                    Return False
                End Try
            End Function
        End Class
    End Namespace
End Namespace

