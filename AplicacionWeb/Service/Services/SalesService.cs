using Model.DTO;
using Model.Models;
using Model.ViewModel;
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

        public string CreateRecord(SalesViewModel producto)
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
        public string ModifySaleHistory(int id, SalesViewModel sale)
        {
            var saleHistory = _context.SalesHistory.FirstOrDefault(s => s.Id == id);

            if (saleHistory != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == sale.UserId);
                var product = _context.Producto.FirstOrDefault(p => p.IdProducto == sale.IdProducto);

                if (user != null && product != null)
                {
                    saleHistory.UserId = user.UserId;
                    saleHistory.ProductId = product.IdProducto;
                    saleHistory.Price = sale.Price;
                    

                    _context.SaveChanges();
                    return "Historial de venta modificado exitosamente";
                }
                else
                {
                    return "Error al modificar el historial de venta (usuario o producto no encontrado)";
                }
            }
            else
            {
                return "Historial de venta no encontrado";
            }
        }

        public string DeleteSaleHistory(int id)
        {
            var saleHistory = _context.SalesHistory.FirstOrDefault(s => s.Id == id);

            if (saleHistory != null)
            {
                _context.SalesHistory.Remove(saleHistory);
                _context.SaveChanges();
                return "Historial de venta eliminado exitosamente";
            }
            else
            {
                return "Historial de venta no encontrado";
            }
        }

        public SalesHistoryReturn GetSaleById(int id)
        {
            var saleHistory = _context.SalesHistory.FirstOrDefault(s => s.Id == id);

            if (saleHistory != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == saleHistory.UserId);
                var product = _context.Producto.FirstOrDefault(p => p.IdProducto == saleHistory.ProductId);

                var saleHistoryReturn = new SalesHistoryReturn()
                {
                    User = user.Name,
                    email = user.Email,
                    Prod = product.Nombre,
                    Price = saleHistory.Price,
                    date = saleHistory.SaleDate
                };

                return saleHistoryReturn;
            }
            else
            {
                return null;
            }
        }

    }
}
