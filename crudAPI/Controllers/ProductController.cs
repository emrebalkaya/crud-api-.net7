using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using crudAPI.Dto;
using crudAPI.Entity;
using crudAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var products = _productService.GetAllProducts();
            var ProductDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return Ok(ProductDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var ProductDto = _mapper.Map<Product, ProductDto>(product);
            return Ok(ProductDto);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create(ProductDto ProductDto)
        {
            var product = _mapper.Map<ProductDto, Product>(ProductDto);
            _productService.AddProduct(product);

            ProductDto.Id = product.Id;

            return CreatedAtAction(nameof(GetById), new { id = ProductDto.Id }, ProductDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDto ProductDto)
        {
            if (id != ProductDto.Id)
            {
                return BadRequest();
            }

            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto, Product>(ProductDto);
            _productService.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
