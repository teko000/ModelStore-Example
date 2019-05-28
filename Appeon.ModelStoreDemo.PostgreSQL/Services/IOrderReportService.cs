using PowerBuilder.Data;
using Appeon.ModelStoreDemo.PostgreSQL.Models;

namespace Appeon.ModelStoreDemo.PostgreSQL.Services
{
    public interface IOrderReportService : IServiceBase
    {
        IModelStore<SubCategorySalesReport> RetrieveSubCategorySalesReport(params object[] salesmonth);

    }
}

