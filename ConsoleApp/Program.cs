using System;
using System.Diagnostics.CodeAnalysis;

string? a = "ala";
string b = ToUpper(a)!;


string? ToUpper(string @string /*!! - null-checking feature - dodaje podczas kompilacji (niejawnie) kod jak poniżej*/)
{
/*    if (@string == null)
        throw new ArgumentNullException(nameof(@string));*/

    return @string?.ToUpper();
}


//namespace ConsoleApp
//{
//    internal class Program
//    {
//        static void Main()
//        {
            //instrukcje najwyższego poziomu
            Console.WriteLine("Hello, World!");

 //       }
//    }
//}
