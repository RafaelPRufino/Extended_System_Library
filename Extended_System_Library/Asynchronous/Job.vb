Imports System.Threading

Namespace Asynchronous
    Public Class Job
        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property State

        '* variable jobstateState
        '* type JobState
        '**/

        Private jobstateState As JobState

        ''' <summary>
        ''' Get variable jobstateState
        ''' @return type JobState
        ''' </summary>
        Public ReadOnly Property State() As JobState
            Get
                Return jobstateState
            End Get
        End Property

        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Thread

        '* variable threadThread
        '* type Thread
        '**/

        Private threadThread As Thread

        ''' <summary>
        ''' Get and Set variable threadThread
        ''' @return and @param type Thread
        ''' </summary>
        Protected Property Thread() As Thread
            Get
                Return threadThread
            End Get
            Set(ByVal newThreadThread As Thread)
                threadThread = newThreadThread
            End Set
        End Property

        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Action

        '* variable actionAction
        '* type Action
        '**/

        Private actionAction

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
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Parameter

        '* variable tParameter
        '* type T
        '**/

        Private tParameter As Object

        ''' <summary>
        ''' Get and Set variable tParameter
        ''' @return and @param type T
        ''' </summary>
        Public Property Parameter() As Object
            Get
                Return tParameter
            End Get
            Set(ByVal newTParameter As Object)
                tParameter = newTParameter
            End Set
        End Property

        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Result

        '* variable objectResult
        '* type Object
        '**/

        Private objectResult As Object

        ''' <summary>
        ''' Get variable objectResult
        ''' @return type Object
        ''' </summary>
        Public ReadOnly Property Result() As Object
            Get
                Return objectResult
            End Get
        End Property

        Protected Sub New()
            Me.jobstateState = JobState.Unstarted
            Me.objectResult = Nothing
            Me.tParameter = Nothing
            Me.threadThread = Nothing
        End Sub

        ''' <summary>
        ''' Instância Função assíncrona
        ''' </summary>
        ''' <param name="Action"> Função assíncrona </param>
        ''' <returns> Instance </returns>
        Public Shared Function Instance(ByVal Action As Object) As Job
            Instance = New Job
            Instance.Action = Action
        End Function

        ''' <summary>
        ''' Inicia a execução do processo
        ''' </summary>
        Public Sub Start()
            Try
                If State = JobState.Unstarted Then
                    jobstateState = JobState.Initialized
                    Thread = New Threading.Thread(AddressOf Invoke)
                    Thread.Start()
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Invoke()
            Try
                jobstateState = JobState.WaitSleep

                If Action.GetType.FullName.Contains("ActionParameter") Then
                    jobstateState = JobState.Running
                    objectResult = Action.Invoke(Parameter)
                ElseIf Action.GetType.FullName.Contains("Action") Then
                    jobstateState = JobState.Running
                    objectResult = Action.Invoke()
                Else
                    Throw New Exception("delegated function not allowed")
                End If
                jobstateState = JobState.Completed
            Catch ex As Exception
                jobstateState = JobState.Stopped
            Finally
                RaiseEvent finalized(Me)
            End Try
        End Sub


        ''' <summary>
        ''' Aguarda a execução do processo
        ''' </summary>
        ''' <returns> Boolean </returns>
        Public Function Wait() As Boolean
            Try

                If Thread IsNot Nothing Then
                    Thread.Join()
                End If

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Aborta a execução do processo
        ''' </summary>
        ''' <returns> Boolean </returns>
        Public Function Abort() As Boolean
            Try
                If jobstateState = JobState.Completed Then
                    Return True
                End If

                jobstateState = JobState.Stopped
                If Thread IsNot Nothing Then
                    If Thread.IsAlive Then
                        Thread.Abort()
                    End If
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        '/**
        '* variable eventFinalized
        '* type Event
        '**/

        ''' <summary>
        ''' Evento assíncrono
        ''' </summary>
        ''' <param name="job"> Micro trabalho finalizado </param>
        Public Event finalized(ByVal job As Job)
    End Class
End Namespace
