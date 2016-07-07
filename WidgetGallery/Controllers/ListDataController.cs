﻿using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WidgetGallery.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class ListDataController : Controller {
        NorthwindContext _nwind;

        public ListDataController(NorthwindContext nwind) {
            _nwind = nwind;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_nwind.Products.Include(p => p.Category), loadOptions);
        }

    }

}
