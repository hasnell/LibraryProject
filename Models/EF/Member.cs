using System;
using System.Collections.Generic;

namespace LibraryProject.Models.EF
{
    public partial class Member
    {
        public Member()
        {
            Histories = new HashSet<History>();
        }

        public int MemberId { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? UserAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public int? BookLimit { get; set; }

        public virtual ICollection<History> Histories { get; set; }
    }
}
