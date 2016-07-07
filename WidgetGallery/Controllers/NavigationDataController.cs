using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery.Models.Northwind;
using DevExtreme.AspNet.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class NavigationDataController : Controller {
        NorthwindContext _nwind;
        IHostingEnvironment _hostingEnvironment;

        public NavigationDataController(NorthwindContext nwind, IHostingEnvironment hostingEnvironment) {
            _nwind = nwind;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public object ProductsByCategories(DataSourceLoadOptions loadOptions) {
            var temp = _nwind.Categories.Include(c => c.Products).Take(4);

            var data = from c in temp
                       select new {
                           name = c.CategoryName,
                           expanded = c.CategoryName == "Beverages",
                           products = from p in c.Products
                                      select new { name = p.ProductName }
                       };

            return DataSourceLoader.Load(data, loadOptions);
        }

        [HttpGet]
        public object ProductsByCategory(int id, DataSourceLoadOptions loadOptions) {
            var products = from p in _nwind.Products
                           where p.CategoryID == id
                           select new { name = p.ProductName };

            return DataSourceLoader.Load(products, loadOptions);
        }

        [HttpGet]
        public object TreeViewData(DataSourceLoadOptions loadOptions) {
#warning using filter parts doesn't feel right, sort it out
            var parentId = (string)loadOptions.Filter[1];

            var rootPath = _hostingEnvironment.ContentRootPath;
            var parentPath = Path.Combine(rootPath, parentId);

            var childNodes = Directory.EnumerateFileSystemEntries(parentPath)
                .Select(path => new {
                    id = Path.Combine(parentId, Path.GetFileName(path)),
                    parentId = parentId,
                    text = Path.GetFileName(path),
                    hasItems = System.IO.File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                })
                .Where(i => i.text != "bin" && i.text != "obj")
                .OrderByDescending(i => i.hasItems)
                .ThenBy(i => i.text);

            return childNodes;
        }
    }
}
