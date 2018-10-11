using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ReaderObject
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Employe employe in EmployeServices.Instance.FindAllEmployes())
            {
                Console.WriteLine(employe);
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine(EmployeServices.Instance.FindEmployeById(4));
            Console.ReadKey();
        }
    }
}
