using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.ProductsActualPart;
using Msr.Repositories;

namespace Msr.Services.ProductsActualParts
{
    public class ProductsActualPartService
    {
        private readonly MsrDbContext _dbContext;

        public ProductsActualPartService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<ProductsActualPartView> GetProductsActualPartQueryable()
        {
            return _dbContext.ProductsActualPartViews;
        }
    }
}
