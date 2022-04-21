using ProtoType.Model;
using ProtoType.Service.Interfaces;
using ProtoType.Common.Constants;
using ProtoTypeAPI.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ProtoType.Model.Models;
using Microsoft.AspNetCore.Cors;
//using ProtoType.Model.DatabaseEntity;

namespace ProtoTypeAPI.Controllers.V1
{
    /// <summary>
    /// BrandAndProduct Api Controller
    /// </summary>

    [ApiController]
    [Route("/brandandproducts")]
    public class BrandAndProductsController : ControllerBase
    {
        private readonly IBrandAndProductService _brandAndProductService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="brandAndProductService"></param>
        public BrandAndProductsController(IBrandAndProductService brandAndProductService)
        {
            _brandAndProductService = brandAndProductService;
        }

        /// <summary>
        /// Get all brandandproducts
        /// </summary>
        /// <remarks>Returns a list of brandandproducts</remarks>
        /// <response code="200">OK</response>        
        [HttpGet]
        [EnableCors("AllowOrigin")]        
        public async Task<IActionResult> GetBrandAndProducts()
        {
            IEnumerable<BrandAndProduct> brandAndProductResponseModel = await _brandAndProductService.GetBrandAndProductsAsync();
            return Ok(brandAndProductResponseModel);
        }

        /// <summary>
        /// Save brandandproduct
        /// </summary>
        /// <remarks>Return id of brandandproduct</remarks>
        /// <response code="200">OK</response>
        [HttpPost]        
        public async Task<IActionResult> SaveBrandAndProducts([FromBody] List<BrandAndProduct> brandAndProducts)        
        {
            var isBrandExists = await _brandAndProductService.IsBrandExists(brandAndProducts);
            if (isBrandExists)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "Brand already exists.");
            }
            else
            {
                var insertedIds = await _brandAndProductService.AddBrandAndProductAsync(brandAndProducts);
                return Created(string.Empty, insertedIds);
            }
        }

        /// <summary>
        /// Update brandandproduct
        /// </summary>
        /// <remarks>Return id of brandandproduct</remarks>
        /// <response code="200">OK</response>
        [HttpPut("{id}")]        
        public async Task<IActionResult> UpdateBrandAndProducts([FromRoute] int id, [FromBody] BrandAndProduct brandAndProduct)
        {
            // check exists brand
            //var getBrandAndProduct = await _brandAndProductService.GetBrandAndProductsAsync(null);
            var getBrandAndProduct = await _brandAndProductService.GetBrandAndProductsAsync();
            if (getBrandAndProduct != null && getBrandAndProduct.Count() > 0)
            {
                var getBrandAndProductExceptCurrent = getBrandAndProduct.Where(a => a.Id != id);
                if (getBrandAndProductExceptCurrent.Select(a => a.BrandId).Contains(brandAndProduct.BrandId))
                {
                    return StatusCode((int)HttpStatusCode.Conflict, "Brand already exists.");
                }

                var existingBrandAndProduct = getBrandAndProduct.Where(a => a.Id == id);
                if (!existingBrandAndProduct.Any())
                {
                    return NotFound();
                }
                await _brandAndProductService.UpdateBrandAndProductAsync(id, brandAndProduct);
                return Ok();
            }
            return Ok();
        }

        /// <summary>
        /// Delete a brandandproduct
        /// </summary>
        /// <remarks>Delete a brandandproduct</remarks>
        /// <param name="id">Id of brandandproduct</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [HttpDelete("{id}")]        
        public IActionResult DeleteBrandAndProductById([FromRoute][Required] int id)
        {
            try
            {
                _brandAndProductService.DeleteBrandAndProductById(id);
                return Ok(id);
            }
            catch (InvalidOperationException)
            {
                return this.NotFoundObject();
            }
        }

        /// <summary>
        /// Get brandandproduct By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]        
        public async Task<IActionResult> GetBrandAndProductById([FromRoute][Required] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var brandAndProduct = await _brandAndProductService.GetBrandAndProductByIdAsync(id);
            if (brandAndProduct == null)
            {
                return NotFound();
            }
            return Ok(brandAndProduct);
        }
    }
}

