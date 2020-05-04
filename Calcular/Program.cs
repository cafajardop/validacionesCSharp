using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calcular
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ENT_RipsParameters param = new ENT_RipsParameters();

            param.CTFileId = "9ffc7b03-47c1-4019-b125-86e589d539c5";
            param.AFFileId = "bd9b5858-0a8b-4ad0-87ac-ac7777c7f39a";
            param.ACFileId = "93a7a322-717a-4146-958f-7704f7c88eaf";
            param.APFileId = "78cef38d-ce93-4fe5-b4b5-b16bd954e0da";
            param.AUFileId = "3c9570f4-bfeb-4bef-930b-615f79cafddb";
            param.AHFileId = "30eee620-a94f-4e2f-96e9-4f0530846302";
            param.ANFileId = "1ddebe9a-01aa-48af-af18-79905ea4a1a6";
            param.ATFileId = "e71ab164-df8f-4438-a15a-c6df387dc1d6";
            param.USFileId = "02e27f77-b056-4021-84ac-66e0f5aa0a8f";
            param.AMFileId = "52dd9d50-2d27-43a4-9fcf-d5ec25335d78";

            param.CompanyId = 102;
            param.CaseNumber = "BDE9AF6C-C82B-492B-8E1F-A2E4CC060DAA";
            param.OperatorId = 23;
            param.LibraryId = 3;
            param.IdTypePopulation = 1;
            param.TrackingCode = "85F22E72-9AF1-4122-8081-71A33FF86607";
            param.UserCode = "carlosf";
            param.InitDate = Convert.ToDateTime("2019-04-01");
            param.EndDate = Convert.ToDateTime("2019-04-30");

            var result = Helper.MainRIPS(param);
            //y.Execute(CompanyIdLn, OperatorIdLn, LibraryIdLn, TemplateIdLn, FrmCodiLn, CaseNumberLn, PeriodLn, YearLn, UserCodeLn, FileIdLn, IdTypePopulationLn);
        }
    }
}
