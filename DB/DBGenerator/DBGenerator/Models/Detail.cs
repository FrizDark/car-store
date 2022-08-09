using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class Detail
    {
        public Detail()
        {
            DetailPropositions = new HashSet<DetailProposition>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int TypeId { get; set; }
        public string Brand { get; set; } = null!;
        public int PhotoId { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        public virtual Photo Photo { get; set; } = null!;
        public virtual DetailType Type { get; set; } = null!;
        public virtual ICollection<DetailProposition> DetailPropositions { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
