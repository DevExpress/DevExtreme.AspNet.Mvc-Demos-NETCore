using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models.Northwind;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class GridDataController : Controller {
        NorthwindContext _nwind;

        public GridDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_nwind.Orders, loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values) {
            var newOrder = new Order();
            JsonConvert.PopulateObject(values, newOrder);

            if(!TryValidateModel(newOrder))
                return BadRequest(ModelState.GetFullErrorMessage());

            _nwind.Orders.Add(newOrder);
            _nwind.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int key, string values) {
            var order = _nwind.Orders.First(o => o.OrderID == key);
            JsonConvert.PopulateObject(values, order);

            if(!TryValidateModel(order))
                return BadRequest(ModelState.GetFullErrorMessage());

            _nwind.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public void Delete(int key) {
            var order = _nwind.Orders.First(o => o.OrderID == key);
            _nwind.Orders.Remove(order);
            _nwind.SaveChanges();
        }

        // additional actions

        [HttpGet]
        public object OrderDetails(int orderID, DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(
                from i in _nwind.Order_Details
                where i.OrderID == orderID
                select new {
                    Product = i.Product.ProductName,
                    Price = i.UnitPrice,
                    Quantity = i.Quantity,
                    Sum = i.UnitPrice * i.Quantity
                },
                loadOptions
            );
        }

        [HttpGet]
        public object ShippersLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _nwind.Shippers
                         orderby i.CompanyName
                         select new {
                             Value = i.ShipperID,
                             Text = i.CompanyName
                         };

            return DataSourceLoader.Load(lookup, loadOptions);
        }

        [HttpGet]
        public object CustomersLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _nwind.Customers
                         let text = i.CompanyName + " (" + i.Country + ")"
                         orderby i.CompanyName
                         select new {
                             Value = i.CustomerID,
                             Text = text
                         };

            return DataSourceLoader.Load(lookup, loadOptions);
        }
    }

}
