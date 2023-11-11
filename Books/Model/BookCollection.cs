using System.Collections.ObjectModel;
using System.Linq;

namespace Books.Model
{
    public class BookCollection
    {
        public readonly ReadOnlyObservableCollection<Book> Books;
        public readonly ReadOnlyObservableCollection<Author> Authors;

        private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private readonly ObservableCollection<Author> _authors = new ObservableCollection<Author>();

        public BookCollection()
        {
            Authors = new ReadOnlyObservableCollection<Author>(_authors);
            Books = new ReadOnlyObservableCollection<Book>(_books);
        }

        public BookCollection(ObservableCollection<Author> authors, ObservableCollection<Book> books)
        {
            _authors = authors;
            _books = books;
            Authors = new ReadOnlyObservableCollection<Author>(_authors);
            Books = new ReadOnlyObservableCollection<Book>(_books);
        }

        public void Add(Book book)
        {
            _books.Add(book);
            _authors
                .FirstOrDefault(author => author.FullName == book.Authors.First().FullName)
                .Add(book);
        }

        public void Add(Author author)
        {
            _authors.Add(author);
            _books
                .FirstOrDefault(book => book.Title == author.Books.First().Title)
                .Add(author);
        }
    }
}
