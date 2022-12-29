using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Startup_B.Services.stock;

namespace Startup_B.Controllers
{
    public class StockController : Controller
    {
        IConfiguration configuration;
        IWebHostEnvironment hostEnvironment;
        IExcelDataReader reader;
        IStock _stock;

        public StockController(IConfiguration configuration, IWebHostEnvironment hostEnvironment, IStock stock)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            this._stock = stock;
        }

        [HttpGet]
        public IActionResult UploadStock()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult UploadStock(IFormFile file)
        {
            if (file == null)
                throw new Exception("File is Not Received...");

            // Create the Directory if it is not exist
            string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedExcelFile");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            // MAke sure that only Excel file is used 
            string dataFileName = Path.GetFileName(file.FileName);

            string extension = Path.GetExtension(dataFileName);

            string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

            if (!allowedExtsnions.Contains(extension))
                throw new Exception("Sorry! This file is not allowed, make sure that file having extension as either.xls or.xlsx is uploaded.");

                // Make a Copy of the Posted File from the Received HTTP Request
            string saveToPath = Path.Combine(dirPath, dataFileName);

            using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var result = _stock.ReadExcelAsync(saveToPath);

            var test = result.Result.Count();
            return View();
        }
    }
}
