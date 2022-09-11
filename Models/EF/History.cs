using System;
using System.Collections.Generic;

namespace LibraryProject.Models.EF
{
    public partial class History
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
