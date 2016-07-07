using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace WidgetGallery.Controllers {

    [Route("api/[controller]/[action]")]
    public class FileUploaderController : Controller {
        IHostingEnvironment _hostingEnvironment;

        public FileUploaderController(IHostingEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Upload() {
            // Learn more on the functionality of the dxFileUploader widget at:
            // http://js.devexpress.com/Documentation/Guide/UI_Widgets/UI_Widgets_-_Deep_Dive/dxFileUploader/

            var myFile = Request.Form.Files["myFile"];
            var targetLocation = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            try {
                var path = Path.Combine(targetLocation, myFile.FileName);

                // Uncomment to save the file
                //using(var fileStream = System.IO.File.Create(path)) {
                //    myFile.CopyTo(fileStream);
                //}
            } catch {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }
    }

}
