using System.Collections.Generic;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Minos.Data.Model
{
    public class Setting : EntityBase
    {
        public int Height { get; set; }
        public int Width { get; set; }
        
        public IEnumerable<Story> Stories { get; set; }
    }
}