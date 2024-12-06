using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Craete(CreateProductCategory command);
        OperationResult Edite(EditeProductCategory command);
        EditeProductCategory GetDetails(long id);

        List<ProductCategoryViewModel> search(ProductCategorySearchModel searchmodel);
    }
}
