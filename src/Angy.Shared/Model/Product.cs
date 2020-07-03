namespace Angy.Shared.Model
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public MicroCategory MicroCategory { get; set; }
    }
}