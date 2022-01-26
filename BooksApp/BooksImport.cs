using System.Collections.Generic;
using System.Linq;
using DataAccess;
using System.Threading.Tasks;
using Domain;
using Dtos;

namespace Application;

public class BooksImport : IImportBooks
{
    private readonly IBooksApiClient _booksApiClient;
    private readonly IWriteBooks _bookWriter;

    public BooksImport(IBooksApiClient booksApiClient, IWriteBooks bookWriter)
    {
        _booksApiClient = booksApiClient;
        _bookWriter = bookWriter;
    }

    public async Task Import()
    {
        var bookDto = await _booksApiClient.GetBooksAsync();

        var books = MapDtoToBookDomain(bookDto);

        //TODO in future check to see if records already exist and update, otherwise add new
        _bookWriter.SaveBooks(books);
    }

    private ICollection<Book> MapDtoToBookDomain(BookstoreDto dto)
    {
        return dto.Book.Select(b => new Book(b.Category, b.Title.Value, b.Price, b.Author)).ToList();
    }
}
