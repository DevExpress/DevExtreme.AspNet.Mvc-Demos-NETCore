using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models;
using WidgetGallery.Models.Northwind;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class SchedulerDataController : Controller {
        NorthwindContext _nwind;

        public SchedulerDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        [HttpGet]
        public object Appointments(DataSourceLoadOptions loadOptions) {
            var q = from o in _nwind.Orders
                    where o.EmployeeID == 7
                    select new {
                        OrderDate = o.OrderDate,
                        ShippedDate = o.ShippedDate,
                        CustomerName = o.Customer.CompanyName,
                        ShipVia = o.ShipVia,
                        OrderID = o.OrderID
                    };

            return DataSourceLoader.Load(q, loadOptions);
        }

        [HttpGet]
        public object Resources() {
            var list = (from s in _nwind.Shippers
                        select new SchedulerResource {
                            ID = s.ShipperID,
                            Text = s.CompanyName
                        })
                        .ToArray();

            list[0].Color = "#cb6bb2";
            list[1].Color = "#56ca85";
            list[2].Color = "#1e90ff";

            return list;
        }

        [HttpPut]
        public ActionResult Put(int key, string values) {
            var valuesObj = JsonConvert.DeserializeObject<JObject>(values);

            var order = _nwind.Orders.First(o => o.OrderID == key);
            order.OrderDate = (DateTime)valuesObj["orderDate"];
            order.ShippedDate = (DateTime)valuesObj["shippedDate"];
            order.ShipVia = (int)valuesObj["shipVia"];

            if(!TryValidateModel(order))
                return BadRequest(ModelState.GetFullErrorMessage());

            _nwind.SaveChanges();

            return Ok();
        }
    }
}
