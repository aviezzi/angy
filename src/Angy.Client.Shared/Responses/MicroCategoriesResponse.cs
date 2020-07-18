using System.Collections.Generic;
using Angy.Model;

namespace Angy.Client.Shared.Responses
{
    public class MicroCategoriesResponse
    {
        public IEnumerable<MicroCategory>? MicroCategories { get; set; }
    }
}