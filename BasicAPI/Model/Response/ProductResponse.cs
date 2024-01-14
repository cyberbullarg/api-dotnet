﻿using BasicAPI.Model.Entities;

namespace BasicAPI.Model.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
