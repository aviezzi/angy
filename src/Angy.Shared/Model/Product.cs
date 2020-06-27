using System;

namespace Angy.Shared.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public MicroCategory MicroCategory { get; set; }
    }
}