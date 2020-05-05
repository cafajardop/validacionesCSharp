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
            int OperatorIdLn = 1477;
            int LibraryIdLn = 19;
            int TemplateIdLn = 2072;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "CC2CD51F-D286-4355-BE5C-DC3423134698"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 4;
            int YearLn = 2019;
            string UserCodeLn = "erikab";
            string FileIdLn = "4030922f-1212-4fe8-bfdb-355aefc7ab3c"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            //RUL_ValidAllRules4505
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
