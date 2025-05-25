using StudentApiForMediator.Data;

namespace StudentApiForMediator.Services
{
    public class BookService
    {
        private Database _database;

        public BookService(Database database)
        {
            _database = database;
        }

        public void AddBook(int id,int studentId)
        {
            var book = _database.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new ArgumentException(nameof(id));
            }
            var student = _database.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new ArgumentException(nameof(studentId));
            }
            book.Students.Add(student);
        }
}
