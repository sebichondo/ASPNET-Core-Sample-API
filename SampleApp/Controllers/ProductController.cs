using Microsoft.AspNetCore.Mvc;
using SampleApp.Controllers.Base;
using SampleApp.Helpers;
using SampleApp.Models;
using SampleApp.Services;
using System;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    [Route(API_PREFIX + "/products")]
    public class ProductController : DemoController
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<PaginatedList<Product>> Get(int pageIndex = 1, int pageSize = PageSize, string filter = null)
        {
            PaginatedList<Product> products = null;
            if (filter == null)
                products = await _productRepository.Paginate(pageIndex, pageSize, c => c.AddedDate);
            else
            {
                products = await _productRepository.Paginate(pageIndex, pageSize, c => c.AddedDate, c => c.ProductName == filter);
            }

            return products;
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(long id)
        {
            //var orgIdGuid = new Guid(orgId);
            var location = this._productRepository.Get(id);
            if (location == null)
            {
                return NotFound();
            }
            return this.Ok(location);
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null)
            {
                return this.NotFound();
            }
            product.AddedDate = DateTime.Now;

            _productRepository.Insert(product);
            string returnUrl = $"{API_PREFIX}/{product.Id}";
            return Created(new Uri(returnUrl, UriKind.Relative), product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [HttpPost("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return this.NotFound();
                }
                
                product.ModifiedDate = DateTime.Now;
                this._productRepository.Update(product);
                return this.Ok(product);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
