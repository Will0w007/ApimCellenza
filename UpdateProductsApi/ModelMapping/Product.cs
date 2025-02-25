﻿using System;

namespace UpdateProductsApi.ModelMapping
{
    public class Product
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
    }
}
