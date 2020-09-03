using System.Collections.Generic;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Minos.Data.Model
{
    public class Photo : EntityBase
    {
        public string Filename { get; set; }
        public char Shot { get; set; }
        public string Extension { get; set; }
        
        public IEnumerable<Story> Stories { get; set; }
    }
}