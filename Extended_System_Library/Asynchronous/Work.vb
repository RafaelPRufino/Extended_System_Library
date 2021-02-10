Imports System.Threading

Namespace Asynchronous
    Public Class Work
        '/**
        '* author Rafael Rufino
        '* date 05/09/2018
        '* property Result

        '* variable listResult
        '* type List(Of Object)
        '**/

        Private listResult As Hashtable

        ''' <summary>
        ''' Get variable listResult
        ''' @return type List(Of Job)
        ''' </summary>
        ReadOnly Property Jobs() As List(Of Job)
            Get
                Return privateToList()
            End Get
        End Property

        Private Function privateToList() As List(Of Job)
            Try
                privateToList = New List(Of Job)
                For Each param In listResult.Values
                    privateToList.Add(param)
                Next
            Catch ex As Exception
                Return New List(Of Job)
            End Try
        End Function

        '/**
        '* variable semaphoreSemaphore
        '* type Semaphore
        '**/
        Private semaphoreSemaphore As Semaphore

        '/**
        '* Get and Set variable semaphoreSemaphore
        '* @return and @param type Semaphore
        '**/
        Protected Property Semaphore() As Semaphore
            Get
                Return semaphoreSemaphore
            End Get
            Set(ByVal newSemaphoreSemaphore As Semaphore)
                semaphoreSemaphore = newSemaphoreSemaphore
            End Set
        End Property

        '/**
        '* variable semaphoreSemaphore
        '* type Semaphore
        '**/
        Private semaphoreEntry As Semaphore

        ''' <summary>
        '''  Inicializado do Objecto
        ''' </summary>
        ''' <param name="runningAtMaximum"> Número maxímo de execuções simultâneas </param>
        Public Sub New(Optional ByVal runningAtMaximum As Integer = RunningAtMaximum.MaximumDefault)
            Try
                listResult = New Hashtable
                Semaphore = New System.Threading.Semaphore(0, runningAtMaximum)
                Semaphore.Release(runningAtMaximum)

                semaphoreEntry = New System.Threading.Semaphore(0, 1)
                semaphoreEntry.Release(1)
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="Action"> Função assíncrona </param>
        Public Sub Invoke(ByVal action As Action)
            Try
                Invoke(Job.Instance(action))
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="Action"> Função assíncrona </param>
        ''' <param name="parameter"> Parâmetro </param>
        Public Sub Invoke(Of T)(ByVal action As ActionParameter, ByVal parameter As T)
            Try
                Dim _job As Job = Job.Instance(action)
                _job.Parameter = parameter
                Invoke(_job)
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="Action"> Função assíncrona </param>
        ''' <param name="parameters"> Lista de Parâmetros </param>
        Public Sub Invoke(Of T)(ByVal action As ActionParameter, ByVal parameters As List(Of T))
            Try
                For Each parameter As T In parameters
                    Dim _job As Job = Job.Instance(action)
                    _job.Parameter = parameter
                    Invoke(_job)
                Next
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        ''' <summary>
        ''' Executa uma função assíncrona
        ''' </summary>
        ''' <param name="Action"> Função assíncrona </param>
        ''' <param name="parameters"> Array de Parâmetros </param>
        Public Sub Invoke(Of T)(ByVal action As ActionParameter, ByVal parameters As T())
            Try
                For Each parameter As T In parameters
                    Dim _job As Job = Job.Instance(action)
                    _job.Parameter = parameter
                    Invoke(_job)
                Next
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        Private Sub Invoke(ByVal _job As Job)
            Try
                Dim _thread As Threading.Thread
                Dim _id As String = Guid.NewGuid.ToString

                booleanCompleted = False
                listResult.Add(_id, _job)

                _thread = New Threading.Thread(AddressOf Invoke)
                _thread.Name = String.Format("{0}.{1}-{2}", "Work-Semaphore", "Job", _id)
                _thread.Start(_id)
                _taks.Add(_thread)
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        Private Sub Invoke(ByVal _id As Object)
            Try
                Call InvokeEntry(listResult(_id.ToString))
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
            End Try
        End Sub

        Private Sub InvokeEntry(ByVal job As Job)
            semaphoreSemaphore.WaitOne()
            Try
                If job Is Nothing = False Then
                    job.Start()
                    job.Wait()
                    RaiseEvent finalized(job)
                End If

                Dim count = Jobs.Where(AddressOf CountWhere).Count
                If count = Jobs.Count AndAlso booleanCompleted = False Then
                    booleanCompleted = True
                    RaiseEvent finalizedAll(Me)
                End If
            Catch ex As Exception
            Finally
                semaphoreSemaphore.Release()
            End Try
        End Sub

        Private booleanCompleted As Boolean = False

        Private Function CountWhere(ByVal job As Job) As Boolean
            Try
                Return job.State = JobState.Completed OrElse job.State = JobState.Stopped
            Catch ex As Exception
                Return True
            End Try
        End Function


        '/**
        '* author Rafael Rufino
        '* date 08/11/2018
        '* property _taks

        '* variable list_taks
        '* type List(Of Thread)
        '**/

        Private list_taks As List(Of Thread) = New List(Of Thread)

        ''' <summary>
        ''' Get and Set variable list_taks
        ''' @return and @param type List(Of Thread)
        ''' </summary>
        Private Property _taks() As List(Of Thread)
            Get
                Return list_taks
            End Get
            Set(ByVal newList_taks As List(Of Thread))
                list_taks = newList_taks
            End Set
        End Property

        ''' <summary>
        ''' Aguarda todos os processos executarem
        ''' </summary>
        ''' <returns> Boolean </returns>
        Public Function Wait() As Boolean
            Try
                If _taks Is Nothing Then Return False

                For index As Integer = 0 To _taks.Count - 1
                    _taks(index).Join()
                Next

                Return True
            Catch ex As Exception
                Throw New WorkException(ex.Message, ex)
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
        ''' <param name="job"> Resultado do trabalho realizado </param>
        Public Event finalized(ByVal job As Job)

        '/**
        '* variable eventfinalizedAll
        '* type Event
        '**/

        ''' <summary>
        ''' Evento assíncrono
        ''' </summary>
        ''' <param name="work"> Resultado do trabalho realizado </param>
        Public Event finalizedAll(ByVal work As Work)
    End Class

    Public Class WorkException
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
