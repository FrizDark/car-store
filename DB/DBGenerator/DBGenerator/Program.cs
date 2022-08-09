using DBGenerator.Models;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using static ImageLib.ImageLib;
using System.IO;
using Microsoft.EntityFrameworkCore;

#region Create marks and models
Console.WriteLine("Creating marks and models");
using (CarstoreDBContext db = new CarstoreDBContext(true))
{

    //List<CarMark> marks = db.CarMarks.Include(x => x.CarModels).ToList();

    //using (StreamWriter sw = File.CreateText("cars.txt"))
    //{
    //    foreach (CarMark mark in marks)
    //    {
    //        sw.WriteLine($"{mark.Name}|{string.Join(";", mark.CarModels.Select(x => x.Name).ToList())}");
    //    }
    //}

    using (StreamReader stream = File.OpenText("cars.txt"))
    {
        while (!stream.EndOfStream)
        {
            string[] line = stream.ReadLine()!.Split('|');
            string mark = line[0];
            string[] models = line[1].Split(';');
            CarMark carMark = new CarMark { Name = mark };
            foreach (string model in models)
            {
                carMark.CarModels.Add(new CarModel { Name = model });
            }
            db.CarMarks.Add(carMark);
        }
    }

    db.SaveChanges();

}

//List<CarMark> _marks = new List<CarMark>();

//XmlTextReader reader = new XmlTextReader("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=XML");
//List<string> marks = new List<string>();

////
//// Fetches marks from received XML file
////
//Console.WriteLine("Fetching marks");
//while (reader.Read())
//{
//    if (reader.Name == "Make_Name")
//    {
//        string content = reader.ReadInnerXml();
//        if (content.Split(' ').Length == 1 && !content.Contains("&") && !content.Contains(";"))
//        {
//            marks.Add(content);
//        }
//    }
//}

////
//// Formats received marks
////
//Console.WriteLine("Formatting marks");
//for (int i = 0; i < marks.Count; i++)
//{
//    if (marks[i].Contains("."))
//    {
//        string[] parts = marks[i].Split('.');
//        for (int j = 0; j < parts.Length; j++)
//        {
//            if (parts[j].Length > 1)
//            {
//                parts[j] = $"{parts[j][0].ToString().ToUpper()}{parts[j].Substring(1).ToLower()}";
//            }
//            else
//            {
//                parts[j] = parts[j].ToUpper();
//            }
//        }
//        marks[i] = string.Join(".", parts);
//    }
//    else
//    {
//        marks[i] = $"{marks[i][0].ToString().ToUpper()}{marks[i].Substring(1).ToLower()}";
//    }
//}

////
//// Saves marks
////
//Console.WriteLine("Saving marks");
//foreach (string mark in marks.Distinct().ToList())
//{
//    _marks.Add(new CarMark { Name = mark });
//}

////
//// Requests models for each mark
////
//Console.WriteLine("Fetching models");
//int n = 0;
//foreach (CarMark item in _marks)
//{
//    Console.SetCursorPosition(0, 5);
//    Console.WriteLine($"Fetching {++n}");
//    //
//    // Requests XML file of models by mark
//    //
//    if (item.Name.Contains("."))
//    {
//        reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.Split('.')[0].ToLower()}?format=xml");
//    }
//    else if (item.Name.Contains("/"))
//    {
//        reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.Split('/')[0].ToLower()}?format=xml");
//    }
//    else
//    {
//        reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.ToLower()}?format=xml");
//    }
//    List<string> models = new List<string>();

//    //
//    // Fetches models from XML file
//    //
//    while (reader.Read())
//    {
//        if (reader.Name == "Make_Name" && reader.ReadInnerXml() == item.Name.ToUpper())
//        {
//            while (reader.Read())
//            {
//                if (reader.Name == "Model_Name")
//                {
//                    string content = reader.ReadInnerXml();
//                    if (content.Contains(";"))
//                    {
//                        foreach (string m in content.Split(';'))
//                        {
//                            models.Add(m);
//                        }
//                    }
//                    else if (content.Contains("/"))
//                    {
//                        foreach (string m in content.Split('/'))
//                        {
//                            models.Add(m);
//                        }
//                    }
//                    else
//                    {
//                        models.Add(content);
//                    }
//                    break;
//                }
//            }
//        }
//    }

