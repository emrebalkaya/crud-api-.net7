using System;
using crudAPI.Entity;
using crudAPI.Repository;

namespace crudAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            if (ValidateProduct(product))
            {
                _productRepository.AddProduct(product);
            }
            else
            {
                throw new ArgumentException("Invalid product data");
            }
        }

        public void UpdateProduct(Product product)
        {
            if (ValidateProduct(product))
            {
                _productRepository.UpdateProduct(product);
            }
            else
            {
                throw new ArgumentException("Invalid product data");
            }
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }

        private bool ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return false;
            }

            if (product.Price <= 0)
            {
                return false;
            }

            return true;
        }
    }
}

