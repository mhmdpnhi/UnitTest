// See https://aka.ms/new-console-template for more information
using ConsoleApp;

Console.WriteLine("Hello, World!");

var mathHelper = new MathHelper();

while(true)
{
    Console.WriteLine("Enter x:");
    var x = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter y:");
    var y = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(mathHelper.Sum(x, y));
}

Console.ReadLine();