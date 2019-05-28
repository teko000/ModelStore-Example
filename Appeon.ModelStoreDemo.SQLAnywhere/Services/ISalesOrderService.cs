using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SQLAnywhere.Models;

namespace Appeon.ModelStoreDemo.SQLAnywhere.Services
{
    public interface ISalesOrderService : IServiceBase
    {
        int SaveSalesOrderAndDetail(IModelStore<SalesOrderHeader> salesOrderHeaders,
                                    IModelStore<SalesOrderDetail> salesOrderDetails);

        string DeleteSalesOrder(int saleOrderId);
    }
}
