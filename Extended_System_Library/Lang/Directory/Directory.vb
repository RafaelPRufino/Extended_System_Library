Imports Microsoft.VisualBasic.FileIO

Namespace Lang
    Namespace Directory
        Public MustInherit Class Directory
            Public Shared ReadOnly Property CurrentPath() As IO.DirectoryInfo
                Get
                    Return New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath)
                End Get
            End Property

            Private Shared ReadOnly Property ToUpper(ByVal v As String) As String
                Get
                    Return v.ToUpper()
                End Get
            End Property

            Public Shared ReadOnly Property DependenciesPath() As IO.DirectoryInfo
                Get
                    Return New IO.DirectoryInfo(Directory.CurrentPath.FullName & ToUpper("\Dependencies"))
                End Get
            End Property

            Public Shared Function Append(ByVal Directory1 As IO.DirectoryInfo, ByVal Directory2 As String) As IO.DirectoryInfo
                Return New IO.DirectoryInfo(Directory1.FullName & IIf(Directory2 = "", "", ToUpper("\") & Directory2))
            End Function

            Public Shared Function Instance(ByVal Directory1 As String) As IO.DirectoryInfo
                Return New IO.DirectoryInfo(Directory1)
            End Function

            Public Shared ReadOnly Property Desktop() As String
                Get
                    Return SpecialDirectories.Desktop
                End Get
            End Property

            Public Shared ReadOnly Property ProgramFiles() As String
                Get
                    Return SpecialDirectories.ProgramFiles
                End Get
            End Property

            Public Shared ReadOnly Property Programs() As String
                Get
                    Return SpecialDirectories.Programs
                End Get
            End Property

            Public Shared ReadOnly Property Temp() As String
                Get
                    Return SpecialDirectories.Temp
                End Get
            End Property

            Public Shared ReadOnly Property MyDocuments() As String
                Get
                    Return SpecialDirectories.MyDocuments
                End Get
            End Property

            Public Shared ReadOnly Property AllUsersApplicationData() As String
                Get
                    Return SpecialDirectories.AllUsersApplicationData
                End Get
            End Property

            Public Shared ReadOnly Property CurrentUserApplicationData() As String
                Get
                    Return SpecialDirectories.CurrentUserApplicationData
                End Get
            End Property

            Public Shared Function Files(ByVal target As IO.DirectoryInfo) As Lang.Collection(Of File)
                Files = New Lang.Collection(Of File)
                Try
                    For Each entry In IO.Directory.GetFiles(target.FullName).ToList
                        Files.Add(File.Instance(entry))
                    Next
                Catch ex As Exception

                End Try
            End Function
        End Class
    End Namespace
End Namespace