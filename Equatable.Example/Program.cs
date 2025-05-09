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