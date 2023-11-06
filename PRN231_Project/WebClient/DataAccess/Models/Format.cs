using System;
using System.Collections.Generic;

namespace CoFAB.DataAccess.Models
{
    public partial class Format
    {
        public Format()
        {
            Tournaments = new HashSet<Tournament>();
        }

        public int FormatId { get; set; }
        public string? FormatName { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
