using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace learningxml
{
    public class Employees
    {
        public List<Employee> employeeList = new List<Employee>();
    }
    public class Employee
    {
        public string ID;
        public string name;
        public string salary;
    }

    public class ReadXML
    {
        public void readdoc(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employees));
            FileStream fs = new FileStream(path, FileMode.Open);
                       
            Employees empList = (Employees)serializer.Deserialize(fs);

            foreach (var emp in empList.employeeList)
            {
                Console.WriteLine($"{emp.ID}, {emp.name}, {emp.salary}");
            }
        }
    }

    class WriteXML
    {  
        public void writedoc(string path)
        {
            Random rand = new Random();

            string[] names = { "Joe", "Emma", "Liam", "Kamil", "Waldo", "Beth", "Liz", "Andrew" };

            
            XmlSerializer serializer = new XmlSerializer(typeof(Employees));
            TextWriter writer = new StreamWriter(path);

            Employees empList = new Employees();

            foreach (string name in names)
            {
                Employee em = new Employee();
                em.ID = $"{rand.Next() % 100}";
                em.name = $"{name}";
                em.salary = $"{30000 + 10000 * (rand.Next() % 10)}";
                empList.employeeList.Add(em);              
            }

            serializer.Serialize(writer, empList);
            writer.Close();      
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteXML wx = new WriteXML();
            ReadXML rx = new ReadXML();
            
            string path = "C:\\Users\\kamil\\source\\repos\\employees.xml";

            wx.writedoc(path);
            rx.readdoc(path);
        }
    }
}