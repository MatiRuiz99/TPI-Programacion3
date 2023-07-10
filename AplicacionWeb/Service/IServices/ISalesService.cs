using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface ISalesService
    {
        string CreateRecord(SalesViewModel producto);
        List<SalesHistoryReturn> GetSalesHistory();
        string ModifySaleHistory(int id, SalesViewModel sale);
        string DeleteSaleHistory(int id);
        SalesHistoryReturn GetSaleById(int id);
    }
}
