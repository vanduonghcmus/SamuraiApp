using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public partial class Clans
    {
        public Clans()
        {
            Samurais = new HashSet<Samurais>();
        }
        public int Id { get; set; }
        public string ClanName { get; set; }
        public virtual ICollection<Samurais> Samurais { get; set; }

    }
}
