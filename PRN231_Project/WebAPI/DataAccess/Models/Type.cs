using System;
using System.Collections.Generic;

namespace WebAPI.DataAccess.Models
{
    public partial class Type
    {
        public Type()
        {
            Tournaments = new HashSet<Tournament>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
