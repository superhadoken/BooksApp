using Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application;

public class BookApiClient : IBooksApiClient
{
    private readonly HttpClient _httpClient;
    private const string Path = "/xml/books.xml"; //todo retrive this from config / appsettings.json
    private const string ApiBaseUrl = "https://www.w3schools.com";

    public BookApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BookstoreDto> GetBooksAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Path);

            _httpClient.BaseAddress = new Uri(ApiBaseUrl);

            /* ConfigureAwait(false) to prevent deadlock in UI application (blocking the thread) 
               SendAsync allows us to stream the response straight into Deserialise method instead of fetching it as a string: 
               ResponseHeadersRead just reads the headers and then returns whereas ResponseContentRead waits until both the headers AND content is read */
            using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            // Don't use `.Result` on async method and try to deserialise the stream asynchronously - ReadAsStreamAsync
            await using var contentStream = await result.Content.ReadAsStreamAsync();

            return Deserialise(contentStream);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private BookstoreDto Deserialise(Stream stream)
    {
        var serializerObj = new XmlSerializer(typeof(BookstoreDto));

        return (BookstoreDto)serializerObj.Deserialize(stream);
    }
}
