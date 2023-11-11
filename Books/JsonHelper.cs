using Books.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace Books
{
    public static class JsonHelper
    {
        private const string _authorsPath = @"json\authors.json";
        private const string _booksPath = @"json\books.json";

        public static void SerializeBooks(ReadOnlyObservableCollection<Book> books)
        {
            string json = JsonConvert.SerializeObject(books);
            File.WriteAllText(GetDirectory(_booksPath), json);
        }

        public static ObservableCollection<Book> DeserializeBooks()
        {
            string json = File.ReadAllText(GetDirectory(_booksPath));
            var books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);

            if (books == null)
                return new ObservableCollection<Book>();

            return books;
        }

        public static void SerializeAuthors(ReadOnlyObservableCollection<Author> authors)
        {
            string json = JsonConvert.SerializeObject(authors);
            File.WriteAllText(GetDirectory(_authorsPath), json);
        }

        public static ObservableCollection<Author> DeserializeAuthors()
        {
            string json = File.ReadAllText(GetDirectory(_authorsPath));
            var authors = JsonConvert.DeserializeObject<ObservableCollection<Author>>(json);

            if (authors == null)
                return new ObservableCollection<Author>();

            return authors;
        }

        private static string GetDirectory(string relativePath)
        {
            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fullPath = Path.Combine(appDir, relativePath);
            return fullPath;
        }
    }
}
