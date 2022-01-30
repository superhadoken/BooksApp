using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Application;

internal class Program
{
    // todo retrive from config/appsettings.json
    private const string ApiBaseUrl = "https://www.w3schools.com";

    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services
            .AddScoped<IBooksApiClient, BookApiClient>()
            .AddScoped<IImportBooks, BooksImport>()
            .AddScoped<IWriteBooks, BookWriter>()
            //TODO use something like Polly for circuit breaker policy/retry policy/etc
            .AddHttpClient<IBooksApiClient, BookApiClient>(
                config =>
                {
                    config.BaseAddress = new Uri(ApiBaseUrl);
                })
            ;

        var serviceProvider = services.BuildServiceProvider();

        var booksImport = serviceProvider.GetService<IImportBooks>();

        if (booksImport != null)
            await booksImport.Import();
    }
}