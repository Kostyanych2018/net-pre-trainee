using System.Diagnostics;

namespace Task2;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var stopwatch = new Stopwatch();
        double syncProcessingTime, asyncProcessingTime;

        Console.WriteLine("Start of synchronous processing\n");
        stopwatch.Start();

        string result1 = ProcessData("File 1");
        Console.WriteLine(result1);

        string result2 = ProcessData("File 2");
        Console.WriteLine(result2);

        string result3 = ProcessData("File 3");
        Console.WriteLine(result3);

        stopwatch.Stop();
        syncProcessingTime = stopwatch.Elapsed.TotalSeconds;
        Console.WriteLine($"Synchronous processing took: {syncProcessingTime:F2} seconds.");

        Console.WriteLine("\nStart of asynchronous processing\n");
        stopwatch.Restart();

        var task1 = ProcessDataAsync("File 1");
        var task2 = ProcessDataAsync("File 2");
        var task3 = ProcessDataAsync("File 3");

        string[] results = await Task.WhenAll(task1, task2, task3);

        stopwatch.Stop();
        asyncProcessingTime = stopwatch.Elapsed.TotalSeconds;

        foreach (var result in results) {
            Console.WriteLine(result);
        }

        Console.WriteLine($"\nAsynchronous processing took: {asyncProcessingTime:F2} seconds.");

        double ratio = syncProcessingTime / asyncProcessingTime;
        Console.WriteLine($"\nConclusion: The asynchronous approach was ~{ratio:F2} times faster.");
    }

    private static string ProcessData(string dataName)
    {
        Console.WriteLine($"Start of synchronous processing '{dataName}'");
        Thread.Sleep(3000);
        Console.WriteLine($"End of synchronous processing '{dataName}'");
        return $"'{dataName}' processing completed in 3 seconds.\n";
    }

    private static async Task<string> ProcessDataAsync(string dataName)
    {
        Console.WriteLine($"Start of asynchronous processing '{dataName}'");
        await Task.Delay(3000);
        Console.WriteLine($"End of asynchronous processing '{dataName}'");
        return $"'{dataName}' processing completed in 3 seconds.";
    }
}