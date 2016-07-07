using System;
using System.Collections.Generic;
using System.Linq;

namespace WidgetGallery.ViewModels {
    public class EditorsViewModel {

        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Extension { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public string Drink { get; set; }
        public string City { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public IEnumerable<string> SelectedColors { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
    }
}
