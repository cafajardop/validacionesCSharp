using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using OpheliaSuiteV2.BRMRuntime;

namespace ReglaMain4505
{
    class Program
    {
        static void Main(string[] args)
        {
            int CompanyIdLn = 102;
            int OperatorIdLn = 1565;
            int LibraryIdLn = 16;
            int TemplateIdLn = 2072;
            string FrmCodiLn = "CWFFPLA1";
            string CaseNumberLn = "E87CD8EE-0652-4123-BE99-39DDE86D90E4"; //EC0E697E-64B8-4847-B353-FDB781D7C2D9 => Registro cargado por Erika 4731
            int PeriodLn = 3;
            int  YearLn = 2019;
            string UserCodeLn = "ERIKAB";
            string FileIdLn = "cc0b2564-5b86-42fc-afcb-df8be8a32a46"; //975e78ba-28b6-4302-8a20-eac4a7c09c09 => Registro cargado por Erika 4731
            int IdTypePopulationLn = 1;
            //RUL_ValidAllRules4505
            ResultPrototype_Expression y = new ResultPrototype_Expression();
            var result =  y.Execute(CompanyIdLn,OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn,IdTypePopulationLn);

            Console.WriteLine(result.Message);
            Console.ReadLine();
        }
    }
}
