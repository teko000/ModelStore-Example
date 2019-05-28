using PowerBuilder.Data;
using Appeon.ModelStoreDemo.Oracle.Models;

namespace Appeon.ModelStoreDemo.Oracle.Services
{
    public interface IOrderReportService : IServiceBase
    {
        IModelStore<SubCategorySalesReport> RetrieveSubCategorySalesReport(params object[] salesmonth);

    }
}

