
namespace Equatable.Example
{
    public class Product : BaseEquatable
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? Tags { get; set; }
        public override List<object?> Props => [Name, Price,  Category, Description, Tags];
        public override bool? Stringify => true;
    }

}