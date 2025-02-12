// See https://aka.ms/new-console-template for more information

using HomeWork_HashTable;

Console.WriteLine("Solution started");
var myHash = new myHashTable();
myHash.AddItem( "Hello");
myHash.AddItem( "How are you");
myHash.AddItem( "Hello1");
Console.WriteLine();
Console.WriteLine("Added 3 items");
myHash.GetItems();
myHash.DeleteItem("How are you");
Console.WriteLine("Deleted item How are you");
myHash.GetItems();
Console.WriteLine();
Console.WriteLine("Looking for items How are you and Hello1");
myHash.SearchItem("How are you");
myHash.SearchItem("Hello1");
Console.WriteLine();
myHash.AddItem( "new Hello");
Console.WriteLine("Adding new Hello item");
myHash.GetItems();


