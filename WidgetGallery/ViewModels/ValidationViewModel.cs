using System;
using System.Collections.Generic;
using System.Linq;

namespace WidgetGallery.ViewModels {
    public class ValidationViewModel {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Agreement { get; set; }
    }
}
