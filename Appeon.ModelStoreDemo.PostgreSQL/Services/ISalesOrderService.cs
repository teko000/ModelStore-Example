using PowerBuilder.Data;
using Appeon.ModelStoreDemo.PostgreSQL.Models;

namespace Appeon.ModelStoreDemo.PostgreSQL.Services
{
    public interface ISalesOrderService : IServiceBase
    {
        int SaveSalesOrderAndDetail(IModelStore<SalesOrderHeader> salesOrderHeaders,
                                    IModelStore<SalesOrderDetail> salesOrderDetails);

        string DeleteSalesOrder(int saleOrderId);
    }
}
