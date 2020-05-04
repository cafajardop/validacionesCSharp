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
            VC_Execute1393File_Expression CL = new VC_Execute1393File_Expression();

            int CompanyIdLn = 102; 
            int OperatorIdLn = 16; 
            int LibraryIdLn = 22; 
            int TemplateIdLn = 2072; 
            string FrmCodiLn = "CWFFPLA1"; 
            string CaseNumberLn = "38D39DD1-DE21-49F2-92FC-503469F4DCA7"; 
            //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731             
            int PeriodLn = 1;             
            int YearLn = 2019;             
            string UserCodeLn = "ERIKAB";             
            string FileIdLn = "14d57fb4-de4d-4152-98c1-6eadf8d8ee98"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731             
            
            var c = CL.Execute(LibraryIdLn, CompanyIdLn, CaseNumberLn, UserCodeLn, FileIdLn, "", PeriodLn, YearLn, OperatorIdLn, 2);

            Console.WriteLine(c.Message);
            Console.ReadLine();

        }
    }
}
