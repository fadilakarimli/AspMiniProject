﻿namespace AspMiniProject.Models
{
    public class ProductImage : BaseEntity
    {
        public string Img { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
