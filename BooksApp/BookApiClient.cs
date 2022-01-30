using Dtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application;

public class BookApiClient : IBooksApiClient
{
    private readonly HttpClient _httpClient;
    private const string Path = "/xml/books.xml"; //todo retrive this from config / appsettings.json

    public BookApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bookstore> GetBooksAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Path);

            /* ConfigureAwait(false) to prevent deadlock in UI application (blocking the thread) 
               SendAsync allows us to stream the response straight into Deserialise method instead of fetching it as a string: 
               ResponseHeadersRead just reads the headers and then returns whereas ResponseContentRead waits until both the headers AND content is read */
            using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            return await DeserialiseXmlResponseAsync<bookstore>(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task<T> DeserialiseXmlResponseAsync<T>(HttpResponseMessage response)
    {
        // Don't use `.Result` on async method and ReadAsStreamAsync to directly stream to deserialiser instead of converting to string and then streaming via StringReader
        var xmlStream = await response.Content.ReadAsStreamAsync();

        var serializerObj = new XmlSerializer(typeof(bookstore));

        return (T)serializerObj.Deserialize(xmlStream);
    }
}
