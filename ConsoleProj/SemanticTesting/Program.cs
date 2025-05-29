using Azure;
using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using OpenAI.Chat;
using System.Threading.Tasks;

namespace SemanticTesting;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Connecting to Azure OpenAI via Semantic Kernel...");

        var deploymentName = "gpt-4o-mini";//Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME");
        var endpoint = "rexhttps://ai2025g2.openai.azure.com/";// Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        var apiKey = "rexCnMI17pBt2x0KPkumFEz3tm8DQDIaIi38lOWtJ7O6ADpIthFuzrUJQQJ99BEACYeBjFXJ3w3AAABACOG0jjI";

        if (string.IsNullOrEmpty(deploymentName) || string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Error: Please set the AZURE_OPENAI_DEPLOYMENT_NAME, AZURE_OPENAI_ENDPOINT, and AZURE_OPENAI_API_KEY environment variables.");
            return;
        }

        try
        {
            var builder = Kernel.CreateBuilder();

            // Add the Azure OpenAI Chat Completion service
            builder.AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                endpoint: endpoint,
                apiKey: apiKey);

            var kernel = builder.Build();

            Console.WriteLine("Successfully connected to Azure OpenAI!");

            // Optional: Make a very simple call to verify
            var result = await kernel.InvokePromptAsync("Say hello.");
            Console.WriteLine($"AI says: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during connection: {ex.Message}");
            Console.WriteLine($"Details: {ex.GetType().Name} - {ex.StackTrace}");
        }

        Console.WriteLine("Connection test complete.");
    }
}
