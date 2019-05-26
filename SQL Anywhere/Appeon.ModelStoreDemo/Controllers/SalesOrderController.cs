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
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderService _saleService;

        public SalesOrderController(ISalesOrderService saleService)
        {
            _saleService = saleService;
        }

        // GET api/salesorder/WinOpen
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> WinOpen()
        {
            var packer = new DataPacker();

            try
            {
                packer.AddModelStore("Customer", 
                _saleService.Retrieve<DdCustomer>());
                packer.AddModelStore("SalesPerson", 
                    _saleService.Retrieve<DdSalesPerson>());
                packer.AddModelStore("SalesTerritory", 
                    _saleService.Retrieve<DdSalesTerritory>());
                packer.AddModelStore("ShipMethod", 
                    _saleService.Retrieve<DdShipMethod>());
                packer.AddModelStore("OrderProduct", 
                    _saleService.Retrieve<DdOrderProduct>());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // GET api/SalesOrder/RetrieveSaleOrderList
        [HttpGet("{customerId}/{dateFrom}/{dateto}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IDataPacker> RetrieveSaleOrderList(
            int customerId, string dateFrom, string dateTo)
        {
            var packer = new DataPacker();
            var fromDate = DateTime.Parse(dateFrom);
            var toDate = DateTime.Parse(dateTo);

            try
            {
                packer.AddModelStore("SalesOrderHeader", 
                    _saleService.Retrieve<SalesOrderHeaderList>(customerId, 
                    fromDate, toDate));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // GET api/SalesOrder/RetrieveSaleOrderDetail
        [HttpGet("{salesOrderId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> RetrieveSaleOrderDetail(
            int salesOrderId, int customerId)
        {
            var packer = new DataPacker();

            try
            {
                packer.AddModelStore("SalesOrderDetail",
                _saleService.Retrieve<SalesOrderDetail>(salesOrderId));

                packer.AddModelStore("DddwAddress",
                    _saleService.Retrieve<DdCustomerAddress>(customerId));

                packer.AddModelStore("DddwCreditcard",
                    _saleService.Retrieve<DdCreditcard>(customerId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // GET api/SalesOrder/RetrieveDropdownModel
        [HttpGet("{modelName}/{CodeId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> RetrieveDropdownModel(string modelName, int CodeId)
        {
            var packer = new DataPacker();

            try
            {
                switch (modelName)
                {
                    case "Customer":
                        packer.AddModelStore("DddwAddress",
                            _saleService.Retrieve<DdCustomerAddress>(CodeId));
                        packer.AddModelStore("DddwCreditcard",
                            _saleService.Retrieve<DdCreditcard>(CodeId));
                        break;
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // POST api/salesorder/SaveSalesOrderAndDetail
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> SaveSalesOrderAndDetail(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();

            var orderHeader = unpacker.GetModelStore<SalesOrderHeader>("dw1",
                                  ChangeTrackingStrategy.PropertyState);

            var orderDetail = unpacker.GetModelStore<SalesOrderDetail>("dw2",
                                  ChangeTrackingStrategy.PropertyState);

            try
            {
                var saleOrderId = _saleService.SaveSalesOrderAndDetail(orderHeader,
                    orderDetail);

                if (saleOrderId > 0)
                {
                    packer.AddModelStore("SalesOrderHeader",
                        _saleService.Retrieve<SalesOrderHeader>(saleOrderId));
                    packer.AddModelStore("SalesOrderHeader.SalesOrderDetail",
                        _saleService.Retrieve<SalesOrderDetail>(saleOrderId));
                }
                packer.AddValue("Status", "Success");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // POST api/salesorder/SaveChanges
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> SaveChanges(IDataUnpacker unpacker)
        {
            string status = "Success";
            var packer = new DataPacker();
            var modelname = unpacker.GetValue<string>("arm1");

            try
            {
                switch (modelname)
                {
                    case "SaleOrderHeader":
                        var orderHeader = unpacker.GetModelStore<SalesOrderHeader>
                            ("dw1", ChangeTrackingStrategy.PropertyState);
                        status = _saleService.Update(true, orderHeader);
                        packer.AddModelStore("SalesOrderHeader", orderHeader);

                        break;

                    case "SaleOrderDetail":
                        var orderDetail = unpacker.GetModelStore<SalesOrderDetail>
                            ("dw1", ChangeTrackingStrategy.PropertyState);
                        status = _saleService.Update(true, orderDetail);

                        packer.AddValue("SaleOrderDetail.SalesOrderDetail", 
                            orderDetail.Count);

                        break;
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            packer.AddValue("Status", status);

            return packer;
        }

        // DELETE api/salesorder/DeleteSalesOrderByKey
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> DeleteSalesOrderByKey(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();
            var modelName = unpacker.GetValue<string>("arm1");
            var saleOrderId = unpacker.GetValue<int>("arm2");
            var status = "Success";

            try
            {
                switch (modelName)
                {
                    case "SaleOrder":
                        status = _saleService.DeleteSalesOrder(saleOrderId);
                        break;

                    case "OrderDetail":
                        var saleDetailId = unpacker.GetValue<int>("arm3");
                        status = _saleService.Delete<SalesOrderDetail>(true,
                            m => m.SalesOrderDetailID == saleDetailId, saleOrderId);
                        break;
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            packer.AddValue("Status", status);

            return packer;
        }
    }
}