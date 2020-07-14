﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    //BU MODELLERİ OLUSTURMAMIZIN SEBEBİ KULLANICIYA GOREV SINIFINDAKİ TÜM ÖZELLİKLERİ GÖSTERMEDEN GÖSTERECEGİMİZ ÖZELLİKLERİ BURADA HAZIRLAMAK.
    public class GorevListViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturulmaTarih { get; set; }

        public int AciliyetId { get; set; }
        public Aciliyet Aciliyet { get; set; }
    }
}
