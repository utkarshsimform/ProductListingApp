using Microsoft.AspNet.OData.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProtoType.Service.Interfaces
{
    public interface IBrandAndProductService
    {
        /// <summary>
        /// Get all brandandproduct method
        /// </summary>
        /// <returns>Return list of all brandandproducts</returns>        
        Task<IEnumerable<Model.Models.BrandAndProduct>> GetBrandAndProductsAsync();

        /// <summary>
        /// Save brandandproduct method
        /// </summary>
        /// <param name="brandAndProduct"></param>
        /// <returns>Return primary key of inserted brandandproduct</returns>
        Task<IEnumerable<int>> AddBrandAndProductAsync(List<Model.Models.BrandAndProduct> brandAndProductList);

        /// <summary>
        /// Update brandandproduct method
        /// </summary>
        /// <param name="brandAndProduct"></param>
        /// <returns>Return primary key of brandandproduct</returns>
        Task<int> UpdateBrandAndProductAsync(int id, Model.Models.BrandAndProduct brandAndProduct);

        /// <summary>
        /// Delete brandandproduct by id
        /// </summary>
        /// <param name="brandAndProductId"></param>
        /// <returns>Return deleted id of brandandproduct</returns>
        int? DeleteBrandAndProductById(int brandAndProductId);

        /// <summary>
        /// Get brandandproduct by Id
        /// </summary>
        /// <param name="brandAndProductId"></param>
        /// <returns>Return brandandproduct model</returns>
        Task<Model.Models.BrandAndProduct> GetBrandAndProductByIdAsync(int brandAndProductId);

        /// <summary>
        /// Check brand is exists or not in brandandproduct
        /// </summary>
        /// <param name="brandAndProductList"></param>
        /// <returns>If brand exists then return true otherwise false</returns>
        Task<bool> IsBrandExists(List<Model.Models.BrandAndProduct> brandAndProductList);
    }
}