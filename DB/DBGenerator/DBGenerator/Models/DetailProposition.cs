using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class DetailProposition
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DetailId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Detail Detail { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
