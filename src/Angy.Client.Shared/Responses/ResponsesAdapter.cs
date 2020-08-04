// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Generic;
using Angy.Model;

namespace Angy.Client.Shared.Responses
{
    public abstract class ResponsesAdapter
    {
        public class ProductsResponse : ResponsesAdapter
        {
            public IEnumerable<Product>? Products { get; set; }
        }

        public class ProductResponse : ResponsesAdapter
        {
            public Product? Product { get; set; }
            public IEnumerable<Category>? Categories { get; set; }
            public IEnumerable<Attribute>? Attributes { get; set; }
        }

        public class CategoriesAttributesResponse : ResponsesAdapter
        {
            public IEnumerable<Category>? Categories { get; set; }
            public IEnumerable<Attribute>? Attributes { get; set; }
        }

        public class CategoriesResponse
        {
            public IEnumerable<Category>? Categories { get; set; }
        }

        public class CategoryResponse
        {
            public Category? Category { get; set; }
        }

        public class AttributesResponse
        {
            public IEnumerable<Attribute>? Attributes { get; set; }
        }

        public class AttributeResponse
        {
            public Attribute? Attribute { get; set; }
        }
    }
}