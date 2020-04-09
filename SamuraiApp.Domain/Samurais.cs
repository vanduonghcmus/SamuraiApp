using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public partial class Samurais
    {
        // Contructure
        public Samurais()
        {
            Quotes = new HashSet<Quotes>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClanId { get; set; }
        public virtual ICollection<Quotes> Quotes { get; set; }
        public virtual Clans Clan { get; set; }

    }
}
