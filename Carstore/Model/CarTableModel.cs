using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carstore.Model
{
    public class CarTableModel
    {

        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public int Power { get; set; }

        public CarTableModel()
        {
            Id = 0;
            Photo = new byte[0];
            Mark = "";
            Model = "";
            Color = "";
            Price = 0;
            Power = 0;
        }

        public CarTableModel(int id, byte[] photo, string mark, string model, string color, int price, int power)
        {
            Id = id;
            Photo = photo;
            Mark = mark;
            Model = model;
            Color = color;
            Price = price;
            Power = power;
        }

        public CarTableModel(Car car)
        {
            Id = car.Id;
            Photo = car.CarPhoto.First().Photo.Data;
            Mark = car.CarModel.CarMark.Name;
            Model = car.CarModel.Name;
            Color = car.Color;
            Price = car.Price;
            Power = car.Power;
        }
    }
}
