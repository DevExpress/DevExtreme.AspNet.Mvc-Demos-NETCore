using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery.Models.Northwind;
using Microsoft.AspNetCore.Mvc;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class ChartsDataController : Controller {
        NorthwindContext _nwind;

        public ChartsDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        [HttpGet]
        public object SalesByCategoryYear() {
            var sales = from d in _nwind.Order_Details
                        let year = d.Order.OrderDate.Value.Year
                        let category = d.Product.Category.CategoryName
                        orderby year, category
                        group d by new { Year = year, Category = category } into g
                        select new {
                            g.Key.Year,
                            g.Key.Category,
                            Sales = g.Sum(d => d.Quantity * d.UnitPrice)
                        };

            return sales;
        }

        [HttpGet]
        public object SalesByCategory() {
            var catSales = from d in _nwind.Order_Details
                           group d by d.Product.Category.CategoryName into g
                           let sales = g.Sum(d => d.Quantity * d.UnitPrice)
                           orderby sales descending
                           select new {
                               Category = g.Key,
                               Sales = sales,
                               Count = g.Count()
                           };

            return catSales;
        }

        [HttpGet]
        public object ShipsByMonth(string shipper) {
            var ships = from o in _nwind.Orders
                        where o.Shipper != null
                        orderby o.OrderDate
                        group o by new DateTime(o.OrderDate.Value.Year, o.OrderDate.Value.Month, 1, 0, 0, 0) into g
                        select new {
                            Month = g.Key,
                            Amount = g.Count(o => o.Shipper.CompanyName == shipper),
                            TotalAmount = g.Count()
                        };

            return ships;
        }

    }
}
