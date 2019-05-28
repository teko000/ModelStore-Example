using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SqlServer.Models;

namespace Appeon.ModelStoreDemo.SqlServer.Services
{
    public interface ISalesOrderService : IServiceBase
    {
        int SaveSalesOrderAndDetail(IModelStore<SalesOrderHeader> salesOrderHeaders,
                                    IModelStore<SalesOrderDetail> salesOrderDetails);

        string DeleteSalesOrder(int saleOrderId);
    }
}
