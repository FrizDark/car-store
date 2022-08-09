using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class CarProposition
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
