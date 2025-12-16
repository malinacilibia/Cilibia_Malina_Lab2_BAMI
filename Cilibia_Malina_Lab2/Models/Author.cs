using Cilibia_Malina_Lab2Context.Models;

namespace Cilibia_Malina_Lab2Context.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
