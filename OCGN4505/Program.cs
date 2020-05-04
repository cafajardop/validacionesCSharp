using OpheliaSuiteV2.BRMRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCGN4505
{
    class Program
    {
        static void Main(string[] args)
        {
            int CompanyIdLn = 102;
            int OperatorIdLn = 1479;
            int LibraryIdLn = 19;
            int TemplateIdLn = 2072;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "38DAAE12-5A8F-4A6B-BDD4-9C21A7474883"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 4;
            int YearLn = 2019;
            string UserCodeLn = "1005625490";
            string FileIdLn = "a2aae584-803d-4b0e-b81f-424cb788ef7a"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            //RUL_ValidAllRules4505
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
