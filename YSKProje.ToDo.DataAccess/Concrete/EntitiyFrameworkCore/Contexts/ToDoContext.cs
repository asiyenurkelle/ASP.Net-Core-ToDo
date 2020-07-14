using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Mapping;
using YSKProje.ToDo.Entities;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts
{
    //oluşturulcak db deki userlar appuser, roller approle, primaryke değerler ise int olsun dedik.
    public class ToDoContext : IdentityDbContext<AppUser, AppRole, int>
    //dbcontext ef tarafında yazılmıs özel bir class bunun aracılıgıyla classlarımızı tabloya dönüstürebiliyoruz.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=. ; database=ToDo; integrated security=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //mappinglerimizi buraya ekleyecegiz.

            modelBuilder.ApplyConfiguration(new GorevMap());
            modelBuilder.ApplyConfiguration(new AciliyetMap());
            modelBuilder.ApplyConfiguration(new RaporMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Gorev> Gorevler { get; set; }
        //Calisma classından da tablomuzu dahil ettik.
        public DbSet<Aciliyet> Aciliyetler { get; set; }

        public DbSet<Rapor> Raporlar { get; set; }
    }
}
