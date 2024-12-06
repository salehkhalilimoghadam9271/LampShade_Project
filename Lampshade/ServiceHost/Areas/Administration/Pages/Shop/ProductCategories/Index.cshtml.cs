using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel searchModel;
        public List<ProductCategoryViewModel> productCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel SearchModel)
        {
            productCategories = _productCategoryApplication.search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Craete(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productcategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", productcategory);
        }

        public JsonResult OnPostEdit(EditeProductCategory command)
        {
            var result = _productCategoryApplication.Edite(command);
            return new JsonResult(result);
        }
    }
}
