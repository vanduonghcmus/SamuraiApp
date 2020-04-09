using System;

namespace SamuraiApp.Domain
{
    public partial class Quotes
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Samurais Samurai { get; set; }
        public int SamuraiId { get; set; }

    }
}
