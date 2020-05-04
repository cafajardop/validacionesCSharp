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

namespace brm
{
    class Program
    {
        static void Main(string[] args)
        {
            //ARCHIVO BUENO  10 registros
            //int LibraryIdIn = 19;
            //int CompanyIdIn = 102;
            //string CaseNumberIn = "DE47AE5B-22FE-404C-AE2C-F9A31F2BF66F";
            //string UserCodeIn = "carlosf";
            //string FileIdIn = "1dd25f56-ba1d-4a15-9fa3-f89b00b07620";
            //string CodeFileIn = "CWFFPLA1";
            //int periodln = 2;w
            //int yearln = 2019;
            //int operatorIdln = 3000;
            //int IdTypePopulationln = 7;


            //ARCHIVO MALO
            int LibraryIdIn = 37;
            int CompanyIdIn = 102;
            string CaseNumberIn = "CA027F90-B7C8-4D62-BADD-150C562B8C65";
            string UserCodeIn = "diego";
            string FileIdIn = "4d360c39-5e71-4442-9b12-90a673ca3ef1";
            string CodeFileIn = "CWFFPLA1";
            int periodln = 2;
            int yearln = 2019;
            int operatorIdln = 1220;
            int IdTypePopulationln = 7;


            //ARCHIVO BUENO CON 5452
            //int LibraryIdIn = 19;
            //int CompanyIdIn = 102;
            //string CaseNumberIn = "6CE3AF60-223D-47B5-8C04-D7E198B75990";
            //string UserCodeIn = "carlosf";
            //string FileIdIn = "f628bf7d-747d-48be-afe1-a82ea1224e81";
            //string CodeFileIn = "CWFFPLA1";
            //int periodln = 2;
            //int yearln = 2019;
            //int operatorIdln = 1220;
            //int IdTypePopulationln = 7;

            ResultPrototype_Expression y = new ResultPrototype_Expression();
            y.Execute(LibraryIdIn, CompanyIdIn,CaseNumberIn,UserCodeIn,FileIdIn,CodeFileIn,periodln,yearln,operatorIdln,IdTypePopulationln);

        }


    }
}
