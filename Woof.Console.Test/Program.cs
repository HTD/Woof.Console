using System;
using System.Threading;
using System.Threading.Tasks;
using Woof.ConsoleEx;
using Woof.ConsoleEx.ConsoleFilters;

class Program {

    static void Main() {        
        Console.SetOut(new HexColor());
        Console.WriteLine("Testing `0ff`Woof.Console`:");
        Console.WriteLine();
        var tasksCount = TasksLeft = 8;
        for (int i = 0; i < tasksCount; i++) {
            Console.Write($"Starting test task #{i + 1}...");
            Task.Run(async () => await TestTask(new ConsoleProgress()));
            Task.Delay(16).Wait();
        }
        Semaphore.Wait();
        Console.WriteLine();
        var hexDump = new HexDump { Format = HexDump.Formats.HexColor };
        var testData = new byte[48];
        PRNG.NextBytes(testData);
        hexDump.Write(testData);
        Console.WriteLine();
        Console.SetOut(new Delay());
        Console.WriteLine("Testing `0ff`Delay` filter..............`070`OK!`");
        Console.WriteLine();
        ConsoleEx.WaitForCtrlC("All test completed successfully, press Ctrl+C to exit...");
    }

    public static async Task TestTask(ConsoleProgress p) {
        var randomDelayValue = PRNG.Next(50, 250);
        for (int i = 0; i < 10; i++) {
            await Task.Delay(randomDelayValue);
            p.Dot();
        }
        lock(L1) p.Done("`070`OK!`");
        if (--TasksLeft < 1) Semaphore.Release();
    }

    private static readonly object L1 = new object();
    private static int TasksLeft;
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(0, 1);
    private static readonly Random PRNG = new Random();

}

