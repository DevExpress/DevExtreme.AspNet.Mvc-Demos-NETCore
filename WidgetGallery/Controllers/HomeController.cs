using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models.Northwind;
using WidgetGallery.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace WidgetGallery.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            return RedirectToAction("DataGrid");
        }

        public ActionResult Editors() {
            return View(new EditorsViewModel {
                Colors = new[] { "red", "orange", "yellow", "green", "blue", "indigo", "violet" },
                SelectedColors = new[] { "red", "orange", "yellow" },
                Color = "#ff8800",
                Date = DateTime.Now,
                Age = 50,
                Accepted = true,
                Drink = "Coffee",
                City = "New York",
                Extension = "5467",
                Phone = "+1(202)555-01-92",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ornare, magna ut lobortis eleifend, tellus est fringilla neque."
            });
        }

        public ActionResult Overlays() {
            return View();
        }

        public ActionResult List() {
            return View();
        }

        public ActionResult Gallery() {
            var images = new List<string>();
            foreach(var i in Enumerable.Range(1, 10))
                images.Add(Url.Content(String.Format("~/images/{0:D2}.png", i)));

            return View(new GalleryViewModel {
                Images = images
            });
        }

        public ActionResult DataGrid() {
            return View();
        }

        public ActionResult PivotGrid() {
            return View();
        }

        public ActionResult DataGridOData() {
            return View();
        }

        public ActionResult Form() {
            var nwind = HttpContext.RequestServices.GetService<NorthwindContext>();

            return View(new FormViewModel {
                FormData = nwind.Employees.First()
            });
        }

        public ActionResult Charts() {
            return View();
        }

        public ActionResult Navigation() {
            return View();
        }

        public ActionResult Scheduler() {
            return View();
        }

        public ActionResult GeoMap() {
            return View();
        }

        public ActionResult Gauges() {
            return View();
        }

        public ActionResult RangeSelector() {
            return View();
        }

        public ActionResult SparklinesBullets() {
            return View();
        }

        public ActionResult VectorMap() {
            return View();
        }

        public ActionResult TreeMap() {
            return View();
        }

        public ActionResult Validation() {
            return View(new ValidationViewModel());
        }

        public ActionResult FileUploader() {
            return View();
        }

    }
}
