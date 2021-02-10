Imports System.Reflection

Namespace Lang
    Namespace Directory
        Public Class File
            Inherits Lang.CollectionItem

            Private outFile As IO.FileInfo

            Private Sub New()
            End Sub

            Private Sub New(ByVal FilePath As String)
                Try
                    outFile = New IO.FileInfo(FilePath)
                Catch ex As Exception
                End Try
            End Sub

            Public Shared Function Instance(ByVal FilePath As IO.FileInfo) As File
                Try
                    Instance = New File(FilePath.FullName)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public Shared Function Instance(ByVal FilePath As String) As File
                Try
                    Instance = New File(FilePath)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public Shared Function Instance(ByVal FilePath As IO.DirectoryInfo, ByVal Name As String) As File
                Try
                    Instance = New File(FilePath.FullName & "\" & Name)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

#Region "Propriedades"
            Public ReadOnly Property Length() As Double
                Get
                    If Exists() = False Then
                        Return 0
                    End If

                    Return outFile.Length
                End Get
            End Property

            Public ReadOnly Property LengthKB() As Double
                Get
                    Return BYTES_TO_KBYTES(Length)
                End Get
            End Property

            Public ReadOnly Property LengthMB() As Double
                Get
                    Return BYTES_TO_MBYTES(Length)
                End Get
            End Property

            Public ReadOnly Property Version() As Double
                Get
                    Try
                        Return AssemblyName.GetAssemblyName(outFile.FullName).Version.ToString()
                    Catch ex As Exception
                        Return ""
                    End Try
                End Get
            End Property

            Public ReadOnly Property FullName() As String
                Get
                    Return outFile.FullName
                End Get
            End Property

            Public ReadOnly Property Name() As String
                Get
                    Return outFile.Name
                End Get
            End Property

            Public ReadOnly Property Extension() As String
                Get
                    Return outFile.Extension
                End Get
            End Property

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return outFile.Exists
                End Get
            End Property
#End Region
#Region "Operações"
            Public Function Copy() As File
                Copy = Nothing
                Try
                    Dim destination As File = File.Instance(outFile.Directory, RandomName(Extension))
                    Copy = File.Instance(File.Copy(Me, destination).FullName)
                Catch ex As Exception
                End Try
            End Function

            Public Function Copy(ByVal FilePath As IO.DirectoryInfo, ByVal Name As String) As File
                Copy = Nothing
                Try
                    Dim destination As File = File.Instance(FilePath, Name)
                    Copy = File.Instance(File.Copy(Me, destination).FullName)
                Catch ex As Exception
                End Try
            End Function

            Public Function Copy(ByVal FilePath As IO.DirectoryInfo) As File
                Copy = Nothing
                Try
                    Dim destination As File = File.Instance(FilePath, Name)
                    Copy = File.Instance(File.Copy(Me, destination).FullName)
                Catch ex As Exception
                End Try
            End Function

            Public Shared Function Copy(ByVal Source As File, ByVal Destiny As File) As File
                Try
                    Destiny.Create()
                    If Source.Exists Then
                        Destiny.Delete()
                        My.Computer.FileSystem.CopyFile(Source.FullName, Destiny.FullName)
                    End If
                    Return Destiny
                Catch ex As Exception
                    Return Destiny
                End Try
            End Function

            Public Function Create() As Boolean
                Try
                    Dim arq As IO.FileStream
                    If Me.Exists = False Then

                        If outFile.Directory.Exists = False Then
                            outFile.Directory.Create()
                        End If

                        arq = outFile.Create()
                        arq.Close()

                        outFile = File.Instance(outFile.FullName).outFile
                    End If
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Public Function Delete() As Boolean
                Try
                    IO.File.Delete(outFile.FullName)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Public Function Move(ByVal Destiny As IO.DirectoryInfo) As Boolean
                Try
                    Dim arqDest = File.Instance(Destiny, Me.Name)

                    If Destiny.Exists = False Then
                        Destiny.Create()
                    End If

                    My.Computer.FileSystem.MoveFile(Me.FullName, arqDest.FullName)

                    Return arqDest.Exists
                Catch ex As Exception
                    Return False
                End Try
            End Function
#End Region
#Region "Conversões"
            Public Overridable Overloads Function ToText(Optional ByVal encoding As System.Text.Encoding = Nothing) As String
                Try
                    Dim arrayBytes() As Byte

                    arrayBytes = ToBytes(encoding)

                    If encoding Is Nothing Then
                        ToText = System.Text.Encoding.UTF8.GetString(arrayBytes).Trim
                    Else
                        ToText = encoding.GetString(arrayBytes).Trim
                    End If

                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public Overridable Overloads Function ToText() As String
                Try
                    ToText = IO.File.ReadAllText(FullName)
                Catch ex As Exception
                    Return ""
                End Try
            End Function

            Public Function ToBytes(Optional ByVal encoding As System.Text.Encoding = Nothing) As Byte()
                Try
                    Dim fileStream As IO.FileStream
                    Dim fileBReader As IO.BinaryReader

                    fileStream = New IO.FileStream(outFile.FullName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

                    If encoding Is Nothing Then
                        fileBReader = New IO.BinaryReader(fileStream, encoding)
                        ToBytes = fileBReader.ReadBytes(fileStream.Length)
                    Else
                        fileBReader = New IO.BinaryReader(fileStream)
                        ToBytes = fileBReader.ReadBytes(fileStream.Length)
                    End If

                    fileStream.Close()
                    fileBReader.Close()
                    fileStream.Dispose()
                    fileStream = Nothing
                    fileBReader = Nothing
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public Function GetStreamText() As IO.StreamReader
                Try
                    GetStreamText = IO.File.OpenText(FullName)
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

            Public Function SetText(ByVal Text As String) As Boolean
                Try
                    If Create() Then
                        IO.File.WriteAllText(FullName, Text)
                    End If
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

            Public Function ToBytes() As Byte()
                ToBytes = Nothing
                Try
                    ToBytes = IO.File.ReadAllBytes(outFile.FullName)
                Catch ex As Exception

                End Try
            End Function

            Public Function SetBytes(ByVal Bytes As Byte()) As Boolean
                Try
                    If Create() Then
                        IO.File.WriteAllBytes(outFile.FullName, Bytes)
                    End If
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function

#End Region
            Public Function Info() As IO.FileInfo
                Info = outFile
            End Function

            Public Overrides Function ToString() As String
                Return FullName()
            End Function

            Private Function BYTES_TO_KBYTES(ByVal bytes As Int64) As Double
                Try
                    Return bytes / 1024.0
                Catch ex As Exception
                    Return 0
                End Try
            End Function

            Private Function BYTES_TO_MBYTES(ByVal bytes As Int64) As Double
                Try
                    Return BYTES_TO_KBYTES(bytes) / 1024.0
                Catch ex As Exception
                    Return 0
                End Try
            End Function


            Shared Function RandomName(Optional ByVal strExtension As String = "") As String
                Try
                    RandomName = HashName()
                    RandomName = RandomName.Replace("-", "_").Trim.ToUpper & strExtension
                    RandomName = FileName(RandomName)
                Catch ex As Exception
                    Return ""
                End Try
            End Function

            Shared Function HashName(Optional ByVal Size As Integer = -1) As String
                Try
                    HashName = System.Guid.NewGuid.ToString()
                    HashName = HashName.Replace("-", "_").Trim.ToUpper
                    HashName = FileName(HashName)

                    If HashName.Length > Size And Size > -1 Then
                        HashName = HashName.Substring(0, Size)
                    End If

                    If Size > -1 Then
                        HashName = HashName.PadLeft(Size, "0")
                    End If

                Catch ex As Exception
                    Return ""
                End Try
            End Function

            Shared Function FileName(ByVal str As String)
                Try
                    Dim invalidChars As String = System.Text.RegularExpressions.Regex.Escape(New String(System.IO.Path.GetInvalidFileNameChars()))
                    Dim invalidReStr As String = String.Format("([{0}]*\.+$)|([{0}]+)", invalidChars)
                    Return System.Text.RegularExpressions.Regex.Replace(str, invalidReStr, "_").Replace(" ", "_")
                Catch ex As Exception
                    Return str
                End Try
            End Function
        End Class
    End Namespace
End Namespace