using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class SiteNavigationDataController : Controller {
        class Node {
            public string Caption;
            public string Url = "#";
            public bool Expanded = true;

            public IEnumerable<Node> Children;
        }

        [HttpGet]
        public object Get() {
            return new[] {
                new Node {
                    Caption = "Grids",
                    Children = new[] {
                        MakeWidgetNode("Data Grid (WebApi)", "DataGrid"),
                        MakeWidgetNode("Data Grid (OData)", "DataGridOData"),
                        MakeWidgetNode("Pivot Grid", "PivotGrid"),
                    }
                },
                new Node {
                    Caption = "Data Visualization",
                    Children = new[] {
                        MakeWidgetNode("Charts", "Charts"),
                        MakeWidgetNode("Gauges", "Gauges"),
                        MakeWidgetNode("Range Selector", "RangeSelector"),
                        MakeWidgetNode("Vector Map", "VectorMap"),
                        MakeWidgetNode("Treemap", "TreeMap"),
                        MakeWidgetNode("Sparklines & Bullets", "SparklinesBullets"),

                    }
                },
                new Node {
                    Caption = "Data Editing",
                    Children = new[] {
                        MakeWidgetNode("Form Widget", "Form"),
                        MakeWidgetNode("Editors", "Editors"),
                        MakeWidgetNode("Validation", "Validation"),
                        MakeWidgetNode("File Uploader", "FileUploader")
                    }
                },
                new Node {
                    Caption = "Multi-Purpose",
                    Children = new[] {
                        MakeWidgetNode("Scheduler", "Scheduler"),
                        MakeWidgetNode("List", "List"),
                        MakeWidgetNode("Navigation", "Navigation"),
                        MakeWidgetNode("Overlays", "Overlays"),
                        MakeWidgetNode("Image Gallery", "Gallery"),
                        MakeWidgetNode("Geo Map", "GeoMap"),
                    }
                }
            };
        }

        Node MakeWidgetNode(string caption, string actionName) {
            return new Node {
                Caption = caption,
                Url = Url.RouteUrl("default", new {
                    controller = "Home",
                    action = actionName
                })
            };
        }

    }
}
