using ProtoType.Model.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtoType.Repository.Interfaces
{
    public interface IBrandAndProductRepository : IGenericRepository<BrandAndProduct>
    {        
        Task<List<BrandAndProduct>> GetBrandAndProductsAsync();
        Task<BrandAndProduct> GetBrandAndProductByIdAsync(int brandAndProductId);
        Task<int> SaveBrandAndProductAsync(BrandAndProduct brandAndProduct);
        Task<int> SaveChangesAsync();
        Task<BrandAndProduct> GetBrandAndProductByBrandId(int brandId);
    }
}