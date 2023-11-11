using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Books.Model
{
    public class Author
    {
        [JsonProperty("fullname")]
        public string FullName { get; }
        [JsonIgnore]
        public ReadOnlyObservableCollection<Book> Books { get { return new ReadOnlyObservableCollection<Book>(_books); } }

        [JsonProperty("books")]
        private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();

        [JsonConstructor]
        public Author(string fullname)
        {
            FullName = fullname;
        }

        public Author(string fullname, string bookTitle)
        {
            FullName = fullname;
            _books.Add(new Book(bookTitle, fullname));
        }

        public void Add(Book book)
        {
            _books.Add(book);
        }
    }
}
