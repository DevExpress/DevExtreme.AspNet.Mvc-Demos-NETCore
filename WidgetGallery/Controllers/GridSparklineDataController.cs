using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models.Northwind;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class GridSparklineDataController : Controller {
        NorthwindContext _nwind;

        public GridSparklineDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        const int YEAR = 1997;

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions) {
            var customers = from c in _nwind.Customers
                            select new {
                                c.CustomerID,
                                c.CompanyName,
                                SumOfSales = (decimal?)(from d in _nwind.Order_Details
                                                        where d.Order.CustomerID == c.CustomerID
                                                        where d.Order.OrderDate.Value.Year == YEAR
                                                        select d.Quantity * d.UnitPrice).Sum()
                            };

            return DataSourceLoader.Load(customers, loadOptions);
        }

        [HttpGet]
        public object Sparkline(string customerID) {
            var salesPerMonth = Enumerable.Range(1, 12).ToDictionary(i => i, i => 0M);

            var salesQuery = from d in _nwind.Order_Details
                             let date = d.Order.OrderDate.Value
                             where d.Order.CustomerID == customerID && date.Year == YEAR
                             select new {
                                 date.Month,
                                 Sum = d.Quantity * d.UnitPrice
                             };

            foreach(var i in salesQuery)
                salesPerMonth[i.Month] += i.Sum;

            return from i in salesPerMonth
                   orderby i.Key
                   select new {
                       Month = i.Key,
                       Sum = i.Value
                   };
        }

    }
}
