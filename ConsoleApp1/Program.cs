using System;
using SamuraiApp.Domain;
using SamuraiApp.Data;
using System.Linq;



namespace ConsoleApp1
{
    internal class Program
    {
        private static SamuraiContext context = new SamuraiContext();
        private static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            GetSamurais("Before Add:");
            AddSamurai();
            GetSamurais("After Add:");
            Console.Write("Press any key...");
            Console.ReadKey();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Dog" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
        private static void GetSamurais(string Text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{Text}: Samurai count is {samurais.Count()}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            };
        }
    }
}

