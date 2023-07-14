using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mappings;
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
        private readonly IMapper _mapper;

        public SalesService(CafeteriaContext context)
        {
            
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public SalesHistoryDTO CreateRecord(SalesViewModel producto)
        {
            SalesHistoryDTO response;

            var user = _context.Users.FirstOrDefault(f => f.UserId == producto.UserId);
            var prod = _context.Producto.FirstOrDefault(s => s.IdProducto == producto.IdProducto);
            if (user != null && prod != null)
            {
                var sale = _mapper.Map<SalesHistory>(producto);
                sale.SaleDate = DateTime.Now;
                _context.SalesHistory.Add(sale);
                _context.SaveChanges();
                response = _mapper.Map<SalesHistoryDTO>(sale);
            }
            else
            {
                response = null;
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
                                                     id = u.Id,
                                                     UserId = r.UserId,
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
                    return "Sale history modified successfully";
                }
                else
                {
                    return "Error modifying sale history (user or product not found)";
                }
            }
            else
            {
                return "Sale history not found";
            }

        }

        public string DeleteSaleHistory(int id)
        {
            var sale = _context.SalesHistory.FirstOrDefault(s => s.Id == id);
            if (sale != null) { 
                _context.SalesHistory.Remove(sale);
                _context.SaveChanges();
                return "Sale history deleted successfully";
            } else
            {
                return null;
            }
        }

        public SalesHistoryReturn GetSaleById(int id)
        {
            var saleHistory = _context.SalesHistory.First(s => s.Id == id);

            if (saleHistory != null)
            {
                var user = _context.Users.First(u => u.UserId == saleHistory.UserId);
                var product = _context.Producto.First(p => p.IdProducto == saleHistory.ProductId);

                var saleHistoryReturn = new SalesHistoryReturn()
                {
                    id = saleHistory.Id,
                    UserId= user.UserId,
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

        public List<BestSellingProdDTO> GetTopSellingItems()
        {
            var topSellingItems = _context.SalesHistory
                .GroupBy(s => s.ProductId)
                .Select(g => new BestSellingProdDTO
                {
                    ProductId = g.Key,
                    Name = _context.Producto.FirstOrDefault(p => p.IdProducto == g.Key).Nombre,
                    QuantitySold = g.Count()
                })
                .OrderByDescending(s => s.QuantitySold)
                .Take(3)
                .ToList();

            return topSellingItems;
        }


    }
}
