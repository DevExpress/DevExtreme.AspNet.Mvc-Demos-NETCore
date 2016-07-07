using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models.Northwind;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class PivotGridDataController : Controller {
        NorthwindContext _nwind;

        public PivotGridDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        [HttpGet]
        public object Get() {
            var q = from d in _nwind.Order_Details
                    let p = d.Product
                    let o = d.Order
                    select new {
                        o.OrderDate,
                        p.ProductName,
                        p.Category.CategoryName,
                        Sum = d.Quantity * d.UnitPrice
                    };

            return q;
        }

    }

}
