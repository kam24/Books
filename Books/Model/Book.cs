using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Books.Model
{
    public class Book
    {
        [JsonProperty("title")]
        public string Title { get; }
        [JsonIgnore]
        public ReadOnlyObservableCollection<Author> Authors { get { return new ReadOnlyObservableCollection<Author>(_authors); } }

        [JsonProperty("authors")]
        private readonly ObservableCollection<Author> _authors = new ObservableCollection<Author>();

        [JsonConstructor]
        public Book(string title)
        {
            Title = title;
        }

        public Book(string title, string authorFullName)
        {
            Title = title;
            _authors.Add(new Author(authorFullName));
        }


        public void Add(Author author)
        {
            _authors.Add(author);
        }
    }
}
