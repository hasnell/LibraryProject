using System;
using System.Collections.Generic;

namespace LibraryProject.Models.EF
{
    public partial class Book
    {
        public Book()
        {
            CurrentLoans = new HashSet<CurrentLoan>();
            Histories = new HashSet<History>();
        }

        public int BookId { get; set; }
        public int Isbn { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int? PublishYear { get; set; }
        public string? Category { get; set; }

        public virtual ICollection<CurrentLoan> CurrentLoans { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
