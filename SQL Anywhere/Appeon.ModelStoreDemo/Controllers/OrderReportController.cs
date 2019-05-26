using SnapObjects.Data;
using PowerBuilder.Data;
using Appeon.ModelStoreDemo.Models;
using Appeon.ModelStoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace Appeon.ModelStoreDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderReportController : Controller
    {
        private readonly IOrderReportService _reportService;

        public OrderReportController(IOrderReportService reportService)
        {
            _reportService = reportService;
        }

        // GET api/OrderReport/WinOpen
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IDataPacker> WinOpen()
        {
            var packer = new DataPacker();
            int cateId = 0;

            var category = _reportService.Retrieve<Category>();
            var subCategory = _reportService.Retrieve<SubCategory>(cateId);

            if (category.Count == 0 || subCategory.Count == 0)
            {
                return NotFound();
            }
            
            packer.AddModelStore("Category", category);
            packer.AddModelStore("SubCategory", subCategory);

            return packer;
        }

        // GET api/OrderReport/CategorySalesReport
        [HttpGet("{queryFrom}/{queryTo}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IDataPacker> CategorySalesReport(
            string queryFrom, string queryTo)
        {
            var packer = new DataPacker();

            var dataFrom = DateTime.Parse(queryFrom);
            var dataTo = DateTime.Parse(queryTo);
            var lastDataFrom = DateTime.Parse(queryFrom).AddYears(-1);
            var lastDataTo = DateTime.Parse(queryTo).AddYears(-1);
            
            var CategoryReport = _reportService.Retrieve<CategorySalesReport_D>(
                dataFrom, dataTo);

            if (CategoryReport.Count == 0)
            {
                return NotFound();
            }

            packer.AddModelStore("Category.SalesReport", CategoryReport);

            packer.AddModelStore("Category.LastYearSalesReport",
                _reportService.Retrieve<CategorySalesReport_D>(lastDataFrom, 
                lastDataTo));

            return packer;
        }

        // GET api/OrderReport/SalesReportByMonth
        [HttpGet("{subCategoryId}/{salesYear}/{halfYear}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IDataPacker> SalesReportByMonth(
            int subCategoryId, string salesYear, string halfYear)
        {
            var packer = new DataPacker();
           
            var fromDate = DateTime.Parse(halfYear == "first" ? 
                salesYear + "-01-01" : salesYear + "-07-01");
            var toDate = DateTime.Parse(halfYear == "first" ? 
                salesYear + "-06-30" : salesYear + "-12-31");
            object[] yearMonth = new object[7];

            yearMonth[0] = subCategoryId;
            for (int month = 1; month < 7; month++)
            {
                yearMonth[month] = halfYear == "first" ? 
                    salesYear + string.Format("{0:00}", month)
                    : salesYear + string.Format("{0:00}", (month + 6));
            }

            var SalesReport = _reportService
                .RetrieveSubCategorySalesReport(yearMonth);
            var ProductReport = _reportService
                .Retrieve<ProductSalesReport>(subCategoryId, fromDate, toDate);

            if (ProductReport.Count == 0)
            {
                return NotFound();
            }

            packer.AddModelStore("SalesReport", SalesReport);
            packer.AddModelStore("ProductReport", ProductReport);

            return packer;
        }
    }
}
