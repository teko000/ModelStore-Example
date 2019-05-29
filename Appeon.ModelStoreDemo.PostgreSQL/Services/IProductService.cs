using PowerBuilder.Data;
using Appeon.ModelStoreDemo.PostgreSQL.Models;

namespace Appeon.ModelStoreDemo.PostgreSQL.Services
{
    public interface IProductService : IServiceBase
    {         
        int SaveProductAndPrice(IModelStore<Product> product,
                                IModelStore<HistoryPrice> prices);       
        int SaveHistoryPrices(IModelStore<SubCategory> subcate,
                              IModelStore<Product> product,
                              IModelStore<HistoryPrice> prices);

        void SaveProductPhoto(int productId, string photoname, byte[] photo);

        string DeleteProduct(int productId);
    }
}
