Namespace Lang
    Public Class Collection(Of T As CollectionItem)
        Inherits Generic.List(Of T)

        Public Function [Next]() As T
            [Next] = Nothing
            Try
                If HasNext() Then
                    [Next] = Me.Where(Function(ByVal element As T) element.Selected = False).Last
                    [Next].Selected = True
                End If
            Catch ex As Exception
                [Next] = Nothing
            End Try
        End Function

        Public Function HasNext() As Boolean
            Try
                Return Me.Exists(Function(ByVal element As T) element.Selected = False)
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class

    Public Class CollectionItem
        Property Selected As Boolean = False
    End Class
End Namespace