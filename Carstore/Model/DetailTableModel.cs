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

        public DetailTableModel(DetailProposition proposition)
        {
            Id = proposition.Id;
            Photo = proposition.Detail.Photo.Data;
            Name = proposition.Detail.Name;
            Brand = proposition.Detail.Brand;
            Type = proposition.Detail.DetailType.Name;
            Price = proposition.Detail.Price;
            Seller = $"{proposition.User.Firstname} {proposition.User.Lastname}";
        }

    }
}
