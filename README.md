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

        private static bool callback(ref string p)
        {
            return !string.IsNullOrEmpty(p);
        }

        static void   updated(Object data)
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
