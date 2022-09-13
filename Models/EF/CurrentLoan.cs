using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace LibraryProject.Models.EF
{
    public partial class CurrentLoan
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual Book Book { get; set; } = null!;
    }

}
