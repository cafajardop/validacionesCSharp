using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiduProduccionRips
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

            param.CTFileId = "cbe86c25-3ccd-40e8-ae8a-278d2050d22b";
            param.AFFileId = "48f16c14-066a-405d-a4a8-cfcf77a84923";
            param.ACFileId = "c4bba24c-d566-4d1e-94da-e8dc83e1344c";
            param.APFileId = "50d91965-5398-4b10-bc62-4cf151ec3b90";
            param.AUFileId = "961adb9d-d91d-4c84-be33-839f19727f09";
            param.AHFileId = "9b3e6457-8c43-4411-a089-706805d111f7";
            param.ANFileId = "a623d288-8d14-4816-911e-92499b659976";
            param.ATFileId = "659bf00f-e7f1-4bcd-ba87-cccd20dc66a3";
            param.USFileId = "b30b4d77-94b0-4dc5-8369-216865e5f141";
            param.AMFileId = "8ef5e754-7d87-45c7-9de4-54a16bf8ce45";

            param.CompanyId = 102;
            param.CaseNumber = "E44B213A-5B93-430C-BC55-E190D548E0FF";
            param.OperatorId = 17;
            param.LibraryId = 2;
            param.IdTypePopulation = 1;
            param.TrackingCode = "85F22E72-9AF1-4122-8081-71A33FF86607";
            param.UserCode = "carlosf";
            param.InitDate = Convert.ToDateTime("2019-04-01");
            param.EndDate = Convert.ToDateTime("2019-04-30");

            var result = Helper.MainRIPS(param);
        }
    }
}
