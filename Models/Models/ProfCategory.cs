using System;
using System.Collections.Generic;

namespace Models
{
    public partial class ProfCategory
    {
        public ProfCategory()
        {
            Profession = new HashSet<Profession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profession> Profession { get; set; }
    }
}
