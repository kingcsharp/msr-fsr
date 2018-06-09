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

        public IQueryable<ProductsView> GetProducts(string ntLogin)
        {
            var sql =
                $"EXEC A_SP_PRODUCTS_SEARCH ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (SUPPLIER_NAME LIKE ''%%'' OR SUPPLIER_NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (PROCEDURE_NAME LIKE ''%%'' OR PROCEDURE_NAME is NULL ) AND  (APP_OBJ_NAME LIKE ''%%'' OR APP_OBJ_NAME is NULL ) AND  (VERB_NAME LIKE ''%%'' OR VERB_NAME is NULL ) AND STATUS IN (''CREATING'',''DENIED'',''APPROVED'',''APPROVED_BUT_REVISING'',''APPROVED_BUT_DELETING'')',' ORDER BY NAME','{ntLogin}'";

            var result = _dbContext.Database.SqlQuery<ProductsView>(sql).ToList().AsQueryable();

            return result;
        }

    }
}
