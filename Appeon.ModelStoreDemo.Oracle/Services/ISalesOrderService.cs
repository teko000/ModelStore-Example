using PowerBuilder.Data;
using Appeon.ModelStoreDemo.Oracle.Models;

namespace Appeon.ModelStoreDemo.Oracle.Services
{
    public interface ISalesOrderService : IServiceBase
    {
        int SaveSalesOrderAndDetail(IModelStore<SalesOrderHeader> salesOrderHeaders,
                                    IModelStore<SalesOrderDetail> salesOrderDetails);

        string DeleteSalesOrder(int saleOrderId);
    }
}
