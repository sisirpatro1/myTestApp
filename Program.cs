// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
for(int i = 0; i < 10000; i++)
{
    Console.WriteLine(" Looped as " + i.ToString());
    Thread.Sleep(1000);
}
Console.WriteLine("Good Bye!");