using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carstore.Model
{
    public class DetailTableModel
    {

        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Seller { get; set; }

        public DetailTableModel()
        {
            Id = 0;
            Photo = new byte[0];
            Name = "";
            Brand = "";
            Type = "";
            Price = 0;
            Seller = "";
        }

        public DetailTableModel(int id, byte[] photo, string name, string brand, string type, int price, string seller)
        {
            Id = id;
            Photo = photo;
            Name = name;
            Brand = brand;
            Type = type;
            Price = price;
            Seller = seller;
        }

        public DetailTableModel(DetailPurpose purpose)
        {
            Id = purpose.Id;
            Photo = purpose.Detail.Photo.Data;
            Name = purpose.Detail.Name;
            Brand = purpose.Detail.Brand;
            Type = purpose.Detail.DetailType.Name;
            Price = purpose.Detail.Price;
            Seller = $"{purpose.User.Firstname} {purpose.User.Lastname}";
        }

    }
}
