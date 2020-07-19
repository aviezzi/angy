using System.Collections.Generic;
using Angy.Model;

namespace Angy.Client.Shared.Responses
{
    public abstract class ResponsesAdapter
    {
        public class ProductsResponse : ResponsesAdapter
        {
            public IEnumerable<Model.Product>? Products { get; set; }
        }
        
        public class ProductResponse : ResponsesAdapter
        {
            public Model.Product? Product { get; set; }
            public IEnumerable<MicroCategory>? MicroCategories { get; set; }
        }
        
        public class MicroCategoriesResponse
        {
            public IEnumerable<MicroCategory>? MicroCategories { get; set; }
        }
        
        public class MicroCategoryResponse
        {
            public MicroCategory? MicroCategory { get; set; }
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