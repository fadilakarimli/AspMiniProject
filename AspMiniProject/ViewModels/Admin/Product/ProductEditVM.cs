using AspMiniProject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }

        public List<IFormFile>? Images { get; set; }
        public List<ProductImageVM> ExistingImages { get; set; } = new List<ProductImageVM>();
        public int MainImageId { get; set; }
    }
}
