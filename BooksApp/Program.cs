using System;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

internal class Program
{
    // todo retrive from config/appsettings.json
    private const string ApiBaseUrl = "https://www.w3schools.com";

    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddScoped<IBooksApiClient, BookApiClient>();
        services.AddScoped<IImportBooks, BooksImport>();
        services.AddScoped<IWriteBooks, BookWriter>();
        services.AddHttpClient<BookApiClient>("BookApiClient", x => { x.BaseAddress = new Uri(ApiBaseUrl); });

        var serviceProvider = services.BuildServiceProvider();

        var booksImport = serviceProvider.GetService<IImportBooks>();

        booksImport!.Import();
    }
}
