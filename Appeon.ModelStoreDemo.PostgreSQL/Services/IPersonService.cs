using PowerBuilder.Data;
using Appeon.ModelStoreDemo.PostgreSQL.Models;

namespace Appeon.ModelStoreDemo.PostgreSQL.Services
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
