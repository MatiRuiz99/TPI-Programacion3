using Model.DTO;
using Model.Models;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SalesService : ISalesService
    {
        private readonly CafeteriaContext _context;

        public SalesService(CafeteriaContext context)
        {
            _context = context;
        }

        public string CreateRecord(SalesHistoryDTO producto)
        {

            string response = string.Empty;

            var user = _context.Users.FirstOrDefault(f => f.UserId == producto.UserId);
            var prod = _context.Producto.FirstOrDefault(s => s.IdProducto == producto.IdProducto);

            if (user != null && prod!=null)
            {
                _context.SalesHistory.Add(new SalesHistory()
                {
                    UserId = user.UserId,
                    ProductId = prod.IdProducto,
                    Price = producto.Price,
                    SaleDate = DateTime.Now,
                });
                _context.SaveChanges();
                response = "Compra exitosa";
            }
            else
            {
                response = "Error en la compra";
            }
            return response;
        }

        public List<SalesHistoryReturn> GetSalesHistory()
        {
            List<SalesHistoryReturn> SalesList = (from u in _context.SalesHistory
                                                 join r in _context.Users
                                                 on u.UserId equals r.UserId
                                                 join s in _context.Producto
                                                 on u.ProductId equals s.IdProducto
                                                 select new SalesHistoryReturn()
                                                 {
                                                     User = r.Name,
                                                     email = r.Email,
                                                     Prod = s.Nombre,
                                                     Price = u.Price,
                                                     date = u.SaleDate

                                                 }).ToList();

            return SalesList;
        }
    }
}
