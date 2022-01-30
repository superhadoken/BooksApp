using DataAccess;
using Domain;
using Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    private ICollection<Book> MapDtoToBookDomain(bookstore dto)
    {
        return dto.book.Select(b => new Book(b.category, b.title.Value, b.price, b.author)).ToList();
    }
}
