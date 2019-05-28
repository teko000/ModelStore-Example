using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SQLAnywhere.Models;

namespace Appeon.ModelStoreDemo.SQLAnywhere.Services
{
    public interface IPersonService : IServiceBase
    {
        int SavePerson(IModelStore<Person> person,
                   IModelStore<BusinessEntityAddress> addresses,
                   IModelStore<PersonPhone> phones,
                   IModelStore<Customer> customers);
        string DeletePerson(int personId);       
    }
}
