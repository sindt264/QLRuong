namespace QL_Ruong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Datacontext : DbContext
    {
        public Datacontext()
            : base("name=Datacontext")
        {
        }

        public virtual DbSet<ChamSoc> ChamSocs { get; set; }
        public virtual DbSet<ChamSocChiTiet> ChamSocChiTiets { get; set; }
        public virtual DbSet<ChiTietUs> ChiTietUses { get; set; }
        public virtual DbSet<Ruong> Ruongs { get; set; }
        public virtual DbSet<ThuHoach> ThuHoaches { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.User_TK)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_MK)
                .IsUnicode(false);
        }
    }
}
