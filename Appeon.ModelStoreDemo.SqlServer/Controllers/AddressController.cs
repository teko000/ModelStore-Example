using SnapObjects.Data;
using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SqlServer.Models;
using Appeon.ModelStoreDemo.SqlServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace Appeon.ModelStoreDemo.SqlServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addService)
        {
            _addressService = addService;
        }

        // GET api/Address/WinOpen
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IDataPacker> WinOpen()
        {
            var packer = new DataPacker();

            var stateProvince = _addressService.Retrieve<DdStateProvince>();

            if (stateProvince.Count == 0)
            {
                return NotFound();
            }

            packer.AddModelStore("StateProvince", stateProvince);

            return packer;
        }

        // GET api/Address/RetrieveAddress
        [HttpGet("{provinceId}/{city}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IDataPacker> RetrieveAddress(int provinceId, string city)
        {
            var packer = new DataPacker();

            if (city == "$") city = "%";

            var addressData = _addressService.Retrieve<AddressList>(provinceId, city);

            if (addressData.Count == 0)
            {
                return NotFound();
            }

            packer.AddModelStore("Address", addressData);

            return packer;
        }

        // POST api/Address/SaveChanges
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> SaveChanges(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();

            var addressModel = unpacker.GetModelStore<Address>("dw1",
                ChangeTrackingStrategy.PropertyState, MappingMethod.Key);

            try
            {
                var status = _addressService.Update(addressModel);
                packer.AddValue("Status", status);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            packer.AddModelStore("Address", addressModel);
         
            return packer;
        }

        // DELETE api/Address/DeleteAddressByKey
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> DeleteAddressByKey(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();
            var addressId = unpacker.GetValue<int>("arm1");

            try
            {
                var status = _addressService.Delete<Address>(addressId);
                packer.AddValue("Status", status);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return packer;
        }
    }
}