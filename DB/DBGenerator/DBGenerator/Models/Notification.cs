using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public int? CarId { get; set; }
        public int? DetailId { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Detail? Detail { get; set; }
        public virtual User UserFrom { get; set; } = null!;
        public virtual User UserTo { get; set; } = null!;
    }
}
