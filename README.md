# Extended System Library

Biblioteca que implementa classes de gerencimento de atividades assincronas para projetos baseados em [![.NET](https://img.shields.io/badge/-.NET-5C2D91?style=flat&logo=.net&logoColor=white)](https://dotnet.microsoft.com) framework 3.5.

```vb
// Testando Promise
Module TestPromise
    Private WithEvents Promise As New Extended_System_Library.Asynchronous.Promise(Of Boolean)

    Sub Main()
        Promise.Invoke(AddressOf callback)
        Console.ReadLine()
    End Sub

    Function callback() As Boolean

        Promise.update("Enviando Atualizações")

        Return True
    End Function

    Private Sub Promise_updated(data As Object) Handles Promise.updated
        Console.WriteLine(data)
    End Sub

    Private Sub Promise_finalized(data As Boolean) Handles Promise.finalized
        If data Then
            Console.WriteLine("Sucesso!!!!!!!!!!!!!!")
        Else
            Console.WriteLine("Falha!!!!!!!!!!!!!!")
        End If
    End Sub
End Module
```
