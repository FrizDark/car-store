using System.Collections.Generic;
using System.Linq;
using System.Xml;

class Model
{
    public string Name { get; set; }
}
class Mark
{
    public string Name { get; set; }
    public ICollection<Model> Models { get; set; }
}

internal class Program
{

    private static List<Mark> _marks = new List<Mark>();

    static void Main(string[] args)
    {

        XmlTextReader reader = new XmlTextReader("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=XML");
        List<string> marks = new List<string>();

        //
        // Fetches marks from received XML file
        //
        while (reader.Read())
        {
            if (reader.Name == "Make_Name")
            {
                string content = reader.ReadInnerXml();
                if (content.Split(' ').Length == 1 && !content.Contains("&") && !content.Contains(";"))
                {
                    marks.Add(content);
                }
            }
        }

        //
        // Formats received marks
        //
        for (int i = 0; i < marks.Count; i++)
        {
            if (marks[i].Contains("."))
            {
                string[] parts = marks[i].Split('.');
                for (int j = 0; j < parts.Length; j++)
                {
                    if (parts[j].Length > 1)
                    {
                        parts[j] = $"{parts[j][0].ToString().ToUpper()}{parts[j].Substring(1).ToLower()}";
                    }
                    else
                    {
                        parts[j] = parts[j].ToUpper();
                    }
                }
                marks[i] = string.Join(".", parts);
            }
            else
            {
                marks[i] = $"{marks[i][0].ToString().ToUpper()}{marks[i].Substring(1).ToLower()}";
            }
        }

        //
        // Saves marks
        //
        foreach (string mark in marks.Distinct().ToList())
        {
            _marks.Add(new Mark { Name = mark });
        }

        //
        // Requests models for each mark
        //
        foreach (Mark item in _marks)
        {

            //
            // Requests XML file of models by mark
            //
            if (item.Name.Contains("."))
            {
                reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.Split('.')[0].ToLower()}?format=xml");
            }
            else if (item.Name.Contains("/"))
            {
                reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.Split('/')[0].ToLower()}?format=xml");
            }
            else
            {
                reader = new XmlTextReader($"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{item.Name.ToLower()}?format=xml");
            }
            List<string> models = new List<string>();

            //
            // Fetches models from XML file
            //
            while (reader.Read())
            {
                if (reader.Name == "Make_Name" && reader.ReadInnerXml() == item.Name.ToUpper())
                {
                    while (reader.Read())
                    {
                        if (reader.Name == "Model_Name")
                        {
                            string content = reader.ReadInnerXml();
                            if (content.Contains(";"))
                            {
                                foreach (string m in content.Split(';'))
                                {
                                    models.Add(m);
                                }
                            }
                            else if (content.Contains("/"))
                            {
                                foreach (string m in content.Split('/'))
                                {
                                    models.Add(m);
                                }
                            }
                            else
                            {
                                models.Add(content);
                            }
                            break;
                        }
                    }
                }
            }

            //
            // Adds models to current mark
            //
            foreach (string model in models.Distinct().ToList())
            {
                item.Models.Add(new Model { Name = model });
            }

        }

    }

}
