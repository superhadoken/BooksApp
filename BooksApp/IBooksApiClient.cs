﻿using System.Threading.Tasks;
using Dtos;

namespace Application;

public interface IBooksApiClient
{
    Task<BookstoreDto> GetBooksAsync(); // Note: async method SHOULD really be using cancellationToken but for brevity's sake I've omitted it
}