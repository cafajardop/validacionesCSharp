using OpheliaSuiteV2.BRMRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglaMain0247
{
    class Program
    {
        static void Main(string[] args)
        {
            //LINEA BASE

            //int CompanyIdLn = 102;
            //int OperatorIdLn = 1455;
            //int LibraryIdLn = 20;
            //int TemplateIdLn = 2080;
            //string FrmCodiLn = "CWFFPLA1";
            //string CaseNumberLn = "9CF836C5-6ED5-41C2-9022-F0CB808C89E7"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            //int PeriodLn = 1;
            //int YearLn = 2019;
            //string UserCodeLn = "ERIKAB";
            //string FileIdLn = "1ee88ab0-aa25-44e2-8cfe-d35762e506b6"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            //int IdTypePopulationLn = 1;
            //ResultPrototype_Expression y = new ResultPrototype_Expression();
            //var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            //OCGN
            //int CompanyIdLn = 102;
            //int OperatorIdLn = 1455;
            //int LibraryIdLn = 20;
            //int TemplateIdLn = 2080;
            //string FrmCodiLn = "CWFFPLA1";
            //string CaseNumberLn = "9CF836C5-6ED5-41C2-9022-F0CB808C89E7"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            //int PeriodLn = 1;
            //int YearLn = 2019;
            //string UserCodeLn = "ERIKAB";
            //string FileIdLn = "1ee88ab0-aa25-44e2-8cfe-d35762e506b6"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            //int IdTypePopulationLn = 1;
            //ResultPrototype_Expression y = new ResultPrototype_Expression();
            //var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            //FIDUPRUEBAS
            int CompanyIdLn = 102;
            int OperatorIdLn = 1435;
            int LibraryIdLn = 20;
            int TemplateIdLn = 2080;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "C692C255-0314-4647-B3D2-63E00F4A5AA9"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 1;
            int YearLn = 2019;
            string UserCodeLn = "erikab";
            string FileIdLn = "d91ce541-0112-4f43-b6ba-ca48ca3ce0e8"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
