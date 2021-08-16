using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class CancelarNoLaco
    {
        private static async Task Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    var input = Console.ReadKey();

                    if (input.Key == ConsoleKey.C)
                    {
                        tokenSource.Cancel();
                    }
                }
            });

            var contador = 0;
            
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000);
                contador++;
                Console.WriteLine($"Contador: {contador}");
            }

            tokenSource.Dispose();

            Console.WriteLine("Finalizado execução");
        }
    }
}