using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class CarPhoto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int PhotoId { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual Photo Photo { get; set; } = null!;
    }
}
