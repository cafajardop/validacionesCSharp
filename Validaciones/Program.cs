using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validaciones
{
    class Program
    {
        static void Main(string[] args)
        {

            string LastName;
            string EsFecha;
            DateTime fecha;
            EsFecha = "20199/11/07";

            //LastName = Console.ReadLine();

            //if (LastName == "")
            //    Console.WriteLine("Debe registrar 0 si no cuenta con un segundo nombre.Validar la variable 5 'Segundo nombre");

            //if (LastName.Length > 7)
            //    Console.WriteLine("El segundo Nombre supera los 7 caracteres");

            bool var1 =  DateTime.TryParse(EsFecha, out fecha);



            Console.ReadLine();

        }
    }
}
