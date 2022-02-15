using System;
using System.Xml;

namespace learningxml
{ 
    class ReadWriteXML
    {
        public void readdoc(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList employeeNodes = xmlDoc.SelectNodes("//employees/employee");

            Console.WriteLine("=============================");

            foreach (XmlNode emp in employeeNodes)
            {
                Console.WriteLine($"ID: {emp.Attributes["ID"].Value} | Name: {emp.Attributes["Name"].Value} | Salary {emp.Attributes["Salary"].Value}");
            }
            Console.ReadKey();
        }

        public void writedoc(string path)
        {
            Random rand = new Random();

            string[] names = { "Joe", "Emma", "Liam", "Kamil", "Waldo", "Beth", "Liz", "Andrew" };

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("employees");
            xmlDoc.AppendChild(rootNode);

            foreach (string name in names)
            {
                XmlNode empNode = xmlDoc.CreateElement("employee");

                XmlAttribute attribute = xmlDoc.CreateAttribute("ID");
                attribute.Value = $"{rand.Next() % 100}";
                empNode.Attributes.Append(attribute);

                attribute = xmlDoc.CreateAttribute("Name");
                attribute.Value = $"{name}";
                empNode.Attributes.Append(attribute);

                attribute = xmlDoc.CreateAttribute("Salary");
                attribute.Value = $"{30000 + 10000 * (rand.Next() % 10)}";
                empNode.Attributes.Append(attribute);

                rootNode.AppendChild(empNode);
            }
            xmlDoc.Save(path);
        }

        public void changeMinSalary(string path, int newMin) //change minimum salary of employees to newMin
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList employeeNodes = xmlDoc.SelectNodes("//employees/employee");

            foreach (XmlNode emp in employeeNodes)
            {
                int empSalary = int.Parse(emp.Attributes["Salary"].Value);

                if (empSalary < newMin)
                {
                    emp.Attributes["Salary"].Value = $"{newMin}";
                }
            }

            Console.WriteLine($"\nMinimum salary changed to ${newMin}\n");
            xmlDoc.Save(path);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            ReadWriteXML rw = new ReadWriteXML();
            string path = "C:\\Users\\kamil\\source\\repos\\employees.xml";

            rw.writedoc(path);
            rw.readdoc(path);
            rw.changeMinSalary(path, 70000);
            rw.readdoc(path);
        }
    }
}