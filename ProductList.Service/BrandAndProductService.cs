//using Deltafs.Utility.Ambient;
using ProtoType.Model;
using ProtoType.Model.DatabaseEntity;
//using ProtoType.Model.EventsBroker.Producers.Revisions;
using ProtoType.Repository.Interfaces;
//using ProtoType.Service.Helper;
using ProtoType.Service.Interfaces;
//using ProtoTypeAPI.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
//using revisions = Deltafs.EventsBroker.Messages.Revisions;

namespace ProtoType.Service
{
    public class BrandAndProductService : IBrandAndProductService
    {
        private readonly IBrandAndProductRepository _brandAndProductRepository;
        private readonly IProtoTypeContext _ProtoTypeContext;

        public BrandAndProductService(
            IBrandAndProductRepository brandAndProductRepository,
            IProtoTypeContext ProtoTypeContext
            )
        {
            _brandAndProductRepository = brandAndProductRepository;
            _ProtoTypeContext = ProtoTypeContext;
        }

        /// <summary>
        /// Get all brandandproduct method
        /// </summary>
        /// <returns>Return list of all brandandproducts</returns>        
        public async Task<IEnumerable<Model.Models.BrandAndProduct>> GetBrandAndProductsAsync()
        {

            IQueryable<BrandAndProduct> brandAndProducts = Enumerable.Empty<BrandAndProduct>().AsQueryable();
            var lstBrandAndProduct = await _brandAndProductRepository.GetBrandAndProductsAsync();
            brandAndProducts = lstBrandAndProduct?.AsQueryable();

            if (brandAndProducts != null)
            {
                return GetResponseModelBrandAndProducts(brandAndProducts); //, dstiProducts
            }
            return null;
        }

        private IEnumerable<Model.Models.BrandAndProduct> GetResponseModelBrandAndProducts(IQueryable<Model.DatabaseEntity.BrandAndProduct> brandAndProducts)
        {
            List<Model.Models.BrandAndProduct> brandAndProductResponseModels = new();
            foreach (var item in brandAndProducts)
            {                
                Model.Models.BrandAndProduct brandAndProduct = new()
                {
                    Id = item.Id,
                    BrandId = item.BrandId,
                    BrandName = item.BrandName,
                    CredentialId = item.CredentialId,                    
                    ProductName = item.ProductName,
                    ArrReference = item.ArrReference
                };
                brandAndProductResponseModels.Add(brandAndProduct);
            }
            return brandAndProductResponseModels;
        }

        /// <summary>
        /// Save brandandproduct method
        /// </summary>
        /// <param name="brandAndProduct"></param>
        /// <returns>Return primary key of inserted brandandproduct</returns>
        public async Task<IEnumerable<int>> AddBrandAndProductAsync(List<Model.Models.BrandAndProduct> brandAndProductList)
        {
            List<int> insertedIds = new List<int>();
            foreach (var brandAndProduct in brandAndProductList)
            {
                Model.DatabaseEntity.BrandAndProduct newBrandAndProduct = new()
                {
                    BrandId = brandAndProduct.BrandId,
                    BrandName = brandAndProduct.BrandName,
                    CredentialId = brandAndProduct.CredentialId,
                    ProductName = brandAndProduct.ProductName,
                    ArrReference = brandAndProduct.ArrReference
                };
                var id = await _brandAndProductRepository.SaveBrandAndProductAsync(newBrandAndProduct);

                Model.Models.BrandAndProduct objBrandAndProduct = new()
                {
                    Id = id,
                    BrandName = newBrandAndProduct.BrandName,
                    CredentialId = newBrandAndProduct.CredentialId,
                    ProductName = newBrandAndProduct.ProductName,
                    CredentialName = brandAndProduct.CredentialName
                };                
                insertedIds.Add(id);
            }
            return insertedIds;
        }

        /// <summary>
        /// Update brandandproduct method
        /// </summary>
        /// <param name="brandAndProduct"></param>
        /// <returns>Return primary key of brandandproduct</returns>
        public async Task<int> UpdateBrandAndProductAsync(int id, Model.Models.BrandAndProduct brandAndProduct)
        {
            var existingbrandAndProduct = _brandAndProductRepository.GetById(id);
            if (existingbrandAndProduct != null)
            {
                existingbrandAndProduct.BrandId = brandAndProduct.BrandId;
                existingbrandAndProduct.BrandName = brandAndProduct.BrandName;
                existingbrandAndProduct.CredentialId = brandAndProduct.CredentialId;
                existingbrandAndProduct.ProductName = brandAndProduct.ProductName;
                existingbrandAndProduct.ArrReference = brandAndProduct.ArrReference;
            }
            _ = await _brandAndProductRepository.SaveChangesAsync();

            Model.Models.BrandAndProduct objBrandAndProduct = new()
            {
                Id = id,
                BrandName = brandAndProduct.BrandName,
                CredentialId = brandAndProduct.CredentialId,
                ProductName = brandAndProduct.ProductName,
                CredentialName = brandAndProduct.CredentialName
            };            
            return id;
        }

        /// <summary>
        /// Delete brandandproduct by id
        /// </summary>
        /// <param name="brandAndProductId"></param>
        /// <returns>Return deleted id of brandandproduct</returns>
        public int? DeleteBrandAndProductById(int brandAndProductId)
        {
            BrandAndProduct existbrandAndProduct = _brandAndProductRepository.GetById(brandAndProductId);
            if (existbrandAndProduct == null)
            {
                return null;
            }

            _brandAndProductRepository.Remove(existbrandAndProduct);
            return brandAndProductId;
        }

        /// <summary>
        /// Get brandandproduct by Id
        /// </summary>
        /// <param name="brandAndProductId"></param>
        /// <returns>Return brandandproduct model</returns>
        public async Task<Model.Models.BrandAndProduct> GetBrandAndProductByIdAsync(int brandAndProductId)
        {
            var brandAndProduct = await _brandAndProductRepository.GetBrandAndProductByIdAsync(brandAndProductId);
            var brandAndProductModel = new Model.Models.BrandAndProduct()
            {
                Id = brandAndProduct.Id,
                BrandId = brandAndProduct.BrandId,
                BrandName = brandAndProduct.BrandName,
                ProductName = brandAndProduct.ProductName,
                CredentialId = brandAndProduct.CredentialId,                
                ArrReference = brandAndProduct.ArrReference
            };
            return brandAndProductModel;
        }

        /// <summary>
        /// Check brand is exists or not in brandandproduct
        /// </summary>
        /// <param name="brandAndProductList"></param>
        /// <returns>If brand exists then return true otherwise false</returns>
        public async Task<bool> IsBrandExists(List<Model.Models.BrandAndProduct> brandAndProductList)
        {
            bool isBrandExists = false;
            isBrandExists = brandAndProductList.GroupBy(x => x.BrandId)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .Any();

            if (!isBrandExists)
            {
                var dbBrandProduct = await GetBrandAndProductsAsync();
                var dbBrandIds = dbBrandProduct.Select(a => a.BrandId).ToList();
                var duplicateBrandId = dbBrandIds.Intersect(brandAndProductList.Select(a => a.BrandId).ToList());
                if (duplicateBrandId.Any())
                {
                    isBrandExists = true;
                }
            }
            return isBrandExists;
        }

    }
}