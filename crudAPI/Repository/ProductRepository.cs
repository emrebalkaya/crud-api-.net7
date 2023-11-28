using System;
using crudAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace crudAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Set<Product>().ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Set<Product>().Find(id);
        }

        public void AddProduct(Product product)
        {
            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _dbContext.Products.Find(product.Id); 

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Set<Product>().Find(id);
            if (product != null)
            {
                _dbContext.Set<Product>().Remove(product);
                _dbContext.SaveChanges();
            }
        }
    }
}

