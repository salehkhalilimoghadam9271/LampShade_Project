using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructrue.Efcore.Repository
{
    public class ProductCategoryRepository : _0_Framework.Infrastructrue.RepositoryBase<long, ProductCategory> , IProductCategoryRepository
    {
        private readonly ShopContext _Context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _Context = context;
        }

        public EditeProductCategory GetDetails(long id)
        {
            return _Context.ProductCategories.Select(x => new EditeProductCategory()
            {
                Id= x.Id,
                Description = x.Description,
                Name = x.Name,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _Context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public string GetSlugById(long id)
        {
            return _Context.ProductCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel)
        {
            var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchmodel.Name))
                query = query.Where(x => x.Name.Contains(searchmodel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
