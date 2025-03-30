using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public class Program
{
    private static async Task Main(string[] args)
    {
        var ctkn = new CancellationTokenSource();
        using var client = new HttpClient();
        var responseTask = client.GetAsync("https://localhost:5001/Laptop", ctkn.Token);

        Console.WriteLine(" Press any key for Request sending or press Esc for cancel: ");
        if (Console.ReadKey().Key == ConsoleKey.Escape)
        {
            ctkn.Cancel();
            Console.WriteLine(" Request canceled ");
        }

        var response = await responseTask;
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var laptops = JsonSerializer.Deserialize<List<LaptopConsoleModel>>(content);
            Console.WriteLine($" Laptops: {content} ");
        }
        else
        {
            var errorCode = response.StatusCode.ToString();
            Console.WriteLine($" Error: {errorCode}");
        }

    }
}

public class LaptopConsoleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}