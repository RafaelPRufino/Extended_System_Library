Namespace Asynchronous
    Public Class Promise(Of T)
        '/**
        '* variable eventFinalized
        '* type Event
        '**/

        ''' <summary>
        ''' Evento assíncrono
        ''' </summary>
        ''' <param name="data"> Resultado do trabalho realizado </param>
        Public Event finalized(ByVal data As T)

        '/**
        '* variable eventFinalized
        '* type Event
        '**/

        ''' <summary>
        ''' Evento assíncrono
        ''' </summary>
        ''' <param name="data"> Resultado do trabalho realizado </param>
        Public Event updated(ByVal data)


        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Action

        '* variable actionAction
        '* type Action
        '**/

        Private actionAction As Job

        ''' <summary>
        ''' Get and Set variable actionAction
        ''' @return and @param type Action
        ''' </summary>
        Protected Property Action()
            Get
                Return actionAction
            End Get
            Set(ByVal newActionAction)
                actionAction = newActionAction
            End Set
        End Property

        '/**
        '* variable controlInvoker
        '* type Control
        '**/
        Private controlInvoker As System.Windows.Forms.Control

        Public Sub New(Optional ByVal invoker As System.Windows.Forms.Control = Nothing)
            controlInvoker = invoker
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="callable"> Função assíncrona </param>
        Public Sub Invoke(ByVal callable As Asynchronous.Action(Of T))
            Try
                actionAction = Job.Instance(callable)
                AddHandler CType(actionAction, Job).finalized, AddressOf callback
                actionAction.Start()
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="callable"> Função assíncrona </param>
        ''' <param name="parameter"> Parâmentro</param>
        Public Sub Invoke(Of P)(ByVal callable As Asynchronous.ActionParameter(Of P, T), ByRef parameter As P)
            Try
                actionAction = Job.Instance(callable)
                CType(actionAction, Job).Parameter = parameter
                AddHandler CType(actionAction, Job).finalized, AddressOf callback
                actionAction.Start()
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        Private Sub callback(ByVal job As Asynchronous.Job)
            Try
                If controlInvoker Is Nothing = False Then
                    If controlInvoker.InvokeRequired Then
                        Dim callback As InvokeRequiredCallback
                        Dim args() As Object = {job}

                        callback = New InvokeRequiredCallback(AddressOf fireEvent)
                        controlInvoker.Invoke(callback, args)

                        args = Nothing
                        callback = Nothing
                    End If
                Else
                    fireEvent(job)
                End If
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        Private Sub fireEvent(ByVal job As Asynchronous.Job)
            Try
                RaiseEvent finalized(job.Result)
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub


        ''' <summary>
        ''' Evento assíncrono
        ''' </summary>
        ''' <param name="data"> Resultado do trabalho realizado </param>
        Public Sub update(ByVal data As Object)
            Try
                callbackUpdate(data)
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        Private Sub callbackUpdate(ByVal data As Object)
            Try
                If controlInvoker Is Nothing = False Then
                    If controlInvoker.InvokeRequired Then
                        Dim callback As InvokeRequiredCallback
                        Dim args() As Object = {data}

                        callback = New InvokeRequiredCallback(AddressOf fireEventUpdate)
                        controlInvoker.Invoke(callback, args)

                        args = Nothing
                        callback = Nothing
                    End If
                Else
                    fireEventUpdate(data)
                End If
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        Private Sub fireEventUpdate(ByVal data As Object)
            Try
                RaiseEvent updated(data)
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Parar Evento assíncrono
        ''' </summary>
        Public Sub Prevent()
            Try
                If actionAction Is Nothing = False Then
                    actionAction.Abort()
                End If
            Catch ex As Exception
                Throw New PromiseException(ex.Message, ex)
            End Try
        End Sub

        Private Delegate Sub InvokeRequiredCallback(ByVal job As Object)
    End Class

    Public Class PromiseException
        Inherits Notification.Notification

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal stringText As String)
            MyBase.New(stringText)
        End Sub

        Public Sub New(ByVal stringText As String, ByVal innerException As System.Exception)
            MyBase.New(stringText, innerException)
        End Sub
    End Class
End Namespace