//    //
//    // Adds models to current mark
//    //
//    foreach (string model in models.Distinct().ToList())
//    {
//        item.CarModels.Add(new CarModel { Name = model });
//    }

//}

//Console.WriteLine("Removing marks dublicates");
//foreach (CarMark mark in _marks)
//{
//    foreach (CarMark dublicate in from m in _marks where m.Id != mark.Id && m.Id < mark.Id && m.Name == mark.Name select m)
//    {
//        Console.WriteLine($"Removed {dublicate.Name}");
//        _marks.Remove(dublicate);
//    }
//}

//Console.WriteLine("Saving marks");
//using (CarstoreDBContext db = new CarstoreDBContext(true))
//{
//    db.CarMarks.AddRange(_marks);
//    db.SaveChanges();
//}

//Console.WriteLine("Removing models dublicates");
//using (CarstoreDBContext db = new CarstoreDBContext())
//{
//    foreach (CarModel model in db.CarModels)
//    {
//        foreach (CarModel dublicate in db.CarModels
//            .Where(m => m.Id != model.Id && m.Id < model.Id && m.Name == model.Name && m.MarkId == model.MarkId))
//        {
//            Console.WriteLine($"Removed {dublicate.Name}");
//            db.CarModels.Remove(dublicate);
//        }
//    }
//    db.SaveChanges();
//}
#endregion
#region Methods
CarModel? GetModelByName(string mark, string model)
{
    try
    {
        using (CarstoreDBContext db = new CarstoreDBContext())
        {
            CarMark? carMark = db.CarMarks.Include(x => x.CarModels).FirstOrDefault(m => m.Name == mark);
            CarModel? carModel = carMark!.CarModels.FirstOrDefault(m => m.Name == model);
            return carModel;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return null;
    }
}
#endregion
#region Add types
Console.WriteLine("Adding types");
using (CarstoreDBContext db = new CarstoreDBContext())
{
    db.UserTypes.Add(new UserType { Name = "userType_User" });
    db.UserTypes.Add(new UserType { Name = "userType_Moderator" });
    db.UserTypes.Add(new UserType { Name = "userType_Admin" });

    db.CarTypes.Add(new CarType { Name = "carType_Hatchback" });
    db.CarTypes.Add(new CarType { Name = "carType_Sedan" });
    db.CarTypes.Add(new CarType { Name = "carType_Coupe" });
    db.CarTypes.Add(new CarType { Name = "carType_SUV" });
    db.CarTypes.Add(new CarType { Name = "carType_Convertible" });
    db.CarTypes.Add(new CarType { Name = "carType_Crossover" });
    db.CarTypes.Add(new CarType { Name = "carType_Pickup" });
    db.CarTypes.Add(new CarType { Name = "carType_Minivan" });
    db.CarTypes.Add(new CarType { Name = "carType_SportsCar" });
    db.CarTypes.Add(new CarType { Name = "carType_Van" });
    db.CarTypes.Add(new CarType { Name = "carType_Roadster" });
    db.CarTypes.Add(new CarType { Name = "carType_Supercar" });
    db.CarTypes.Add(new CarType { Name = "carType_Hypercar" });
    db.CarTypes.Add(new CarType { Name = "carType_MuscleCar" });
    db.CarTypes.Add(new CarType { Name = "carType_Compact" });

    db.DetailTypes.Add(new DetailType { Name = "detailType_Filters" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_Suspension" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_Steering" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_CoolingAndHeating" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_Lighting" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_Engine" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_Transmission" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_ElectronicsAndIgnitionSystem" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_CarBody" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_ExhaustSystem" });
    db.DetailTypes.Add(new DetailType { Name = "detailType_BrakeSystem" });

    db.SaveChanges();
}
#endregion
#region Add users
Console.WriteLine("Adding users");
using (CarstoreDBContext db = new CarstoreDBContext())
{
    byte[] vasya = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Vasya"));
    byte[] danya = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Petya"));
    byte[] anna = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Anna"));
    byte[] andrey = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Andrey"));
    byte[] igor = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Igor"));
    db.Users.Add(new User { Firstname = "Василій", Lastname = "Пупкін",     Email = "vasya@mail.com",   Phone = "(000)000-00-00", Password = vasya, TypeId = 1 });
    db.Users.Add(new User { Firstname = "Даниїл", Lastname = "Сідоров",     Email = "danya@mail.com",   Phone = "(111)111-11-11", Password = danya, TypeId = 1 });
    db.Users.Add(new User { Firstname = "Анна", Lastname = "Огурцова",      Email = "anna@mail.com",    Phone = "(222)222-22-22", Password = anna, TypeId = 2 });
    db.Users.Add(new User { Firstname = "Андрей", Lastname = "Кавунов",     Email = "andrey@mail.com",  Phone = "(333)333-33-33", Password = andrey, TypeId = 3 });
    db.Users.Add(new User { Firstname = "Ігор", Lastname = "Терабайтов",    Email = "igor@mail.com",    Phone = "(444)444-44-44", Password = igor, TypeId = 2 });

    db.SaveChanges();
}
#endregion
#region Add colors
using (CarstoreDBContext db = new CarstoreDBContext())
{
    db.Colors.Add(new Color { Name = "color_Red" });
    db.Colors.Add(new Color { Name = "color_Blue" });
    db.Colors.Add(new Color { Name = "color_Green" });
    db.Colors.Add(new Color { Name = "color_Orange" });
    db.Colors.Add(new Color { Name = "color_White" });
    db.Colors.Add(new Color { Name = "color_Black" });
    db.Colors.Add(new Color { Name = "color_Yellow" });
    db.Colors.Add(new Color { Name = "color_Purple" });
    db.Colors.Add(new Color { Name = "color_Brown" });
    db.Colors.Add(new Color { Name = "color_Gray" });
    db.Colors.Add(new Color { Name = "color_Pink" });
    db.SaveChanges();
}
#endregion
#region Add cars
Console.WriteLine("Adding cars");
byte[] bugattiChironImage = GetBytes(@"CarsImages\bugattiChiron.jpg", 1920, 1080);
byte[] chevroletCorvetteC8Image = GetBytes(@"CarsImages\chevroletCorvetteC8.jpg", 1920, 1080);
byte[] fordMustang2017Image = GetBytes(@"CarsImages\fordMustang2017.jpg", 1920, 1080);
byte[] hondaNSXImage = GetBytes(@"CarsImages\hondaNSX.jpg", 1920, 1080);
byte[] porscheTaycanImage = GetBytes(@"CarsImages\porscheTaycan.jpg", 1920, 1080);

CarModel bugattiChiron = GetModelByName("Bugatti", "Chiron")!;
CarModel chevroletCorvetteC8 = GetModelByName("Chevrolet", "C8")!;
CarModel fordMustang2017 = GetModelByName("Ford", "Mustang")!;
CarModel hondaNSX = GetModelByName("Acura", "NSX")!;
CarModel porscheTaycan = GetModelByName("Porsche", "Taycan")!;

using (CarstoreDBContext db = new CarstoreDBContext())
{
    Car bugatti = new Car
    {
        TypeId = 13,
        ModelId = bugattiChiron.Id,
        Price = 2990000,
        ColorId = 2,
        IsElectrical = false,
        Description = "середньомоторний, двомісний спортивний автомобіль, розроблений автобудівною компанією Bugatti (яка перебуває у власності Volkswagen Group) як наступник[1] Bugatti Veyron. Chiron був уперше показаним на Женевському автосалоні 1 березня 2016 року.",
        Length = 4544,
        Width = 2038,
        Height = 1212,
        Weight = 1888,
        Power = 1500,
        TankSize = 100,
        Seats = 2
    };
    Car chevrolet = new Car
    {
        TypeId = 12,
        ModelId = chevroletCorvetteC8.Id,
        Price = 59995,
        ColorId = 1,
        IsElectrical = false,
        Description = "В 2019 році дебютувало восьме покоління Corvette з двигуном, розташованим за кріслами, перед задньою віссю. Імовірно, Chevrolet Corvette C8 отримав мотор V8 6.2 л LT2, який буде в атмосферному варіанті або з двома турбокомпресорами. Автомобіль зможе успішніше боротися з центральномоторними конкурентами в кузовних серіях або гонках на витривалість.",
        Length = 4630,
        Width = 1933,
        Height = 1234,
        Weight = 1527,
        Power = 490,
        TankSize = 70,
        Seats = 2
    };
    Car ford = new Car
    {
        TypeId = 12,
        ModelId = fordMustang2017.Id,
        Price = 32800,
        ColorId = 1,
        IsElectrical = false,
        Description = "В грудні 2013 року представлено Mustang шостого покоління. Автомобіль отримав 3 бензинові двигуни, перший — це 3.7 V6 (304 к.с., 366 Нм), від попереднього покоління, другий — двигун 5.0 V8 (426 к.с., 529 Нм), також від попередника і новий двигун 2.3 EcoBoost (309 к.с., 407 Нм) від нового Ford Focus RS. Автомобіль вперше в історії отримав задню незалежну п'ятиважільну підвіску, а не залежну балку.",
        Length = 4784,
        Width = 1916,
        Height = 1381,
        Weight = 1680,
        Power = 300,
        TankSize = 60,
        Seats = 4
    };
    Car honda = new Car
    {
        TypeId = 12,
        ModelId = hondaNSX.Id,
        Price = 196300,
        ColorId = 4,
        IsElectrical = false,
        Description = "Acura NSX Concept був представлений 9 січня 2012 року на Північноамериканському міжнародному автосалоні. Друге покоління NSX — це гібридне повнопривідне купе зі середньомоторним компонуванням. Як силова установка використовується бензиновий бітурбодвигун V6 3,5 л потужністю 507 к.с. при 6500-7500 об/хв крутним моментом 550 Нм при 2000-6000 об/хв. Крім нього на передній осі встановлені два електромотора, які можуть генерувати позитивний і негативний крутний момент, тим самим збільшуючи стабільність і керованість при проходженні поворотів. Третій електромотор встановлений в коробці передач, він відповідає за напрямок крутного моменту до задньої осі.[1] Сумарна потужність складає 581 к.с. і 646 Нм. Розгін від 0 до 100 км/год займає 2.9 с, а максимальна швидкість 308 км/год.",
        Length = 4470,
        Width = 1930,
        Height = 1193,
        Weight = 1759,
        Power = 573,
        TankSize = 56,
        Seats = 2
    };
    Car porsche = new Car
    {
        TypeId = 12,
        ModelId = porscheTaycan.Id,
        Price = 153000,
        ColorId = 5,
        IsElectrical = true,
        Description = "Перший електромобіль виробництва німецької компанії Porsche.",
        Length = 4850,
        Width = 1990,
        Height = 1300,
        Weight = 2000,
        Power = 522,
        TankSize = 93,
        Seats = 4
    };
    bugatti.CarPhotos.Add(new CarPhoto
    {
        Photo = new Photo { Data = bugattiChironImage }
    });
    chevrolet.CarPhotos.Add(new CarPhoto
    {
        Photo = new Photo { Data = chevroletCorvetteC8Image }
    });
    ford.CarPhotos.Add(new CarPhoto
    {
        Photo = new Photo { Data = fordMustang2017Image }
    });
    honda.CarPhotos.Add(new CarPhoto
    {
        Photo = new Photo { Data = hondaNSXImage }
    });
    porsche.CarPhotos.Add(new CarPhoto
    {
        Photo = new Photo { Data = porscheTaycanImage }
    });
    db.Cars.Add(bugatti);
    db.Cars.Add(chevrolet);
    db.Cars.Add(ford);
    db.Cars.Add(honda);
    db.Cars.Add(porsche);

    db.SaveChanges();
}
#endregion
#region Add details
Console.WriteLine("Adding details");
using (CarstoreDBContext db = new CarstoreDBContext())
{
    Photo ph1 = new Photo { Data = GetBytes(@"DetailsImages\1.jpg", 800, 800) };
    Photo ph2 = new Photo { Data = GetBytes(@"DetailsImages\2.jpg", 800, 800) };
    Photo ph3 = new Photo { Data = GetBytes(@"DetailsImages\3.jpg", 800, 800) };
    Photo ph4 = new Photo { Data = GetBytes(@"DetailsImages\4.jpg", 800, 800) };
    Photo ph5 = new Photo { Data = GetBytes(@"DetailsImages\5.jpg", 800, 800) };

    db.Details.Add(new Detail
    {
        Name = "Амортизатор підвіски задньої масляний Premium",
        TypeId = 2,
        Brand = "KYB",
        Photo = ph1,
        Price = 1082
    });
    db.Details.Add(new Detail
    {
        Name = "Фільтр повітряний",
        TypeId = 1,
        Brand = "Eurorepar",
        Photo = ph2,
        Price = 555,
        Description = "Бренд EUROREPAR представлений на ринках понад 100 країн. EUROREPAR пропонує лінійки мультибрендових продуктів (запасні частини, аксесуари, шини, олії, витратні матеріали тощо) від концерну Stellantis для технічного обслуговування та ремонту транспортних засобів."
    });
    db.Details.Add(new Detail
    {
        Name = "Батарея акумуляторна 12В 60Ач 540A",
        TypeId = 8,
        Brand = "Bosch",
        Photo = ph3,
        Price = 3118
    });
    db.Details.Add(new Detail
    {
        Name = "Свічка запалювання Nickel TT K16TT",
        TypeId = 8,
        Brand = "Denso",
        Photo = ph4,
        Price = 102,
        Description = "Свічки запалювання з поліпшеним іскроутворенням практично досягають ефективності високоякісних іридієвих свічок DENSO, при цьому не використовуючи дорогих дорогоцінних металів. Кращі показники при менших витратах. Термін служби: до 30 000 км. Створені для холодної погоди. Оскільки тепер потрібно більш низьку напругу для того, щоб запустити ваш двигун, то двигун заводиться набагато швидше і впевненіше навіть при екстремально холодних погодних умовах."
    });
    db.Details.Add(new Detail
    {
        Name = "Лампа галогенна +30% H7 12V 55W",
        TypeId = 5,
        Brand = "Philips",
        Photo = ph5,
        Price = 150
    });

    db.SaveChanges();
}
#endregion
#region Add propositions
Console.WriteLine("Adding propositions");
using (CarstoreDBContext db = new CarstoreDBContext())
{
    Random rnd = new Random();
    List<Car> cars = db.Cars.ToList();
    List<Detail> details = db.Details.ToList();
    List<User> users = db.Users.ToList();
    for (int i = 0; i < Math.Min(cars.Count, users.Count); i++)
    {
        db.CarPropositions.Add(new CarProposition
        {
            CarId = cars[i].Id,
            UserId = users[i].Id,
            CreationDate = new DateTime(2022, rnd.Next(5, 9), rnd.Next(1, 31))
        });
    }
    for (int i = 0; i < Math.Min(details.Count, users.Count); i++)
    {
        db.DetailPropositions.Add(new DetailProposition
        {
            DetailId = details[i].Id,
            UserId = users[i].Id,
            CreationDate = new DateTime(2022, rnd.Next(5, 9), rnd.Next(1, 31))
        });
    }

    db.SaveChanges();
}
#endregion