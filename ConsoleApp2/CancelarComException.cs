using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal static class CancelarComException
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

            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(1000);
                    contador++;
                    Console.WriteLine($"Contador: {contador}");
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                tokenSource.Dispose();
                Console.WriteLine("Finalizado execução");
            }
        }
    }
}