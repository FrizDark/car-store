using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carstore.Model;

namespace Carstore.Model
{
    public class NotificationTableModel
    {

        public int Id { get; set; }
        public string UserFrom { get; set; }
        public string UserFromEmail { get; set; }
        public string UserFromPhone { get; set; }
        public string UserTo { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public string Date { get; set; }

        public NotificationTableModel()
        {
            Id = 0;
            UserFrom = "";
            UserTo = "";
            Description = "";
            IsRead = false;
            Date = "1.1.2022";
        }

        public NotificationTableModel(
            int id, 
            string userFrom, 
            string userFromEmail, 
            string userFormPhone, 
            string userTo, 
            string description, 
            bool isRead, 
            string date)
        {
            Id = id;
            UserFrom = userFrom;
            UserFromEmail = userFromEmail;
            UserFromPhone = userFormPhone;
            UserTo = userTo;
            Description = description;
            IsRead = isRead;
            Date = date;
        }

        public NotificationTableModel(Notification notification)
        {
            Id = notification.Id;
            UserFrom = $"{notification.User.Firstname} {notification.User.Lastname}";
            UserFromEmail = notification.User.Email;
            UserFromPhone = notification.User.Phone;
            UserTo = $"{notification.User1.Firstname} {notification.User1.Lastname}";
            if (notification.CarId != null)
            {
                Description = $"{UserFrom} {Properties.Resources.notificationsView_wantsToBuy} {notification.Car.CarModel.CarMark.Name} {notification.Car.CarModel.Name}";
            }
            else if (notification.DetailId != null)
            {
                Description = $"{UserFrom} {Properties.Resources.notificationsView_wantsToBuy} {notification.Detail.Name} {Properties.Resources.notificationsView_by} {notification.Detail.Brand}";
            }
            else
            {
                Description = notification.Description;
            }
            IsRead = notification.IsRead;
            Date = notification.Date.ToShortDateString();
        }

    }
}
