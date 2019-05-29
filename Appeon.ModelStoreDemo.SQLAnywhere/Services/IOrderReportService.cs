using PowerBuilder.Data;
using Appeon.ModelStoreDemo.SQLAnywhere.Models;

namespace Appeon.ModelStoreDemo.SQLAnywhere.Services
{
    public interface IOrderReportService : IServiceBase
    {
        IModelStore<SubCategorySalesReport> RetrieveSubCategorySalesReport(params object[] salesmonth);

    }
}

