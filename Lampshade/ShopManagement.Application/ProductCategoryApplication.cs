using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Craete(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجدد تلاش بفرمایید.");

            var slug = command.Slug.Slugify();
            var productcategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                slug);
            _productCategoryRepository.Create(productcategory);
            _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edite(EditeProductCategory command)
        {
            var operation = new OperationResult();
            var productcategory = _productCategoryRepository.Get(command.Id);
            if (productcategory == null)
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد. لطفا مجدد تلاش بفرمایید");
            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجدد تلاش بفرمایید.");

            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditeProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> search(ProductCategorySearchModel searchmodel)
        {
            return _productCategoryRepository.Search(searchmodel);
        }
    }
}
