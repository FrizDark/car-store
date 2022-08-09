using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class DetailType
    {
        public DetailType()
        {
            Details = new HashSet<Detail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Detail> Details { get; set; }
    }
}
