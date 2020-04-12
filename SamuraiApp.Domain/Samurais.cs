using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public partial class Samurais
    {
        // Constructor
        public Samurais()
        {
            Quote = new List<Quotes>();
            SamuraiBattle = new List<SamuraiBattle>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClanId { get; set; }
        public  List<Quotes> Quote { get; set; }
        public  Clans Clan { get; set; }
        public List<SamuraiBattle> SamuraiBattle { get; set; }
        public Horse Horse { get; set; }
      

    }
}
