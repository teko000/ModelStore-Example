using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SqlServer.Models;

namespace Appeon.ModelStoreDemo.SqlServer.Services
{
    public interface IOrderReportService : IServiceBase
    {
        IModelStore<SubCategorySalesReport> RetrieveSubCategorySalesReport(params object[] salesmonth);

    }
}

