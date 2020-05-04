using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolucion0247_OCGN
{
    class Program
    {
        static void Main(string[] args)
        {
            int CompanyIdLn = 102;
            int OperatorIdLn = 1252;
            int LibraryIdLn = 20;
            int TemplateIdLn = 2080;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "BA902441-24A8-4700-8587-80403D167844"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 1;
            int YearLn = 2019;
            string UserCodeLn = "ERIKAB";
            string FileIdLn = "91f49bc7-c08f-4270-982a-d28946e4e995"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result = y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
