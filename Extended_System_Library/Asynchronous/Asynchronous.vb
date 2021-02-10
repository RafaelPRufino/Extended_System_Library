Namespace Asynchronous
    ''' <summary>
    ''' Função assíncrona
    ''' </summary>
    ''' <returns> Action </returns> 
    Public Delegate Function Action()

    ''' <summary>
    ''' Função assíncrona
    ''' </summary>
    ''' <param name="p"> Parâmentro </param>
    ''' <returns> Action Of Parameter </returns>
    Public Delegate Function ActionParameter(ByRef p As Object)

    Public Enum RunningAtMaximum As Integer
        MaximumDefault = 50
    End Enum

    Public Enum JobState As Integer
        Unstarted = 0
        Initialized = 1
        WaitSleep = 2
        Running = 3
        Stopped = 4
        Completed = 5
    End Enum
End Namespace
