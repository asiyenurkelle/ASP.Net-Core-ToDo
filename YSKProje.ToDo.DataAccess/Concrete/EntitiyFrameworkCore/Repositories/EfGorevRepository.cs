using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetileId(int id)
        {
            using var context = new ToDoContext();
            return context.Gorevler.Include(I => I.Aciliyet).FirstOrDefault
                (I => !I.Durum && I.Id == id);
        }
        public Gorev GetirRaporlarileId(int id)
        {
            using var context = new ToDoContext();
            return context.Gorevler.Include(I => I.Raporlar).Include(I => I.AppUser)
                .Where(I => I.Id == id).FirstOrDefault();
        }

        public List<Gorev> GetirAciliyetIleTamamlanmayan()
        {
            //EAGER LOADİNG KISMI
            using var context = new ToDoContext();
            // ! koydugumuz icin aciliyetler içerisinden durumu false olanları getircek.
            return context.Gorevler.Include(I => I.Aciliyet).Where
                (I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarih).ToList();
        }

        public List<Gorev> GetirileAppUserId(int appUserId)
        {
            using var context = new ToDoContext();
            return context.Gorevler.Where(I => I.AppUserId == appUserId).ToList();
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using var context = new ToDoContext();
            return context.Gorevler.Include(I => I.Aciliyet).Include(I => I.Raporlar).Include(I => I.AppUser).Where
                (I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarih).ToList();
        }
    }
}
