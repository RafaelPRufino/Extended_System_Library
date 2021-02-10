Namespace Notification
    Public Class Notification
        Inherits System.Exception

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal stringText As String)
            MyBase.New(stringText)
        End Sub

        Public Sub New(ByVal stringText As String, ByVal innerException As System.Exception)
            MyBase.New(stringText, innerException)
        End Sub

        Public Shared Function getLine(ByVal e As Exception) As Integer
            Try
                Return (New StackTrace(e, True)).GetFrame(0).GetFileLineNumber()
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public ReadOnly Property Line As String
            Get
                Return getLine(Me)
            End Get
        End Property

        Private _StackTrace As StackTrace
        Public Shadows ReadOnly Property StackTrace As StackTrace
            Get
                Return _StackTrace
            End Get
        End Property
    End Class
End Namespace