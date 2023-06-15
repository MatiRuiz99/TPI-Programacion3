using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SalesService
    {

        public string CreateRecord(SalesHistoryDTO producto)
        {

            return "Creado correctamente";
        }

        public string GetSalesHistory()
        {
            return "Lista de datos";
        }
    }
}
