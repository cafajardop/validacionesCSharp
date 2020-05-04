using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiduPruebasRips
{
    class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ENT_RipsParameters param = new ENT_RipsParameters();

            param.CTFileId = "be16c64c-a9f5-4d8e-9cf9-bb7e745ba650";
            param.AFFileId = "6074ad8e-3713-4d95-be28-8cac06e1a741";
            param.ACFileId = "e6c266bb-95f2-4db5-8799-35409058d79f";
            param.APFileId = "ff00392a-9a70-434b-8773-0186581d6c07";
            param.AUFileId = "f395df29-33ec-4179-9237-f081b50ec34f";
            param.AHFileId = "d8f692a9-8b87-42f9-ac41-0483149ecab9";
            param.ANFileId = "e8b182e2-ca32-4198-b462-74f754bc0815";
            param.ATFileId = "70a757e0-3581-4e60-a473-061288528c36";
            param.USFileId = "bdca35d5-2758-4be9-a0db-faf3e0845674";
            param.AMFileId = "117740f4-2a20-4e12-81e8-2df463716cdb";

            param.CompanyId = 102;
            param.CaseNumber = "6A19A003-FD82-462E-B061-6D7415DA0CE7";
            param.OperatorId = 15;
            param.LibraryId = 3;
            param.IdTypePopulation = 1;
            param.TrackingCode = "85F22E72-9AF1-4122-8081-71A33FF86607";
            param.UserCode = "carlosf";
            param.InitDate = Convert.ToDateTime("2019-04-01");
            param.EndDate = Convert.ToDateTime("2019-04-30");

            var result = Helper.MainRIPS(param);
        }
    }
}
