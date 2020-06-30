using OpheliaSuiteV2.BRMRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidupruebas4505
{
    class Program
    {
        static void Main(string[] args)
        {

            int CompanyIdLn = 102;
            int OperatorIdLn = 17;
            int LibraryIdLn = 16;
            int TemplateIdLn = 2072;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "57D3C2B9-1C85-46C1-8A53-A35E6E597B7E"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 3;
            int YearLn = 2019;
            string UserCodeLn = "Erikab";
            string FileIdLn = "e6538ab6-f43f-4da2-8db4-50272502e02d"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            //RUL_ValidAllRules4505
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();

        }
    }
}
