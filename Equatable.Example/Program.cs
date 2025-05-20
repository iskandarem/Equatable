// See https://aka.ms/new-console-template for more information
using System;
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
    Quantity = 11,
    Category = "Electronics",
    Description = "A high-performance laptop.",
    Tags = new List<string> { "Electronics", "Computers" }
};
var product2 = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    Quantity = 11,
    Category = "Electronics",
    Description = "A high-performance laptop.",
    Tags = new List<string> { "Electronics", "Computers" }
};
var product3 = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    Quantity = 11,
    Category = "Electronics",
    Description = "A high-performance laptop.",
    Tags = new List<string> { "Electronics", "Computer" }
};
//Even though the quantity is different, the Equals method will only check the properties defined in the Props list
Console.WriteLine(product.Equals(product2)); // True
//returns false because the tags are different
Console.WriteLine(product.Equals(product3)); // False

// Outputs the string representation of the objects using their base ToString method
Console.WriteLine(product); // Laptop, 999.99, 11, Electronics, A high-performance laptop., System.Collections.Generic.List`1[System.String]

var MyDictionaryClass = new MyDictionaryClass
{
    MyProp = new Dictionary<object, object>
    {
        { "Key1", "Value1" },
        { "Key2", "Value2" }
    }
};
var MyDictionaryClass2 = new MyDictionaryClass
{
    MyProp = new Dictionary<object, object>
    {
        { "Key1", "Value1" },
        { "Key2", "Value2" }
    }
};
var MyDictionaryClass3 = new MyDictionaryClass
{
    MyProp = new Dictionary<object, object>
    {
        { "Key1", "Value1" },
        { "Key2", "Value3" }
    }
};



// returns true because the values are the same
Console.WriteLine(MyDictionaryClass.Equals(MyDictionaryClass2)); // True

// returns false because the values are different   
Console.WriteLine(MyDictionaryClass.Equals(MyDictionaryClass3)); // False

Console.WriteLine(MyDictionaryClass.ToString()); // System.Collections.Generic.Dictionary`2[System.Object,System.Object]