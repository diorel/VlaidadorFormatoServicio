using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instancia
{
    class Program
    {
        static void Main(string[] args)
        {
            metodos obj = new metodos();

            obj.suma(2, 4);

            Console.WriteLine(obj.suma(2,4));

            obj.ImrpimirMensaje();

        }
    }
}
