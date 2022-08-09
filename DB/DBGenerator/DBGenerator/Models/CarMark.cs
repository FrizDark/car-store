using System;
using System.Collections.Generic;

namespace DBGenerator.Models
{
    public partial class CarMark
    {
        public CarMark()
        {
            CarModels = new HashSet<CarModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}
