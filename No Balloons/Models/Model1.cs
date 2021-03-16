using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace No_Balloons.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Horoscope> Horoscopes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Main)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Horoscope>()
                .Property(e => e.Message)
                .IsUnicode(false);
        }
    }
}
