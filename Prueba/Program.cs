using OpheliaSuiteV2.BRMRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {

            string param1 = "55";
            string param2 = "C";

            VC_Retorno_Expression y = new VC_Retorno_Expression();
            var result = y.Execute(param1, param2);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
