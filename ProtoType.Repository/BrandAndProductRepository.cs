using ProtoType.Model;
using ProtoType.Model.DatabaseEntity;
using ProtoType.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtoType.Repository
{
    public class BrandAndProductRepository : GenericRepository<BrandAndProduct>, IBrandAndProductRepository
    {
        public BrandAndProductRepository(IProtoTypeContext context) : base(context)
        {

        }

        /// <summary>
        /// Get all brandandproducts to repository
        /// </summary>
        /// <returns>list of brandandproducts</returns>
        public async Task<List<BrandAndProduct>> GetBrandAndProductsAsync()
        {
            return await _context.BrandAndProducts.ToListAsync();
        }

        /// <summary>
        /// Add brandandproducts to repository
        /// </summary>
        /// <param name="brandAndProduct"></param>
        /// <returns>return primary key of added brandandproduct</returns>
        public async Task<int> SaveBrandAndProductAsync(BrandAndProduct brandAndProduct)
        {
            await base.AddAsync(brandAndProduct);
            return brandAndProduct.Id;
        }

        /// <summary>
        /// Save brandandproducts to repository
        /// </summary>
        /// <returns>return brandandproduct is save or not in 1 or 0 format</returns>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public async Task<BrandAndProduct> GetBrandAndProductByIdAsync(int brandAndProductId)
        {
            //return await _context.BrandAndProducts.Include(x => x.Credential).FirstOrDefaultAsync(x => x.Id == brandAndProductId);
            return await _context.BrandAndProducts.FirstOrDefaultAsync(x => x.Id == brandAndProductId);
        }

        /// <summary>
        ///  Get Credential By BrandId
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>Return brandandproduct</returns>
        public async Task<BrandAndProduct> GetBrandAndProductByBrandId(int brandId)
        {
            return await _context.BrandAndProducts.FirstOrDefaultAsync(x => x.BrandId == brandId);
        }
    }
}