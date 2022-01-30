CREATE TABLE [dbo].[BookAuthors]
(
	[BookId] UNIQUEIDENTIFIER NOT NULL,
	[AuthorId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_BookAuthors] PRIMARY KEY CLUSTERED ([BookId] ASC, [AuthorId] ASC),
    CONSTRAINT [FK_BookAuthors_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]),
    CONSTRAINT [FK_BookAuthors_Authors] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id])
)
