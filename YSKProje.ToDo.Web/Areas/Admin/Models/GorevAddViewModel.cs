using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class GorevAddViewModel
    {
        //Aciliyet durumunun seçilip seçilmediğini kontrol etmek için range kısıtlayıcısını kullandık.
        //range kısıtlayıcısıyla minimum değeri 0 olsun ve min ile max değeri arasında olmadıgı durumlarda hata versin dedik.
        [Required(ErrorMessage ="Ad Alanı Gereklidir")]
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Lütfen bir aciliyet durumu seçiniz")]
        public int AciliyetId { get; set; }

    }
}
