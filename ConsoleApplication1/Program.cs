using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            tokenSource.CancelAfter(2000);
            

            Task task = Task.Run(() =>
            {
               
                System.Threading.Thread.Sleep(15000);
                token.ThrowIfCancellationRequested();

                Console.WriteLine("TEST");

            },token);

            Console.WriteLine("Task Complete");

            try
            {
                task.Wait();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(task.Status);
            }
            Console.WriteLine("END");
            Console.ReadKey();
        }
    }
}
