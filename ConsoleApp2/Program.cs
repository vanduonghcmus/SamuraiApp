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
            //AddQuoteToExistingSamuraiNotTracked(1);
            //EggerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraiWithQuotes();
            //ExplicitLoadQuotes();
            //FilterLoadedData();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoABattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithHorse();
            //AddNewHorseToSamuraiUsingId();
            //AddNewHorseToSamuraiObject();
            //AddNewHorseToDisconnectedSamuraiObject();
            //ReplaceAHorse();
            //GetSamuraiWithHorse();
            //GetHorseWithSamurai();
            //GetSamuraiWithClan();
            //GetClanWithSamurai();
            //QuerySamuraiBattleStats();
            //QueryUsingRawSql();
            //QueryUsingRawSqlWithInterpolation();
            //QueryUsingFromRawSqlStoreProc();
            ExcuteSomeRawSql();

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
                Name="Battle of Luxury",
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

        private static void EggerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Where(x=>x.Name.Contains("Dog"))
                .Include(x => x.Quote).FirstOrDefault();
        }

        private static void ProjectSomeProperties()
        {
            // projecting an undefined "anonymous" type
            var someProperties = _context.Samurais.Select(x => new { x.Id, x.Name }).ToList();
            // Return 2 properties from samurai type

            //Casting to The List of Definied Type
            var idsAndName = _context.Samurais.Select(x => new IdAndName(x.Id, x.Name)).ToList();
            
        }

        // Tạo ra 1 cấu trúc chứa Id và Name
        public struct IdAndName
        {
            // Tạo ra biến chứa parameter id, nam
            public IdAndName(int id,string name)
            {
                Id = id;
                Name = name;
            }
            // khai báo từng thành phần trong biến
            public int Id;
            public string Name;
        }

        public static void ProjectSamuraiWithQuotes()
        {
            //// select 2 scalars and List<Quote> from Samurai type
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(x => new { x.Id, x.Name, x.Quote }).ToList();

            //// Selecting an aggregate of related data
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(x => new { x.Id, x.Name, x.Quote.Count }).ToList();

            // Filter the Related dat thas's returned in the projected type
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(x => new { x.Id, x.Name,
            //        HappyQuotes = x.Quote.Where(x => x.Text.Contains("dinner"))}).ToList();

            // Projecting full entity object while filtering the retaled 
            var samuraiWithHappyQuotes = _context.Samurais
                .Select(x => new
                {
                    Samurai = x,
                    HappyQuotes = x.Quote.Where(s => s.Text.Contains("Dinner"))
                }).ToList();
            var firstsamurai = samuraiWithHappyQuotes[0].Samurai.Name += "Happyiest";
            var result = _context.ChangeTracker.Entries();
        }

        public static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(x => x.Name.Contains("San"));
            _context.Entry(samurai).Collection(x => x.Quote).Load();
            _context.Entry(samurai).Reference(x => x.Horse).Load();
        }

        private static void LazyLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(x => x.Name.Contains("San"));
            var quoteCount = samurai.Quote.Count();
        }

        private static void FilterLoadedData()
        {
            var samurai = _context.Samurais.FirstOrDefault(x => x.Name.Contains("San"));
            var happyQuote = _context.Entry(samurai)
                .Collection(x => x.Quote)
                .Query().Where(x => x.Text.Contains("Dinner")).ToList();
        }

        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais.Where(x => x.Quote.Any(x => x.Text.Contains("Dinner")))
                .ToList();
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(x => x.Quote).FirstOrDefault(x => x.Id == 1);
            samurai.Quote[0].Text = "Did you hear that";
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(x => x.Quote).FirstOrDefault(x => x.Id == 1);
            var quote = samurai.Quote[0];
            quote.Text += "Did you hear that again";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            };
        }

        private static void JoinBattleAndSamurai()
        {
            // Samurai and Battle already exist and we have their Id
            var sbJoin = new SamuraiBattle
            {
                SamuraiId = 2,
                BattleId = 3
            };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattle.Add(new SamuraiBattle { SamuraiId = 21 });
            _context.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var Join = new SamuraiBattle { BattleId = 1, SamuraiId = 1 };
            _context.Remove(Join);
            _context.SaveChanges();
            
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
                .Include(x => x.SamuraiBattle)
                .ThenInclude(x => x.Battle).FirstOrDefault(x => x.Id == 2);
            var samuraiWithBattlesCleaner = _context.Samurais.Where(x => x.Id == 1)
                .Select(x => new
                {
                    Samurais=x,
                    Battle=x.SamuraiBattle.Select(x=>x.Battle)
                }).FirstOrDefault();
        }
        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurais { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Gold" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Silver", SamuraiId = 2 };
            _context.Add(horse);
            _context.SaveChanges();
        }

        // Ada new horse to samurai object
        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(15);
            samurai.Horse = new Horse { Name = "Black Berry" };
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisconnectedSamuraiObject()
        {
            var samurai = _context.Samurais.Find(14);
            samurai.Horse = new Horse { Name = "Golden" };
            using (var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void ReplaceAHorse()
        {
            //var samurai = _context.Samurais.Include(x => x.Horse).FirstOrDefault(x => x.Id == 15);
            var samurai = _context.Samurais.Find(15);// has a horse 
            samurai.Horse = new Horse { Name = "Harry" };
            _context.SaveChanges();
        }

        private static void GetSamuraiWithHorse()
        {
            var samurai = _context.Samurais.Include(x => x.Horse).Where(x=>x.Horse.Id!=null).ToList();
        }

        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);

            var horseWithSamurai = _context.Samurais.Include(x => x.Horse)
                .FirstOrDefault(x=>x.Id==3);

            var horseWithSamurais = _context.Samurais
                .Where(x => x.Horse != null)
                .Select(s => new { Horse = s, Samurais = s })
                .ToList();
        }

        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clan).FirstOrDefault();
            // include: t/ứng với left join trong SQL, với dkiện khóa chính và khóa ngoại
        }

        private static void GetClanWithSamurai()
        {
            //var clan = _context.Clans.Include(x => x.???);
            var clan = _context.Clans.Find(3);
            var samuraisForClan = _context.Samurais.Where(x => x.ClanId == 3).ToList();
        }

        private static void QuerySamuraiBattleStats()
        {
            //var stats = _context.SamuraiBattleStats.ToList();
            var firstStat = _context.SamuraiBattleStats.FirstOrDefault();
            var royalStat = _context.SamuraiBattleStats.Where(x => x.Name.Contains("Dang")).FirstOrDefault();
            var checkChange= _context.ChangeTracker.Entries();
            var finedone = _context.SamuraiBattleStats.Find(2);// MAke no sence, because not Key value
        }

        private static void QueryUsingRawSql()
        {
            //var samurais=_context.Samurais.FromSqlRaw("select Name from Samurais").ToList();
            // Sử dung sqlRaw phải lấy hết dữ liệu từ bảng
            var samurai = _context.Samurais.FromSqlRaw(
                "Select Id, Name, ClanId, HorseId from Samurais").Include(s => s.Quote).ToList();
        }

        private static void QueryUsingRawSqlWithInterpolation()
        {
            string name = "Samurai3";
            // Không nên dùng theo cách này
            //var samurais = _context.Samurais
            //    .FromSqlRaw($"select * from Samurais where Name = '{name}'")
            //    .ToList();

            // Nên dùng theo
            var samurais = _context.Samurais
                .FromSqlInterpolated($"select * from Samurais where Name = {name}")
                .ToList();
        }

        private static void QueryUsingFromRawSqlStoreProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlRaw("Exec dbo.SamuraisWhoSaidAWord {0}", text).ToList();
        }

        private static void ExcuteSomeRawSql()
        {
            //var samuraiId = 22;
            //// ExcuteSqlRaw will return số row bị ảnh hưởng
            //var x = _context.Database.ExecuteSqlRaw("Exec DeleteQuotesForSamurai {0}", samuraiId);


            var samuraiId = 15;

            _context.Database.ExecuteSqlInterpolated($"Exec DeleteQuotesForSamurai {samuraiId} ");
           

        }


    }

}
