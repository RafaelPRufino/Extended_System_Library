# Extended System Library

Biblioteca criada para facilitar a implementação de atividades assincronas, requisições HTTP, gereciamento de arquivos e diretórios para projetos baseados em .NET framework 3.5. [![.NET](https://img.shields.io/badge/-.NET-5C2D91?style=flat&logo=.net&logoColor=white)](https://dotnet.microsoft.com)

## Index
* [Asynchronous](#Asynchronous)
### Asynchronous
Conjunto de classes que implementam o gerenciamente de atividades assincronas!!!!!!
#### Promise
Promise auxilia na execução de uma atividade assincrona e permite gerenciar eventos de atualização e finalização da promisse.
```vb
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

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extended_System_Library.Asynchronous;

namespace TestePromise
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Promise<Boolean> promise = new Promise<Boolean> ();
            string pParams = "";

            promise.updated += updated;
            promise.finalized += finalized;
            promise.Invoke<string>(callback,ref pParams);
            Console.ReadLine();
        }

        static bool callback(ref string p)
        {
            return !string.IsNullOrEmpty(p);
        }

        static void updated(Object data)
        {
            Console.WriteLine(data);
        }

        static void finalized(Boolean data)
        {
            if( data)
            {
                Console.WriteLine("Sucesso!!!!!!!!!!!!!!");
            }else
            {
                Console.WriteLine("Falha!!!!!!!!!!!!!!");
            }
        }
    }
}
```
#### Work
A classe Work auxilia na execução ordenada de atividades, permitindo limitar de 0 até N os trabalhos feitos de forma simultânea.
Após a execução das tarefas é possível capturar os retornos de cada uma dessas tarefas.
```vb
Module Testwork

    Sub Main()
        Dim work As New Extended_System_Library.Asynchronous.Work(2)
        Dim params As New List(Of Int32)

        For iIndex As Int32 = 0 To 32
            params.Add(iIndex)
        Next

        work.Invoke(Of Int32)(AddressOf callback, params)

        work.Invoke(Of Int32)(AddressOf callback, 547)

        work.Invoke(Of String)(AddressOf callback, "Passando string")
        
        work.Invoke(Of Date)(AddressOf callback, Now)
        
        ''Aguarda até a finalização de todas as atividades empilhadas
        work.Wait()
        
        ''.Jobs é a lista de respostas de cada tarefa
        For Each Job In work.Jobs
            Console.WriteLine(Job.Result)
        Next

        Console.ReadLine()
    End Sub

    Function callback(ByRef param As Object) As Boolean
        Console.WriteLine(param.ToString)

        If Int32.TryParse(param, 0) Then
            Return param * Now.Millisecond
        End If

        Return True
    End Function
End Module
```
