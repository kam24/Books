using Books.Model;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Books.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public ReadOnlyObservableCollection<Author> Authors => _bookCollection.Authors;
        public ReadOnlyObservableCollection<Book> Books => _bookCollection.Books;
        public ReadOnlyObservableCollection<string> AuthorsBooks => GetAuthorsOrBooks();
        public DelegateCommand<string> AddCommand { get; }

        private readonly BookCollection _bookCollection;
        private object _selectedItem;

        public MainViewModel()
        {
            ObservableCollection<Author> authors = JsonHelper.DeserializeAuthors();
            ObservableCollection<Book> books = JsonHelper.DeserializeBooks();

            _bookCollection = new BookCollection(authors, books);

            AddCommand = new DelegateCommand<string>(AddToBookCollection);
        }

        private void AddToBookCollection(string str)
        {
            if (string.IsNullOrWhiteSpace(str) == false)
            {
                if (_selectedItem is Author author)
                {
                    _bookCollection.Add(new Book(str, author.FullName));
                    RaisePropertyChanged(nameof(AuthorsBooks));
                }
                else if (_selectedItem is Book book)
                {
                    _bookCollection.Add(new Author(str, book.Title));
                    RaisePropertyChanged(nameof(AuthorsBooks));
                }
            }
            else
            {
                MessageBox.Show("Введите имя");
            }
        }

        private DelegateCommand<Book> getAuthors;

        public ICommand GetAuthors
        {
            get
            {
                if (getAuthors == null)
                {
                    getAuthors = new DelegateCommand<Book>(PerformGetAuthors);
                }

                return getAuthors;
            }
        }

        private void PerformGetAuthors(Book book)
        {
            _selectedItem = book;
            RaisePropertyChanged(nameof(AuthorsBooks));
        }

        private DelegateCommand<Author> getBooks;

        public ICommand GetBooks
        {
            get
            {
                if (getBooks == null)
                {
                    getBooks = new DelegateCommand<Author>(PerformGetBooks);
                }

                return getBooks;
            }
        }

        private void PerformGetBooks(Author author)
        {
            _selectedItem = author;
            RaisePropertyChanged(nameof(AuthorsBooks));
        }

        private ReadOnlyObservableCollection<string> GetAuthorsOrBooks()
        {
            var collection = new ObservableCollection<string>();
            if (_selectedItem is Book book)
            {
                foreach (var author in book.Authors)
                {
                    collection.Add(author.FullName);
                }
            }
            else if (_selectedItem is Author author)
            {
                foreach (var book1 in author.Books)
                {
                    collection.Add(book1.Title);
                }
            }

            return new ReadOnlyObservableCollection<string>(collection);
        }

        private DelegateCommand windowClosing;

        public ICommand WindowClosing
        {
            get
            {
                if (windowClosing == null)
                {
                    windowClosing = new DelegateCommand(PerformWindowClosing);
                }

                return windowClosing;
            }
        }

        private void PerformWindowClosing()
        {
            JsonHelper.SerializeAuthors(_bookCollection.Authors);
            JsonHelper.SerializeBooks(_bookCollection.Books);
        }
    }
}
