using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //GetSamurais("Before Add:")
            //AddSamurai();
            //GetSamurais("After Add:");
            //InsertMultipleSanurais();
            //InsertVariousTypes();
            //QueryFilters();
            //RetrieveAndUpdateMultipleSamurai();
            //RemoveSamurais();
            //InsertNewSamuraiWithAQuote();
            //InsertBattle();
            AddQuoteToExistingSamuraiNotTracked(1);
            Console.Write("Press any key...");
            Console.ReadKey();
        }
        private static void InsertMultipleSanurais()
        {
            var samurai = new Samurais { Name = "Duong" };
            var samurai2 = new Samurais { Name = "Dang" };
            var samurai3 = new Samurais { Name = "Samurai3" };
            var samurai4 = new Samurais { Name = "Samurai4" };
            // var SamuraisList=new List<Samurai>...
            // context.Samurais.AddRange(samuraiList)
            _context.Samurais.AddRange(samurai,samurai2,samurai3,samurai4);
            _context.SaveChanges();
        }
        private static void InsertVariousTypes()
        {
            var samurai = new Samurais { Name = "Samurai5" };
            var clan = new Clans { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }
        private static void GetSamuraisSimpler()
        {
            // var samurais =context.Samurai.ToList();
            var query = _context.Samurais;
            //var samurais = query.ToList();
            foreach(var samurais in query)
            {
                Console.WriteLine(samurais.Name);
            }
        }

        private static void AddSamurai( )
        {
            var samurai = new Samurais { Name = "Duong" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count()}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void QueryFilters()
        {
            var name = "Dog";
            //var filter = "D%";
            //var samurai = _context.Samurais.FirstOrDefault(s => s.Name==name);
            //var samurai1 = _context.Samurais.Find(2);
            //var samurais = _context.Samurais.Where(x=>EF.Functions.Like(x.Name,name)).ToList();
            var last = _context.Samurais.OrderBy(x => x.Id).LastOrDefault(x => x.Name == name);
        }
        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        } 
        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(x => x.Name += "San");
            _context.SaveChanges();
        }
        private static void RemoveSamurais()
        {
            var samurai = _context.Samurais.Find(18);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name="Battle of Royal",
                StartDate=new DateTime(1999,04,26),
                EndDate=DateTime.Now
            });
            _context.SaveChanges();
        }
        private static void QueryAndUpdate_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1995, 02, 25);
            using(var newContextInstance=new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();

            }
        }
        private static void InsertNewSamuraiWithAQuote()
        {
            // Create Contructor
            var samurai = new Samurais
            {
                Name = "Maria Ozawa",
                Quote = new List<Quotes>
                {
                    new Quotes
                    {
                        Text = "T've come to save you"
                    }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            // lấy ra biến samurai theo parameter samuraiId 
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quote.Add(new Quotes
            {
                Text = "Now that I saved you, will you feed me dinner"
            });
            using(var newContext=new SamuraiContext())
            {
                newContext.Samurais.Attach(samurai);
                newContext.SaveChanges();
            }

        }
        private static void AddQuoteToExistingSamuraiWhileTracked()
        {

        }
    }
    
}
