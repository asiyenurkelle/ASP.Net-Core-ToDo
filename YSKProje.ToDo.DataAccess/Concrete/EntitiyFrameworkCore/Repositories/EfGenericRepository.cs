﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    //Bir tablo veriyoruz bu tabloyu IGenericdal'a vericez ve bu tablo bir class olucak ıtablodan implemente edilcek.
    public class EfGenericRepository<Tablo> : IGenericDal<Tablo>
        where Tablo : class, ITablo, new()
    {
        public List<Tablo> GetirHepsi()
        {
            //using keywordu parametre aldığı kaynak(burda context) tükendiği zaman içerisindeki kodları garbage collactora gönderir.
            using var context = new ToDoContext();
            return context.Set<Tablo>().ToList();
        }

        public Tablo GetirIdile(int id)
        {
            // context tükendiğinde bize id değerini getirmesini istedik.
            using var context = new ToDoContext();
            return context.Set<Tablo>().Find(id);
        }

        public void Guncelle(Tablo tablo)
        {
            //Güncelleme işlemi gerçekleştiğinde state'in değişeceğini bildirdik.
            using var context = new ToDoContext();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }

        public void Kaydet(Tablo tablo)
        {
            using var context = new ToDoContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }

        public void Sil(Tablo tablo)
        {
            using var context = new ToDoContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }
    }
}
