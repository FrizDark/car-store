using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class Photo
    {
        public Photo()
        {
            CarPhotos = new HashSet<CarPhoto>();
            Details = new HashSet<Detail>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public byte[] Data { get; set; } = null!;

        public virtual ICollection<CarPhoto> CarPhotos { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
