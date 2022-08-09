using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int MarkId { get; set; }

        public virtual CarMark Mark { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }
    }
}
