using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class User
    {
        public User()
        {
            CarPropositions = new HashSet<CarProposition>();
            DetailPropositions = new HashSet<DetailProposition>();
            NotificationUserFroms = new HashSet<Notification>();
            NotificationUserTos = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public int TypeId { get; set; }
        public int? AvatarId { get; set; }

        public virtual Photo? Avatar { get; set; }
        public virtual UserType Type { get; set; } = null!;
        public virtual ICollection<CarProposition> CarPropositions { get; set; }
        public virtual ICollection<DetailProposition> DetailPropositions { get; set; }
        public virtual ICollection<Notification> NotificationUserFroms { get; set; }
        public virtual ICollection<Notification> NotificationUserTos { get; set; }
    }
}
