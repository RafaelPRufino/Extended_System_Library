<Serializable()>
Public MustInherit Class Dispose
    Implements IDisposable

    Private disposedValue As Boolean = False        ' Para detectar chamadas redundantes

    ' IDisposable
    Private Sub Dispose(ByVal disposing As Boolean)
        Try
            If Not Me.disposedValue Then
                If disposing Then
                    DisposeObjects()
                End If
                Me.disposedValue = True
            End If
            Me.disposedValue = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Overridable Sub DisposeObjects()
    End Sub

#Region " IDisposable Support "
    ' Código adicionado pelo Visual Basic para implementar corretamente o padrão descartável.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Não altere este código. Coloque o código de limpeza em Dispose(ByVal disposing As Boolean) acima.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Simply call Dispose(False).
        Dispose(False)
    End Sub
#End Region
End Class
