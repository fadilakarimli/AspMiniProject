using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AspMiniProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ToListAsync();

            return products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category?.Name ?? "No Category",
                Image = p.ProductImages.FirstOrDefault(pi => pi.IsMain)?.Img
            }).ToList();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductByIdForEditAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task CreateProductAsync(ProductCreateVM request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ProductImages = new List<ProductImage>()
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            if (request.Images != null && request.Images.Count > 0)
            {
                bool isFirst = true;
                foreach (var image in request.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    newProduct.ProductImages.Add(new ProductImage
                    {
                        Img = fileName,
                        IsMain = isFirst,
                        ProductId = newProduct.Id
                    });

                    isFirst = false;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task EditProductAsync(int id, ProductEditVM request)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return;

            if (request.Price <= 0)
            {
                throw new Exception("Price must be a positive number.");
            }

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;

            product.CategoryId = request.CategoryId;

            if (request.Images != null && request.Images.Count > 0)
            {
                foreach (var image in request.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    await _context.ProductImages.AddAsync(new ProductImage
                    {
                        Img = fileName,
                        IsMain = false,
                        ProductId = product.Id
                    });
                }
            }

            foreach (var image in product.ProductImages)
            {
                image.IsMain = (image.Id == request.MainImageId);
            }

            await _context.SaveChangesAsync();
        }






        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return;

            foreach (var image in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "img", image.Img);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductDetailVM> GetDetailAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return new ProductDetailVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "No Category",
                MainImages = product.ProductImages
                                    .Where(x => x.IsMain)
                                    .Select(x => x.Img)
                                    .ToList(),
                ExtraImages = product.ProductImages
                                     .Where(x => !x.IsMain)
                                     .Select(x => x.Img)
                                     .ToList()
            };
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

    }
}
