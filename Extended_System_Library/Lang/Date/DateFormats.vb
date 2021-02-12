Namespace Lang
    Namespace [Date]
        Public Class [DateFormats]
            Enum [Formats] As Integer
                ddmmyy = 1
                dmyhmms = 2
                ddmmyyyy = 3
                dmy = 4
                mmyyyy = 5
                dmyyyyhmms = 6
            End Enum

            Public Shared ReadOnly Property [get](ByVal formats As DateFormats.Formats)
                Get
                    If formats = [Formats].ddmmyy Then
                        Return "dd/mm/yy"
                    ElseIf formats = [Formats].dmyhmms Then
                        Return "dd/MM/yy HH:mm:ss"
                    ElseIf formats = [Formats].dmyyyyhmms Then
                        Return "dd/MM/yyyy HH:mm:ss"
                    ElseIf formats = [Formats].ddmmyyyy Then
                        Return "dd/mm/yyyy"
                    ElseIf formats = [Formats].dmy Then
                        Return "dd.mm.yyyy"
                    ElseIf formats = [Formats].mmyyyy Then
                        Return "mm.yyyy"
                    End If
                    Return "dd/mm/yy"
                End Get
            End Property
        End Class
    End Namespace
End Namespace