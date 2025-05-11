// See https://aka.ms/new-console-template for more information
using Equatable.Example;
var person  = new Person
{
    Name = "John",
    Age = 30
};
var person2 = new Person
{
    Name = "John",
    Age = 30
};
Console.WriteLine(person.Equals(person2)); // True

var person3 = new Person
{
    Name = "Jane",
    Age = 25
};
Console.WriteLine(person.Equals(person3)); // False

Console.WriteLine(person); // John, 30

var product = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    Quantity = 10,
    Category = "Electronics",
    Description = "A high-performance laptop."
};
var product2 = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    Quantity = 11,
    Category = "Electronics",
    Description = "A high-performance laptop."
};
//Even though the quantity is different, the Equals method will only check the properties defined in the Props list
Console.WriteLine(product.Equals(product2)); // True