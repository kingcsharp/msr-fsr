using Msr.Models.Products;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Products
{
    public class ProductService
    {
        private readonly MsrDbContext _dbContext;

        public ProductService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ProductsView> GetProductsQueryable()
        {
            return _dbContext.ProductsViews;
        }

        public IQueryable<ProductsView> GetProducts()
        {
            var sql = "SELECT * FROM A_V_PRODUCT_SEARCH_DATA";

            var result = _dbContext.Database.SqlQuery<ProductsView>(sql).ToList().AsQueryable();

            return result;
        }

    }
}
