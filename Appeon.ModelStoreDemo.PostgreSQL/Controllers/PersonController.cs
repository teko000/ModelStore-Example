using SnapObjects.Data;
using PowerBuilder.Data;
using Appeon.ModelStoreDemo.PostgreSQL.Models;
using Appeon.ModelStoreDemo.PostgreSQL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Appeon.ModelStoreDemo.PostgreSQL.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService perService)
        {
            _personService = perService;
        }

        // GET api/Person/WinOpen
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> WinOpen()
        {
            var packer = new DataPacker();

            try
            {
                packer.AddModelStore("Address", 
                _personService.Retrieve<DdAddress>());
                packer.AddModelStore("AddressType", 
                    _personService.Retrieve<DdAddressType>());
                packer.AddModelStore("PhonenumberType", 
                    _personService.Retrieve<DdPhoneNumberType>());
                packer.AddModelStore("CustomerTerritory", 
                    _personService.Retrieve<DdTerritory>());
                packer.AddModelStore("Store", 
                    _personService.Retrieve<DdStore>());
                packer.AddModelStore("Person", 
                    _personService.Retrieve<PersonList>("IN"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // GET api/Person/RetrievePersonByKey
        [HttpGet("{personId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> RetrievePersonByKey(int personId)
        {
            var packer = new DataPacker();

            try
            {
                packer.AddModelStore("Person", 
                _personService.Retrieve<Person>(personId));
                packer.AddModelStore("Person.PersonAddress", 
                    _personService.Retrieve<BusinessEntityAddress>(personId));
                packer.AddModelStore("Person.PersonPhone", 
                    _personService.Retrieve<PersonPhone>(personId));
                packer.AddModelStore("Person.Customer", 
                    _personService.Retrieve<Customer>(personId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // POST api/Person/SavePerson
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> SavePerson(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();

            try
            {
                var person = unpacker.GetModelStore<Person>("dw1",
                                  ChangeTrackingStrategy.PropertyState);

                var personAddress = unpacker.GetModelStore<BusinessEntityAddress>("dw2",
                                      ChangeTrackingStrategy.PropertyState);

                var personPhone = unpacker.GetModelStore<PersonPhone>("dw3",
                                      ChangeTrackingStrategy.PropertyState);

                var customer = unpacker.GetModelStore<Customer>("dw4",
                                     ChangeTrackingStrategy.PropertyState);
            
                var personId = _personService.SavePerson(person, personAddress, 
                    personPhone, customer);

                if (personId > 0)
                {
                    packer.AddModelStore("Person", 
                        _personService.Retrieve<Person>(personId));
                    packer.AddModelStore("Person.PersonAddress", 
                        _personService.Retrieve<BusinessEntityAddress>(personId));
                    packer.AddModelStore("Person.PersonPhone", 
                        _personService.Retrieve<PersonPhone>(personId));
                    packer.AddModelStore("Person.Customer", 
                        _personService.Retrieve<Customer>(personId));
                }
                packer.AddValue("Status", "Success");
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return packer;
        }

        // POST api/Person/Savechanges
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> Savechanges(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();

            var personAddress = unpacker.GetModelStore<BusinessEntityAddress>("dw1",
                                        ChangeTrackingStrategy.PropertyState);
            var personPhone = unpacker.GetModelStore<PersonPhone>("dw2",
                                        ChangeTrackingStrategy.PropertyState);
            var customer = unpacker.GetModelStore<Customer>("dw3",
                                        ChangeTrackingStrategy.PropertyState);

            var status = "Success";
            int? intPersonId = 0;

            try
            {
                if (personAddress.Count() > 0)
                {
                    status = _personService.Update(true, personAddress);
                    intPersonId = personAddress.FirstOrDefault().Businessentityid;
                }

                if (personPhone.Count() > 0 && status == "Success")
                {
                    status = _personService.Update(true, personPhone);
                    intPersonId = personPhone.FirstOrDefault().Businessentityid;
                }

                if (customer.Count() > 0 && status == "Success")
                {
                    status = _personService.Update(true, customer);
                    intPersonId = customer.FirstOrDefault().Personid;
                }
            
                if (status == "Success")
                {
                    packer.AddModelStore("Person",
                            _personService.Retrieve<Person>(intPersonId));
                    packer.AddModelStore("Person.PersonAddress",
                        _personService.Retrieve<BusinessEntityAddress>(intPersonId));
                    packer.AddModelStore("Person.PersonPhone",
                        _personService.Retrieve<PersonPhone>(intPersonId));
                    packer.AddModelStore("Person.Customer",
                        _personService.Retrieve<Customer>(intPersonId));
                }

                packer.AddValue("Status", status);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return packer;
        }

        // Delete api/Person/DeleteByKey
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IDataPacker> DeleteByKey(IDataUnpacker unpacker)
        {
            var packer = new DataPacker();

            var dwname = unpacker.GetValue<string>("arm1");
            var personId = unpacker.GetValue<int>("arm2");
            var status = "Success";

            try
            {
                switch (dwname)
                {
                    case "Person":
                        status = _personService.DeletePerson(personId);

                        break;

                    case "PersonAddress":
                        var addressId = unpacker.GetValue<int>("arm3");
                        var addressTypeId = unpacker.GetValue<int>("arm4");
                        status = _personService.Delete<BusinessEntityAddress>(true,
                            m => m.Addressid == addressId &&
                            m.Addresstypeid == addressTypeId, personId);

                        break;

                    case "PersonPhone":
                        var personNumber = unpacker.GetValue<string>("arm3");
                        var phonenumbertypeid = unpacker.GetValue<int>("arm4");
                        status = _personService.Delete<PersonPhone>(true,
                            m => m.Phonenumber == personNumber &&
                            m.Phonenumbertypeid == phonenumbertypeid, personId);
                        break;

                    case "Customer":
                        var customerId = unpacker.GetValue<int>("arm3");

                        status = _personService.Delete<Customer>(true, customerId);

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