using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class Car
    {
        public Car()
        {
            CarPhotos = new HashSet<CarPhoto>();
            CarPropositions = new HashSet<CarProposition>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int ModelId { get; set; }
        public int Price { get; set; }
        public int ColorId { get; set; }
        public bool IsElectrical { get; set; }
        public string Description { get; set; } = null!;
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Power { get; set; }
        public int TankSize { get; set; }
        public int Seats { get; set; }

        public virtual CarModel Model { get; set; } = null!;
        public virtual CarType Type { get; set; } = null!;
        public virtual Color Color { get; set; } = null!;
        public virtual ICollection<CarPhoto> CarPhotos { get; set; }
        public virtual ICollection<CarProposition> CarPropositions { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
